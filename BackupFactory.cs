
using System.Diagnostics;

namespace BackupSystem
{
    internal class BackupFactory
    {

        public static IBackupStrategy GetBackupPlan(BackupPlans backupPlan)
        {
            switch (backupPlan)
            {
                case BackupPlans.fullBackup:
                    return new FullBackup();
                case BackupPlans.incrementalBackup:
                    break;
                case BackupPlans.differentialBackup:
                    return new DifferentialBackup();
                default:

                    throw new ArgumentException("invalid backup plan");

            }
            return null;

        }

    }
}
