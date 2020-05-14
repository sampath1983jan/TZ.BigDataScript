using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;

namespace TZ.Data
{
    internal class DataBase
    {
        private DBDatabase _database;

        public DBDatabase Database
        {
            get { return _database; }
        }
        private string _Schema;
        public string Schema { get { return _Schema; } }
        public const string DbProvider = "MySql.Data.MySqlClient";
        public const string DbConnection = "Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312";
        public void InitDbs(string conn)
        {
            //if (conn == "")
            //{
            //    string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            //    assemblyFolder = assemblyFolder.Replace("\\Debug", "");
            //    assemblyFolder = assemblyFolder.Replace("\\bin", "");
            //    assemblyFolder = assemblyFolder.Replace("\\netcoreapp2.1", "");

            //    assemblyFolder = assemblyFolder.Replace("file:\\", "");
            //    string text = System.IO.File.ReadAllText(assemblyFolder + @"\appsettings.json");
            //    dynamic o = Newtonsoft.Json.JsonConvert.DeserializeObject<appSetting>(text);
            //    conn = o.Conn;

            //    if (conn == null)
            //    {
            //        conn = DbConnection;
            //    }
            //}

            //Modify the connections to suit
            //Comment any databases that should not be executed against
            DBDatabase mysql = DBDatabase.Create("MySQL"
                                                    , conn
                                                    , DbProvider);
            this._database = mysql;
            this._database.HandleException += new DBExceptionHandler(database_HandleException);
            var il = conn.Split(';').Where(x => x.ToLower().IndexOf("initial") >= 0).ToList();
            if (il.Count > 0)
            {
                _Schema = il[0].ToLower().Replace("initial catalog=", "");
                _Schema = _Schema.Replace("database=", "");
            }
        }
        void database_HandleException(object sender, DBExceptionEventArgs args)
        {
            System.Console.WriteLine("Database encountered an error : {0}", args.Message);
            //Not nescessary - but hey, validates it's writable.
            args.Handled = false;
        }
    }
}
