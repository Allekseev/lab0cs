using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace Lab3
{
    abstract class Building : IComparable<Building>
    {
        public string address { get; set; }
        abstract public double MidCount();
        abstract override public string ToString();
        public Building()
        {
            address = "without address";
        }
        public void Write(string name)
        {
            Console.WriteLine($"{name}: {ToString()} {address} кол-во жильцов/работников: {MidCount()}");
        }
        public int CompareTo(Building other)
        {
            if (MidCount() != other.MidCount())
                return other.MidCount().CompareTo(MidCount());
            else if (ToString() != other.ToString())
                return ToString().CompareTo(other.ToString());
            return address.CompareTo(other.address);
        }
    }
    class Residential : Building
    {
        public int flatCount { get; set; }
        public int roomCount { get; set; }
        public Residential() : base()
        {
            flatCount = 0;
            roomCount = 0;
        }
        override public double MidCount() => flatCount * roomCount * 1.3;
        override public string ToString() => "Жилое";
    }
    class NonResidential : Building
    {
        public double S{ get; set; }
        public NonResidential() : base()
        {
            S = 0;
        }
        override public double MidCount() => S * 0.2;
        override public string ToString() => "Нежилое";
    }
    class Company
    {
        readonly List<Building> realty;
        private List<Residential> rList;
        private List<NonResidential> nList;
        private double allMids;
        public double buildingMid { get; private set; }
        public Company()
        {
            realty = new List<Building>();
            rList = new List<Residential>();
            nList = new List<NonResidential>();
            allMids = 0;
            buildingMid = 0;
        }
        public void Sort()
        {
            realty.Sort();
        }
        private void Calc(Building b)
        {
            allMids += b.MidCount();
            buildingMid = allMids / realty.Count;
        }
        public void Add(Residential r)
        {
            realty.Add(r);
            rList.Add(r);
            Calc(r);
        }
        public void Add(NonResidential n)
        {
            realty.Add(n);
            nList.Add(n);
            Calc(n);
        }
        public void Del(int num)
        {
            if (num > realty.Count)
                Console.WriteLine($"Зданий в компании только {realty.Count}:");
            else
                realty.RemoveAt(num-1);
        }
        public void Write()
        {
            realty.Sort();
            for (int i = 0; i < realty.Count; i++)
                realty[i].Write((i + 1).ToString());
        }
        public void Write(int part)
        {
            realty.Sort();
            if(Math.Abs(part)>realty.Count)
            {
                Console.WriteLine($"Зданий в компании только {realty.Count}:");
                if (part > 0)
                    part = realty.Count;
                else
                    part = -realty.Count;
            }
            if (part >= 0)
                for(int i = 0; i < part; i++)
                    realty[i].Write((i + 1).ToString());
            else
                for(int i = realty.Count - 1; i >= realty.Count - part; i--)
                    realty[i].Write((i + 1).ToString());
        }
        public void Save(string pathR, string pathN)
        {
            if (Path.GetExtension(pathR) != ".json" || Path.GetExtension(pathN) != ".json")
            {
                Console.WriteLine("Расширение обоих фыйлов должнл быть .json");
                return;
            }
            string jsonString;
            jsonString = JsonSerializer.Serialize(rList);
            File.WriteAllText(pathR, jsonString);
            jsonString = JsonSerializer.Serialize(nList);
            File.WriteAllText(pathN, jsonString);
        }
        public void Load(string pathR, string pathN)
        {
            if (Path.GetExtension(pathR) != ".json" || Path.GetExtension(pathN) != ".json")
            {
                Console.WriteLine("Расширение обоих фыйлов должнл быть .json");
                return;
            }
            allMids = 0;
            buildingMid = 0;
            string jsonString = File.ReadAllText(pathR);
            rList = JsonSerializer.Deserialize<List<Residential>>(jsonString);
            jsonString = File.ReadAllText(pathN);
            nList = JsonSerializer.Deserialize<List<NonResidential>>(jsonString);

            foreach (Residential r in rList)
            {
                realty.Add(r);
                Calc(r);
            }
            foreach (NonResidential n in nList)
            {
                realty.Add(n);
                Calc(n);
            }
            realty.Sort();
        }
    }
    class Program
    {
        static int GetPositive()
        {
            while(true)
            {
                int pos;
                Console.WriteLine("Введите положительное целое число:");
                try
                {
                    pos = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    continue;
                }
                if (pos > 0)
                    return pos;
            }
        }
        static void Main(string[] args)
        {
            string pathR = @"C:\Users\semen\source\repos\Lab3\Lab3\res.json";
            string pathN = @"C:\Users\semen\source\repos\Lab3\Lab3\nonRes.json";
            Company c = new Company();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Список функций:");
                Console.WriteLine("0 Закрыть программу");
                Console.WriteLine("1 Добавить строение");
                Console.WriteLine("2 Список всех строений");
                Console.WriteLine("3 Удалить строение");
                Console.WriteLine("4 Вывести первые 2 объекта");
                Console.WriteLine("5 Вывести последние 3 объекта");
                Console.WriteLine("6 Среднее кол-во жильцов/работников здания по компании");
                Console.WriteLine("7 Сохранить");
                Console.WriteLine("8 Загрузить");
                Console.WriteLine("Введите номер функции");
                string str = Console.ReadLine();
                if (str == "0")
                    break;
                if (str == "1")
                {
                    Console.WriteLine("Введите тип строения (жилое/нежилое):");
                    string t = Console.ReadLine();
                    if (t == "жилое")
                    {
                        Residential r = new Residential();
                        Console.WriteLine("Введите адресс");
                        r.address = Console.ReadLine();
                        Console.WriteLine("Укажите кол-во квартир");
                        r.flatCount = GetPositive();
                        Console.WriteLine("Укажите кол-во комнат в квартире");
                        r.roomCount = GetPositive();
                        c.Add(r);
                    }
                    else if (t == "нежилое")
                    {
                        NonResidential n = new NonResidential();
                        Console.WriteLine("Введите адресс");
                        n.address = Console.ReadLine();
                        Console.WriteLine("Укажите площадь");
                        n.S = GetPositive();
                        c.Add(n);
                    }
                    else
                        Console.WriteLine("Недопустимый тип строения");
                }
                else if (str == "2")
                    c.Write();
                else if (str == "3")
                {
                    Console.WriteLine("Укажите номер строения");
                    c.Del(GetPositive());
                }
                else if (str == "4")
                    c.Write(2);
                else if (str == "5")
                    c.Write(-3);
                else if (str == "6")
                    Console.WriteLine($"Среднее кол-во жильцов/работников здания по компании - {c.buildingMid}");
                else if (str == "7")
                    c.Save(pathR, pathN);
                else if (str == "8")
                    c.Load(pathR, pathN);
                else
                    Console.WriteLine("Допустимые варианты ввода - цифры от 0 до 8");
            }
        }
    }
}
