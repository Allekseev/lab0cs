using System;

namespace iab2
{
    class MyString
    {
        public readonly char[] line;
        public MyString(string str)
        {
            line = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
                line[i] = str[i];
        }
        public MyString(char[] newLine) 
        {
            line = newLine;
        }
        public int Length => line.Length;
        public char this [int CharNum]
        {
            get { return line[CharNum]; }
        }
        public static MyString operator +(MyString line1, MyString line2)
        {
            char[]res = new char[line1.Length + line2.Length];
            for (int i = 0; i < line1.Length; i++)
                res[i] = line1[i];
            for (int i = 0; i < line2.Length; i++)
                res[i + line1.Length] = line2[i];
            return new MyString(res);
        }
        public static MyString operator *(MyString one, uint mult)
        {
            char[] res = new char[one.Length * mult];
            for (int i = 0; i < mult; i++)
                for (int j = 0; j < one.Length; j++)
                    res[i * one.Length + j] = one[j];
            return new MyString(res);
        }
        public static MyString operator *(uint mult, MyString one) => one * mult;
        public static bool operator ==(MyString line1, MyString line2)
        {
            if (line1.Length != line2.Length)
                return false;
            for (int i = 0; i < line1.Length; i++)
                if (line1[i] != line2[i])
                    return false;
            return true;
        }
        public static bool operator !=(MyString line1, MyString line2) => !(line1 == line2);
        public static bool operator >(MyString line1, MyString line2)
        {
            for (int i = 0; i < Math.Min(line1.Length, line2.Length); i++)
                if (line1[i] > line2[i])
                    return true;
            if (line1.Length > line2.Length)
                return true;
            return false;
        }
        public static bool operator <(MyString line1, MyString line2)
        {
            for (int i = 0; i < Math.Min(line1.Length, line2.Length); i++)
                if (line1[i] < line2[i])
                    return true;
            if (line1.Length < line2.Length)
                return true;
            return false;
        }
        public static bool operator >=(MyString line1, MyString line2) => (line1 > line2) || (line1 == line2);
        public static bool operator <=(MyString line1, MyString line2) => (line1 < line2) || (line1 == line2);
    }
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите номер операции:");
                Console.WriteLine("0 выход из программы");
                Console.WriteLine("1 Сравнение");
                Console.WriteLine("2 Конкатенация");
                Console.WriteLine("3 Умножение на число");
                string op = Console.ReadLine();
                if (op == "0")
                    break;
                else if (op == "1")
                {
                    Console.WriteLine("Введите первую строку:");
                    MyString line1 = new MyString(Console.ReadLine());
                    Console.WriteLine("Выбирите вид сравнения:");
                    Console.WriteLine("== | != | > | >= | < | <=");
                    string com = Console.ReadLine();
                    Console.WriteLine("Введите вторую строку:");
                    MyString line2 = new MyString(Console.ReadLine());
                    if (com == "==")
                        Console.WriteLine($"Результат: {line1 == line2}");
                    else if (com == "!=")
                        Console.WriteLine($"Результат: {line1 != line2}");
                    else if (com == ">")
                        Console.WriteLine($"Результат: {line1 > line2}");
                    else if (com == ">=")
                        Console.WriteLine($"Результат: {line1 >= line2}");
                    else if (com == "<")
                        Console.WriteLine($"Результат: {line1 < line2}");
                    else if (com == "<=")
                        Console.WriteLine($"Результат: {line1 <= line2}");
                    else
                        Console.WriteLine("Такого варианта сравнения нет");
                }
                else if (op == "2")
                {
                    Console.WriteLine("Введите первую строку:");
                    MyString line1 = new MyString(Console.ReadLine());
                    Console.WriteLine("Введите вторую строку:");
                    MyString line2 = new MyString(Console.ReadLine());
                    Console.WriteLine("Результат:");
                    line1 += line2;
                    Console.WriteLine(line1.line);
                }
                else if (op == "3")
                {
                    Console.WriteLine("Введите строку:");
                    MyString line1 = new MyString(Console.ReadLine());
                    Console.WriteLine("Введите число:");
                    UInt32 mult = Convert.ToUInt32(Console.ReadLine());
                    Console.WriteLine("Результат:");
                    line1 *= mult;
                    Console.WriteLine(line1.line);
                }
                else
                    Console.WriteLine("Допустимые варианты ввода: 0 1 2 или 3");
            }
        }
    }
}
