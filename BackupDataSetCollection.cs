

using BackupSystem.Enums;
using Newtonsoft.Json;

namespace BackupSystem
{
    internal class BackupDataSetCollection
    {
        public Dictionary<string, BackupDataSet> backupDataSets = new();


        public void addSet(string id, BackupDataSet _backupDataSet)
        {
            backupDataSets.Add(id, _backupDataSet);
        }
        public void updateSet(BackupDataSet _backupSet,EnumBackupPlans backupPlan)
        {
            if (backupPlan == EnumBackupPlans.incrementalBackup)
            {
                backupDataSets[$"{_backupSet.backup.DatabaseName}_{_backupSet.backup.baseId}"].incrementalStack = _backupSet.incrementalStack;
                backupDataSets[$"{_backupSet.backup.DatabaseName}_{_backupSet.backup.baseId}"].incrementalStack.Reverse();
            }
            else
            {
                backupDataSets[$"{_backupSet.backup.DatabaseName}_{_backupSet.backup.baseId}"].differentalLinked = _backupSet.differentalLinked;
            }
        }
        public void saveSets(string path)
        {
            string x = JsonConvert.SerializeObject(backupDataSets, Formatting.Indented);
            Directory.CreateDirectory(path);
            using (StreamWriter writer = new StreamWriter($@"{path}\BackupDataSets.json",false))
            {
                writer.Write(x);
            }
        }
        public int loadSets(string path)
        {
            string _path = @$"{path}\BackupDataSets.json";

            if (File.Exists(_path))
            {
                using (StreamReader reader = new StreamReader(@_path))
                {
                    string json = @reader.ReadToEnd();
                    backupDataSets = JsonConvert.DeserializeObject<Dictionary<string, BackupDataSet>>(json);
                }
            }

            return backupDataSets.Count;
        }
        public BackupDataSet getBackupSet()
        {
            int count = 0;
            Console.WriteLine("Choose one of the options below or create a backup manually.");

            Console.WriteLine("===============================");
            Console.WriteLine("(x) manual backup");
            foreach (var set in backupDataSets)
            {
                Console.WriteLine($"({count}) Backup ID: {set.Key}");
            }
            Console.WriteLine("===============================");

            var gate = true;
            while (gate)
            {
                var choice = Console.ReadLine();

                if (int.TryParse(choice, out int result) && result >= 0 && result < backupDataSets.Count && backupDataSets.TryGetValue(backupDataSets.Keys.ElementAt(result), out BackupDataSet set))
                {
                    Console.WriteLine($"selected set: {backupDataSets.Keys.ElementAt(result)}");
                    return set;
                }


                else if (choice == "x")
                {
                    Console.WriteLine("Starting manual backup.");
                    return null;
                }

                Console.WriteLine("Invalid input. Press 'x' to create a backup manually or choose a valid backup set.");
            }
            return null;

        }
    }
}

