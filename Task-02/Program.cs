using System;
class Person {
  string name;
  int age;
  public Person(string name, int age) {
    this.name = name;
    this.age = age;
  }

  public void Introduce() {
    Console.WriteLine($"My name is {name} and I'm {age} years old.");
  }
}
public class Task02 {
  public static void Main(string[] args)
  {
    Person p1 = new Person("Vasu", 22);
    p1.Introduce();
    Person p2 = new Person("Mathi", 19);
    p2.Introduce();
  }
}
  