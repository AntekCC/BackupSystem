using BackupSystem.Enums;
using bckp;
using System.Collections.ObjectModel;


namespace BackupSystem
{
    public static class BackupInput
    {
        public static EnumBackupPlans GetBackupStrategy()
        {
            Console.WriteLine("Choose backup plan\n(1) Full backup\n(2) Incremental backup\n(3) Differential backup");
            int choice2 = int.Parse(Console.ReadLine());
            EnumBackupPlans plan = (EnumBackupPlans)choice2;
            switch (plan)
            {
                case EnumBackupPlans.fullBackup:
                    Console.WriteLine("You have chosen Full backup");
                    return EnumBackupPlans.fullBackup;

                case EnumBackupPlans.incrementalBackup:
                    Console.WriteLine("You have chosen Incremental backup");
                    return EnumBackupPlans.incrementalBackup;

                case EnumBackupPlans.differentialBackup:
                    Console.WriteLine("You have chosen Differential backup");
                    return EnumBackupPlans.differentialBackup;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return 0;
        }
        public static void NextBackupPlanning()
        {
            Console.WriteLine("Do you want to scheduele your next backup\n(1)yes\n(2)no");
            int choice3 = int.Parse(Console.ReadLine());
            if (choice3 == 1)
            {
                Console.WriteLine("=== Backup Schedule Setup ===");
                Console.WriteLine();
                Console.WriteLine("How often should the backup run?");
                Console.WriteLine();
                Console.WriteLine("(1) Daily");
                Console.WriteLine("(2) Weekly");
                Console.WriteLine("(3) Monthly");
                Console.Write("Your choice (1-3): ");
                int schedueleChoice = int.Parse(Console.ReadLine());
                EnumBackupSchedule scheduelePlan = (EnumBackupSchedule)schedueleChoice;
                var backupPlan = GetBackupStrategy();
            }
            else if (choice3 == 2)
            {
                Console.WriteLine("No backups scheduled");
            }
        }
        public static string RequestedBackupID(ReadOnlyDictionary<string, BackupMetrics> _wrapped)
        {
            Console.WriteLine("");
            Console.WriteLine("Most recent 10 backups (for reference only, you can enter any ID)");
            foreach (var key in _wrapped.Reverse().Take(10))
            {
                Console.WriteLine($"| Backup ID: {key.Key}| Database Name: {key.Value.DatabaseName}| Backup Type: {key.Value.BackupType}| Backup Date: {key.Value.BackupDate} | Backup  completed: {key.Value.IsSuccess} |");
            }
            Console.WriteLine("");
            Console.WriteLine("Enter the backup ID you want to use as the base for your incremental/differential backup.");
            string backupId = Console.ReadLine();
            return backupId;
        }
    
    public static InitialConfig ConfigurationSetup(string _initialConfigFilePath)
        {
            Console.WriteLine("=== Initial Backup Configuration ===");
            Console.WriteLine();
            Console.WriteLine("These settings can only be configured once.");
            Console.WriteLine("These settings cannot be changed within the application. To change them, modify or delete the initial configuration file manually.");
            Console.WriteLine();

            Console.Write("Enter backups directory path: ");
            string backupsPath = Console.ReadLine();

            Console.Write("Enter logs directory path: ");
            string logsPath = Console.ReadLine();
            if (Directory.Exists(logsPath) && Directory.Exists(_initialConfigFilePath) && Directory.Exists(backupsPath)) { return new InitialConfig(_initialConfigFilePath, backupsPath, logsPath); }
            else
            {
                Console.WriteLine("One or more of the provided paths do not exist. Please ensure that the paths are correct and try again.");
                ConfigurationSetup(_initialConfigFilePath);
            }
            return null;
        }
    }

}




