using System;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a positive integer:");
            int num = Convert.ToInt32(Console.ReadLine());
            if(num < 0) {
                Console.WriteLine("Please Enter a positive integer.");
            } else {
                Console.WriteLine("Factorial of " + num + " is " + Factorial(num));
            }
        }

        static int Factorial(int num) {
            int fact = 1;
            for(int i=1; i<=num; i++){
                fact *= i;
            }
            return fact;
        }
    }
}