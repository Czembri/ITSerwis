using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace ItSerwis_Merge_v2
{

    class DbClass
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger(); //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void ConnectToDatabase()
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


        //variables for mysql connection
        public static string connectionString = @"server=localhost;userid=root;password=root;database=itserwis";
        public MySqlConnection conn = new MySqlConnection(connectionString);
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

        /// <summary>
        /// method that creates login session
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void CreateSession(string username, string password)
        {
            log.Info("Trying establish the connection to database.");
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

