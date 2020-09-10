using System;

namespace Lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите последовательность:");
            string answer = "Последовательность возрастающая";
            int num1 = Convert.ToInt32(Console.ReadLine());
            while (true)
            {
                int num2 = Convert.ToInt32(Console.ReadLine());
                if (num2 == 0)
                    break;
                if (num1 >= num2)
                    answer = "Последовательность не возрастающая";
                num1 = num2;
            }
            Console.WriteLine(answer);
        }
    }
}
