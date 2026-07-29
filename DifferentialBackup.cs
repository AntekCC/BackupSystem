
namespace BackupSystem
{
    public class DifferentialBackup : IBackupStrategy
    {
        public void ExecuteBackup(string connectionString)
        {
            Console.WriteLine("Executing differential backup...");
            // Implement the logic for differential backup here
        }

    }
}
