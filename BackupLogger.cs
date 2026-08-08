

using Newtonsoft.Json;


namespace BackupSystem
{
    public class BackupLogger
    {
        private Dictionary<string, BackupMetrics> backups = new Dictionary<string, BackupMetrics>();


        public void addBackupMetrics(BackupMetrics _backupMetrics, string id)
        {
            backups.Add(id, _backupMetrics);
        }

        public void saveLoggerState()
        {
            string x = JsonConvert.SerializeObject(backups, Formatting.Indented);
            string backupPath = @"D:\BackupLogger";
            Directory.CreateDirectory(backupPath);
            using (StreamWriter writer = new StreamWriter($@"{backupPath}\BackupLogger.json"))
            {
                writer.Write(x);

            }

        }
        public void LoadLoggerState()
        {
            string backupPath = @"D:\BackupLogger\BackupLogger.json";
            if (File.Exists(backupPath))
            {
                using (StreamReader reader = new StreamReader(backupPath))
                {
                    string json = reader.ReadToEnd();
                    backups = JsonConvert.DeserializeObject<Dictionary<string, BackupMetrics>>(json);
                }
            }
        }

        public int getBackupCount()
        {
            return backups.Count;
        }
    }
}
