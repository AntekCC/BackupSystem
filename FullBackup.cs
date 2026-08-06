using bckp;
using System.Diagnostics;

namespace BackupSystem
{
    public class FullBackup : IBackupStrategy
    {
        public BackupMetrics ExecuteBackup(ConnectionParameters _connectionParameters, EnumDataBaseType _databaseType ,EnumBackupPlans _backupType)
        {
            Process backup = new Process();
            string backupDate = DateTime.Now.ToString("yyyy-MM-dd");
            string backupPath = $@"D:\backups\Backup_{backupDate}";
            Directory.CreateDirectory(backupPath);
            var backupId = BackupIdGenerator.generateId();
            string path = @$"{backupPath}\Full-{_databaseType}_{backupId}";
            backup.StartInfo.UseShellExecute = false;
            backup.StartInfo.FileName = @"D:\BackupSystem\BackupSystem\Tools\mariabackup.exe";
            backup.StartInfo.CreateNoWindow = true;
            backup.StartInfo.Arguments = @$"--backup --target-dir={path} --user={_connectionParameters.User} --password={_connectionParameters.Password} --port={_connectionParameters.Port} --host={_connectionParameters.Host} --databases={_connectionParameters.DatabaseName} --history={backupId}";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                backup.Start();
                backup.WaitForExit();
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"Error:Failed to start the backup process.");
                return new BackupMetrics(false, TimeSpan.Zero, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate,_backupType);
            }
            stopwatch.Stop();
            if (backup.ExitCode != 0)
            {
                Console.WriteLine($"Error:Failed to backup database.");
                return new BackupMetrics(false, TimeSpan.Zero, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType);
            }
            return new BackupMetrics(true, stopwatch.Elapsed, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType);
        }

    }
}

