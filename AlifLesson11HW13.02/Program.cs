using System;
using System.Collections.Generic;
using System.Threading;

namespace AlifLesson11HW13._02
{
    class Program
    {
        static void Main(string[] args)
        {
            TimerCallback timer = new TimerCallback(Client.updateBalance);
            Timer timerC = new Timer(timer, Client.customer, 0, 1000);
            while (true)
            {
                Console.WriteLine(@"Choose Command:
                        1. Insert  ---> 1
                        2. Select  ---> 2
                        3. Update  ---> 3
                        4. Delete  ---> 4");
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Thread insert = new Thread(new ThreadStart(Client.Insert));
                        insert.Start(); insert.Join();
                        break;
                    case 2:
                        Thread select = new Thread(new ThreadStart(Client.Select));
                        select.Start(); select.Join();
                        break;
                    case 3:
                        Thread update = new Thread(new ThreadStart(Client.UpdateById));
                        update.Start(); update.Join();
                        break;
                    case 4:
                        Thread delete = new Thread(new ThreadStart(Client.DeleteById));
                        delete.Start(); delete.Join();
                        break;
                    default:
                        Console.WriteLine("Incorrect Command");
                        break;
                }
            }
        }
    }
    class Client
    {
        public static List<Client> customer = new List<Client>();
        public static List<Client> Checkcustomer = new List<Client>();
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public Client(int ID, string firstName, string lastName, decimal balance)
        {
            Id = ID;
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
        }
        public Client(string firstName, string lastName, decimal balance)
        {
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
        }
        public static void Select()
        {
            foreach (var item in customer)
            {
                Console.WriteLine($"Id: {item.Id}|\t FirstName: {item.FirstName}|\t LastName: {item.LastName}|\t " +
                    $"Balace: {item.Balance}|");
            }
        }
        public static void Insert()
        {
            Console.Clear();
            Console.Write("Enter FirstName: ");
            string FirstName = Console.ReadLine();
            Console.Write("Enter LastName: ");
            string LastName = Console.ReadLine();
            Console.Write("Enter Balance: ");
            var Balance = decimal.Parse(Console.ReadLine());
            Client clients = new Client(FirstName, LastName, Balance);
            customer.Add(clients);
            Checkcustomer.Add(clients);
        }
        public static void UpdateById()
        {
            Console.Clear();
            Console.Write("Enter Id: ");
            int Id = int.Parse(Console.ReadLine());
            Console.Write("Enter FirstName: ");
            string FirstName = Console.ReadLine();
            Console.Write("Enter LastName: ");
            string LastName = Console.ReadLine();
            Console.Write("Enter Balance: ");
            var Balance = decimal.Parse(Console.ReadLine());
            Client clients = new Client(Id, FirstName, LastName, Balance);
            foreach (var item in customer)
            {
                if (Id == item.Id)
                {
                    int index = customer.IndexOf(item);
                    customer[index] = clients;
                    break;
                }
            }
        }
        public static void DeleteById()
        {
            Console.Clear();
            Console.Write("Id: ");
            int Id = int.Parse(Console.ReadLine());
            foreach (var item in customer)
            {
                if (Id == item.Id)
                {
                    Checkcustomer.Remove(item);
                    customer.Remove(item);
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Element deleted from list"); Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }
        }
        public static void updateBalance(object objects)
        {
            List<Client> list = objects as List<Client>;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Balance > Checkcustomer[i].Balance)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID:{list[i].Id}\nBalance before transaction:{Checkcustomer[i].Balance}\nBalance after transaction: {list[i].Balance}\nDifference: +{list[i].Balance - Checkcustomer[i].Balance}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Checkcustomer[i].Balance = list[i].Balance;
                }
                else if (list[i].Balance < Checkcustomer[i].Balance)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ID:{list[i].Id}\nBalance before transaction:{Checkcustomer[i].Balance}\nBalance after transaction: {list[i].Balance}\nDifference: {list[i].Balance - Checkcustomer[i].Balance}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Checkcustomer[i].Balance = list[i].Balance;
                }
            }
        }
    }
}
