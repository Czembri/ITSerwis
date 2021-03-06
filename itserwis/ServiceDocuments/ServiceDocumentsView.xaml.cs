﻿using System;
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
    public partial class ServiceDocumentsView : Window
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public ServiceDocumentsView()
        {
            InitializeComponent();
            FillDocumentsGrid();
        }

        private void addDocument(object sender, EventArgs e)
        {
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
            ServiceDocumentsAndDataSets dbconn = new ServiceDocumentsAndDataSets();
            string sqlDocs = "SELECT id, documentdate, clientname, clientsurename from servicedocument";
            DataSet docsData = dbconn.fillDataset("LoadDataBindingDocs", sqlDocs, "Documents");
            ServiceDocuments.DataContext = docsData;
        }



        private void MenuItem_RightClickEdit(object sender, EventArgs e)
        {

            DataRowView dataRowView = (DataRowView)ServiceDocuments.SelectedItem;
            try
            {
                int ID = Convert.ToInt32(dataRowView.Row[0]);
                log.Info($"Retrieving id from DataGrid: ['DataGrid':'Retrieving', 'DocumentId':{ID}]");
                ServiceDocumentsAndDataSets db_conn_2 = new ServiceDocumentsAndDataSets();
                var docDetails = db_conn_2.GetServiceDocumentFromDatabase(ID);
                log.Debug($"Invoking method: [{db_conn_2.GetServiceDocumentFromDatabase(ID)}] with 'ID':{ID} as an argument");

                var showDocument = new ShortServiceDocument(ID);
                showDocument.Show();
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

            var documentsClassHandler = new ServiceDocumentsAndDataSets();

            try
            {
                documentsClassHandler.DeleteServiceDocument(ID);
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