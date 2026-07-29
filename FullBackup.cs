using MySql.Data.MySqlClient;

namespace BackupSystem
{
    public class FullBackup : IBackupStrategy
    {
        public void ExecuteBackup(string connectionString)
        {
            string filePath = @"D:\Backups\db.sql";
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            conn.Open();
                            mb.ExportToFile(filePath);
                        }

                    }

                }

            }
        }
    }
}
