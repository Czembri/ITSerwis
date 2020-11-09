using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Main window of the application in general it is used just to display other forms
    /// </summary>
    public partial class Program : Page
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger(); 
        public Program()
        {
            InitializeComponent();
        }
        /// <summary>
        /// method for "Wyjście" option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWindow(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            
            Database_transactions_1 session = new Database_transactions_1();
            log.Info("Session closed");
            log.Debug($"Invoking [CloseSession method]");
            session.CloseSession();

            log.Info("Window closed.");
            Application.Current.Shutdown();
        }

        /// <summary>
        /// shows us help info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            MessageBox.Show("Aby uzyskać pomoc wyślij e-mail na: help@itserwis.com");
        }

        private void ManageClients(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            var clientsForm = new Clients();
            log.Debug($"Invoking form [{clientsForm}]");
            clientsForm.Show();
        }

        private void ShowSerwisClients(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            var SerwisClientsForm = new SerwisClients();
            log.Debug($"Invoking form [{SerwisClientsForm}]");
            SerwisClientsForm.Show();
        }

        private void ShowUsers(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            var AllUsers = new Users();
            log.Debug($"Invoking form [{AllUsers}]");
            AllUsers.Show();
        }

        public void CreateShortServiceDocument(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            var ServiceShortDoc = new ShortServiceDocument();
            log.Debug($"Invoking form [{ServiceShortDoc}]");
            ServiceShortDoc.Show();
        }

        public void Logout(object sender, EventArgs e)
        {
            Database_transactions_1 mySQL = new Database_transactions_1();
            mySQL.CloseSession();
            log.Info("Session closed.");
            var w = Application.Current.Windows[0];
            w.Hide();
            log.Debug($"Invoking [{w}]");
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }

        public void ShowServiceDocuments(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            var ServiceDocView = new ServiceDocumentsView();
            log.Debug($"Invoking form [{ServiceDocView}]");
            ServiceDocView.Show();

        }

        public void Item(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            var ItemView = new item();
            log.Debug($"Invoking form [{ItemView}]");
            ItemView.Show();
        }


    }
}
