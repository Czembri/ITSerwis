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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]


namespace ItSerwis_Merge_v2

{

    public partial class MainWindow : Window
    {
        public bool checker;
        private static readonly log4net.ILog log = LogHelper.GetLogger(); //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        
        public MainWindow()
        {
            
            InitializeComponent();
            SessionManager();
        }

        private void SessionManager()
        {
            Session dbSession = new Session();
            var checkIfOpen = dbSession.ManageSessions();
            if (checkIfOpen == true)
            {
                log.Info("Session managed properly.");
            }
            else
            {
                log.Warn("Something went wrong with session's management.");
            }
        }

        /// <summary>
        /// method for login to application -> click and validate -> takes us to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            checker = false;
            var newForm = new Program();
            var enc = new Encryptor();

            string encrLogin = enc.EncryptData(Login.Text);
            string encrPass = enc.EncryptData(Password.Password.ToString());


            Session session = new Session();
            UserValidation us = new UserValidation();

            log.Info("Trying establish the connection to database.");

            try
            {
                checker = us.CheckLog(encrLogin, encrPass);
                log.Info($"User has been found - [Login:{Login.Text}, Password:{checker}, Login:{checker}]");

                session.CreateSession(encrLogin, encrPass);
                log.Info($"Session created: [Login:{Login.Text}, Password:{checker}, Login:{checker}]");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                log.Error($"Error occured: {err.Message}");
            }
            finally
            {
                if (checker == true)
                {
                    try
                    {
                        this.Content = newForm;

                    }
                    catch (Exception err)
                    {

                        MessageBox.Show($"Wystąpił błąd: '{err}'");
                        log.Error($"Message error: {err.Message}");
                    }


                }
                else
                {
                    MessageBox.Show("Nieprawidłowa nazwa użytkownika lub hasło, spróbuj pownonie");
                    log.Error($"Inncorect login or password, user {Login.Text} does not exist.");
                }

            }




        }
    }
}

