
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
    connectionParameters = ConnectionInput.getParameters();
    connectionStringBuilder = new ConnectionStringBuilder(connectionParameters, databaseType);
    connectionString = connectionStringBuilder.GetConnectionString();
    connectionService = new ConnectionService(connectionString);
    isOpen = connectionService.CheckDbConnection();

}
var backupPlan = BackupInput.GetBackupPlans();
var backup = BackupFactory.GetBackupPlan(backupPlan);
backup.ExecuteBackup(connectionString);









