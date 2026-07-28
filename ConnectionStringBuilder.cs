using System;
using System.Collections.Generic;
using System.Text;

namespace BackupSystem
{
    public class ConnectionStringBuilder
    {
       private ConnectionParameters ConnectionParameters {  get; set; }
       private DatabaseType DatabaseType { get; set; }
        public ConnectionStringBuilder(ConnectionParameters _connectionParameters, DatabaseType _databaseType)
        {
            this.ConnectionParameters = _connectionParameters;
            this.DatabaseType = _databaseType;
        }
        
    }

}

