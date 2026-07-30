

namespace BackupSystem
{
    public class BackupLogger
    {
        private Dictionary<DatabaseType, BackupMetrics> BackupLogs = new Dictionary<DatabaseType, BackupMetrics>(); 
        
        public BackupLogger(BackupMetrics _backupMetrics,DatabaseType _databaseType)
        {
            BackupLogs.Add(_databaseType, _backupMetrics);
        }


    }
}
