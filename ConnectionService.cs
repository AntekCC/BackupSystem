using MySql.Data.MySqlClient;


namespace BackupSystem
{
    public class ConnectionService
    {
        readonly ConnectionParameters _connectionParameters;
        public ConnectionService(ConnectionParameters connectionParameters)
        {
            this._connectionParameters = connectionParameters;
        }

        public bool CheckDbConnection()
        {
            string connString = $"server={_connectionParameters.Host};Port={_connectionParameters.Port};database={_connectionParameters.DatabaseName};user={_connectionParameters.User};password={_connectionParameters.Password};";
            using (MySqlConnection connCheck = new MySqlConnection(connString))

            {
                try
                {
                    connCheck.Open();
                    return true;
                }
                catch (Exception e) 
                { 
                Console.WriteLine(e.ToString());
                    return false;
                }
               
            }

        }
    }
}
