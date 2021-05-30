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

namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy CreateClient.xaml
    /// </summary>
    public partial class CreateClient : Window
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger(); //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public CreateClient()
        {
            InitializeComponent();
        }

        private void SaveClient(object sender, EventArgs e)
        {
            log.Debug($"Invoking: {sender}");

            ClientsEditor ce = new ClientsEditor();

            try
            {
                ce.InsertIntoClients(name.Text, lastname.Text, address.Text, city.Text, voivodeship.Text, postcode.Text, country.Text, phone.Text, email.Text, nip.Text, accno.Text);
            }
            catch (Exception err)
            {
                log.Error($"Could not save client to database.\nError: [{err.Message}]");
            }

        }
    }
}
