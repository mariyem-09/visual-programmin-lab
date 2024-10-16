using System;

enum Department { CS, IT, ECE }

class Person
{
    public string? Name { get; set; }

    public Person()
    {
        Name = string.Empty;
    }

    public Person(string name)
    {
        Name = name;
    }
}

class Student : Person
{
    public string? RegNo { get; set; }
    public int Age { get; set; }
    public Department Program { get; set; }

    public Student() : base()
    {
        RegNo = string.Empty;
        Age = 0;
        Program = Department.CS;
    }

    public Student(string name, string regNo, int age, Department program) : base(name)
    {
        RegNo = regNo;
        Age = age;
        Program = program;
    }
}

class Program
{
    static void Main()
    {
        Student s1 = new Student();
        Student s2 = new Student("John", "12345", 20, Department.IT);

        Console.WriteLine($"Student 1: Name={s1.Name}, RegNo={s1.RegNo}, Age={s1.Age}, Program={s1.Program}");
        Console.WriteLine($"Student 2: Name={s2.Name}, RegNo={s2.RegNo}, Age={s2.Age}, Program={s2.Program}");
    }
}
