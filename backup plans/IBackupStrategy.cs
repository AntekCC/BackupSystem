
using BackupSystem.Enums;
using System.Diagnostics;

namespace BackupSystem
{
    public interface IBackupStrategy
    {
        
        BackupMetrics ExecuteBackup(ConnectionParameters connectionParameters,EnumDataBaseType DatabaseType, EnumBackupPlans BackupType,string path);
        

    }
}
