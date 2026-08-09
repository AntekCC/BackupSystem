using BackupSystem.Enums;
using bckp;
using System;
using System.Diagnostics;


namespace BackupSystem
{
    public static class FullMaria
    {
        public static BackupMetrics FullMariaBackup(ConnectionParameters _connectionParameters, EnumDataBaseType _databaseType, EnumBackupPlans _backupType,string _path)
        {
            Process backup = new Process();
            var backupId = BackupIdGenerator.generateId();
            string backupDate = DateTime.Now.ToString("yyyy-MM-dd");

            string backupPath = @$"{_path}\{_backupType}{_databaseType}_#{backupId}";

            backup.StartInfo.UseShellExecute = false;
            backup.StartInfo.FileName = @"D:\bin\mariabackup.exe";
            backup.StartInfo.CreateNoWindow = true;
            backup.StartInfo.Arguments = @$"--backup --target-dir={backupPath} --user={_connectionParameters.User} --password={_connectionParameters.Password} --port={_connectionParameters.Port} --host={_connectionParameters.Host} --databases={_connectionParameters.DatabaseName} --history={_connectionParameters.DatabaseName}_{backupId}";
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                backup.Start();
                stopwatch.Start();
                backup.WaitForExit();
                stopwatch.Stop();

            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"Error:Failed to start the backup process.");
                return new BackupMetrics(false, TimeSpan.Zero, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, "No path-backup_failed",backupId);
            }
            
            if (backup.ExitCode != 0)
            {
                Console.WriteLine($"Error:Failed to backup database.");
                return new BackupMetrics(false, TimeSpan.Zero, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, "No path-backup_failed",backupId);
            }
            return new BackupMetrics(true, stopwatch.Elapsed, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, backupPath,backupId);

        }

    }
}
