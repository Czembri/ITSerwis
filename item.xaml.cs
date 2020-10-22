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
    /// Logika interakcji dla klasy item.xaml
    /// </summary>
    public partial class item : Window
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

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
            ServiceClients.DataContext = itemsdata;
        }
    }
}
