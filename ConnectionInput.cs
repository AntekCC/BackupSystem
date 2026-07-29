using System;
using System.Collections.Generic;
using System.Text;

namespace BackupSystem
{
    public static class  ConnectionInput
    {
        public static  ConnectionParameters getParameters()
        {
            Console.WriteLine("Enter database connection details:");
            Console.Write("Host: ");
            string Host = Console.ReadLine();
            Console.Write("Port: ");
            int Port = int.Parse(Console.ReadLine());
            Console.Write("User: ");
            string User = Console.ReadLine();
            Console.Write("Password: ");
            string Password = Console.ReadLine();
            Console.Write("Database name: ");
            string DatabaseName = Console.ReadLine();
            return new ConnectionParameters(Host, Port, User, Password, DatabaseName);
        }
    }
}
