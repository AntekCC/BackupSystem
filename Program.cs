
using BackupSystem;

Console.WriteLine("Select database type for backup:\n(1)MariaDB\n(2)PostgreSQL");
int choice = int.Parse(Console.ReadLine());
DatabaseType databaseType = (DatabaseType)choice;
var connectionParameters = ConnectionInput.getParameters();

ConnectionStringBuilder connectionStringBuilder = new ConnectionStringBuilder(connectionParameters, databaseType);
var connectionString = connectionStringBuilder.GetConnectionString();
ConnectionService connectionService = new ConnectionService(connectionString);
Boolean isOpen = connectionService.CheckDbConnection();

while (!isOpen)
{
    Console.WriteLine("connection failed , check connection details");
    var newConnectionParameters = ConnectionInput.getParameters();
    connectionStringBuilder = new ConnectionStringBuilder(newConnectionParameters, databaseType);
    connectionService = new ConnectionService(connectionStringBuilder.GetConnectionString());
    isOpen = connectionService.CheckDbConnection();

}

Console.WriteLine("Choose backup plan\n(1) Full backup\n(2) Incremental backup\n(3) Differential backup");
int choice2 = int.Parse(Console.ReadLine());
BackupPlans backupPlans = (BackupPlans)choice2;





