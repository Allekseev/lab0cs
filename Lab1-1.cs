using System;

namespace Lab1-1cs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("x = ");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.Write("y = ");
            double y = Convert.ToDouble(Console.ReadLine());
            Console.Write("z = ");
            double z = Convert.ToDouble(Console.ReadLine());

            double a1 = (Math.Abs((y - Math.Sqrt(Math.Abs(x))) * (x - y / (z + Math.Pow(x, 2) / 4))));
            double a = Math.Log(a1);
            if (double.IsNaN(a) || a1==0)
                Console.WriteLine("a не удовлетворяет ОДЗ");
            else
                Console.WriteLine($"a = {a}");

            double b = x - Math.Pow(x, 2) / 6 + Math.Pow(x, 5) / 15;
            if (double.IsNaN(b))
                Console.WriteLine("b не удовлетворяет ОДЗ");
            else
                Console.WriteLine($"b = {b}");
        }
    }
}
