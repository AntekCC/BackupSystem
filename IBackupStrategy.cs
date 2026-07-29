
namespace BackupSystem
{
    public interface IBackupStrategy
    {
        void ExecuteBackup(string connectionString);
    }
}
