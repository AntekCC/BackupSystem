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
            Random rand = new Random();
            string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?";
            var generator = chars.ToList().Select(y => chars[rand.Next(0, chars.Count())]).Take(5).ToList();
            var id = String.Join("", generator);
            return id;
        }
    }
}
