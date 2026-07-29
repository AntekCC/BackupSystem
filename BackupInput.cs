using System;
using System.Collections.Generic;
using System.Text;

namespace BackupSystem
{
    public static class BackupInput
    {
        public static BackupPlans GetBackupPlans()
        {
            Console.WriteLine("Choose backup plan\n(1) Full backup\n(2) Incremental backup\n(3) Differential backup");
            int choice2 = int.Parse(Console.ReadLine());
            BackupPlans plan = (BackupPlans)choice2;
            switch (plan)
            {
                case BackupPlans.fullBackup:
                    Console.WriteLine("You have chosen Full backup");
                    return BackupPlans.fullBackup;

                case BackupPlans.incrementalBackup:
                    Console.WriteLine("You have chosen Incremental backup");
                    return BackupPlans.incrementalBackup;

                case BackupPlans.differentialBackup:
                    Console.WriteLine("You have chosen Differential backup");
                    return BackupPlans.differentialBackup;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return 0 ;
        }
    }
}
