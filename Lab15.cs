using System;

namespace Lab15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("n = ");
            int n = Convert.ToInt32(Console.ReadLine());
            double res = 0;
            for (int i = n; i > 0; i--)
                res = Math.Sqrt(res) + 3 * i;
            Console.WriteLine($"b = {Math.Sqrt(res)}");
        }
    }
}
