using System;

namespace Lab13
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            for (int i = 22; i < 100; i += 2)
                if (i % 10 != 0 && i % 3 == 0)
                    sum+=i;
            Console.WriteLine($"сумма равна {sum}");
        }
    }
}
