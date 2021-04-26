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
    public partial class SerwisClients : Window, ISerwisClients
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        IServiceDocumentsAndDataSets _serviceDocumentsAndDataSets;
        ICreateClient _createClient;
        public SerwisClients(IServiceDocumentsAndDataSets serviceDocumentsAndDataSets, ICreateClient createClient)
        {
            InitializeComponent();
            FillServiceClientsDataGrid();
            _serviceDocumentsAndDataSets = serviceDocumentsAndDataSets;
            _createClient = createClient;
        }

        /// <summary>
        /// displays service clients datagrid -> takes info from database
        /// </summary>
        private void FillServiceClientsDataGrid()
        {
            string sqlSelectServiceClients = "SELECT firstname, lastname, clientaddress from SERVICECLIENTS";
            DataSet serviceClientsData = _serviceDocumentsAndDataSets.fillDataset("LoadDataBindingClients", sqlSelectServiceClients, "Service Clients");
            ServiceClients.DataContext = serviceClientsData;
        }

        private void CreateClient(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            log.Debug($"Invoking form [{_createClient}]");
            _createClient.InitializeComponent();
        }
    }
}
