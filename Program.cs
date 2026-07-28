
using BackupSystem;



Console.WriteLine("Select database type for backup:\n(1)MariaDB\n(2)PostgreSQL");
int choice = int.Parse(Console.ReadLine());
DatabaseType type = (DatabaseType)choice;

Console.WriteLine("Enter database connection details:");
Console.Write("Host: ");
string host = Console.ReadLine();

Console.Write("Port: ");
int port = int.Parse(Console.ReadLine());

Console.Write("User: ");
string user = Console.ReadLine();

Console.Write("Password: ");
string password = Console.ReadLine();

Console.Write("Database name: ");
string databaseName = Console.ReadLine();

ConnectionParameters connectionParameters = new ConnectionParameters(host, port, user, password, databaseName);
ConnectionStringBuilder connectionStringBuilder = new ConnectionStringBuilder(connectionParameters, type);

ConnectionService connectionService = new ConnectionService(connectionParameters);
connectionService.CheckDbConnection();

