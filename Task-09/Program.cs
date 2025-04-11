using System;
using System.Reflection;

namespace Task09 {
  //Runnable Attribute can be added only for the methods
  [AttributeUsage(AttributeTargets.Method)]
  class RunnableAttribute : Attribute {}

  class A {
    [RunnableAttribute]
    public void SayHello() {
      Console.WriteLine("Hello from Class A");
    }

    // This method will not be executed by reflection because it does not have the [Runnable] attribute. 
    public void SayBye() {
      Console.WriteLine("Bye from Class A");
    }
  }

  class B {
    [RunnableAttribute]
    public void SayHi() {
      Console.WriteLine("Hi from Class B");
    }

    [RunnableAttribute]
    public void SayYo() {
      Console.WriteLine("Yo from Class B");
    }
  }

  class Program {
    static void Main(string[] args) {
      var types = Assembly.GetExecutingAssembly()
                          .GetTypes();

      foreach (var type in types)
      {
          // Getting the methods that are public and instance (non-static) methods
          var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance );
          Console.WriteLine($"Calling methods in Class {type.Name}");

          int count = 0;
          foreach (var method in methods)
          {
              if (method.GetCustomAttributes(typeof(RunnableAttribute), false).Length > 0)
              {
                  count++;
                  var obj = Activator.CreateInstance(type);
                  method.Invoke(obj, null);           
              }
          }
          Console.WriteLine($"{count} runnable methods found in Class{type.Name}\n");
      }
    }
  }
}