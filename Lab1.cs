using System;

namespace Lab1
{
    class Angle
    {
        public double selfX;
        public double selfY;   //координаты угла
        public double selfZ;
        public Angle()
        {
            Console.Write("x: ");
            selfX = Convert.ToDouble(Console.ReadLine());
            Console.Write("y: ");
            selfY = Convert.ToDouble(Console.ReadLine());
            Console.Write("z: ");
            selfZ = Convert.ToDouble(Console.ReadLine());
        }
        public Angle(double x, double y, double z)
        {
            selfX = x;
            selfY = y;
            selfZ = z;
        }
        public void Move(double x, double y, double z)   //перемещение угла
        {
            selfX += x;
            selfY += y;
            selfZ += z;
        }
    }


    class Parallelepiped
    {
        public Angle max;   //координаты верхнего левого переднего угла
        public Angle min;   //координаты нижнего правого заднего угла

        public Parallelepiped()
        {
            Console.WriteLine("Введите координаты угла параллелепипеда");
            Angle a1 = new Angle();
            Console.WriteLine("Введите координаты противоположного угла");
            Angle a2 = new Angle();
            Create(a1, a2);
        }
        public void Create(Angle a1, Angle a2)   //нахождение угла max и угла min
        {
            max = new Angle(Math.Max(a1.selfX, a2.selfX), Math.Max(a1.selfY, a2.selfY), Math.Max(a1.selfZ, a2.selfZ));
            min = new Angle(Math.Min(a1.selfX, a2.selfX), Math.Min(a1.selfY, a2.selfY), Math.Min(a1.selfZ, a2.selfZ));
        }

        private double[] Intersection(double maxA, double minA, double maxB, double minB)
        {
            if (minA >= maxB || minB >= maxA)
                return new double[] {0,0};
            return new double[] { Math.Min(maxA, maxB), Math.Max(minA, minB) };
        }
        public Parallelepiped(Parallelepiped p1, Parallelepiped p2)   //новый параллелепипед из пересечения двух других
        {
            double[] x = Intersection(p1.max.selfX, p1.min.selfX, p2.max.selfX, p2.min.selfX);
            double[] y = Intersection(p1.max.selfY, p1.min.selfY, p2.max.selfY, p2.min.selfY);
            double[] z = Intersection(p1.max.selfZ, p1.min.selfZ, p2.max.selfZ, p2.min.selfZ);
            max = new Angle(x[0], y[0], z[0]);
            min = new Angle(x[1], y[1], z[1]);
        }
        public bool IsExist()   //проверка существует ли параллелепипед с углами min и max
        {
            if(max.selfX > min.selfX && max.selfY > min.selfY && max.selfZ > min.selfZ)
                return true;
            return false;
        }

        public void Move(double x, double y, double z)   //перемещение параллелепипеда
        {
            max.Move(x, y, z);
            min.Move(x, y, z);
        }

        public void Resize(double x, double y, double z)   //изменение размеров параллелепипеда
        {
            max.Move(x, y, z);
        }

        public void Read()   //вывод на консоль координат углов min и max параллелепипеда
        {
            Console.WriteLine($"координаты угла1 - x:{max.selfX} y:{max.selfY} z:{max.selfZ}");
            Console.WriteLine($"координаты угла2 - x:{min.selfX} y:{min.selfY} z:{min.selfZ}");
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
                    Parallelepiped par = new Parallelepiped();
                    par.Read();
                    if (par.IsExist())
                    {
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
                    else
                        Console.WriteLine("такого параллелепипеда не существует");
                }
                else if (str == "2")
                {
                    Parallelepiped par = new Parallelepiped();
                    par.Read();
                    if (par.IsExist())
                    {
                        Console.WriteLine("Изменение размера");
                        Console.Write("по x ");
                        double x = Convert.ToDouble(Console.ReadLine());
                        Console.Write("по y ");
                        double y = Convert.ToDouble(Console.ReadLine());
                        Console.Write("по z ");
                        double z = Convert.ToDouble(Console.ReadLine());
                        par.Resize(x, y, z);
                        Console.Write("Параллелепипед c изменёнными размерами");
                        if (par.IsExist())
                        {
                            Console.WriteLine();
                            par.Read();
                        }
                        else
                            Console.Write(" не существует");
                    }
                    else
                        Console.WriteLine("такого параллелепипеда не существует");
                }
                else if (str == "3")
                {
                    Console.WriteLine("Параллелепипед 1:");
                    Parallelepiped par1 = new Parallelepiped();
                    Console.WriteLine("Параллелепипед 2:");
                    Parallelepiped par2 = new Parallelepiped();
                    Parallelepiped par = new Parallelepiped(par1, par2);
                    if (par.IsExist())
                    {
                        Console.WriteLine("Пересечение:");
                        par.Read();
                    }
                    else
                        Console.WriteLine("Эти параллелепипеды не пересекаются");
                }
                else
                    Console.WriteLine("Допустимые варианты ввода: 0 1 2 или 3");
            }
        }
    }
}
