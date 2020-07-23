using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.OleDb;
using Newtonsoft.Json;

namespace TZ.Import.Step
{
    //Step -2
   public class ImportFile : BaseImportHandler
    {
        public ImportFile(BaseImportHandler nextHandler, ImportContext context) : base(nextHandler, context)
        {

        }

        public override ImportError Validate()
        {
            if (!File.Exists( this.Context.DataLocation  + this.Context.DataFilePath))
            {
                return new ImportError() { Message = "Import File does not exist.", Type = ErrorType.ERROR };
            }
            else {                
                return new ImportError() { Message = "No Error", Type = ErrorType.NOERROR };
            }
        }

        private DataTable ReadData() {
            string ConnectionString = "";
            if (this.Context.FileType == ImportFileType.excelx || this.Context.FileType == ImportFileType.excel)
            {
                System.Data.OleDb.OleDbConnectionStringBuilder builder = new System.Data.OleDb.OleDbConnectionStringBuilder();
                builder.DataSource = this.Context.DataLocation + this.Context.DataFilePath;
                builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                if (this.Context.FileType == ImportFileType.excel)
                {
                    builder.Add("Extended Properties", "Excel 8.0;HDR=Yes;IMEX=1");
                } else {
                    builder.Add("Extended Properties", "Excel 12.0;HDR=Yes;IMEX=1");
                }

                ConnectionString = builder.ConnectionString;
                var con = new System.Data.OleDb.OleDbConnection(ConnectionString);
                System.Data.DataSet ds = new System.Data.DataSet();
                System.Data.DataTable dt = new DataTable();
                con.Open();
                var dtSheets = new System.Data.DataTable();
             
                dtSheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dtSheets.Rows.Count == 0)
                {
                    throw new Exception("No Sheet exist in the excel file");
                }
                else if (dtSheets.Rows[0].IsNull(2))
                {
                    throw new Exception("No Data Avaiable");
                }
                System.Data.OleDb.OleDbDataAdapter MyCommand;
                MyCommand = new System.Data.OleDb.OleDbDataAdapter("Select * from " + "[" + dtSheets.Rows[0][2] + "]", con);
                //    MyCommand.TableMappings.Add("Table", "TestTable");
                MyCommand.Fill(ds);
                dt = ds.Tables[0];
                List<DataColumn> dc = new List<DataColumn>();
                foreach (DataColumn c in dt.Columns) { 
                if (c.DataType == typeof(string)){
                        dc.Add(c);
                    }
                }
                con.Close();
                con.Dispose();
                foreach (DataColumn c in dc) {
                    foreach (DataRow dr in dt.Rows) {
                        if (dr[c.ColumnName] != null) {
                            dr[c.ColumnName] = dr[c.ColumnName].ToString().Trim();
                        }                   
                    }
                }              
                return dt;
            }
            else if (this.Context.FileType == ImportFileType.csv) {
                ConnectionString = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + this.Context.DataLocation + this.Context.DataFilePath + ";Extensions=asc,csv,tab,txt;";
            }
            return new DataTable();
        }
        private string toJSONLine( DataTable dt)
        {
            string JSONString = string.Empty;
            //JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new JsonSerializerSettings
            //{
            //    DateFormatString = "MM-dd-yyyy"
            //});
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            //JSONString = JSONString.Replace("[", "").Replace("]", "");
            //JSONString = JSONString.Replace("},", "},\n");
            return JSONString;

        }
        public override ImportError HandleRequest(string logPath)
        {
            ImportError ie = Validate();
            if (ie.Type == ErrorType.NOERROR)
            {
                try
                {
                    DataTable dt = ReadData();
                    string jsonline = toJSONLine(dt);
                    System.IO.File.WriteAllText(logPath + "/" + this.Context.ID + ".json", jsonline + Environment.NewLine);
                    this.Context.Status = ImportStatus.pending;

                    var con = this.Context.Clone<ImportContext>();
                    con.View = null;
                    con.ComponentData = new List<ComponentData>();
                    con.Template.TemplateFields = new List<CompExtention.ImportTemplate.TemplateField>();
                    con.Template.View = null;
                    con.DataLocation = "";

                    Global.SaveImportContext(this.Context.ID, Newtonsoft.Json.JsonConvert.SerializeObject(con), 2,
                     this.Context.ActionBy,
                     this.Context.Connection, this.Context.ClientID
                     );
                    //Global.SaveImportContext(Newtonsoft.Json.JsonConvert.SerializeObject(this.Context), 2,
                    //    this.Context.ID,
                    //    this.Context.ActionBy,
                    //    logPath
                    //    );
                    return new ImportError() { Message = "No Error", Type = ErrorType.NOERROR };
                }
                catch (Exception Ex) {
                    throw Ex;
                }                
            }
            else
            {
                return ie;
            }           
        }

      
    }
}
