using System.Data;
using System.Windows;


namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy item.xaml
    /// </summary>
    public partial class item : Window
    {
        public item()
        {
            InitializeComponent();
            FillItemsGrid();
        }

        private void FillItemsGrid()
        {
            DbClass dbconn = new DbClass();
            string sqlItems = "SELECT id, name, barcode, productindex from item";
            DataSet itemsdata = dbconn.fillDataset("LoadDataBindingsItems", sqlItems, "Items");
            Articles.DataContext = itemsdata;
        }
    }
}
