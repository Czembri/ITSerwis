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
        public static string connectionString = @"server=localhost;userid=root;password=root;database=itserwis";
        public MySqlConnection conn = new MySqlConnection(connectionString);
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
            try
            {
                conn.Open();
            }
            catch (Exception err)
            {
                log.Error($"Could not get database connection: error - [{err.Message}]");
            }


            string sqlSelectServiceClients = "SELECT clientname, clientsurename, clientaddress from servicedocument";
            MySqlDataAdapter MyDA = new MySqlDataAdapter(sqlSelectServiceClients, conn);
            DataSet ServiceClientsData = new DataSet();
            MyDA.Fill(ServiceClientsData, "LoadDataBindingClients");
            ServiceClients.DataContext = ServiceClientsData;

            log.Debug("Clients data set created.");

            conn.Close();
        }
    }
}
