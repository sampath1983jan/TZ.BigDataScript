using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Data
{
    public class DataManager
    {
        private string _schema;
        public IDictionary<string, string> Param { get; set; }
        private string Output { get; set; }
        private string DataScriptPath { get; set; }
        private string DataAsScript { get; set; }
        private string[] DataAsScripts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private baseDataSchema dSchema;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>

        public event EventHandler AfterProcessed;
        public DataManager(string schema)
        {
            Param = new Dictionary<string, string>();
            _schema = schema;
            dSchema = Newtonsoft.Json.JsonConvert.DeserializeObject<DataSchema>(_schema);
            DataAsScript = "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public DataManager()
        {
            Param = new Dictionary<string, string>();
            _schema = "";
            dSchema = new DataSchema();
            DataAsScript = "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public baseDataSchema GetSchema()
        {
            return dSchema;
        }
        /// <summary>
        /// date seperated by comma
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public DataManager Data(string s) {
            DataAsScript = s;
            return this;
        }
        public DataManager Data(string[] s) {
            DataAsScripts = s;
            return this;
        }
        public DataManager InsertInto(string field, FieldType type) {            
                dSchema.Fields.Add(new TableField() { Table = dSchema.Table, Field = field ,Type=type});            
            return this;
        }
        public DataManager UpdateInto(string field, FieldType type,string fieldvalue)
        {
            dSchema.Fields.Add(new TableField() { Table = dSchema.Table, Field = field, Type = type,FieldValue =fieldvalue });
            return this;
        }
        public DataManager tableName(string tableName) {
            dSchema.Table = tableName;
            return this;
        }       
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public DataManager SelectFields(string[] fields, string tablename)
        {
            foreach (string s in fields)
            {
                dSchema.Fields.Add(new TableField() { Table = tablename, Field = s });
            }
            return this;
        }
        public DataManager SelectFields(string fields, string tablename,FieldType type,string dfName = "")
        {           
                dSchema.Fields.Add(new TableField() { Table = tablename, Field = fields,Type =type ,DFName=dfName});            
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        public DataManager From(string table)
        {
            dSchema.Table = table;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="table"></param>
        /// <param name="joinField"></param>
        /// <returns></returns>
        public DataManager JoinTo(string field, string table, string joinField)
        {
            dSchema.Joins.Add(new Join() { TableName = dSchema.Table, Field = field, JoinField = joinField, JoinTable = table });
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="field"></param>
        /// <param name="joinTable"></param>
        /// <param name="joinfield"></param>
        /// <returns></returns>
        public DataManager JoinTo(string tableName, string field, string joinTable, string joinfield)
        {
            dSchema.Joins.Add(new Join() { TableName = tableName, Field = field, JoinField = joinfield, JoinTable = joinTable });
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <param name="oper"></param>
        /// <param name="value"></param>
        public DataManager Where(string table, string field, string oper, string value)
        {
            dSchema.Wheres.Add(new WhereStatement() { FieldName = field, Operator = oper, FieldValue = value, TableName = table });
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public  void  GetData( string conn,string op,IDictionary<string,string> param) {
            var p = param.ToList();
            foreach (WhereStatement w in dSchema.Wheres) {
                foreach (KeyValuePair<string,string> d in p) {
                    w.FieldValue= w.FieldValue.ToLower().Replace("#" + d.Key.ToLower() + "#", d.Value);
                }
            }
            var dt=   ((DataSchema)dSchema).GetData(conn);
            // string json = JsonConvert.SerializeObject(d, Formatting.Indented, new KeysJsonConverter(typeof(DataTable)));            
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            });
            JSONString = JSONString.Replace("[","").Replace("]", "");
            JSONString = JSONString.Replace("},", "},\n");            
            System.IO.File.WriteAllText(op, JSONString + Environment.NewLine);
        }
        public DataTable GetData(string conn)
        {
            //var p = param.ToList();
            //foreach (WhereStatement w in dSchema.Wheres)
            //{
            //    foreach (KeyValuePair<string, string> d in p)
            //    {
            //        w.FieldValue = w.FieldValue.ToLower().Replace("#" + d.Key.ToLower() + "#", d.Value);
            //    }
            //}
            var dt = ((DataSchema)dSchema).GetData(conn);
            // string json = JsonConvert.SerializeObject(d, Formatting.Indented, new KeysJsonConverter(typeof(DataTable)));            
            //string JSONString = string.Empty;
            //JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new JsonSerializerSettings
            //{
            //    DateFormatString = "MM-dd-yyyy"
            //});
            //JSONString = JSONString.Replace("[", "").Replace("]", "");
            //JSONString = JSONString.Replace("},", "},\n");
          //  System.IO.File.WriteAllText(op, JSONString);
            return dt;
        }

        public List<DataError> ExecuteNonQuery(string conn) {
            ((DataSchema)dSchema).AfterProcessed += new EventHandler(Processed);
            if (DataAsScript != "" && DataAsScripts == null)
            {                
                return ((DataSchema)dSchema).ExecuteNonQuery(DataAsScript, conn);
            }
            else {
                return ((DataSchema)dSchema).ExecuteNonQuery(DataAsScripts, conn);
            }         
        }
        public void Processed(object sender, EventArgs e) {
            if (AfterProcessed != null) {
                AfterProcessed(this, null);
            }          
        }
        public   List<DataError> ExecuteNonQuery(string scriptPath,string conn) {
            List<DataError> errs = new List<DataError>();
            ((DataSchema)dSchema).AfterProcessed += new EventHandler(Processed);
            errs = (((DataSchema)dSchema).UpdateScript(scriptPath,conn));           
            return errs;
        }
        private string formatstring(DataRow dr, DataColumn dc) {
            if (dc.DataType.Name == "DateTime")
            {
                if (dr[dc.ColumnName] != null && dr[dc.ColumnName] != System.DBNull.Value)
                    return   Convert.ToDateTime(dr[dc.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss")  ;
                else
                    return   "0001-01-01 00:00:00";
            }
            else if (dc.DataType.Name == "Int32" || dc.DataType.Name == "Int64"
                || dc.DataType.Name == "Int16" || dc.DataType.Name == "UInt64"
                || dc.DataType.Name == "Double" || dc.DataType.Name == "UInt32"
                || dc.DataType.Name == "Decimal") {
                return ((dr[dc.ColumnName])).ToString();
            }
            else
            {
                if (dr[dc.ColumnName] != null && dr[dc.ColumnName] != System.DBNull.Value)
                    return "\"" + dr[dc.ColumnName].ToString() + "\"" ;
                else
                    return "";
            }
        }
        private string Serialize(DataTable dt)
        {


            var JSONString = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                //    JSONString.Append("[");
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    int j = 0;
                    JSONString.Append("{");
                    foreach (DataColumn dc in dt.Columns)
                    {                       
                        if (j < dt.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + dc.ColumnName.ToString() + "\":" +  formatstring(dr, dc) +  ",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + dc.ColumnName.ToString() + "\":"  + formatstring(dr,dc) + "");
                        }
                        j += 1;
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("}" + Environment.NewLine);
                    }
                    i += 1;
                }
                //    JSONString.Append("]");
            }
            return JSONString.ToString();
        }
  
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetStatement(string conn) {           
            return ((DataSchema)dSchema).GetSchema(conn);
        }
    }
}
