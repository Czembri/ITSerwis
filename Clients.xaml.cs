using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        public static string connectionString = @"server=localhost;userid=root;password=root;database=itserwis";
        public MySqlConnection conn = new MySqlConnection(connectionString);

        public Clients()
        {
            InitializeComponent();
            FillClientsDataGrid();
        }

        private void FillClientsDataGrid()
        {


            try
            {
                conn.Open();
            }
            catch (Exception err)
            {
                log.Error($"Could not get database connection: error - [{err.Message}]");
            }



            string sqlSelectAll = "SELECT firstname, lastname, concat(address1, ' ', address2) as address, city, postcode, VOIVODESHIP, country, phone, email from CUSTOMERS";
            //var cmd = new MySqlCommand(sqlSelectAll);
            MySqlDataAdapter MyDA = new MySqlDataAdapter(sqlSelectAll, conn);
            DataSet clientsData = new DataSet();
            //DataTable clientsData = new DataTable();
            MyDA.Fill(clientsData, "DataBindClients");
            clients.DataContext = clientsData;
            log.Debug("Clients data set created.");
            conn.Close();

        }
    }
}
