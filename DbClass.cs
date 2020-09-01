using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace ItSerwis_Merge_v2
{
    class DbClass
    {
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
            try
            {
                conn.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Błąd połączenia z bazą danych: <{err}>");
            }


            var stm = $"SELECT LOGINHASH, PASSWORDHASH FROM USERLOGIN WHERE LOGINHASH='{encryptedLog}' AND PASSWORDHASH='{encryptedPass}'";
            var cmd = new MySqlCommand(stm, conn);

            MySqlDataReader reader;

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                conn.Dispose();
                return true;
            }
            else
            {
                conn.Dispose();
                return false;
            }

        }
        /// <summary>
        /// checks if user (who filled short service document form) exist in database
        /// </summary>
        /// <param name="empID"></param>
        /// <returns></returns>
        private bool ValidateUser(int empID)
        {
            try
            {
                conn.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }


            var stm = $"select ID from USERDATA where ID={empID}";
            var cmd = new MySqlCommand(stm, conn);


            MySqlDataReader reader;

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                conn.Dispose();
                return true;
            }
            else
            {
                conn.Dispose();
                return false;
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
            var checkIfEmpExists = ValidateUser(empNum);
            if (checkIfEmpExists == true)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception err)
                {
                    MessageBox.Show($"Błąd połączenia z bazą danych: <{err}>");
                }

                try
                {
                    var stm = $"INSERT INTO ITSERWIS.SERVICEDOCUMENT VALUES (NULL, '{date}', '{customerName}', '{customerLastName}', '{customerAddress}', '{empName}', '{empLastName}', {empNum}, '{devType}', '{devBrand}', '{devModel}', '{descr}', '{documentnumber}')";
                    var cmd = new MySqlCommand(stm, conn);

                    MySqlDataReader reader;

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show($"Pracownik: {empName} {empLastName} o numerze {empNum} nie istnieje.");
            }


        }

    }
}
