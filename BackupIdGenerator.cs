using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bckp
{
    public static class BackupIdGenerator
    {
        public static string generateId()
        {
            return Guid.NewGuid().ToString("N")[..8].ToString();
        }
    }
}
