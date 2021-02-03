using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ItSerwis_Merge_v2;
using static ItSerwis_Merge_v2.ConnectDB;
using MySql.Data.MySqlClient;
using System.Windows;

namespace ItSerwisAPI
{
    public class DbConnection
    {

       public MySqlConnection conn = new MySqlConnection(DatabaseConnectionString());
        public void CloseConnection()
        {
            conn.Close();
        }

        public void ConnectToDatabase()
        {
            try
            {
                conn.Open();
            } catch (Exception err)
            {
                Console.WriteLine(err); // remember to place logs here 
            }
        }

    

        public void DisposeConnection()
        {
            conn.Dispose();
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


        public struct SelectItems
        {
            public string id { get; set; }
            public string name { get; set; }
            public string barcode { get; set; }
            public string productindex { get; set; }
            public string description { get; set; }
            public string weight { get; set; }
            public string activitystatus { get; set; }
        }

        public SelectItems GetItems()
        {
            string sql = $"SELECT * from item";
            var cmd = new MySqlCommand(sql, conn);
            ConnectToDatabase();
            var reader = cmd.ExecuteReader();
            reader.Read();
            var result = new SelectItems
            {
                id = reader.GetString(0),
                name = reader.GetString(1),
                barcode = reader.GetString(2),
                productindex = reader.GetString(3),
                description = reader.GetString(6),
                weight = reader.GetString(7),
                activitystatus = reader.GetString(11),

            };

            CloseConnection();
            return result;
        }
    }
}