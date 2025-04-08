using System;
using System.Collections.Generic;

class User {
    private List<string> list = new List<string>();
    public void AddName() {
      Console.WriteLine("Enter the name to add");
      string? name = Console.ReadLine();
      if(!string.IsNullOrWhiteSpace(name)) {
        name = name.Trim().ToUpper();
        list.Add(name);
        Console.WriteLine($"{name} is added to the list.");
      } else {
        Console.WriteLine("Please enter a valid name !");
      }
    }

    public void RemoveName() {
      Console.WriteLine("Enter the name to remove");
      string? nameToRemove = Console.ReadLine();
      if(!string.IsNullOrWhiteSpace(nameToRemove)){
        nameToRemove = nameToRemove.Trim().ToUpper();
        if (list.Contains(nameToRemove)) {
          list.Remove(nameToRemove);
          Console.WriteLine($"Name {nameToRemove} is removed from the list");
        }
        else {
          Console.WriteLine("Name not found");
        }
      } else {
        Console.WriteLine("Please enter a valid name !");
      }
    }

    public void DisplayNames() {
      Console.WriteLine("List of names:");
      foreach (string name in list) {
        Console.Write(name + " ");
      }
      Console.WriteLine();
    }
}