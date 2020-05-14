using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using static TZ.Data.DataSchema;

namespace TZ.Data
{
    public enum FieldType
    {
        _string,
        _number,
        _decimal,
        _datetime,
        _bool,
    }
    internal class DataSchema : baseDataSchema
    {

        public event EventHandler AfterProcessed;

        public DataSchema() {
            this.Fields = new List<TableField>();
            this.Wheres = new List<WhereStatement>();
            this.Joins = new List<Join>();
            this.Table = "";
        }
        public DataTable GetData(string conn) {
            Data.Schema s = new Schema(this);
            return s.GetData(conn);
        }
        public List<DataError> ExecuteNonQuery(string[] st, string conn) {
            Data.Schema s = new Schema(this);
            return s.ExecuteNonQuery(st, conn);
        }
        public List<DataError> ExecuteNonQuery(string st, string conn)
        {
            Data.Schema s = new Schema(this);
            s.AfterProcessed += new EventHandler(Processed);
            return s.ExecuteNonQuery(st, conn);
        }
        public string GetSchema(string conn) {
            Data.Schema s = new Schema(this);
            return s.GetStatement();
        }
        public List<DataError> UpdateScript(string path, string conn) {
            Data.Schema s = new Schema(this);
            s.AfterProcessed += new EventHandler(Processed);
            return s.UpdateScript(path,conn);
        }
        public void Processed(object sender, EventArgs e) {
            this.AfterProcessed(this, null);
        } 
    }
    public abstract class baseDataSchema {
         public List<TableField> Fields { get; set; }
    public List<WhereStatement> Wheres { get; set; }
    public List<Join> Joins { get; set; }
        public string Table { get; set; }
    }
    public class WhereStatement
    {
        
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string Operator { get; set; }
        public string TableName { get; set; }
    }
    public class TableField
    {
        public string Table { get; set; }
        public string Field { get; set; }
        public string FieldAlias { get; set; }
        public FieldType Type { get; set; }
        public string FieldValue { get; set; }
        public string DFName { get; set; }
    }
    public class Join
    {
        public string TableName { get; set; }
        public string Field { get; set; }
        public string JoinField { get; set; }
        public string JoinTable { get; set; }
    }
}
