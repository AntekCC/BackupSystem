using System;
using System.Collections.Generic;
using System.Text;

namespace BackupSystem
{
    public class ConnectionStringBuilder
    {
        private ConnectionParameters ConnectionParameters { get; set; }
        private DatabaseType databaseType { get; set; }
        public ConnectionStringBuilder(ConnectionParameters _connectionParameters, DatabaseType _databaseType)
        {
            this.ConnectionParameters = _connectionParameters;
            this.databaseType = _databaseType;
        }
        
        public string GetConnectionString()
        {
            switch (this.databaseType)
            {
                case DatabaseType.MariaDB:
                    return $"server={ConnectionParameters.Host};Port={ConnectionParameters.Port};database={ConnectionParameters.DatabaseName};user={ConnectionParameters.User};password={ConnectionParameters.Password};";
                case DatabaseType.PostgreSQL:
                    return $"Host={ConnectionParameters.Host};Port={ConnectionParameters.Port};Database={ConnectionParameters.DatabaseName};Username={ConnectionParameters.User};Password={ConnectionParameters.Password};";
                default:
                    throw new Exception("Unsupported database type.");
            }
        }

    }

}

