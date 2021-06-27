using MySql.Data.MySqlClient;
using System;
using System.Xml;

namespace ItSerwis_Merge_v2
{
    public struct ConfigDatabase
    {
        public string server { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public string database { get; set; }

        public void SetDatabaseConfigData()
        {
            string path = @"G:\Temp\Itserwis\config\database\config.xml";
            System.Xml.XmlDocument xdc = new XmlDocument();
            xdc.Load(path);
            // login
            var login = xdc.SelectSingleNode("root/user/login").InnerText;
            // password
            var pass = xdc.SelectSingleNode("root/user/password").InnerText;
            // server
            var server = xdc.SelectSingleNode("root/connection/server").InnerText;
            // database
            var db = xdc.SelectSingleNode("root/connection/database").InnerText;



            this.server = server;
            this.userid = login;
            this.database = db;
            this.password = pass;

        }
    }
    public class DatabaseConnClass
    {
        public MySqlConnection conn = new MySqlConnection(DatabaseConnectionString());
        private static readonly log4net.ILog log = LogHelper.GetLogger(); //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static string DatabaseConnectionString() // return connection string
        {
            var connString = new ConfigDatabase();
            connString.SetDatabaseConfigData();
            string connectionString = $@"server={connString.server};userid={connString.userid};password={connString.password};database={connString.database}";
            return connectionString;
        }
        public void ConnectToDatabase() // opens database connection
        {
            log.Info("Trying establish the connection to database.");
            try
            {
                conn.Open();
            }
            catch (Exception err)
            {
                log.Error($"Could not establish database connection, the cause: [{err.Message}]");
            }

        }


        public void DisposeConnection()
        {
            conn.Dispose();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public bool IfReaderHasRows(bool rows)
        {
            if (rows)
            {
                DisposeConnection();
                return true;
            }
            DisposeConnection();
            return false;
        }
    }
}
