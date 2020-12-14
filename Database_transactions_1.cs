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
    // Strucure that contains database config data
    public struct ConfigDatabase
    {
        public string server { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public string database { get; set; }

        public void SetDatabaseConfigData()
        {
            string path = @"G:\Temp\Itserwis\config\database\config.xml";
            XmlDocument xdc = new XmlDocument();
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


    public class Database_transactions_1
    {


            // database connection string method
            public static string DatabaseConnectionString()
        {
            var connString = new ConfigDatabase();
            connString.SetDatabaseConfigData();
            string connectionString = $@"server={connString.server};userid={connString.userid};password={connString.password};database={connString.database}";
            return connectionString;
        }


        // init MySQL conn
        internal MySqlConnection conn = new MySqlConnection(DatabaseConnectionString());
        private static readonly log4net.ILog log = LogHelper.GetLogger(); //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal void ConnectToDatabase()
        {
            log.Info("Trying establish the connection to database.");
            try
            {
                conn.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Błąd połączenia z bazą danych: <{err}>");
                log.Error($"Could not establish database connection, the cause: [{err.Message}]");
            }

        }

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


      


        /// <summary>
        /// validate users that login to application
        /// </summary>
        /// <param name="encryptedLog"></param>
        /// <param name="encryptedPass"></param>
        /// <returns></returns>
        public bool CheckLog(string encryptedLog, string encryptedPass)
        {


            ConnectToDatabase();

            var stm = $"SELECT LOGINHASH, PASSWORDHASH FROM USERLOGIN WHERE LOGINHASH='{encryptedLog}' AND PASSWORDHASH='{encryptedPass}'";
            var cmd = new MySqlCommand(stm, conn);

            MySqlDataReader reader;

            reader = cmd.ExecuteReader();

            bool checkIfLogged = IfReaderHasRows(reader.HasRows);

            return checkIfLogged;
        }

        public struct UserCredentials
        {
            public string docid;
            public string firstname;
            public string lastname;

        }

        public UserCredentials GetUserCredentials()
        {
            string docID, firstName, lastName;
            string sql = "SELECT id, firstname, lastname from userdata where id = (select userid from session where status=0 order by 1 desc limit 1) order by 1 desc limit 1";
            var cmd = new MySqlCommand(sql, conn);
            ConnectToDatabase();
            var reader = cmd.ExecuteReader();
            reader.Read();
            docID = reader.GetValue(0).ToString();
            firstName = reader.GetValue(1).ToString();
            lastName = reader.GetValue(2).ToString();
            var result = new UserCredentials
            {
                docid = docID,
                firstname = firstName,
                lastname = lastName
            };


            CloseConnection();
            return result;
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
            }
            catch (Exception err)
            {
                MessageBox.Show($"Wystąpił błąd: {err.Message}");
                log.Error($"Error occured: [{err.Message}]");
            }
        }

        /// <summary>
        /// method that creates login session
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void CreateSession(string username, string password)
        {
            ConnectToDatabase();

            Guid obj = Guid.NewGuid();
            var sessionnumber = obj.ToString();
            try
            {
                var sql = $"INSERT INTO SESSION VALUES (NULL, (SELECT FIRSTNAME FROM USERDATA WHERE ID = (SELECT USERID FROM USERLOGIN WHERE LOGINHASH='{username}' and PASSWORDHASH='{password}')), (SELECT USERID FROM USERLOGIN WHERE LOGINHASH='{username}' and PASSWORDHASH='{password}'), 0, '{sessionnumber}')";
                var cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                }
                CloseConnection();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Wystąpił błąd: {err.Message}");
                log.Error($"Error occured: [{err.Message}]");
            }


        }
        /// <summary>
        /// method that closes login session via update table session (status=1)
        /// </summary>
        public void CloseSession()
        {
            ConnectToDatabase();

            try
            {
                var sql = $"Update session set status=1 where status=0";
                var cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                }


            }
            catch (Exception err)
            {
                MessageBox.Show($"Wystąpił błąd: {err.Message}");
                log.Error($"Error occured: [{err.Message}]");

            }
            CloseConnection();
        }

        /// <summary>
        /// checks if user (who filled short service document form) exist in database
        /// </summary>
        /// <param name="empID"></param>
        /// <returns></returns>
        private bool ValidateUser(int empID)
        {
            ConnectToDatabase();


            var stm = $"select ID from USERDATA where ID={empID}";
            var cmd = new MySqlCommand(stm, conn);


            MySqlDataReader reader;

            reader = cmd.ExecuteReader();


            bool checkIfValid = IfReaderHasRows(reader.HasRows);

            return checkIfValid;
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
            var checkIfEmpExists = ValidateUser(empNum);
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

        public bool ManageSessions()
        {
            var isClosed = false;

            ConnectToDatabase();

            try
            {
                var stm = $"SELECT STATUS FROM SESSION WHERE STATUS=0";
                var cmd = new MySqlCommand(stm, conn);

                MySqlDataReader reader;

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    CloseConnection();
                    var stm2 = $"UPDATE SESSION SET STATUS=1 WHERE STATUS=0";
                    var cmd2 = new MySqlCommand(stm2, conn);

                    MySqlDataReader reader2;
                    try
                    {
                        ConnectToDatabase();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show($"Nie udało się zamknąć sesji: <{err}>");
                        log.Error($"According to previous error [database connection failure], could not close session: [{err.Message}]");
                    }
                    finally
                    {
                        try
                        {
                            reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {

                            }
                        }
                        catch (Exception err)
                        {

                            log.Fatal($"Could not close session: [{err.Message}]");
                        }
                        finally
                        {
                            isClosed = true;
                            conn.Close();
                        }

                    }


                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                log.Error($"Error occured: [{e.Message}]");
            }

            return isClosed;
        }

    }
}

