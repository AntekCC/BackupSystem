
using BackupSystem;
using BackupSystem.Enums;
using System.Collections.ObjectModel;

string configFolder = Path.Combine(AppContext.BaseDirectory, "config");
string configFilePath = Path.Combine(configFolder, "initialConfig.json");
InitialConfig initialConfig;
BackupLogger backupLogger = new BackupLogger();
Directory.CreateDirectory(configFolder);
BackupDataSetCollection backupDataSetCollection = new BackupDataSetCollection();

//
EnumDataBaseType databaseType;
ConnectionParameters connectionParameters;
EnumBackupPlans backupPlan;
ReadOnlyDictionary<string, BackupMetrics> wrappedBackups;
bool isAvaiable;
//
var gate = true;
while (gate)
{
    //checks if the initial config file exists, if it does it loads the configuration from the file, if not it creates a new configuration and saves it to the file
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
    var backupSetsPath = initialConfig.GetbackupSetsPath(); //path to the backup sets directory(all backups sets metadata)

    backupLogger.LoadLoggerState(logsPath);
    var loggedBackups = backupLogger.getBackupCount();

    var LoadSetsAndGetCount = backupDataSetCollection.loadSets(backupSetsPath);//load the backup sets from the backup sets directory AND  returns the count of the sets loaded
    var backupSet = backupDataSetCollection.getBackupSet();



    if (backupSet == null)
    {
        //user input for connection parameters and database type
        databaseType = (EnumDataBaseType)BackupInput.GetbackupType();
        connectionParameters = ConnectionInput.getParameters();
    }
    else
    {
        //if valid set was chosen , vars from it are used here.
        databaseType = backupSet.backup.DatabaseType;
        connectionParameters = backupSet.connectionParameters;
    }

    backupPlan = BackupInput.GetBackupStrategy();
    wrappedBackups = backupLogger.BackupLoggerWrapped();
    isAvaiable = backupLogger.isAvailable(connectionParameters.DatabaseName);


    //connection test
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
        isAvaiable = backupLogger.isAvailable(connectionParameters.DatabaseName);
    }


    if ((loggedBackups == 0 && (backupPlan == EnumBackupPlans.incrementalBackup || backupPlan == EnumBackupPlans.differentialBackup)) || (isAvaiable == false))
    {
        Console.WriteLine("No previous backups found, performing full backup first ");
        backupPlan = EnumBackupPlans.fullBackup;
    }

    var baseId = "";
    baseId = BackupInput.RequestedBackupID(wrappedBackups, backupSet, backupPlan);



    //this part is where the backup plan is executed
    var backup = BackupFactory.GetBackupPlan(backupPlan, wrappedBackups, baseId);
    BackupMetrics backupMetrics = backup.ExecuteBackup(connectionParameters, databaseType, backupPlan, backupsPath);
    //


     var decision = BackupInput.AskToCreateBackupSet(backupMetrics.IsSuccess, backupPlan);
    if (decision)
    {
        var set = new BackupDataSet(connectionParameters, backupMetrics);
        set.addIncremental($"{backupMetrics.DatabaseName}_{backupMetrics.baseId}");
        set.addDifferental(new KeyValuePair<string, string>($"{backupMetrics.DatabaseName}_{backupMetrics.baseId}", ""));
        backupDataSetCollection.addSet($"{backupMetrics.DatabaseName}_{backupMetrics.BackupId}", set);
        backupDataSetCollection.saveSets(backupSetsPath);
    }
    else if (backupSet != null && backupPlan != EnumBackupPlans.fullBackup)
    {
        backupSet.setIdBasedOnType(backupPlan, backupMetrics.BackupId,backupMetrics.DatabaseName, backupSet);
        backupDataSetCollection.updateSet(backupSet,backupPlan);
        backupDataSetCollection.saveSets(backupSetsPath);

    }



    backupLogger.addBackupMetrics(backupMetrics, backupMetrics.BackupId);
    backupLogger.saveLoggerState(logsPath);
    Console.WriteLine("===========================================");
    Console.WriteLine($"If you want to change the paths where  program saves files, go to: {initialConfig.GetintialConfigFilePath()}, or delete the file to start new config setup");
    Console.WriteLine("If you want to quit the application, press 'x'. Otherwise, press any key.");
    var quit = Console.ReadKey();
    if (quit.Key == ConsoleKey.X)
    {
        gate = false;
    }
    Console.WriteLine("");
    Console.WriteLine("===========================================");
}









