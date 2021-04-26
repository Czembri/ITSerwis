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
using System.Web.UI.WebControls;

namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy ServiceDocumentsView.xaml
    /// </summary>
    public partial class ServiceDocumentsView : Window, IServiceDocumentsView
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        IShortServiceDocument _shortServiceDocument;
        IDocumentsFilter _documentsFilter;
        IServiceDocumentsAndDataSets _serviceDocumentsAndDataSets;

        public ServiceDocumentsView(IShortServiceDocument shortServiceDocument, 
            IDocumentsFilter documentsFilter,
            IServiceDocumentsAndDataSets serviceDocumentsAndDataSets
            )
        {
            InitializeComponent();
            FillDocumentsGrid();
            _shortServiceDocument = shortServiceDocument;
            _documentsFilter = documentsFilter;
            _serviceDocumentsAndDataSets = serviceDocumentsAndDataSets;
        }

        private void addDocument(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            log.Debug($"Invoking form [{_shortServiceDocument}]");
            _shortServiceDocument.InitializeComponent();

        }

        private void DocumentsFilterFunc(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            log.Debug($"Invoking form [{_documentsFilter}]");
            _documentsFilter.InitializeComponent();
        }

        private void FillDocumentsGrid()
        {
            string sqlDocs = "SELECT id, documentdate, clientname, clientsurename from servicedocument";
            DataSet docsData = _serviceDocumentsAndDataSets.fillDataset("LoadDataBindingDocs", sqlDocs, "Documents");
            ServiceDocuments.DataContext = docsData;
        }



        private void MenuItem_RightClickEdit(object sender, EventArgs e)
        {

            DataRowView dataRowView = (DataRowView)ServiceDocuments.SelectedItem;
            try
            {
                int ID = Convert.ToInt32(dataRowView.Row[0]);
                log.Info($"Retrieving id from DataGrid: ['DataGrid':'Retrieving', 'DocumentId':{ID}]");
                var docDetails = _serviceDocumentsAndDataSets.GetServiceDocumentFromDatabase(ID);
                log.Debug($"Invoking method: [{_serviceDocumentsAndDataSets.GetServiceDocumentFromDatabase(ID)}] with 'ID':{ID} as an argument");

                _shortServiceDocument.docID = ID;
                _shortServiceDocument.InitializeComponent();
            }
            catch (Exception err)
            {
                log.Error($"Could not open service document\nError: [{err}]");
                Close();
            }





        }

        private void MenuItem_RightClickDelete(object sender, EventArgs e)
        {
            DataRowView dataRowView = (DataRowView)ServiceDocuments.SelectedItem;
            int ID = Convert.ToInt32(dataRowView.Row[0]);

            log.Info($"Retrieving id from DataGrid: ['DataGrid':'Retrieving', 'DocumentId':{ID}]");


            try
            {
                _serviceDocumentsAndDataSets.DeleteServiceDocument(ID);
                log.Info($"Deleting service document: ['ServiceDocument':'{ID}']");
                MessageBox.Show("Dokument został usunięty.");
            }
            catch (Exception err)
            {
                log.Error($"Error occured: ['Error':{err.Message}]");
                MessageBox.Show($"Wystąpił błąd: [{err.Message}]");
            }
            ServiceDocuments.Items.Refresh();
            Close();
        }


    }
}