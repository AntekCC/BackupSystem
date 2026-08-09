
using BackupSystem.Enums;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Diagnostics;

namespace BackupSystem
{
    public class BackupMetrics
    {
        public EnumDataBaseType DatabaseType { get; set; }
        public bool IsSuccess { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public string DatabaseName { get; set; }
        public string BackupId { get; set; }
        public string BackupDate { get; set; }
        public EnumBackupPlans BackupType { get; set; }
        public string path { get; set; }
        public string baseId { get; set; }//for incremental /differental to know which base backup it is associated with , full backup will have  its own backupId as baseId


        public BackupMetrics(bool _isSuccess, TimeSpan _timeTaken, string _databaseName, EnumDataBaseType _databaseType, string id_, string _backupDate, EnumBackupPlans _backupType, string _path, string _baseId)
        {
            IsSuccess = _isSuccess;
            TimeTaken = _timeTaken;
            DatabaseName = _databaseName;
            DatabaseType = _databaseType;
            BackupId = id_;
            BackupDate = _backupDate;
            BackupType = _backupType;
            path = _path;
            baseId = _baseId;
        }


    }
}
