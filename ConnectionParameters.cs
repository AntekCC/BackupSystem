using System;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BackupSystem
{
    public class ConnectionParameters
    {
        public readonly string Host;
        public readonly int Port;
        public readonly string User;
        public readonly string Password;
        public readonly string DatabaseName;


            public ConnectionParameters(string host, int port, string user, string password, string databaseName)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
            DatabaseName = databaseName;
        }


    }
}

