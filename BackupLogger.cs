

using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Security.Principal;
using ZstdSharp.Unsafe;



namespace BackupSystem
{
    public class BackupLogger
    {
        private Dictionary<string, BackupMetrics> backups = new();

        public void addBackupMetrics(BackupMetrics _backupMetrics, string id)
        {
            backups.Add($"{_backupMetrics.DatabaseName}_{id}", _backupMetrics);
        }
        public void saveLoggerState(string path)
        {

            string x = JsonConvert.SerializeObject(backups, Formatting.Indented);

            Directory.CreateDirectory(path);
            using (StreamWriter writer = new StreamWriter($@"{path}\BackupsLogs.json"))
            {
                writer.Write(x);

            }
        }
        public void LoadLoggerState(string path)
        {
            string _path = @$"{path}\BackupsLogs.json";
            if (File.Exists(@$"{_path}"))
            {
                using (StreamReader reader = new StreamReader(@$"{_path}"))
                {
                    string json = @reader.ReadToEnd();
                    backups = JsonConvert.DeserializeObject<Dictionary<string, BackupMetrics>>(json);
                }
            }
        }

        public int getBackupCount()
        {
            return backups.Count;
        }
        public ReadOnlyDictionary<string, BackupMetrics> BackupLoggerWrapped()
        {
            ReadOnlyDictionary<string, BackupMetrics> backupsWrapper = new ReadOnlyDictionary<string, BackupMetrics>(backups);
            return backupsWrapper;
        }

        public void saveInitialConfiguration(InitialConfig _initialConfig) //this method will save the initial configuration of the backup system to a file and it will never used again until the file is deleted :p 
        {
            var x = JsonConvert.SerializeObject(_initialConfig, Formatting.Indented);
            Directory.CreateDirectory(_initialConfig.GetintialConfigFilePath());
            using (StreamWriter writer = new StreamWriter($@"{_initialConfig.GetintialConfigFilePath()}\initialConfig.json"))
            {
                writer.Write(x);
            }

        }
        public InitialConfig LoadInitialConfiguration(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = @reader.ReadToEnd();
                    InitialConfig initialConfig = JsonConvert.DeserializeObject<InitialConfig>(json);
                    return initialConfig;
                }
            }
            return null;

        }
        public  bool isAvailable(string dataBaseName) // If at least one successful full backup was performed with the specified name,
                                                      // incremental and differential backups will be unlocked.
        {
            foreach (var backup in backups)
            {
                if (backup.Key.Contains($"{dataBaseName}")&& backup.Value.IsSuccess==true)
                {
                    return true;
                }
            }

            return false;
        }


    }
}

