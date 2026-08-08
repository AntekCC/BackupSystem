
using System.Diagnostics;

namespace BackupSystem
{
    internal class BackupFactory
    {

        public static IBackupStrategy GetBackupPlan(EnumBackupPlans backupPlan)
        {
            switch (backupPlan)
            {
                case EnumBackupPlans.fullBackup:
                    return new FullBackup();
                case EnumBackupPlans.incrementalBackup:
                    return new IncrementalBackup();
                case EnumBackupPlans.differentialBackup:
                    return new DifferentialBackup();
                default:

                    throw new ArgumentException("invalid backup plan");

            }
        }

    }
}
