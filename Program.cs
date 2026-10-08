using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    public class OrderManager
    {
        private static readonly OrderManager instance = new OrderManager();
        private decimal totalsum;
        private int servedClients;
        private OrderManager()
        {
            totalsum = 0;
            servedClients = 0;
        }
        public static OrderManager Instance
        {
            get
            {
                return instance;
            }
        }
        public void AddOrder(decimal orderAmount)
        {
            if (orderAmount <= 0)
            {
                throw new ArgumentException("Сумма заказа должна быть больше нуля.");
            }

            totalsum += orderAmount;
            servedClients++;
        }
        public void PrintStatistics()
        {
            Console.WriteLine($"Общая выручка: {totalsum:C}");
            Console.WriteLine($"Количество обслуженных клиентов: {servedClients}");
        }
        public decimal Totalsum
        {
            get { return totalsum; }
        }

        public int ServedClients
        {
            get { return servedClients; }
        }
    }

    ///

    public abstract class Drink
    {
        public abstract string Name { get; }
        public abstract double Price { get; }

        public abstract void Prepare();

        public void Serve()
        {
            Console.WriteLine($"Напиток {Name} подан!");
        }
    }

    public class Coffee : Drink
    {
        public override string Name => "Кофе";
        public override double Price => 150;

        public override void Prepare()
        {
            Console.WriteLine("Кофе приготовлен.");
        }
    }

    public class Tea : Drink
    {
        public override string Name => "Чай";
        public override double Price => 100;

        public override void Prepare()
        {
            Console.WriteLine("Чай приготовлен.");
        }
    }

    public class Smoothie : Drink
    {
        public override string Name => "Смузи";
        public override double Price => 200;

        public override void Prepare()
        {
            Console.WriteLine("Смузи приготовлен.");
        }
    }

    public class DrinkFactory
    {
        public static Drink CreateDrink(string type)
        {
            switch (type)
            {
                case "Кофе":
                    return new Coffee();

                case "Чай":
                    return new Tea();

                case "Смузи":
                    return new Smoothie();

                default:
                    return null;
            }
        }
    }


    ///

    internal class Program
    {

        static void Main(string[] args)
        {
            OrderManager manager = OrderManager.Instance;

            manager.AddOrder(1500);
            manager.AddOrder(2300.50m);
            manager.AddOrder(799.99m);

            manager.PrintStatistics();

            Console.WriteLine();

            OrderManager anotherManager = OrderManager.Instance;

            Console.WriteLine(manager == anotherManager ? "Используется один экземпляр OrderManager" : "Созданы разные экземпляры");

            //

            Console.WriteLine("Какой напиток приготовить? Кофе, Чай, Смузи");

            string userChoice = Console.ReadLine();

            Drink newDrink = DrinkFactory.CreateDrink(userChoice);

            if (newDrink == null)
            {
                Console.WriteLine("Неизвестный тип напитка!");
                return;
            }

            Console.WriteLine($"Цена напитка: {newDrink.Price} рублей");

            newDrink.Prepare();
            newDrink.Serve();
        }
    }
}
