using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml;
using MySql.Data.MySqlClient;

namespace ItSerwis_Merge_v2
{
    class Database_transactions_2 : Database_transactions_1
    {

        private static readonly log4net.ILog log = LogHelper.GetLogger();

        private void ConnectToDatabaseInh()
        {
            Database_transactions_1 dbTrans_1 = new Database_transactions_1();
            dbTrans_1.ConnectToDatabase();
        }   

        public struct ServiceDocumentOnRowClickValues
        {
            public string id { get; set; }
            public string documentdate { get; set; }
            public string clientname { get; set; }
            public string clientsurename { get; set; }
            public string clientaddress { get; set; }
            public string employeename { get; set; }
            public string employeesurename { get; set; }
            public string employeeid { get; set; }
            public string devicetype { get; set; }
            public string devicebrand { get; set; }
            public string devicemodel { get; set; }
            public string description { get; set; }
            public string internaldocumentid { get; set; }
        }

              public ServiceDocumentOnRowClickValues GetServiceDocumentFromDatabase(int id)
        {
            string sql = $"select * from servicedocument where id={id}";
            Database_transactions_1 dbTrans_1 = new Database_transactions_1();
            ConnectToDatabase();
            conn = dbTrans_1.conn;
            var cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();
            var result = new ServiceDocumentOnRowClickValues
            {
                id = reader.GetString(0),
                documentdate = reader.GetString(1),
                clientname = reader.GetString(2),
                clientsurename = reader.GetString(3),
                clientaddress = reader.GetString(4),
                employeename = reader.GetString(5),
                employeesurename = reader.GetString(6),
                employeeid = reader.GetString(7),
                devicetype = reader.GetString(8),
                devicebrand = reader.GetString(9),
                devicemodel = reader.GetString(10),
                description = reader.GetString(11),
                internaldocumentid = reader.GetString(12)
            };

            return result;
        }
        

  
    }
}
