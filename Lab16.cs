using System;

namespace Lab16
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("E = ");
            double E = Convert.ToDouble(Console.ReadLine());
            double sum = 0;
            for (int i = 1; true; i++)
            {
                double n = Math.Pow(-1, i) / (i * (i + 1) * (i + 2));
                if (E > Math.Abs(n))
                    break;
                sum += n;
            }
            Console.WriteLine($"сумма = {sum}");
        }
    }
}
