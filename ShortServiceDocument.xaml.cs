using System;
using System.Windows;


namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy ShortServiceDocument.xaml
    /// </summary>
    public partial class ShortServiceDocument : Window
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        public ShortServiceDocument()
        {
            InitializeComponent();
            DisplayDate();
        }
        /// <summary>
        /// method that displays date instead of hand write it
        /// </summary>
        private void DisplayDate()
        {
            DateTime today = DateTime.Today;
            date.Text = today.ToString("dd/MM/yyyy");
        }
        /// <summary>
        /// method that takes all values from textblocks and calls, DbClass method to insert data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertData(object sender, EventArgs e)
        {
            DbClass db = new DbClass();
            DateTime today = DateTime.Today;
            var now = today.ToString("dd/MM/yyyy");


            var customerName = name.Text.ToString();
            var customerLastName = lastname.Text.ToString();
            var empNum = empnumber.Text;
            var parsedEmpNum = Int32.Parse(empNum);
            var customerAddr = address.Text.ToString();
            var employeeName = empname.Text.ToString();
            var employeeLastName = emplastname.Text.ToString();
            var deviceType = type.Text.ToString();
            var deviceBrand = brand.Text.ToString();
            var deviceModel = model.Text.ToString();
            var descr = description.Text.ToString();
            try
            {
                db.InsertIntoServiceDocuments(now, customerName, customerLastName, customerAddr, employeeName, employeeLastName, parsedEmpNum, deviceType, deviceBrand, deviceModel, descr);
                log.Debug($"Short Service Document created: [CustomerName:{customerName}, CustomerLastName:{customerLastName}, CustomerAddress:{customerAddr}, EmployeeName:{employeeName}, EmployeeLastName:{ employeeLastName}, EmployeeNumber:{empNum}, DeviceType:{deviceType}, DeviceBrand:{deviceBrand}, DeviceModel:{deviceModel}]");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                log.Error($"Message error: [{err.Message}]");
            }
            finally
            {
                log.Debug("Data inserted. Closing window.");
                this.Close();
            }

        }
        /// <summary>
        /// method to generate pdf from filled short document form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneratePdf(object sender, EventArgs e)
        {
            MessageBox.Show("Remember to create pdf generator");
        }

        private void Close(object sender, EventArgs e)
        {
            log.Debug($"Invoking: {sender}");
            this.Close();
        }
    }
}
