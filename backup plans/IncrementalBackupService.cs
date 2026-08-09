
using BackupSystem.Enums;
using System.Collections.ObjectModel;

namespace BackupSystem
{
    internal class IncrementalBackupService : IBackupStrategy
    {
        private readonly ReadOnlyDictionary<string, BackupMetrics> _logger;
        private string baseId { get; set; }

        public IncrementalBackupService(ReadOnlyDictionary<string, BackupMetrics> logger, string _baseId)
        {
            _logger = logger;
            this.baseId = _baseId;
        }

        public BackupMetrics ExecuteBackup(ConnectionParameters _connectionParameters, EnumDataBaseType _databaseType, EnumBackupPlans _backupType,string path)
        {
            bool isBaseBackupAvailable = IsBaseBackupAvailable();
            switch (_databaseType)
            {
                case EnumDataBaseType.MariaDB:
                    BackupMetrics maria = IncrementalMaria.IncrementalMariaBackup(_connectionParameters, _databaseType, _backupType, baseId, isBaseBackupAvailable);
                    return maria;
                case EnumDataBaseType.PostgreSQL:
                    break;
                case EnumDataBaseType.MySQL:
                    break;
                default:
                    throw new NotImplementedException($"Backup strategy for {_databaseType} is not implemented.");
            }
            return null;
        }
        public bool IsBaseBackupAvailable()
        {
            if (_logger.ContainsKey(baseId))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

}

