using System;
using System.Collections.Generic;
using System.Text;

namespace BackupSystem
{
    public static class ConnectionInput
    {
        public static ConnectionParameters getParameters()
        {
            Console.WriteLine("Enter database connection details:");
            Console.Write("Host: ");
            string Host = Console.ReadLine();
            Console.Write("Port: ");
            int choice;
            bool parse = int.TryParse(Console.ReadLine(), out choice);
            while (!parse)
            {
                Console.WriteLine("port must be a number");
                Console.Write("Port: ");
                parse = int.TryParse(Console.ReadLine(), out choice);

            }
            int Port = choice;
            Console.Write("User: ");
            string User = Console.ReadLine();
            Console.Write("Password: ");
            string Password = Console.ReadLine();
            Console.Write("Database name: ");
            string DatabaseName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(DatabaseName))
            {
                Console.WriteLine("Database name cannot be empty.");
                Console.Write("Database name: ");
                DatabaseName = Console.ReadLine();
            }
            return new ConnectionParameters(Host, Port, User, Password, DatabaseName);
        }
    }
}
