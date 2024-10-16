using System;

public class Program
{
    public static void Main(string[] args)
    {
        
        WorkItem item = new WorkItem("Test Title", "Test Description", TimeSpan.FromHours(1));

        
        ChangeRequest changeRequest = new ChangeRequest(item.ID, "Change Request Title", "Change Description", TimeSpan.FromHours(2));

       
        Console.WriteLine(item.ToString());
        Console.WriteLine(changeRequest.ToString());
    }
}

public class WorkItem
{
    
    private static int currentID;

 
    public int ID { get; private set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TimeSpan JobLength { get; set; }

 
    static WorkItem()
    {
        currentID = 0;
    }

 
    public WorkItem()
    {
        ID = GetNextID();
        Title = "Default title";
        Description = "Default description.";
        JobLength = TimeSpan.Zero;
    }

    
    public WorkItem(string title, string desc, TimeSpan joblen)
    {
        ID = GetNextID();
        Title = title;
        Description = desc;
        JobLength = joblen;
    }


    protected static int GetNextID()
    {
        return ++currentID;
    }

    
    public void Update(string title, TimeSpan joblen)
    {
        Title = title;
        JobLength = joblen;
    }

    public override string ToString()
    {
        return $"{ID}: {Title} (Duration: {JobLength})";
    }
}

public class ChangeRequest : WorkItem
{
    public int OriginalItemID { get; private set; }


    public ChangeRequest(int originalItemID, string title, string desc, TimeSpan joblen)
        : base(title, desc, joblen)
    {
        OriginalItemID = originalItemID;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Original Item ID: {OriginalItemID}";
    }
}
