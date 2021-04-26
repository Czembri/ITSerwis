using System.Data;
using System.Windows;


namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy item.xaml
    /// </summary>
    public partial class item : Window, Iitem
    {
        IServiceDocumentsAndDataSets _serviceDocumentsAndDataSets;
        public item(IServiceDocumentsAndDataSets serviceDocumentsAndDataSets)
        {
            InitializeComponent();
            FillItemsGrid();
            _serviceDocumentsAndDataSets = serviceDocumentsAndDataSets;
        }

        private void FillItemsGrid()
        {
            string sqlItems = "SELECT id, name, barcode, productindex from item";
            DataSet itemsdata = _serviceDocumentsAndDataSets.fillDataset("LoadDataBindingsItems", sqlItems, "Items");
            Articles.DataContext = itemsdata;
        }
    }
}
