using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Data;
using System.IO.Packaging;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Xml;
using System.IO;
using System.Reflection;


namespace ItSerwis_Merge_v2
{
   
    public class ServiceDocumentsAndDataSets : DatabaseConnClass
    {

        private static readonly log4net.ILog log = LogHelper.GetLogger(); //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DataSet fillDataset(string loaddatabindings, string sql, string nameOfDataSet)
        {
            ConnectToDatabase();

            MySqlDataAdapter MyDA = new MySqlDataAdapter(sql, conn);
            DataSet ItemsData = new DataSet();
            MyDA.Fill(ItemsData, loaddatabindings);
            log.Debug($"{nameOfDataSet} data set created.");
            CloseConnection();
            return ItemsData;
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
            var cmd = new MySqlCommand(sql, conn);
            ConnectToDatabase();
            var reader = cmd.ExecuteReader();
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

            CloseConnection();
            return result;
        }

        public string GetLastDocumentID()
        {
            ConnectToDatabase();
            string docID;
            var sql = "SELECT id from servicedocument order by id desc limit 1";
            var cmd = new MySqlCommand(sql, conn);

            var reader = cmd.ExecuteReader();
            reader.Read();
            try
            {
                docID = reader.GetValue(0).ToString();
                return docID;
            }
            catch (Exception err)
            {
                log.Error($"Error while getting first document id: [{err.Message}]");
            }

            CloseConnection();
            return "";
        }


        public void UpdateServiceDocument(int docID, string customerName, string customerLastName, string customerAddress, string empName, string empLastName, int empNum, string devType, string devBrand, string devModel, string descr)
        {
            try
            {
                ConnectToDatabase();
                var sql = $"UPDATE ITSERWIS.SERVICEDOCUMENT SET " +
                    $"CLIENTNAME='{customerName}', CLIENTSURENAME='{customerLastName}', " +
                    $"CLIENTADDRESS='{customerAddress}', EMPLOYEENAME='{empName}', EMPLOYEESURNAME='{empLastName}', EMPLOYEEID={empNum}, DEVICETYPE='{devType}', " +
                    $"DEVICEBRAND='{devBrand}', DEVICEMODEL='{devModel}', DESCRIPTION='{descr}' WHERE ID={docID}";
                var cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                }
                log.Info($"Updating service document: ['ID':'{docID}', 'CLIENTNAME':'{customerName}'," +
                    $" 'CLIENTSURENAME':'{customerLastName}', 'CLIENTADDRESS':'{customerAddress}']" +
                    $"'EMPLOYEENAME':'{empName}', 'EMPLOYEESURNAME':'{empLastName}', 'EMPLOYEEID':'{empNum}'" +
                    $"'DEVICETYPE':'{devType}', 'DEVICEBRAND':'{devBrand}', 'DEVICEMODEL':'{devModel}', 'DESCRIPTION':'{descr}'");
                CloseConnection();
                MessageBox.Show("Dokument został zaktualizowany");
            }
            catch (Exception err)
            {
                MessageBox.Show($"Wystąpił błąd: {err.Message}");
                log.Error($"Error occured: [{err.Message}]");
            }
        }






        /// <summary>
        /// DbClass method that inserts data into database from short service document form
        /// </summary>
        /// <param name="date"></param>
        /// <param name="customerName"></param>
        /// <param name="customerLastName"></param>
        /// <param name="customerAddress"></param>
        /// <param name="empName"></param>
        /// <param name="empLastName"></param>
        /// <param name="empNum"></param>
        /// <param name="devType"></param>
        /// <param name="devBrand"></param>
        /// <param name="devModel"></param>
        /// <param name="descr"></param>
        public void InsertIntoServiceDocuments(string date, string customerName, string customerLastName, string customerAddress, string empName, string empLastName, int empNum, string devType, string devBrand, string devModel, string descr, string documentnumber)
        {
            UserValidation us = new UserValidation();
            var checkIfEmpExists = us.ValidateUser(empNum);
            if (checkIfEmpExists == true)
            {
                ConnectToDatabase();

                try
                {
                    var stm = $"INSERT INTO ITSERWIS.SERVICEDOCUMENT VALUES (NULL, '{date}', '{customerName}', '{customerLastName}', '{customerAddress}', '{empName}', '{empLastName}', {empNum}, '{devType}', '{devBrand}', '{devModel}', '{descr}', '{documentnumber}')";
                    var cmd = new MySqlCommand(stm, conn);

                    MySqlDataReader reader;

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    log.Error($"Error occured: [{e.Message}]");
                }
            }
            else
            {
                MessageBox.Show($"Pracownik: {empName} {empLastName} o numerze {empNum} nie istnieje.");
            }

            CloseConnection();
        }

        public void InsertIntoClientsFromServiceDocs(string customerName, string customerLastName, string customerAddress, string date, int docID)
        {
            try
            {
                ConnectToDatabase();
                var sql = $"INSERT INTO SERVICECLIENTS(FIRSTNAME, LASTNAME, CREATIONDATE, SERVICEDOCUMENTID, CLIENTADDRESS) VALUES ('{customerName}', '{customerLastName}', '{date}', '{docID}', '{customerAddress}')";
                var cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                }
                log.Info($"Adding client to CUSTOMERS table."+
                    $"CLIENT: ['FIRSTNAME':'{customerName}',"+
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

        public void DeleteServiceDocument(int id)
        {
                ConnectToDatabase();

                try
                {
                    var stm = $"DELETE FROM SERVICEDOCUMENT WHERE ID={id}";
                    var cmd = new MySqlCommand(stm, conn);

                    MySqlDataReader reader;

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                    log.Debug($"Row found: [{reader.HasRows}]");
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    log.Error($"Error occured: [{e.Message}]");
                }
            

            CloseConnection();
        }

    }
}

