using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItSerwis_Merge_v2
{
    class UserValidation : DatabaseConnClass
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger(); //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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



        /// <summary>
        /// checks if user (who filled short service document form) exist in database
        /// </summary>
        /// <param name="empID"></param>
        /// <returns></returns>
        public bool ValidateUser(int empID)
        {
            ConnectToDatabase();


            var stm = $"select ID from USERDATA where ID={empID}";
            var cmd = new MySqlCommand(stm, conn);


            MySqlDataReader reader;

            reader = cmd.ExecuteReader();


            bool checkIfValid = IfReaderHasRows(reader.HasRows);

            return checkIfValid;
        }

    }
}
