using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data.Query;
using TZ.CompExtention.Builder.Data;

namespace TZ.CompExtention
{
    public static class Shared
    {
        public static string generateID()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = String.Format("{0:d4}", (DateTime.Now.Ticks / 10) % 1000000000);

            return Guid.NewGuid().ToString("N") + number;
        }

        public static void ExecuteSetup(string conn) {
            Builder.Data.Setup s = new Builder.Data.Setup(conn);         
            s.Install();
        }

        public static void ClearSchema(string conn) {
            Builder.Data.Setup s = new Builder.Data.Setup(conn);
            s.Clear();
        }
        public static DataTable GetClientList (string conn)
        {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery select;
            string[] a = { "ClientID", "CustomerName"};
            select = DBQuery.Select().Fields(a).From("sys_client").Distinct();
            return db.Database.GetDatatable(select);
        }
        public static bool IsValidConnection(string conn) {
            try
            {
                DataBase db = new DataBase();
                db.InitDbs(conn);
             var dbqu=   DBQuery.SelectAll().From("sys_client").TopN(1) ;
                db.Database.GetDatatable(dbqu);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public static DataTable GetComponentList(int clientid,string conn) {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBComparison dbClient = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(clientid));
            DBComparison dbclientZero = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(0));
            DBComparison ft = DBComparison.In (DBField.Field("CompType"), DBConst.Int32(320000),DBConst.Int32(300000));
            
            DBQuery select;
            select = DBQuery.Select().Field("sys_FieldInstance", "CompType")
                .Field("sys_FieldType", "CompAttribute")
                 .Field("sys_FieldInstance", "FieldInstanceID")
                 .Field("sys_FieldInstance", "FieldTypeID")
                  .Field("sys_FieldInstance", "FieldAttribute")
                  .Field("sys_FieldInstance", "FieldDescription")
                .From("sys_FieldType").InnerJoin("sys_FieldInstance").On("sys_FieldType", "FieldTypeID", Tech.Data.Compare.Equals, "sys_FieldInstance", "CompType").WhereAll( dbClient);
            return db.Database.GetDatatable(select);
        }
        public static string[] GetCSVToArray(this DataTable dataTable) {
            List<string> fileContent = new List<string>();
            List<string> cols= new List<string>();

            foreach (var col in dataTable.Columns)
            {
                cols.Add(("\"" + col.ToString() + "\""));
            }
            fileContent.Add(string.Join(",", cols.ToArray()));
           
            foreach (DataRow dr in dataTable.Rows)
            {
                List<string> rows = new List<string>();
                foreach (var column in dr.ItemArray)
                {
                    rows.Add("\"" + column.ToString() + "\"");
                }
                fileContent.Add(string.Join(",", rows.ToArray()));           
            }
            return fileContent.ToArray();
        }
        public static string GetCSV(this DataTable dataTable)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            return fileContent.ToString();
        }

        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        public static DataTable GetLookup(int clientid,string conn)
        {
            DBComparison dbClient = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(clientid));
            DBComparison dbclientZero = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(0));
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery select;
            string[] a = { "FieldInstanceID", "Name", "Type" };
            select = DBQuery.Select().Fields(a).From("sys_lookup").WhereAny(dbClient, dbclientZero).Distinct();
            return db.Database.GetDatatable(select);
        }

    }
}
