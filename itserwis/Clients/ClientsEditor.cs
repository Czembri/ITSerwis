using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ItSerwis_Merge_v2
{
    public class ClientsEditor : ConnectDB
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger(); //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void InsertIntoClients(string customerName, 
            string customerLastName, string customerAddress, string city, string voivodeship, 
            string postcode, string country, string phone, string email, string nip, string accno)
        {
            try
            {
                ConnectToDatabase();
                var sql = $"INSERT INTO CUSTOMERS(FIRSTNAME, LASTNAME, ADDRESS1, CITY, VOIVODESHIP, POSTCODE, COUNTRY, PHONE, EMAIL, NIP, ACCOUNTNUMBER) VALUES ('{customerName}', '{customerLastName}', '{customerAddress}', '{city}', '{voivodeship}', '{postcode}', '{country}', '{phone}','{email}','{nip}','{accno}')";
                var cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                }
                log.Info($"Adding client to CUSTOMERS table." +
                    $"CLIENT: ['FIRSTNAME':'{customerName}'," +
                    $"'LASTNAME':'{customerLastName}'," +
                    $"'ADDRESS':{customerAddress}");
                CloseConnection();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Wystąpił błąd: {err.Message}");
                log.Error($"Error occured: [{err.Message}]");
            }
        }
    }
}
