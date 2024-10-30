using System;
using System.Collections.Generic;

abstract class User // abstarct class for both user and rider info
{
    public int UserID { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public abstract void Register();
    public abstract void Login();
    public virtual void DisplayProfile()
    {
        Console.WriteLine($"User ID: {UserID}, Name: {Name}, Phone: {PhoneNumber}");
    }
}

class Rider : User // inherit from user class 
{
    public List<Trip> RideHistory { get; set; } = new List<Trip>();
    public override void Register()
    {
        Console.WriteLine("Rider registered.");
    }
    public override void Login()
    {
        Console.WriteLine("Rider logged in.");
    }
    public void RequestRide(RideSharingSystem system, string startLocation, string destination)
    {
        system.RequestRide(this, startLocation, destination);
    }
    public void ViewRideHistory()
    {
        Console.WriteLine("Ride History:");
        foreach (var trip in RideHistory)
        {
            trip.DisplayTripDetails();
        }
    }
}

class Driver : User // inherit from the user 
{
    public int DriverID { get; set; }
    public string VehicleDetails { get; set; }
    public bool IsAvailable { get; set; } = true;
    public List<Trip> TripHistory { get; set; } = new List<Trip>();
    public override void Register()
    {
        Console.WriteLine("Driver registered.");
    }
    public override void Login()
    {
        Console.WriteLine("Driver logged in.");
    }

    public void AcceptRide(Trip trip)
    {
        IsAvailable = false;
        trip.Status = "Accepted";
        TripHistory.Add(trip);
        Console.WriteLine($"Ride accepted by Driver {Name}");
    }
    public void ViewTripHistory()
    {
        Console.WriteLine("Trip History:");
        foreach (var trip in TripHistory)
        {
            trip.DisplayTripDetails();
        }
    }

    public void ToggleAvailability()// method to check if driver avlaible
    {
        IsAvailable = !IsAvailable;
        Console.WriteLine($"Driver availability set to: {IsAvailable}");
    }
}

class Trip
{
    public int TripID { get; set; }
    public string RiderName { get; set; }
    public string DriverName { get; set; }
    public string StartLocation { get; set; }
    public string Destination { get; set; }
    public double Fare { get; private set; }
    public string Status { get; set; }

    public void CalculateFare() // fare for the trip
    {
        Fare = new Random().Next(50, 200); // random fare 
    }

    public void StartTrip()
    {
        Status = "In Progress";
        Console.WriteLine("Trip started.");
    }

    public void EndTrip()
    {
        Status = "Completed";
        Console.WriteLine("Trip completed.");
    }

    public void DisplayTripDetails()
    {
        Console.WriteLine($"TripID: {TripID}, Rider: {RiderName}, Driver: {DriverName}, Start: {StartLocation}, Destination: {Destination}, Fare: {Fare}, Status: {Status}");
    }
}

class RideSharingSystem // manage all operation
{
    private int _tripCounter = 1; //trip id unique
    public List<Rider> RegisteredRiders { get; set; } = new List<Rider>();
    public List<Driver> RegisteredDrivers { get; set; } = new List<Driver>();
    public List<Trip> AvailableTrips { get; set; } = new List<Trip>();

    public void RegisterUser(User user) // register user rider/driver
    {
        user.Register();
        if (user is Rider rider)
            RegisteredRiders.Add(rider);
        else if (user is Driver driver)
            RegisteredDrivers.Add(driver);
    }

    public void RequestRide(Rider rider, string startLocation, string destination)
    {
        var availableDriver = FindAvailableDriver();
        if (availableDriver == null)
        {
            Console.WriteLine("No drivers available at the moment.");
            return;
        }

        var trip = new Trip
        {
            TripID = _tripCounter++, // unique trip id
            RiderName = rider.Name,
            DriverName = availableDriver.Name,
            StartLocation = startLocation,
            Destination = destination,
            Status = "Pending"
        };
        trip.CalculateFare();
        availableDriver.AcceptRide(trip);
        rider.RideHistory.Add(trip);
        AvailableTrips.Add(trip);
    }

