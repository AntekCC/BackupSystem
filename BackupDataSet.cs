

using BackupSystem.Enums;
using System.Runtime.InteropServices;

namespace BackupSystem
{
    public class BackupDataSet
    {
        public ConnectionParameters connectionParameters { get; set; }//login cridentials for the database
        public  BackupMetrics backup { get;  set; } //backup metrics of the database
        public Stack<string> incrementalStack = new(); // chained incremental backups 
        public KeyValuePair<string, string> differentalLinked; // linked differential backup to the base backup - differentalLinked <baseId,DifferentalId>
        public BackupDataSet(ConnectionParameters _connectionParameters, BackupMetrics _backup)
        {
            connectionParameters = _connectionParameters;
            backup = _backup;
           
        }
        public void addIncremental(string backupId)
        {
            incrementalStack.Push(backupId);
        }
        public void addDifferental(KeyValuePair<string, string> Base_Differental) 
        {
            differentalLinked = Base_Differental;
        }
        public  string getBaseId(EnumBackupPlans backupPlan) 
        {
            if (backupPlan == EnumBackupPlans.incrementalBackup)
            {
                var top = incrementalStack.Reverse().First();
                return top ;
            }
          
            else
            {
                return differentalLinked.Key;
            }
        } 
        public void setIdBasedOnType(EnumBackupPlans backupPlan,string id,string databasename, BackupDataSet backupSet)
        {
            if(backupPlan == EnumBackupPlans.incrementalBackup)
            {
                backupSet.incrementalStack.Push($"{databasename}_{id}");
            }
            else
            {
                var baseiId = backupSet.differentalLinked.Key;
                backupSet.differentalLinked = new KeyValuePair<string,string>(baseiId, $"{databasename}_{id}");
            }
        }
    }
}