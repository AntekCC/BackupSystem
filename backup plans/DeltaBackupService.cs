
using BackupSystem.Enums;
using System.Collections.ObjectModel;

namespace BackupSystem
{
    internal class DeltaBackupService : IBackupStrategy
    {
        private readonly ReadOnlyDictionary<string, BackupMetrics> _logger;
        private string baseId { get; set; }


        public DeltaBackupService(ReadOnlyDictionary<string, BackupMetrics> logger, string _baseId)
        {
            _logger = logger;
            this.baseId = _baseId;

        }

        public BackupMetrics ExecuteBackup(ConnectionParameters _connectionParameters, EnumDataBaseType _databaseType, EnumBackupPlans _backupType, string path)
        {
            var isBaseBackupAvailable = IsBaseBackupAvailable();

            if (_backupType == EnumBackupPlans.differentialBackup)
            {
                this.baseId = DifferentalBackupService();
            }

            switch (_databaseType)
            {
                case EnumDataBaseType.MariaDB:
                    BackupMetrics maria = DeltaMaria.DeltaMariaBackup(_connectionParameters, _databaseType, _backupType, baseId, isBaseBackupAvailable, path);
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
        public string DifferentalBackupService()
        {
            var isBaseFound = _logger.TryGetValue(baseId, out BackupMetrics metrics);
            if (isBaseFound && metrics.BackupType == EnumBackupPlans.fullBackup)
            {
                return baseId;
            }
            else if (isBaseFound && (metrics.BackupType == EnumBackupPlans.differentialBackup || metrics.BackupType == EnumBackupPlans.incrementalBackup))
            {
                Console.WriteLine("selected ID must refer to a full backup only");
                return null;
            }
            return null;
        }

    }
}

