
namespace BackupSystem
{
    public  abstract class FullBackup : IBackupStrategy
    {

        public BackupMetrics ExecuteBackup(ConnectionParameters _connectionParameters, EnumDataBaseType _databaseType, EnumBackupPlans _backupType)
        {

            switch (_databaseType)
            {
                case EnumDataBaseType.MariaDB:
                    BackupMetrics maria = FullMaria.FullMariaBackup(_connectionParameters, _databaseType, _backupType);
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

    }
}

