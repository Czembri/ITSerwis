using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy SerwisClients.xaml
    /// </summary>
    public partial class SerwisClients : Window
    {
        public SerwisClients()
        {
            InitializeComponent();
            FillServiceClientsDataGrid();
        }

        /// <summary>
        /// displays service clients datagrid -> takes info from database
        /// </summary>
        private void FillServiceClientsDataGrid()
        {
            DbClass dbconn = new DbClass();
            string sqlSelectServiceClients = "SELECT clientname, clientsurename, clientaddress from servicedocument";
            DataSet serviceClientsData = dbconn.fillDataset("LoadDataBindingClients", sqlSelectServiceClients, "Service Clients");
            ServiceClients.DataContext = serviceClientsData;
        }
    }
}
