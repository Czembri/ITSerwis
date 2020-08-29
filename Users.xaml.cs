using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System;

namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        public static string connectionString = @"server=localhost;userid=root;password=root;database=itserwis";
        public MySqlConnection conn = new MySqlConnection(connectionString);
        public Users()
        {
            InitializeComponent();
            ShowAllUsers();
        }

        /// <summary>
        /// displays all application users
        /// </summary>
        public void ShowAllUsers()
        {
            try
            {
                conn.Open();
            } 
            catch (Exception err)
            {
                log.Error($"Could not get database connection: error - [{err.Message}]");
            }
            


            string sqlSelectAll = "SELECT Id, Firstname, Lastname, Age  from USERDATA";

            MySqlDataAdapter MyDA = new MySqlDataAdapter(sqlSelectAll, conn);
            DataSet UsersData = new DataSet();

            MyDA.Fill(UsersData, "LoadDataBinding");
            ListUsers.DataContext = UsersData;

            log.Debug("Users data set created.");

            conn.Close();
        }
    }
}
