using System;
using System.Collections.Generic;
using System.Text;

namespace BackupSystem
{
    public enum BackupPlans
    {
        fullBackup = 1,
        incrementalBackup,
        differentialBackup
    }
}
