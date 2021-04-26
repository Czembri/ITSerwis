using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System;

namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy Users.xaml
    /// </summary>
    public partial class Users : Window, IUsers
    {
        IServiceDocumentsAndDataSets _serviceDocumentsAndDataSets;
        public Users(IServiceDocumentsAndDataSets serviceDocumentsAndDataSets)
        {
            InitializeComponent();
            ShowAllUsers();
            _serviceDocumentsAndDataSets = serviceDocumentsAndDataSets;
        }

        /// <summary>
        /// displays all application users
        /// </summary>
        public void ShowAllUsers()
        {
            string sqlSelectAll = "SELECT Id, Firstname, Lastname, Age  from USERDATA";
            DataSet UsersData = _serviceDocumentsAndDataSets.fillDataset("LoadDataBinding", sqlSelectAll, "Users");
            ListUsers.DataContext = UsersData;
        }
    }
}
