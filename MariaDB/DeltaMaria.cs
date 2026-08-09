using BackupSystem.Enums;
using bckp;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BackupSystem
{
    public static class DeltaMaria
    {
        public static BackupMetrics DeltaMariaBackup(ConnectionParameters _connectionParameters, EnumDataBaseType _databaseType, EnumBackupPlans _backupType, string baseId, bool _isBaseBackupAvailable, string _path)
        {
            var backupId = BackupIdGenerator.generateId();
            string backupDate = DateTime.Now.ToString("yyyy-MM-dd");
            string backupPath = @$"{_path}\{_backupType}{_databaseType}_#{backupId}";
            if (!_isBaseBackupAvailable)
            {
                Console.WriteLine("Base backup is not available. Cannot perform  backup.");
                return new BackupMetrics(false, TimeSpan.Zero, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, "No path - backup_failed", baseId);
            }
            if (_backupType == EnumBackupPlans.differentialBackup)
            {
                string id = baseId.Split('_').Last();
                backupPath = @$"{_path}\{_backupType}{_databaseType}_#{id}";
                Directory.Delete(backupPath,true);//Very unstable method — needs improvement.
            }
            Process backup = new Process();
            backup.StartInfo.FileName = @"D:\bin\mariabackup.exe";
            backup.StartInfo.Arguments = @$"--backup --target-dir={backupPath} --incremental-history-name={baseId} --history={_connectionParameters.DatabaseName}_{backupId} --user={_connectionParameters.User} --password={_connectionParameters.Password} --port={_connectionParameters.Port} --host={_connectionParameters.Host} --databases={_connectionParameters.DatabaseName}";
            backup.StartInfo.UseShellExecute = false;
            backup.StartInfo.CreateNoWindow = true;
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
                return new BackupMetrics(false, TimeSpan.Zero, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, "No path - backup_failed", baseId);
            }
            if (backup.ExitCode != 0)
            {
                Console.WriteLine($"Error:Failed to backup database.");
                return new BackupMetrics(false, TimeSpan.Zero, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, "No path - backup_failed", baseId);
            }

            return new BackupMetrics(true, stopwatch.Elapsed, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, backupPath, baseId);
        }
    }
}
