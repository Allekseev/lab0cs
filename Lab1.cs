using System;

namespace Lab1
{
    class Top
    {
        public double selfX { get; protected set; }
        public double selfY { get; protected set; }   //координаты вершины
        public double selfZ { get; protected set; }
        public Top()
        {
            Console.Write("x: ");
            selfX = Convert.ToDouble(Console.ReadLine());
            Console.Write("y: ");
            selfY = Convert.ToDouble(Console.ReadLine());
            Console.Write("z: ");
            selfZ = Convert.ToDouble(Console.ReadLine());
        }
        public Top(double x, double y, double z)
        {
            selfX = x;
            selfY = y;
            selfZ = z;
        }
        public static bool operator == (Top a, Top b) => (a.selfX == b.selfX) && (a.selfY == b.selfY) && (a.selfZ == b.selfZ);
        public static bool operator != (Top a, Top b) => !(a==b);
        public void Move(double x, double y, double z)   //перемещение вершины
        {
            selfX += x;
            selfY += y;
            selfZ += z;
        }
    }


    class Parallelepiped
    {
        private Top maxTop;   //координаты верхней левой передней вершины
        private Top minTop;   //координаты нижней правой задней вершины

        public Parallelepiped()
        {
            double[] x = new double[2];
            double[] y = new double[2];
            double[] z = new double[2];
            Top[] userTops = new Top[8];
            Console.WriteLine("Введите координаты вершин параллелепипеда");
            Console.WriteLine("Вершина 1:");
            userTops[0] = new Top();
            x[0] = userTops[0].selfX;
            y[0] = userTops[0].selfY;
            z[0] = userTops[0].selfZ;
            for(int i = 1; i < 8; i++)
            {
                Console.WriteLine($"Вершина {i+1}:");
                userTops[i] = new Top();
                if (userTops[i].selfX != x[0])
                    x[1] = userTops[i].selfX;
                if (userTops[i].selfY != y[0])
                    y[1] = userTops[i].selfY;
                if (userTops[i].selfZ !=z[0])
                    z[1] = userTops[i].selfZ;
            }
            minTop = new Top(Math.Min(x[0], x[1]), Math.Min(y[0], y[1]), Math.Min(z[0], z[1]));
            maxTop = new Top(Math.Max(x[0], x[1]), Math.Max(y[0], y[1]), Math.Max(z[0], z[1]));
            Top[] expectedTops = All();
            if (! IsEqual(userTops, expectedTops))
                throw new Exception("Введенные вершины не образуют прямоугольный параллелограмм");
        }
        public void Create(Top a1, Top a2)   //нахождение вершин maxTop и угла minTop
        {
            maxTop = new Top(Math.Max(a1.selfX, a2.selfX), Math.Max(a1.selfY, a2.selfY), Math.Max(a1.selfZ, a2.selfZ));
            minTop = new Top(Math.Min(a1.selfX, a2.selfX), Math.Min(a1.selfY, a2.selfY), Math.Min(a1.selfZ, a2.selfZ));
        }

        private double[] Intersection(double maxA, double minA, double maxB, double minB)
        {
            if (minA >= maxB || minB >= maxA)
                throw new Exception("Нет пересечения");
            return new double[] { Math.Min(maxA, maxB), Math.Max(minA, minB) };
        }
        public Parallelepiped(Parallelepiped p1, Parallelepiped p2)   //новый параллелепипед из пересечения двух других
        {
            double[] x = Intersection(p1.maxTop.selfX, p1.minTop.selfX, p2.maxTop.selfX, p2.minTop.selfX);
            double[] y = Intersection(p1.maxTop.selfY, p1.minTop.selfY, p2.maxTop.selfY, p2.minTop.selfY);
            double[] z = Intersection(p1.maxTop.selfZ, p1.minTop.selfZ, p2.maxTop.selfZ, p2.minTop.selfZ);
            maxTop = new Top(x[0], y[0], z[0]);
            minTop = new Top(x[1], y[1], z[1]);
        }

