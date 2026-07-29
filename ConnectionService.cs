using MySql.Data.MySqlClient;


namespace BackupSystem
{
    public class ConnectionService
    {
        readonly string connectionString;
        public ConnectionService(string _connectionString)
        {
            this.connectionString = _connectionString;
        }


        public bool CheckDbConnection()
        {


            using (MySqlConnection connCheck = new MySqlConnection(connectionString))

            {
                try
                {
                    connCheck.Open();
                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }

            }

        }
    }
}
