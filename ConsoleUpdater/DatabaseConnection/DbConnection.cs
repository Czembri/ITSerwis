using ItSerwis_Merge_v2;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Xml;

namespace ConsoleUpdater
{


    public class DbConnection : DatabaseConnClass
    {

        public new void ConnectToDatabase() // opens database connection
        {
            Console.WriteLine("Trying establish the connection to database.");
            try
            {
                conn.Open();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Could not establish database connection, the cause: [{err.Message}]");
            }

        }

    }
}
