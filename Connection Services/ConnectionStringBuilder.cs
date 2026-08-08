using System;
using System.Collections.Generic;
using System.Text;

namespace BackupSystem
{
    public class ConnectionStringBuilder
    {
        private ConnectionParameters ConnectionParameters { get; set; }
        private EnumDataBaseType databaseType { get; set; }
        public ConnectionStringBuilder(ConnectionParameters _connectionParameters, EnumDataBaseType _databaseType)
        {
            this.ConnectionParameters = _connectionParameters;
            this.databaseType = _databaseType;
        }
        
        public string GetConnectionString()
        {
            switch (this.databaseType)
            {
                case EnumDataBaseType.MariaDB:
                    return $"server={ConnectionParameters.Host};Port={ConnectionParameters.Port};database={ConnectionParameters.DatabaseName};user={ConnectionParameters.User};password={ConnectionParameters.Password};";
                case EnumDataBaseType.PostgreSQL:
                    return $"Host={ConnectionParameters.Host};Port={ConnectionParameters.Port};Database={ConnectionParameters.DatabaseName};Username={ConnectionParameters.User};Password={ConnectionParameters.Password};";
                default:
                    throw new Exception("Unsupported database type.");
            }
        }

    }

}

