using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZ.CompExtention;
using TZ.Import;

namespace TZ.ImportDesk
{
    public partial class Stepup : Form
    {
        public Stepup()
        {
            InitializeComponent();
        }
        public Stepup(string conn,int cid)
        {
            Connection = conn;
            ClientID = cid;
            InitializeComponent();
        }
        public int ClientID { get; set; }
        public string Connection { get; set; }
        List<ComponentView> Views = new List<ComponentView>();
        int selectedView = 0;
         
        string path = @"C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.ImportDesk\ComponentLibrary";
    
        private void Form1_Load(object sender, EventArgs e)
        {
         //   var path=      @"C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.ImportDesk\ComponentLibrary";
            ComponentView vm = new ComponentView();
            ComponentViewManager cvm = new ComponentViewManager(new ComponentViewDataAccess(path));
         //   Views =     cvm.GetViews();
            //var view= cvm.NewView("EmployeeView");

            // ComponentManager cmg = new ComponentManager("ed04c35e434145bba3529d7365ff1ff5186455310", new ComponentDataAccess(path) { });

            // view.Components.Add( cmg.GetComponent());
            // cvm.Save(view);
            //cmbData.Items.Add("Choose from the list");
            //cmbData.SelectedIndex = 0;
            //foreach (ComponentView cv in Views) {
            //    cmbData.Items.Add(cv.Name);
            //}
     
             

            //TZ.CompExtention.  Component c = new CompExtention.Component();
            //  c.ID = "";
            //  c.Name = "Employee";
            //  c.TableName = "sys_user";
            //  c.Keys = new List<IAttribute>();
            //  c.Keys.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "1",
            //      Name = "UserID",
            //      DisplayName = "User Key",
            //      Type = AttributeType._number,
            //      IsKey = true,
            //      IsUnique = true,
            //  });
            //  c.Attributes = new List<IAttribute>();
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "1",
            //      Name = "UserID",
            //      DisplayName = "User Key",
            //      Type = AttributeType._number,
            //      IsKey = true,
            //      IsUnique = true,
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "2",
            //      Name = "F_200005",
            //      DisplayName = "Employee No",
            //      Type = AttributeType._string,
            //      IsKey = false,
            //      IsUnique = true,
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "3",
            //      Name = "F_200015",
            //      DisplayName = "Employee Name",
            //      Type = AttributeType._string,
            //      IsKey = false,
            //      IsUnique = false,
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "4",
            //      Name = "F_200115",
            //      DisplayName = "Email",
            //      Type = AttributeType._string,
            //      IsKey = false,
            //      IsUnique = false,
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "5",
            //      Name = "F_200040",
            //      DisplayName = "Gender",
            //      Type = AttributeType._lookup,
            //      IsKey = false,
            //      IsUnique = false,
            //      LookupInstanceID ="102",
            //      LookupDisplayField ="Description",

            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "6",
            //      Name = "F_200050",
            //      DisplayName = "Nationality",
            //      Type = AttributeType._lookup,
            //      IsKey = false,
            //      IsUnique = false,
            //      LookupInstanceID = "107",
            //      LookupDisplayField = "Description",
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "7",
            //      Name = "F_31075",
            //      DisplayName = "Marital Status",
            //      Type = AttributeType._lookup,
            //      IsKey = false,
            //      IsUnique = false,
            //      LookupInstanceID = "254",
            //      LookupDisplayField = "Description",
            //  });
            // cmg.Save(c);
        }

        public void UpdateChange(string conn,int clientID) {
            ClientID = clientID;
            Connection = conn;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lblTime.Text = "Start at:" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");             
        }
     
         
       
        private void lbFile_Click(object sender, EventArgs e)
        {

        }
      