        public void Move(double x, double y, double z)   //перемещение параллелепипеда
        {
            maxTop.Move(x, y, z);
            minTop.Move(x, y, z);
        }

        public void Resize(double x, double y, double z)   //изменение размеров параллелепипеда
        {
            maxTop.Move(x, y, z);
            if (!(maxTop.selfX > minTop.selfX && maxTop.selfY > minTop.selfY && maxTop.selfZ > minTop.selfZ))
                throw new Exception("Нельзя уменьшить параллелепипед до нулевых/отрицательных размеров");
        }

        private Top[] All()
        {
            Top[] allTops = new Top[8];
            allTops[0] = minTop;
            allTops[1] = new Top(minTop.selfX, minTop.selfY, maxTop.selfZ);
            allTops[2] = new Top(minTop.selfX, maxTop.selfY, minTop.selfZ);
            allTops[3] = new Top(minTop.selfX, maxTop.selfY, maxTop.selfZ);
            allTops[4] = new Top(maxTop.selfX, minTop.selfY, minTop.selfZ);
            allTops[5] = new Top(maxTop.selfX, minTop.selfY, maxTop.selfZ);
            allTops[6] = new Top(maxTop.selfX, maxTop.selfY, minTop.selfZ);
            allTops[7] = maxTop;
            return allTops;
        }

        private bool IsEqual(Top[] userTops, Top[] expectedTops)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (expectedTops[i] == userTops[j])
                        break;
                    if (j == 7)
                        return false;
                }
            return true;
        }

        public void Read()   //вывод на консоль координат вершин параллелепипеда
        {
            Top[] tops = All();
            for (int i = 0; i < 8; i++)
                Console.WriteLine($"координаты вершины {i+1} - x:{tops[i].selfX} y:{tops[i].selfY} z:{tops[i].selfZ}");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Список функций:");
                Console.WriteLine("0 Закрыть программу");
                Console.WriteLine("1 Перемещение");
                Console.WriteLine("2 Изменить размеры");
                Console.WriteLine("3 Пересечение параллелепипедов");
                Console.WriteLine("Введите номер функции");
                string str = Console.ReadLine();
                if (str == "0")
                    break;
                if (str == "1")
                {
                    try
                    {
                        Parallelepiped par = new Parallelepiped();
                        Console.WriteLine("Введите сдвиг");
                        Console.Write("по x ");
                        double x = Convert.ToDouble(Console.ReadLine());
                        Console.Write("по y ");
                        double y = Convert.ToDouble(Console.ReadLine());
                        Console.Write("по z ");
                        double z = Convert.ToDouble(Console.ReadLine());
                        par.Move(x, y, z);
                        Console.WriteLine("Параллелепипед после перемещения");
                        par.Read();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (str == "2")
                {
                    try
                    {
                        Parallelepiped par = new Parallelepiped();
                        Console.WriteLine("Изменение размера");
                        Console.Write("по x ");
                        double x = Convert.ToDouble(Console.ReadLine());
                        Console.Write("по y ");
                        double y = Convert.ToDouble(Console.ReadLine());
                        Console.Write("по z ");
                        double z = Convert.ToDouble(Console.ReadLine());
                        par.Resize(x, y, z);
                        Console.Write("Параллелепипед c изменёнными размерами");
                        par.Read();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (str == "3")
                {
                    try
                    {
                        Console.WriteLine("Параллелепипед 1:");
                        Parallelepiped par1 = new Parallelepiped();
                        Console.WriteLine("Параллелепипед 2:");
                        Parallelepiped par2 = new Parallelepiped();
                        Parallelepiped par = new Parallelepiped(par1, par2);
                        Console.WriteLine("Пересечение:");
                        par.Read();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    Console.WriteLine("Допустимые варианты ввода: 0 1 2 или 3");
            }
        }
    }
}
