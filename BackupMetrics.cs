
using System.Diagnostics;

namespace BackupSystem
{
    public class BackupMetrics
    {
        public string DatabaseType { get; set; }
        public bool IsSuccess { get; }
        public TimeSpan TimeTaken { get; }
        public string DatabaseName { get; }

        public BackupMetrics(bool _isSuccess, TimeSpan _timeTaken, string _databaseName, string databaseType)
        {
            IsSuccess = _isSuccess;
            TimeTaken = _timeTaken;
            DatabaseName = _databaseName;
            DatabaseType = databaseType;
        }


    }
}
