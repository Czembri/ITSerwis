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
        private static readonly log4net.ILog log = LogHelper.GetLogger();
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
            Database_transactions_1 dbconn = new Database_transactions_1();
            string sqlSelectServiceClients = "SELECT FIRSTNAME, LASTNAME, CLIENTADDRESS from SERVICECLIENTS";
            DataSet serviceClientsData = dbconn.fillDataset("LoadDataBindingClients", sqlSelectServiceClients, "Service Clients");
            ServiceClients.DataContext = serviceClientsData;
        }

        private void CreateClient(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            var clientsCreateForm = new CreateClient();
            log.Debug($"Invoking form [{clientsCreateForm}]");
            clientsCreateForm.Show();
        }
    }
}
