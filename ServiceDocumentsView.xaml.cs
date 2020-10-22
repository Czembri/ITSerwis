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
using System.Data;

namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy ServiceDocumentsView.xaml
    /// </summary>
    public partial class ServiceDocumentsView : Window
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

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
            DbClass dbconn = new DbClass();
            string sqlDocs = "SELECT id, documentdate, clientname, clientsurename from servicedocument";
            DataSet docsData = dbconn.fillDataset("LoadDataBindingDocs", sqlDocs, "Documents");
            ServiceClients.DataContext = docsData;
        }
    }
}
