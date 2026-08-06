using bckp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackupSystem
{
    public static class BackupInput
    {
        public static EnumBackupPlans GetBackupPlans()
        {
            Console.WriteLine("Choose backup plan\n(1) Full backup\n(2) Incremental backup\n(3) Differential backup");
            int choice2 = int.Parse(Console.ReadLine());
            EnumBackupPlans plan = (EnumBackupPlans)choice2;
            switch (plan)
            {
                case EnumBackupPlans.fullBackup:
                    Console.WriteLine("You have chosen Full backup");
                    return EnumBackupPlans.fullBackup;

                case EnumBackupPlans.incrementalBackup:
                    Console.WriteLine("You have chosen Incremental backup");
                    return EnumBackupPlans.incrementalBackup;

                case EnumBackupPlans.differentialBackup:
                    Console.WriteLine("You have chosen Differential backup");
                    return EnumBackupPlans.differentialBackup;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return 0;
        }
        public static void NextBackupPlanning()
        {
            Console.WriteLine("Do you want to scheduele your next backup\n(1)yes\n(2)no");
            int choice3 = int.Parse(Console.ReadLine());
            if (choice3 == 1)
            {
                Console.WriteLine("=== Backup Schedule Setup ===");
                Console.WriteLine();
                Console.WriteLine("How often should the backup run?");
                Console.WriteLine();
                Console.WriteLine("(1) Daily");
                Console.WriteLine("(2) Weekly");
                Console.WriteLine("(3) Monthly");
                Console.Write("Your choice (1-3): ");
                int schedueleChoice = int.Parse(Console.ReadLine());
                EnumBackupSchedule scheduelePlan = (EnumBackupSchedule)schedueleChoice;
                var backupPlan = GetBackupPlans();
            }
            else if (choice3 == 2)
            {
                Console.WriteLine("No backups scheduled");
            }
        }
    }
}
