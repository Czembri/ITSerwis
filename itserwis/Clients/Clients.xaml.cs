using System.Data;
using System.Windows;


namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        public Clients()
        {
            InitializeComponent();
            FillClientsDataGrid();
        }

        private void FillClientsDataGrid()
        {
            ServiceDocumentsAndDataSets dbconn = new ServiceDocumentsAndDataSets();
            string sqlSelectAll = "SELECT firstname, lastname, concat(address1, ' ', address2) as address, city, postcode, VOIVODESHIP, country, phone, email from CUSTOMERS";
            DataSet clientsData = dbconn.fillDataset("DataBindClients", sqlSelectAll, "Clients");
            JustClients.DataContext = clientsData;
        }
    }
}
