using System;

namespace Program {
  class Task03 {
    public static void Main(string[] args) {
      User user = new User();
      bool flag = true;
      while(flag) {
        Console.WriteLine("\n----------------------------");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Remove");
        Console.WriteLine("3. Display");
        Console.WriteLine("4. Exit");
        Console.WriteLine("----------------------------");

        Console.WriteLine("Enter your choice:");
        string? input = Console.ReadLine();
        int choice;
        if (int.TryParse(input, out choice)) {
          switch(choice) {
            case 1:
              user.AddName();
              break;

            case 2:
              user.RemoveName();
              break;

            case 3:
              user.DisplayNames();
              break;

            case 4:
              flag = false;
              Console.WriteLine("Exiting....");
              break;

            default: 
              Console.WriteLine("Invalid choice");
              break;
          } 
        } else {
          Console.WriteLine("Invalid input");
        }
      }
    }
  }

}