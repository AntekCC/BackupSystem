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

        public ConnectionParameters(string _host, int _port, string _user, string _password, string _databaseName)
        {
            Host = _host;
            Port = _port;
            User = _user;
            Password = _password;
            DatabaseName = _databaseName;

        }

    }
}

