using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;

namespace TZ.DataAnalyzer.Window
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string datascript = "";
        //Server=192.168.0.80;Database=talentozdev;Uid=admin;Pwd=admin312 Catalog=spic_talentoz_23jan;Uid=admin;Pwd=admin312  Server=devserver;Initial Catalog=spic_talentoz_23jan;Uid=admin;Pwd=admin312
        private void btnExecute_Click(object sender, EventArgs e)
        {           
            AScript script = new AScript();
            lbl1.Text = DateTime.Now.ToString();
            script.Param.Add("ClientID", "2");
            script.Param.Add("UserID", "90");
            script.Param.Add("smonth", "10");
            script.Param.Add("syear", "2019");
            script.Compile("demo",textBox1.Text, "Server=127.0.0.1;Database=talentozdev;Uid=root;Pwd=admin312", @"c:\csv\result.json",
                @"C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.DataAnalyzer.Window\bin\Debug");
            StringBuilder sb = new StringBuilder();
            if (script.Errors != null)
            {
                sb.AppendLine(string.Join(",", script.Errors.ToList()));
                textBox2.Text = (sb.ToString());
            }
            else
            {
                lbl2.Text = DateTime.Now.ToString();
                MessageBox.Show("done");
            }

            //%SPARK_HOME%\bin\spark-submit --class org.apache.spark.deploy.dotnet.DotnetRunner --master local  C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.DataAnalyzer.Window\bin\Debug\microsoft-spark-2.4.x-0.8.0.jar\  debug

        }

        public void   ExecuteCommand(string exeDir, string args)
        {


            //using (TcpClient tcpClient = new TcpClient())
            //{
            //    try
            //    {
            //        tcpClient.Connect("127.0.0.1", 9081);
            //        Console.WriteLine("Port open");
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("Port closed");
            //    }
            //}
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect("localhost", 5567);

                    Console.WriteLine("Port open" + tcpClient.Connected);
                }
                catch (Exception)
                {
                    try
                    {
                        System.Diagnostics.ProcessStartInfo procStartInfo =
                            new System.Diagnostics.ProcessStartInfo("cmd", "/c " + args);
                        procStartInfo.RedirectStandardOutput = false;
                        procStartInfo.UseShellExecute = true;
                        procStartInfo.CreateNoWindow = true;
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        proc.StartInfo = procStartInfo;
                        proc.Start();                       
                        //string result = proc.StandardOutput.ReadToEnd();
                        ExecuteCommand("", args);
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                        // Log the exception
                    }
                }
            }
             

         
        }
   

            private void Form1_Load(object sender, EventArgs e)
        {
            //datascript = "";
            TZ.Data.DataManager dm = new Data.DataManager();
            string[] flds = { "userid","F_200015", "F_200005", "F_200115", "F_200030" };
            string[] pos = { "positionid", "F_360005", "F_360010", "F_360085" };
            string[] emp_post = { "userid", "clientid", "positionid","status" };
            datascript =Newtonsoft.Json.JsonConvert.SerializeObject( dm.SelectFields(flds, "sys_User").From("sys_User").Where("sys_User","ClientID","=" ,"43").GetSchema ());
            File.WriteAllText(@"c:\csv\client\sysuser.json", datascript);
            datascript = "";
            dm = new Data.DataManager();
            datascript = Newtonsoft.Json.JsonConvert.SerializeObject(dm.SelectFields(pos, "position").From("position").Where("position", "ClientID", "=", "43").GetSchema());
            File.WriteAllText(@"c:\csv\client\position.json", datascript);
            datascript = "";
            dm = new Data.DataManager();
            datascript = Newtonsoft.Json.JsonConvert.SerializeObject(dm.SelectFields(emp_post, "Employee_position").From("Employee_position").Where("Employee_position", "ClientID", "=", "43").GetSchema());
            File.WriteAllText(@"c:\csv\client\employeeposition.json", datascript);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            lbl1.Text = "";
            lbl2.Text = "";
            textBox2.Text = "";
        }
    }
    public class Student
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
        public string Seat { get; set; }
        public string Club { get; set; }
        public string Persona { get; set; }
        public string Crush { get; set; }
        public string BreastSize { get; set; }
        public string Strength { get; set; }
        public string Hairstyle { get; set; }
        public string Color { get; set; }
        public string Eyes { get; set; }
        public string EyeType { get; set; }
        public string Stockings { get; set; }
        public string Accessory { get; set; }
        public string ScheduleTime { get; set; }
        public string ScheduleDestination { get; set; }
        public string ScheduleAction { get; set; }
        public string Info { get; set; }
    }
    public class KeysJsonConverter : JsonConverter
    {
        private readonly Type[] _types;

        public KeysJsonConverter(params Type[] types)
        {
            _types = types;
        }
        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Newtonsoft.Json.Linq.JToken t = Newtonsoft.Json.Linq.JToken.FromObject(value);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();

            JObject o = (JObject)t;
            IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();
            // List<JToken> jts = ((Newtonsoft.Json.Linq.JObject)t).Children;  writer.WriteValue("\n");
            foreach (string p in propertyNames)
            {
                writer.Formatting = Formatting.None;
                writer.WritePropertyName(p);
                var s = value.GetType().GetProperty(p).GetValue(value, null); ;
                writer.WriteValue(s);
            }

            // if (t.Type != JTokenType.Object)
            // {
            //   t.WriteTo(writer);
            // }
            //else
            //{
            //    JObject o = (JObject)t;
            //    IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

            //    o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

            //    o.WriteTo(writer);
            //}
            writer.WriteEndObject();
            writer.CloseOutput = false;


        }
    }
}
