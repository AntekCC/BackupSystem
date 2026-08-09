using BackupSystem.Enums;
using bckp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace BackupSystem
{
    public static class FullMaria
    {
        public static BackupMetrics FullMariaBackup(ConnectionParameters _connectionParameters, EnumDataBaseType _databaseType, EnumBackupPlans _backupType,string _path)
        {
            Process backup = new Process();
            var backupId = BackupIdGenerator.generateId();
            string backupDate = DateTime.Now.ToString("yyyy-MM-dd");

            string backupPath = @$"{_path}\{_backupType}{_databaseType}{backupDate}_#{backupId}";

            backup.StartInfo.UseShellExecute = false;
            backup.StartInfo.FileName = @"D:\BackupSystem\BackupSystem\Tools\mariabackup.exe";
            backup.StartInfo.CreateNoWindow = true;
            backup.StartInfo.Arguments = @$"--backup --target-dir={backupPath} --user={_connectionParameters.User} --password={_connectionParameters.Password} --port={_connectionParameters.Port} --host={_connectionParameters.Host} --databases={_connectionParameters.DatabaseName} --history={backupId}";
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
                return new BackupMetrics(false, TimeSpan.Zero, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, "No path-backup_failed");
            }
            stopwatch.Stop();
            if (backup.ExitCode != 0)
            {
                Console.WriteLine($"Error:Failed to backup database.");
                return new BackupMetrics(false, TimeSpan.Zero, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, "No path-backup_failed");
            }
            return new BackupMetrics(true, stopwatch.Elapsed, _connectionParameters.DatabaseName, _databaseType, backupId, backupDate, _backupType, backupPath);

        }

    }
}
