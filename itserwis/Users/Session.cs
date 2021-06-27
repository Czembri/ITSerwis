using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ItSerwis_Merge_v2
{
    class Session : DatabaseConnClass
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger(); //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                            CloseConnection();
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
