
using BackupSystem;
using BackupSystem.Enums;

string configFolder = Path.Combine(AppContext.BaseDirectory, "config");
string configFilePath = Path.Combine(configFolder, "initialConfig.json");
Directory.CreateDirectory(configFolder);
while (true)
{
    InitialConfig initialConfig;
    BackupLogger backupLogger = new BackupLogger();
   
    if (File.Exists(configFilePath))
    {
        initialConfig = backupLogger.LoadInitialConfiguration(configFilePath);
    }
    else
    {
        initialConfig = BackupInput.ConfigurationSetup(configFolder);
        backupLogger.saveInitialConfiguration(initialConfig);
    }
    var backupsPath = initialConfig.GetBackupsPath();//path to the backups directory(all backups data)
    var logsPath = initialConfig.GetLogsPath(); //path to the logs directory(all backups metadata)

    backupLogger.LoadLoggerState(logsPath);
    var loggedBackups = backupLogger.getBackupCount();


    Console.WriteLine("Select database type for backup:\n(1)MariaDB\n(2)PostgreSQL");
    int choice;
    bool parse = int.TryParse(Console.ReadLine(), out choice);
    while (!parse)
    {
        Console.WriteLine("Select a number");
        parse = int.TryParse(Console.ReadLine(), out choice);

    }
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

    var backupPlan = BackupInput.GetBackupStrategy();
    var wrappedBackups = backupLogger.BackupLoggerWrapped();
    if (loggedBackups == 0 && (backupPlan == EnumBackupPlans.incrementalBackup || backupPlan == EnumBackupPlans.differentialBackup))
    {
        Console.WriteLine("No previous backups found, performing full backup first ");
        backupPlan = EnumBackupPlans.fullBackup;
    }
    var baseId = "";
    if (backupPlan == EnumBackupPlans.incrementalBackup || backupPlan == EnumBackupPlans.differentialBackup)
    {
        baseId = BackupInput.RequestedBackupID(wrappedBackups);
    }


    var backup = BackupFactory.GetBackupPlan(backupPlan, wrappedBackups, baseId);



    BackupMetrics backupMetrics = backup.ExecuteBackup(connectionParameters, databaseType, backupPlan, backupsPath);

    backupLogger.addBackupMetrics(backupMetrics, backupMetrics.BackupId);
    backupLogger.saveLoggerState(logsPath);


    Console.WriteLine("===========================================");
    Console.WriteLine($"If you want to change the paths where  program saves files, go to: {initialConfig.GetintialConfigFilePath()}, or delete the file to start new config setup");
    Console.WriteLine("===========================================");
}









