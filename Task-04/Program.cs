using System;

namespace StudentManagement
{
  class Student {
    public string? name {get; set;}
    public int? age {get; set;}
    public int? marks {get; set;}
  }

  class Program {
    static void Main(string[] args) {
      List<Student> students = new List<Student> {
        new Student {name = "Vasu", age = 22, marks = 95},
        new Student { name = "Luffy", age = 19, marks = 45 },
        new Student { name = "Zoro", age = 21, marks = 92 },
        new Student { name = "Nami", age = 20, marks = 88 },
        new Student { name = "Usopp", age = 19, marks = 47 },
        new Student { name = "Sanji", age = 21, marks = 91 },
        new Student { name = "Chopper", age = 19, marks = 49 },
        new Student { name = "Robin", age = 21, marks = 94 },
        new Student { name = "Franky", age = 21, marks = 50 },
        new Student { name = "Brook", age = 22, marks = 87 },
        new Student { name = "Jinbe", age = 20, marks = 94 }
      };


      Console.WriteLine("Enter the passing mark: ");
      int passingMark = Convert.ToInt32(Console.ReadLine());

      var filteredStudents = students.Where(s => s.marks >= passingMark)
                                     .OrderByDescending(s => s.marks)
                                     .ThenBy(s => s.name);


      Console.WriteLine("Students who passed with marks above " + passingMark + " are:");

      foreach(var student in filteredStudents) {
        Console.WriteLine($"Name: {student.name}, Age: {student.age}, Marks: {student.marks}");
      }
    }
  }
}