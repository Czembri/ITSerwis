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

            string encrLogin = EncryptData(Login.Text);
            string encrPass = EncryptData(Password.Password.ToString());


            DbClass dbSql = new DbClass();

            log.Info("Trying establish the connection to database.");

            try
            {
                checker = dbSql.CheckLog(encrLogin, encrPass);
                log.Info($"User has been found - [Login:{Login.Text}, Password:{checker}, Login:{checker}]");

                dbSql.CreateSession(encrLogin, encrPass);
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



        /// <summary>
        /// method that encrypts user login and password - encrypted data is stored in database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string EncryptData(string data)
        {
            log.Info("Encrypting user's login and password...");

            MD5 md5Hash = MD5.Create();

            byte[] HashedData = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < HashedData.Length; i++)
            {
                sBuilder.Append(HashedData[i].ToString("x2"));
            }


            var encrypted = sBuilder.ToString();

            log.Info("Data encrypted.");
            return encrypted;

        }


    }
}

