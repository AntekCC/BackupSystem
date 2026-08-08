
using BackupSystem;

BackupLogger backupLogger = new BackupLogger();
backupLogger.LoadLoggerState();

while (true)
{
    Console.WriteLine("Select database type for backup:\n(1)MariaDB\n(2)PostgreSQL");
    int choice = int.Parse(Console.ReadLine());
    EnumDataBaseType databaseType = (EnumDataBaseType)choice;
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
    var loggedBackups = backupLogger.getBackupCount();
    if (loggedBackups == 0)
    {
        Console.WriteLine("No previous backups found, performing full backup first");
        backupPlan = EnumBackupPlans.fullBackup;
    }
    var backup = BackupFactory.GetBackupPlan(backupPlan);

    BackupMetrics backupMetrics = backup.ExecuteBackup(connectionParameters, databaseType, backupPlan);
    backupLogger.addBackupMetrics(backupMetrics, backupMetrics.BackupId);
    backupLogger.saveLoggerState();



    Console.WriteLine("siema");

}









