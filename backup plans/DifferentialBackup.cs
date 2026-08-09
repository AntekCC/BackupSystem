
using BackupSystem.Enums;

namespace BackupSystem
{
    public class DifferentialBackup : IBackupStrategy
    {
        public BackupMetrics ExecuteBackup(ConnectionParameters connectionParameters, EnumDataBaseType databaseType,EnumBackupPlans backupPlans,string path)
        {
            return null;
        }

    }
}
