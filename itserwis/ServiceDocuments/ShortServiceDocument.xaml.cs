using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Reflection;
using System.Net.Http;
using System.Collections.Generic;

namespace ItSerwis_Merge_v2
{

    /// <summary>
    /// Logika interakcji dla klasy ShortServiceDocument.xaml
    /// </summary>
    public partial class ShortServiceDocument : Window
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private static readonly HttpClient client = new HttpClient();

        public string GetDateString()
        {
            DateTime today = DateTime.Today;
            string now = today.ToString("yyyy-MM-dd");
            return now;
        }
    

        public int docID { get; set; }
   
        public ShortServiceDocument(int? documentID=0)
        {
            InitializeComponent();
            this.docID = (int)documentID;
            DisplayDate();
            FillEmployeeData();


        }

        /// <summary>
        /// method that fills the blanks (about employee) by data from session table
        /// </summary>
        private void FillEmployeeData()
        {
            UserValidation us = new UserValidation();
            ServiceDocumentsAndDataSets svdt = new ServiceDocumentsAndDataSets();
            // The purpose of this method is to fill  the blanks by data from mysql database, table -> session
            var user = us.GetUserCredentials();

            empname.Text = user.firstname;
            emplastname.Text = user.lastname;
            empnumber.Text = user.docid;

            if (docID != 0)
            {
                var docServ = svdt.GetServiceDocumentFromDatabase(docID);
                name.Text = docServ.clientname;
                lastname.Text = docServ.clientsurename;
                address.Text = docServ.clientaddress;
                type.Text = docServ.devicetype;
                brand.Text = docServ.devicebrand;
                model.Text = docServ.devicemodel;
                description.Text = docServ.description;
            }

            }
        private void Update(object sender, EventArgs e)
        {
            log.Debug($"Invoking: {sender}");

            UserValidation us = new UserValidation();
            ServiceDocumentsAndDataSets svdt = new ServiceDocumentsAndDataSets();
            var user = us.GetUserCredentials();
            var userId = Int32.Parse(user.docid);
            
            try
            {
                svdt.UpdateServiceDocument(docID, name.Text, lastname.Text, address.Text, user.firstname, user.lastname, userId, type.Text, brand.Text, model.Text, description.Text);
            }
            catch (Exception err)
            {
                log.Error($"Could not update ServiceDocument: ['ID':'{docID}']\nError: [{err.Message}]");
            }

            finally
            {
                MessageBox.Show("Dokument został zaktualizowany");
            }


        }


        /// <summary>
        /// method that displays date instead of hand write it
        /// </summary>
        private void DisplayDate()
        {
            DateTime today = DateTime.Today;
            date.Text = today.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// method that takes all values from textblocks and calls, DbClass method to insert data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertData(string now, string customerName, string customerLastName, string customerAddr, string employeeName, string employeeLastName, int parsedEmpNum, string deviceType, string deviceBrand, string deviceModel, string descr, string documentnumber)
        {
            ServiceDocumentsAndDataSets db = new ServiceDocumentsAndDataSets();

            try
            {
                db.InsertIntoServiceDocuments(now, customerName, customerLastName, customerAddr, employeeName, employeeLastName, parsedEmpNum, deviceType, deviceBrand, deviceModel, descr, documentnumber);
                log.Info($"Short Service Document created: [CustomerName:{customerName}, CustomerLastName:{customerLastName}, CustomerAddress:{customerAddr}, EmployeeName:{employeeName}, EmployeeLastName:{ employeeLastName}, EmployeeNumber:{parsedEmpNum}, DeviceType:{deviceType}, DeviceBrand:{deviceBrand}, DeviceModel:{deviceModel}, DocumentNumber:{documentnumber}]");
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



        private void InsertIntoClients(object sender, EventArgs e)
        {
            ServiceDocumentsAndDataSets db = new ServiceDocumentsAndDataSets();
            var customerName = name.Text.ToString();
            var customerLastName = lastname.Text.ToString();
            var customerAddr = address.Text.ToString();

            var date = GetDateString();
            var lastDocID = Get_LastDocID();
            var parsedDocumentID = Int32.Parse(lastDocID);

            try
            {
                db.InsertIntoClientsFromServiceDocs(customerName, customerLastName, customerAddr, date, parsedDocumentID);
                log.Info("Adding client to database table.");
                MessageBox.Show($"Customer: " +
                    $"Name - {customerName} {customerLastName}" +
                    $"Address - {customerAddr}" +
                    $" added to database");

            }catch (Exception err)
            {
                log.Error($"Something went wrong: [ERROR: '{err.Message}']");
            }
        }
        /// <summary>
        /// method to generate pdf from filled short document form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneratePdfAsync(object sender, EventArgs e)
        {
            
            string chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var now = GetDateString();
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


            Random rdNum = new Random();

            // variables for randomNumber string - inserted to column documentnumber
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
            var lastDocID = Get_LastDocID();
            var parsedDocumentID = Int32.Parse(lastDocID);

            parsedDocumentID += 1;

            var documentInternalID = $"ITSD/{now}/{parsedDocumentID}/{parsedEmpNum}/{randomNumber}";

            try
            {
                InsertData(now, customerName, customerLastName, customerAddr, employeeName, employeeLastName, parsedEmpNum, deviceType, deviceBrand, deviceModel, descr, documentInternalID);
            }
            catch (Exception err)
            {
                MessageBox.Show($"Wystąpił błąd: {err.Message}");
                log.Error($"Error while inserting data to database: [{err.Message}]");
            }
            finally
            {
                log.Info($"Document - [{documentInternalID}] - as parsed and inserted to local database.");
            }

            SendPostInfoAsync($"{parsedDocumentID}");



            RunCmd($"D:/Temp/Itserwis/generate_pdf.py", $"{parsedDocumentID}");
        }

        private async System.Threading.Tasks.Task SendPostInfoAsync(string parsedDocumentID)
        {
            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("", $"{parsedDocumentID}")
            });

            var response = await client.PostAsync("https://localhost:44399/api/Documents", content);

            var responseString = await response.Content.ReadAsStringAsync();

            log.Debug(responseString);
        }

        private void Close(object sender, EventArgs e)
        {
            log.Debug($"Invoking: {sender}");
            this.Close();
        }


        /// <summary>
        /// runs python script which generates pdf file for servicedocument
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="args"></param>
        private void RunCmd(string cmd, string args)
        {
            //PythonValidation pythoner = new PythonValidation();
            //string pyPath = pythoner.GetPythonPath();
            //string EditPyPath = pyPath.Replace(@"\", "/");
            //string AppLocal = "AppLocal";
            
            //if (EditPyPath.Contains(AppLocal) == true)
            //{

            //}

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "E:/Program Files (x86)/Python38-32/python.exe";
            start.Arguments = $"{cmd} {args}";
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

        }

        /// <summary>
        /// method that gets document id for last position which has been saved to database
        /// </summary>
        /// <returns></returns>

        private string Get_LastDocID()
        {
            ServiceDocumentsAndDataSets conndb = new ServiceDocumentsAndDataSets();
            var lastDocId = conndb.GetLastDocumentID();

            if (lastDocId != "")
            {
                return lastDocId;
            } 

            this.Close();
            return lastDocId;
        }
    }
}
