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
            DbClass dbconn = new DbClass();
            string sqlSelectAll = "SELECT Id, Firstname, Lastname, Age  from USERDATA";
            DataSet UsersData = dbconn.fillDataset("LoadDataBinding", sqlSelectAll, "Users");
            ListUsers.DataContext = UsersData;
        }
    }
}
