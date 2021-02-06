using System;
using ItSerwis_Merge_v2;
using MySql.Data.MySqlClient;

namespace ConsoleUpdater
{
    public class UserModule 
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Age { get; set; }

        DbConnection conn = new DbConnection();

        public void InsertNewUserCommand()
        {
            conn.ConnectToDatabase();

            try
            {
                var age_ = Convert.ToInt32(this.Age);
                var sql = $"INSERT INTO USERDATA VALUES (NULL, '{this.Username}', '{this.Password}', {age_})";
                var cmd = new MySqlCommand(sql, conn.conn);

                MySqlDataReader reader;

                reader = cmd.ExecuteReader();
                Console.WriteLine("Inserting data to USERDATA table...");
                while (reader.Read())
                {
                    
                }

                conn.CloseConnection();

                conn.ConnectToDatabase();

                var enc = new Encryptor();
                try
                {
                    string encrLogin = enc.EncryptData(Username);
                    string encrPass = enc.EncryptData(Password);

                    var sql__ = $"INSERT INTO USERLOGIN VALUES (NULL, (SELECT ID FROM USERDATA ORDER BY ID DESC LIMIT 1), '{encrLogin}', '{encrPass}')";
                    var cmd__ = new MySqlCommand(sql__, conn.conn);
                    MySqlDataReader reader__;

                    reader__ = cmd__.ExecuteReader();
                    Console.WriteLine("Inserting data to USERLOGIN table...");
                    while (reader__.Read())
                    {
                        
                    }

                    conn.CloseConnection();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
                

                
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

            


        }
    }
}
