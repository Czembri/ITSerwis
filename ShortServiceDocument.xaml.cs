using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;



namespace ItSerwis_Merge_v2
{
    /// <summary>
    /// Logika interakcji dla klasy ShortServiceDocument.xaml
    /// </summary>
    public partial class ShortServiceDocument : Window
    {
        public static string connectionString = @"server=localhost;userid=root;password=root;database=itserwis";
        public MySqlConnection conn = new MySqlConnection(connectionString);
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
        private void InsertData(string now, string customerName, string customerLastName, string customerAddr, string employeeName, string employeeLastName, int parsedEmpNum, string deviceType, string deviceBrand, string deviceModel, string descr, string documentnumber)
        {
            DbClass db = new DbClass();

            try
            {
                db.InsertIntoServiceDocuments(now, customerName, customerLastName, customerAddr, employeeName, employeeLastName, parsedEmpNum, deviceType, deviceBrand, deviceModel, descr, documentnumber);
                log.Debug($"Short Service Document created: [CustomerName:{customerName}, CustomerLastName:{customerLastName}, CustomerAddress:{customerAddr}, EmployeeName:{employeeName}, EmployeeLastName:{ employeeLastName}, EmployeeNumber:{parsedEmpNum}, DeviceType:{deviceType}, DeviceBrand:{deviceBrand}, DeviceModel:{deviceModel}, DocumentNumber:{documentnumber}]");
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
            DateTime today = DateTime.Today;
            var now = today.ToString("dd/MM/yyyy");
            string chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";

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

            var lastDocID = Get_LastDocID();
            Random rdNum = new Random();
            //var RandomDocumentNumber = new List<int>();
            //var RandomDocumentChar = new List<char>();

            //for (int i= 0; i <5; i++)
            //{
            //    int generatedRandNum = rdNum.Next();
            //    RandomDocumentNumber.Add(generatedRandNum);

            //    RandomDocumentChar.Add(chars[rdNum.Next(chars.Length-1)]);
            //}

            int rand1 = rdNum.Next(1, 100);
            int rand2 = rdNum.Next(1, 100);
            int rand3 = rdNum.Next(1, 100);

            int randChar1 = rdNum.Next(chars.Length - 1);
            char char1 = chars[randChar1];

            int randChar2 = rdNum.Next(chars.Length - 1);
            char char2 = chars[randChar2];

            int randChar3 = rdNum.Next(chars.Length - 1);
            char char3 = chars[randChar3];

            string randomNumber = $"NR{rand1}{rand2}{rand3}-{char1}{char2}{char3}";

            var parsedDocumentID = Int32.Parse(lastDocID);

            parsedDocumentID += 1;

            var documentInternalID = $"ITSD/{now}/{parsedDocumentID}/{parsedEmpNum}/{randomNumber}";

            InsertData(now, customerName, customerLastName, customerAddr, employeeName, employeeLastName, parsedEmpNum, deviceType, deviceBrand, deviceModel, descr, documentInternalID);

            var stringDocumentId = parsedDocumentID.ToString();

            run_cmd("generate_pdf.py", stringDocumentId);

            MessageBox.Show("Remember to create pdf generator");
        }

        private void Close(object sender, EventArgs e)
        {
            log.Debug($"Invoking: {sender}");
            this.Close();
        }

        private void run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "cmd.exe";
            start.Arguments = $"C:/Users/Ola/AppData/Local/Programs/Python/Python38/python.exe {cmd} {args}";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }


            //Process process = new Process();
            //ProcessStartInfo processInfo = new ProcessStartInfo();
            //processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //processInfo.FileName = "cmd.exe";
            //processInfo.Arguments = $"/C C:/Users/Ola/AppData/Local/Programs/Python/Python38/python.exe {cmd} {args}";
            //process.StartInfo = processInfo;
            //process.Start();
        }

        private string Get_LastDocID()
        {
            string docID;
            // Because you know that id is int so imho you don't need parameters.
            var sql = "SELECT id from servicedocument order by id desc limit 1";
            // This is right.
            var cmd = new MySqlCommand(sql, conn);
            conn.Open();
            var reader = cmd.ExecuteReader();
            reader.Read();
            docID = reader.GetValue(0).ToString();
            conn.Close();// Close connection.
            return docID;
        }
    }
}
