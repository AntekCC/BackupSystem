using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BackupSystem
{
    public class InitialConfig
    {
        public string backupsPath { get; set; }// Path to the backups directory(all backups data)
        public string logsPath { get; set; }// Path to the logs directory(all backups metadata)
        public string intialConfigFilePath { get; set; } // Path to the initial configuration file
        public InitialConfig(string _intialConfigFilePath, string _backupspath, string _logspath)
        {
            intialConfigFilePath = _intialConfigFilePath;
            logsPath = _logspath;
            backupsPath = _backupspath;
        }
        public string GetBackupsPath()
        {
            return backupsPath;
        }
        public string GetLogsPath()
        {
            return logsPath;
        }
        public string GetintialConfigFilePath()
        {
            return intialConfigFilePath;
        }
    }
}
