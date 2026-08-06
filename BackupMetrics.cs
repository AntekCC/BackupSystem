
using System.Diagnostics;

namespace BackupSystem
{
    public class BackupMetrics
    {
        public EnumDataBaseType DatabaseType { get; set; }
        public bool IsSuccess { get; }
        public TimeSpan TimeTaken { get; }
        public string DatabaseName { get; }
        public string BackupId { get; set; }
        public string BackupDate { get; set; }
        public EnumBackupPlans BackupType { get; set; }

        public BackupMetrics(bool _isSuccess, TimeSpan _timeTaken, string _databaseName, EnumDataBaseType _databaseType, string id_, string _backupDate,EnumBackupPlans _backupType)
        {
            IsSuccess = _isSuccess;
            TimeTaken = _timeTaken;
            DatabaseName = _databaseName;
            DatabaseType = _databaseType;
            BackupId = id_;
            BackupDate = _backupDate;
            BackupType = _backupType;

        }


    }
}
