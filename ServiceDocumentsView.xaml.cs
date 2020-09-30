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
    /// Logika interakcji dla klasy ServiceDocumentsView.xaml
    /// </summary>
    public partial class ServiceDocumentsView : Window
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        public static string connectionString = @"server=localhost;userid=root;password=root;database=itserwis";
        public MySqlConnection conn = new MySqlConnection(connectionString);

        public ServiceDocumentsView()
        {
            InitializeComponent();
            FillDocumentsGrid();
        }

        private void addDocument(object sender, EventArgs e){
            log.Debug($"Invoking [{sender}].");
            var ServiceShortDoc = new ShortServiceDocument();
            log.Debug($"Invoking form [{ServiceShortDoc}]");
            ServiceShortDoc.Show();

        }

        private void DocumentsFilterFunc(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            var FilterDoc = new DocumentsFilter();
            log.Debug($"Invoking form [{FilterDoc}]");
            FilterDoc.Show();
        }

        private void FillDocumentsGrid()
        {
            try
            {
                conn.Open();
            }
            catch (Exception err)
            {
                log.Error($"Could not get database connection: error - [{err.Message}]");
            }


            string sqlSelectServiceClients = "SELECT id, documentdate, clientname, clientsurename from servicedocument";
            MySqlDataAdapter MyDA = new MySqlDataAdapter(sqlSelectServiceClients, conn);
            DataSet ServiceClientsData = new DataSet();
            MyDA.Fill(ServiceClientsData, "LoadDataBindingDocs");
            ServiceClients.DataContext = ServiceClientsData;

            log.Debug("Documents data set created.");

            conn.Close();
        }
    }
}