    private Driver FindAvailableDriver()
    {
        foreach (var driver in RegisteredDrivers)
        {
            if (driver.IsAvailable) return driver;
        }
        return null;
    }

    public void CompleteTrip(Trip trip)
    {
        trip.EndTrip();
        var driver = RegisteredDrivers.Find(d => d.Name == trip.DriverName);
        driver.IsAvailable = true;
    }

    public void DisplayAllTrips()
    {
        Console.WriteLine("All Trips:");
        foreach (var trip in AvailableTrips)
        {
            trip.DisplayTripDetails();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var rideSharingSystem = new RideSharingSystem();

        while (true)
        {
            Console.WriteLine("\n|| Welcome to the Ride Sharing System ||");
            Console.WriteLine("1. Register as Rider");
            Console.WriteLine("2. Register as Driver");
            Console.WriteLine("3. Request Ride (Rider)");
            Console.WriteLine("4. Accept Ride (Driver)");
            Console.WriteLine("5. Complete a Trip (Driver)");
            Console.WriteLine("6. View Ride History (Rider)");
            Console.WriteLine("7. View Trip History (Driver)");
            Console.WriteLine("8. Display All Trips");
            Console.WriteLine("9. Exit");
            Console.Write("Please choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Rider Name: ");
                    string riderName = Console.ReadLine();
                    Console.Write("Enter Phone Number: ");
                    string riderPhone = Console.ReadLine();
                    var rider = new Rider { UserID = rideSharingSystem.RegisteredRiders.Count + 1, Name = riderName, PhoneNumber = riderPhone };
                    rideSharingSystem.RegisterUser(rider);
                    break;

                case 2:
                    Console.Write("Enter Driver Name: ");
                    string driverName = Console.ReadLine();
                    Console.Write("Enter Phone Number: ");
                    string driverPhone = Console.ReadLine();
                    Console.Write("Enter Vehicle Details: ");
                    string vehicleDetails = Console.ReadLine();
                    var driver = new Driver { UserID = rideSharingSystem.RegisteredDrivers.Count + 1, Name = driverName, PhoneNumber = driverPhone, VehicleDetails = vehicleDetails };
                    rideSharingSystem.RegisterUser(driver);
                    break;

                case 3:
                    if (rideSharingSystem.RegisteredRiders.Count > 0)
                    {
                        var currentRider = rideSharingSystem.RegisteredRiders[0];
                        Console.Write("Enter Start Location: ");
                        string startLocation = Console.ReadLine();
                        Console.Write("Enter Destination: ");
                        string destination = Console.ReadLine();
                        currentRider.RequestRide(rideSharingSystem, startLocation, destination);
                    }
                    else
                    {
                        Console.WriteLine("No riders registered.");
                    }
                    break;

                case 4:
                    if (rideSharingSystem.AvailableTrips.Count > 0)
                    {
                        var tripToAccept = rideSharingSystem.AvailableTrips[0];
                        var currentDriver = rideSharingSystem.RegisteredDrivers[0];
                        currentDriver.AcceptRide(tripToAccept);
                    }
                    else
                    {
                        Console.WriteLine("No available trips to accept.");
                    }
                    break;

                case 5:
                    if (rideSharingSystem.AvailableTrips.Count > 0)
                    {
                        var trip = rideSharingSystem.AvailableTrips[0];
                        rideSharingSystem.CompleteTrip(trip);
                    }
                    else
                    {
                        Console.WriteLine("No trips to complete.");
                    }
                    break;

                case 6:
                    if (rideSharingSystem.RegisteredRiders.Count > 0)
                    {
                        rideSharingSystem.RegisteredRiders[0].ViewRideHistory();
                    }
                    break;

                case 7:
                    if (rideSharingSystem.RegisteredDrivers.Count > 0)
                    {
                        rideSharingSystem.RegisteredDrivers[0].ViewTripHistory();
                    }
                    break;

                case 8:
                    rideSharingSystem.DisplayAllTrips();
                    break;

                case 9:
                    Console.WriteLine("Exiting the system Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option Please try again.");
                    break;
            }
        }
    }
}
