using System;

namespace Prime.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var primeDector = new PrimeDetector();

            while (true)
            {
                Console.Write("Enter a number: ");
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (primeDector.IsPrime(number))
                    {
                        Console.WriteLine("=> {0} is a prime number.", number);
                    }
                    else
                    {
                        Console.WriteLine("=> {0} is not a prime number.", number);
                    }
                }

                Console.WriteLine();
                Console.Write("Do you want to exit [y/n]: ");
                if (Console.ReadLine().ToLowerInvariant() == "y")
                {
                    break;
                }
                Console.WriteLine();
            }
        }
    }
}