        private void button4_Click(object sender, EventArgs e)
        {
            //Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312
            if (Connection != "") {
                try
                {
                    TZ.CompExtention.Shared.ExecuteSetup(Connection);
                    MessageBox.Show("done");
                }
                catch (Exception ex) {
                    MessageBox.Show("Error:" + ex.Message );
                }               
            } 
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (Connection != "")
            {
                TZ.CompExtention.Shared.ClearSchema(Connection);
                MessageBox.Show("cleared");
            }
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {

            if (Connection != "")
            {

                try
                {
                    TZ.CompExtention.Shared.ConvertoImportComponent(Connection, ClientID);
                    MessageBox.Show("done");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);

                }
               
                
            }
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                TZ.CompExtention.Shared.ConvertEmployeePositionPayAsia(Connection, ClientID);
                MessageBox.Show("done");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);

            }
          
        }


        /// <summary&RT;
        /// Author : Himasagar Kutikuppala
        ///A utility method that runs the batch file with supplied arguments.
        /// </summary&RT;
        /// <param name="batchFileName"&RT;Name of the batch file that should be run</param&RT;
        /// <param name="argumentsToBatchFile"&RT;Arguments to the batch file</param&RT;
        /// <returns&RT;Status of running the batch file</returns&RT;
        public bool ExecuteBatchFile(string batchFileName, string[] argumentsToBatchFile)
        {
            string argumentsString = string.Empty;
            try
            {
                //Add up all arguments as string with space separator between the arguments
                if (argumentsToBatchFile != null)
                {
                    for (int count = 0; count < argumentsToBatchFile.Length; count++)
                    {
                        argumentsString += " ";
                        argumentsString += argumentsToBatchFile[count];
                        //argumentsString += "\"";
                    }
                }

                //Create process start information
                System.Diagnostics.ProcessStartInfo DBProcessStartInfo = new System.Diagnostics.ProcessStartInfo(batchFileName, argumentsString);

                //Redirect the output to standard window
                DBProcessStartInfo.RedirectStandardOutput = true;

                //The output display window need not be falshed onto the front.
                DBProcessStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                DBProcessStartInfo.UseShellExecute = false;

                //Create the process and run it
                System.Diagnostics.Process dbProcess;
                dbProcess = System.Diagnostics.Process.Start(DBProcessStartInfo);

                //Catch the output text from the console so that if error happens, the output text can be logged.
                System.IO.StreamReader standardOutput = dbProcess.StandardOutput;

                /* Wait as long as the DB Backup or Restore or Repair is going on. 
                Ping once in every 2 seconds to check whether process is completed. */
                while (!dbProcess.HasExited)
                    dbProcess.WaitForExit(2000);

                if (dbProcess.HasExited)
                {
                    string consoleOutputText = standardOutput.ReadToEnd();
                    //TODO - log consoleOutputText to the log file.

                }

                return true;
            }
            // Catch the SQL exception and throw the customized exception made out of that
            catch (SqlException ex)
            {


                throw ex;
            }
            // Catch all general exceptions
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Backup()
        {
            try
            {
 
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = @"c:/sampath/backup/MySqlBackup.sql";
              //  StreamWriter file = new StreamWriter(path);
                System.IO.File.Create(path);

                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "cmd.exe";
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.WorkingDirectory = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\";
                myProcess.StartInfo.RedirectStandardInput = true;
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.Start();
                StreamWriter myStreamWriter = myProcess.StandardInput;
                StreamReader mystreamreader = myProcess.StandardOutput;
                string arg = @"mysqldump --routines -u root -padmin312 talentozdev > c:\sampath\backup\mysql" + new Guid().ToString() +".sql ";
                myStreamWriter.WriteLine(arg);
                myStreamWriter.Close();
                myProcess.WaitForExit();
                myProcess.Close();
                MessageBox.Show("db backup done");

                //ProcessStartInfo pro = new ProcessStartInfo();
                //pro.FileName = "cmd.exe";
                //pro.Arguments = string.Format(@"mysqldump -u{0} -p{1} {2} > {3}", "root", "admin312", "talentozdev", path);
                //pro.WorkingDirectory = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\";
                //Process proStart = new Process();



                //pro.RedirectStandardOutput = true;
                //pro.UseShellExecute = false;
                // Do not create the black window.




                //string result = proStart.StandardOutput.ReadToEnd();
                //Console.WriteLine(result);


                //ProcessStartInfo psi = new ProcessStartInfo();
                //psi.FileName = "mysqldump";
                //psi.WorkingDirectory = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\";
                //psi.RedirectStandardInput = false;
                //psi.RedirectStandardOutput = true;
                //psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                //    "root", "admin312", "localhost", "talentozdev");
                //psi.UseShellExecute = false;

                //Process process = Process.Start(psi);

                //string output;
                //output = process.StandardOutput.ReadToEnd();
                //file.WriteLine(output);
                //process.WaitForExit();
                //file.Close();
                //process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to backup!");
            }
        }

        private void GetScript() {
            string insertString = @"INSERT INTO `laravel`.`main` (`ID`, `Name`) VALUES ('5', 'dfgds');";
            IEnumerable<string> inserts = insertString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string firstInsert = inserts.First();
            int tableIndex = firstInsert.IndexOf("INSERT INTO ") + "INSERT INTO ".Length;
            string table = firstInsert.Substring(
                tableIndex, firstInsert.IndexOf("(", tableIndex) - tableIndex);
            var regex = new System.Text.RegularExpressions.Regex(@"\(([^)]+)\)", System.Text.RegularExpressions.RegexOptions.Compiled);
            string columns = regex.Matches(firstInsert)[0].Value;

            IEnumerable<string> values = inserts.Select(sql => regex.Matches(sql)[1].Value);

            string insertAll = string.Format("INSERT INTO {0}{1} VALUES {2};"
                , table
                , columns
                , string.Join(",", values.ToArray()));

            Console.Write("Result: " + insertAll);
        }


        private void button2_Click(object sender, EventArgs e)
        {
        //    GetScript();


        //    DateTime Time = DateTime.Now;
        //    int year = Time.Year;
        //    int month = Time.Month;
        //    int day = Time.Day;
        //    int hour = Time.Hour;
        //    int minute = Time.Minute;
        //    int second = Time.Second;
        //    int millisecond = Time.Millisecond;
        //    string path;
        //    path = @"C:\sampath\backup\MySqlBackup" + year + "-" + month + "-" + day +
        //"-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
        //    System.IO.File.Create(path);

            //    string[] arr;
            //    var a = "talentozdev" + ",root" + ",admin312" + "," + path;
            //    arr = a.Split(',');
            //    ExecuteBatchFile(@"C:\sampath\backup\mysqlbk.bat", arr);
        }
    }
}
