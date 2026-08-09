
using BackupSystem.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BackupSystem
{
    internal class BackupFactory
    {

        public static IBackupStrategy GetBackupPlan(EnumBackupPlans backupPlan, ReadOnlyDictionary<string, BackupMetrics> BackupsWrapped, string baseId)
        {
            switch (backupPlan)
            {
                case EnumBackupPlans.fullBackup:
                    return new FullBackupService();
                case EnumBackupPlans.incrementalBackup or EnumBackupPlans.differentialBackup:
                    return new DeltaBackupService(BackupsWrapped, baseId);
                default:

                    throw new ArgumentException("invalid backup plan");

            }
        }

    }
}
