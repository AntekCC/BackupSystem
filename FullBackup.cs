using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace BackupSystem
{
    public class FullBackup : IBackupStrategy
    {
       
        public BackupMetrics ExecuteBackup(string connectionString)
        {
            string filePath = @"D:\Backups";
            Directory.CreateDirectory(filePath);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        Stopwatch FullBackuptimer = new Stopwatch();
                        FullBackuptimer.Start();
                        conn.Open();
                        string backupFile = Path.Combine(filePath, $"FullBackup-{conn.Database}-{DateTime.Now:yyyyMMdd_HHmmss}.sql");
                        mb.ExportToFile(backupFile);
                        FullBackuptimer.Stop();
                        if (mb.LastError != null)
                        {
                            Console.WriteLine("backup failed!");
                            return new BackupMetrics(false, FullBackuptimer.Elapsed, conn.Database,conn.DataSource);
                        }
                        return new BackupMetrics(true, FullBackuptimer.Elapsed, conn.Database, conn.DataSource);

                    }

                }

            }


        }
    }
}
