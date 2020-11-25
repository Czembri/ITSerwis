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
            Database_transactions_1 dbconn = new Database_transactions_1();
            string sqlSelectAll = "SELECT Id, Firstname, Lastname, Age  from USERDATA";
            DataSet UsersData = dbconn.fillDataset("LoadDataBinding", sqlSelectAll, "Users");
            ListUsers.DataContext = UsersData;
        }
    }
}
