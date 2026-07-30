
using System.Diagnostics;

namespace BackupSystem
{
    public interface IBackupStrategy
    {
        BackupMetrics ExecuteBackup(string connectionString);
        

    }
}
