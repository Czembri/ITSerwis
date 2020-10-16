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
        public static string connectionString = @"server=localhost;userid=root;password=root;database=itserwis";
        public MySqlConnection conn = new MySqlConnection(connectionString);

        public item()
        {
            InitializeComponent();
            FillItemsGrid();
        }

        private void FillItemsGrid()
        {
            try
            {
                conn.Open();
            }
            catch (Exception err)
            {
                log.Error($"Could not get database connection: error - [{err.Message}]");
            }


            string sqlItems = "SELECT id, name, barcode, productindex from item";
            MySqlDataAdapter MyDA = new MySqlDataAdapter(sqlItems, conn);
            DataSet ItemsData = new DataSet();
            MyDA.Fill(ItemsData, "LoadDataBindingsItems");
            ServiceClients.DataContext = ItemsData;

            log.Debug("Items data set created.");

            conn.Close();
        }
    }
}
