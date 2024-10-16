using System;
using System.Collections.Generic;

class Customer
{
    public string CustomerId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Customer(string customerId, string lastName, string firstName)
    {
        CustomerId = customerId;
        LastName = lastName;
        FirstName = firstName;
    }
}

class RetailCustomer : Customer
{
    public string CreditCardType { get; set; }
    public string CreditCardNo { get; set; }

    public RetailCustomer(string customerId, string lastName, string firstName, string cardType, string cardNo)
        : base(customerId, lastName, firstName)
    {
        CreditCardType = cardType;
        CreditCardNo = cardNo;
    }
}

class CorporateCustomer : Customer
{
    public string CompanyName { get; set; }
    public int FrequentFlyerPts { get; set; }
    public string BillingAccountNo { get; set; }

    public CorporateCustomer(string customerId, string lastName, string firstName, string companyName, int points, string billingNo)
        : base(customerId, lastName, firstName)
    {
        CompanyName = companyName;
        FrequentFlyerPts = points;
        BillingAccountNo = billingNo;
    }
}

class Reservation
{
    public string ReservationNo { get; set; }
    public DateTime Date { get; set; }
}

class Seat
{
    public string RowNo { get; set; }
    public string SeatNo { get; set; }
    public double Price { get; set; }
    public string Status { get; set; }
}

class Flight
{
    public string FlightId { get; set; }
    public DateTime Date { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int SeatingCapacity { get; set; }

    public List<Seat> Seats { get; set; } = new List<Seat>();
}

class Program
{
    static void Main()
    {
        RetailCustomer retailCustomer = new RetailCustomer("C001", "Doe", "John", "Visa", "1234-5678-9012");
        CorporateCustomer corporateCustomer = new CorporateCustomer("C002", "Smith", "Jane", "ABC Corp", 5000, "BA1001");

        Flight flight = new Flight
        {
            FlightId = "F100",
            Date = DateTime.Now,
            Origin = "NYC",
            Destination = "LA",
            DepartureTime = DateTime.Now.AddHours(2),
            ArrivalTime = DateTime.Now.AddHours(5),
            SeatingCapacity = 150
        };

        flight.Seats.Add(new Seat { RowNo = "1", SeatNo = "A", Price = 200, Status = "Available" });
        flight.Seats.Add(new Seat { RowNo = "1", SeatNo = "B", Price = 200, Status = "Booked" });

        Console.WriteLine($"Retail Customer: {retailCustomer.FirstName} {retailCustomer.LastName}, Card: {retailCustomer.CreditCardType}");
        Console.WriteLine($"Corporate Customer: {corporateCustomer.FirstName} {corporateCustomer.LastName}, Company: {corporateCustomer.CompanyName}");

        Console.WriteLine($"Flight: {flight.FlightId}, From: {flight.Origin} to {flight.Destination}, Seats:");
        foreach (var seat in flight.Seats)
        {
            Console.WriteLine($"Row {seat.RowNo}, Seat {seat.SeatNo}, Price: {seat.Price}, Status: {seat.Status}");
        }
    }
}
