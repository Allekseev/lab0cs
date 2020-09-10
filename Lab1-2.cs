using System;

namespace Lab1-2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("a1 = ");
            double a1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("b1 = ");
            double b1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("c1 = ");
            double c1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("a2 = ");
            double a2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("b2 = ");
            double b2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("c2 = ");
            double c2 = Convert.ToDouble(Console.ReadLine());

            if (Math.Abs(a1 * b2 - a2 * b1) >= 0.0001)
            {
                b2 -= a2 / a1 * b1;
                c2 -= a2 / a1 * c1;
                double y = -c2 / b2;
                double x = (-b1 * y - c1) / a1;
                Console.WriteLine($"x = {x}");
                Console.WriteLine($"y = {y}");
            }
            else
                Console.WriteLine("решение этой системы не единственно");
        }
    }
}
