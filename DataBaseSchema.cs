using BackupSystem;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bckp
{
    public class DataBaseSchema
    {
        public void getDataBaseSchema(string connectionString)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT TABLE_NAME AS 'table' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{conn.Database}';";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["table"]}"); 
                        }
                    }

                }

            }
        }
    }
}
