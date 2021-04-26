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
    public partial class Program : Page, IProgram
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        IMainWindow _mainWindow;
        ISession _session;
        Iitem _item;
        IUsers _users;
        IClients _clients;
        ISerwisClients _serwisClients;
        IShortServiceDocument _shortServiceDocument;
        IServiceDocumentsView _serviceDocumentsView;

        public Program(
            IMainWindow mainWindow,
            ISession session,
            Iitem item,
            IUsers users,
            IClients clients,
            ISerwisClients serwisClients,
            IShortServiceDocument shortServiceDocument,
            IServiceDocumentsView serviceDocumentsView
            )
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _session = session;
            _item = item;
            _users = users;
            _clients = clients;
            _serwisClients = serwisClients;
            _shortServiceDocument = shortServiceDocument;
            _serviceDocumentsView = serviceDocumentsView;
        }
        /// <summary>
        /// method for "Wyjście" option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWindow(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");

            log.Info("Session closed");
            log.Debug($"Invoking [CloseSession method]");
            _session.CloseSession();

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
            log.Debug($"Invoking form [{_clients}]");
            _clients.InitializeComponent();
        }

        private void ShowServiceClients(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            log.Debug($"Invoking form [{_serwisClients}]");
            _serwisClients.InitializeComponent();
        }

        private void ShowUsers(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            log.Debug($"Invoking form [{_users}]");
            _users.InitializeComponent();
        }

        public void CreateShortServiceDocument(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            log.Debug($"Invoking form [{_shortServiceDocument}]");
            _shortServiceDocument.InitializeComponent();
        }

        public void Logout(object sender, EventArgs e)
        {
            _session.CloseSession();
            log.Info("Session closed.");
            var w = Application.Current.Windows[0];
            w.Hide();
            log.Debug($"Invoking [{w}]");
            _mainWindow.InitializeComponent();
        }

        public void ShowServiceDocuments(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            log.Debug($"Invoking form [{_serviceDocumentsView}]");
            _serviceDocumentsView.InitializeComponent();

        }

        public void Item(object sender, EventArgs e)
        {
            log.Debug($"Invoking [{sender}].");
            log.Debug($"Invoking form [{_item}]");
            _item.InitializeComponent();
        }


    }
}
