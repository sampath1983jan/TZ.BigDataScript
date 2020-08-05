using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.CompExtention;
using System.IO;
using Tech.Data.Query;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Reflection;
using Tech.Data.Schema;
using Newtonsoft.Json;

namespace TZ.Import
{
   public static class Global
    {        
        public static List<ComponentView> GetComponentList(string viewPath) {
            IComponentViewDataAccess cv = new ComponentViewDataAccess(viewPath);
            ComponentViewManager vm = new ComponentViewManager(cv);
            return vm.GetViews();           
        }
        public static ImportLog GetLog(string id,string logPath) {
            var strLog = File.ReadAllText(logPath + "/ImportLog.json");
            var implogs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ImportLog>>(strLog);
            var log = implogs.Where(x => x.ID == id).FirstOrDefault();
            return log;
        }

        public static string DataTableToJSON(this DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        public static DataTable ToDataTable<T>(this List<T> items)

        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names

                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)

                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable GetImportContext(string importID, string conn)
        {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery select;
            DBComparison dbcompo = DBComparison.Equal(DBField.Field("importID"), DBConst.String(importID));
            string[] a = { "importID", "ImportSchema", "ActionBy", "Step" };
            select = DBQuery.Select().Fields(a).From("tz_import_log").Distinct().Where(dbcompo);
            return db.Database.GetDatatable(select);
        }

        public static bool RemoveContext(string importID, string conn)
        {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery select;
            DBComparison dbcompo = DBComparison.Equal(DBField.Field("importID"), DBConst.String(importID));
            string[] a = { "importID", "ImportSchema", "ActionBy", "Step" };
            select = DBQuery.DeleteFrom("tz_import_log").Where(dbcompo);
            if (db.Database.ExecuteNonQuery(select) > 0)
            {
                return true;
            }
            else
                return false;
        }


        public static DataTable GetImportContext(int clientID,int actionBy, string conn) {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery select;
            DBComparison dbcompo = DBComparison.Equal(DBField.Field("ClientID"), DBConst.Int32(clientID));
            DBComparison dbAction = DBComparison.Equal(DBField.Field("actionBy"), DBConst.Int32(actionBy));
            string[] a = { "importID", "ImportSchema", "ActionBy", "Step","LastUPD" };
            select = DBQuery.Select().Fields(a).From("tz_import_log").Distinct().WhereAll(dbcompo, dbAction).OrderBy("LastUPD", Tech.Data.Order.Descending);
            return db.Database.GetDatatable(select);
        }

        public static DataTable GetLookup(int clientID, string conn) {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery select;
            DBComparison dbcompo = DBComparison.Equal(DBField.Field("ClientID"), DBConst.Int32(clientID));
            DBComparison dbclient = DBComparison.Equal(DBField.Field("ClientID"), DBConst.Int32(0));
            string[] a = { "FieldInstanceID", "Name", "Type"};
            select = DBQuery.Select().Fields(a).From("sys_lookup").Distinct().WhereAny(dbcompo, dbclient);
            return db.Database.GetDatatable(select);
        }

        public static bool HasNull(this DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                if (table.Rows.OfType<DataRow>().Any(r => r.IsNull(column)))
                    return true;
            }

            return false;
        }
        public static bool HasNull(this DataTable table, string cname)
        {
            if (table.Rows.OfType<DataRow>().Any(r => r.IsNull(cname)))
                return true;

            return false;
        }
        public static bool SaveImportContext(string importID, string importContext, int step, string actionby,string conn,int clientID) {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            if (GetImportContext(importID, conn).Rows.Count == 0)
            {
                DBQuery insert = DBQuery.InsertInto("tz_import_log")
                        .Field("importID")
                        .Field("ImportSchema")
                        .Field("Step")
                        .Field("ActionBy")                        
                        .Field("LastUPD")
                         //.Field("CreatedOn")
                         .Field("ClientID")
                        .Value(DBConst.String(importID))
                        .Value(DBConst.String(importContext))
                        .Value(DBConst.Int32(step))
                        .Value(DBConst.String(actionby))
                        .Value(DBConst.DateTime(DateTime.Now))
                           //.Value(DBConst.DateTime(DateTime.Now))
                        .Value(DBConst.Int32(clientID)
                        );
                if (db.Database.ExecuteNonQuery(insert) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {             
                DBComparison dbcompo = DBComparison.Equal(DBField.Field("importID"), DBConst.String(importID));
                // DBComparison dbclient = DBComparison.Equal(DBField.Field("NextID"), DBConst.Int32 (nextindex));
                DBQuery update = DBQuery.Update("tz_import_log")
                    .Set("ImportSchema", DBConst.String(importContext))
                     .AndSet("ActionBy", DBConst.String(actionby))
                      .AndSet("Step", DBConst.Int32(step))
                       .AndSet("LastUPD", DBConst.DateTime(DateTime.Now)
                ).WhereAll(dbcompo);

                int i = db.Database.ExecuteNonQuery(update);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }            
        }
        
        public static bool SaveImportContext(string importContext, int step, string id,string actionby,string logPath) {
           var path= logPath + "/ImportLog.json";
            if (!Directory.Exists(logPath)) {
                Directory.CreateDirectory(logPath);
            }
            if (!File.Exists(path)) {
                File.WriteAllText (path,"");
            }
            var strLog=    File.ReadAllText(logPath + "/ImportLog.json");
            var implogs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ImportLog>>(strLog);
            if (implogs == null)
            {
                implogs = new List<ImportLog>();
               var log = new ImportLog()
                {
                    ActionBy = actionby,
                    Step = step,
                   ID = id,
                   ImportContext = importContext,
                    DateonCreate = DateTime.Today,
                };
                implogs.Add(log);
            }
            else {
                var log = implogs.Where(x => x.ID == id).FirstOrDefault();
                if (log == null)
                {
                    log = new ImportLog()
                    {
                        ActionBy = actionby,
                        Step = step,
                        ID=id,
                        ImportContext = importContext,
                        DateonCreate = DateTime.Today,
                    };
                    implogs.Add(log);
                }
                else
                {
                    log.ActionBy = actionby;
                    log.Step = step;                    
                    log.ImportContext = importContext;
                    log.LastModified = DateTime.Today;
                }
            }
            
            File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(implogs, Newtonsoft.Json.Formatting.None));
            if (step == 5) {
                File.WriteAllText(logPath + "/impLog_" + id + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(implogs, Newtonsoft.Json.Formatting.None));
            }
            return true;
        }
        public static DataTable GetLookup(string conn) {
            DataBase db = new DataBase();
            db.InitDbs(conn);          
            DBQuery select;
            string[] a = { "FieldInstanceID", "Name", "Type" };
            select = DBQuery.Select().Fields(a).From("sys_lookup").Distinct();           
            return db.Database.GetDatatable(select);
        }
        public static DataTable GetLookup(int clientID, string FieldInstanceIDs, string conn)
        {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            List<DBClause> items = new List<DBClause>();
            var fids = FieldInstanceIDs.Split(',');
            foreach (string itm in fids)
            {
                items.Add(DBConst.Const(itm));
            }
            DBQuery select;
            DBComparison client = DBComparison.Equal(DBField.Field("ClientID"), DBConst.Int32(clientID));
            DBComparison fieldinstance = DBComparison.In(DBField.Field("FieldInstanceID"), items.ToArray());
            DBConst dbclientid = DBConst.String("ClientID");
            select = DBQuery.SelectAll("sys_FieldInstanceLookUp").From("sys_FieldInstanceLookUp")
                  .WhereAll(client, fieldinstance);
            return db.Database.GetDatatable(select);
        }
        private static bool UpdateNextIndex(string conn,string entityid,int nextindex) {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBComparison dbcompo = DBComparison.Equal(DBField.Field("EntityID"), DBConst.String(entityid));
           // DBComparison dbclient = DBComparison.Equal(DBField.Field("NextID"), DBConst.Int32 (nextindex));
            DBQuery update = DBQuery.Update("sys_Entity").Set(
            "NextID", DBConst.Int32(nextindex)
            ).WhereAll(dbcompo);

            int i = db.Database.ExecuteNonQuery(update);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int GetCurrentID(string pEntityID, string conn) {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DataTable dtResult = new DataTable();
            DBQuery select;
            DBComparison EntityID = DBComparison.Equal(DBField.Field("EntityID"), DBConst.String(pEntityID));
            // DBConst dbclientid = DBConst.String("ClientID");
            select = DBQuery.SelectAll("sys_Entity").From("sys_Entity")
                  .WhereAll(EntityID);
            dtResult = db.Database.GetDatatable(select);
            int iNext = 0;
            if ((dtResult.Rows.Count > 0))
            {
                if (!dtResult.Rows[0].IsNull("NextID"))
                {
                    iNext = System.Convert.ToInt32(dtResult.Rows[0]["NextID"]);              
                }               
            }
            return iNext;
        }
        public static bool CheckFieldExist(string tbName, string fieldName, string conn) {
            DataBase db = new DataBase();
            db.InitDbs(conn);

            DBSchemaTable tables;
            DBSchemaProvider provider = db.Database.GetSchemaProvider();
            tables = provider.GetTable(tbName);
            var fd =tables.Columns.Where(x => x.Name.ToLower() == fieldName.ToLower()).FirstOrDefault();
            if (fd == null)
            {
                return false;
            }
            else
                return true;
        }
        
        public static int GetNextID(string pEntityID,string conn,int LastIndex)
        {
            try
            {
                int iNext;
                DataBase db = new DataBase();
                db.InitDbs(conn);
                DataTable dtResult = new DataTable();
                DBQuery select;
                DBComparison EntityID = DBComparison.Equal(DBField.Field("EntityID"), DBConst.String(pEntityID));               
               // DBConst dbclientid = DBConst.String("ClientID");
                select = DBQuery.SelectAll("sys_Entity").From("sys_Entity")
                      .WhereAll(EntityID);
                dtResult= db.Database.GetDatatable(select);

                if ((dtResult.Rows.Count > 0))
                {
                    if (!dtResult.Rows[0].IsNull("NextID"))
                    {
                        iNext = System.Convert.ToInt32(dtResult.Rows[0]["NextID"]) + LastIndex ;
                        UpdateNextIndex(conn,pEntityID, iNext);
                    }
                    else
                    {
                        iNext = LastIndex;
                        UpdateNextIndex(conn,pEntityID, iNext);
                    }
                }
                else
                {
                    iNext = LastIndex;

                     
                    DBConst  eID = DBConst.String(pEntityID);
                    DBConst next = DBConst.Int32(iNext);
                
                    DBQuery insert = DBQuery.InsertInto("sys_Entity").Fields(
                     "EntityID", "NextID"
                      )
                      .Values(
                      eID,
                      next                     
                      );
                    int val = 0;
                    using (DbTransaction trans = db.Database.BeginTransaction())
                    {
                        val = db.Database.ExecuteNonQuery(trans, insert);
                        trans.Commit();
                    }
                   


                    //// 'Pradeep added this code on july 10 2014
                    //mSQLBuilder = new SMRHRT.Data.SQLBuilder(SQLType.SQL_INSERT, DB);
                    //{
                    //    var withBlock = mSQLBuilder;
                    //    withBlock.AddTable("sys_entity");
                    //    withBlock.AddField("sys_Entity", "EntityID", FieldType.FLD_STRING, null/* Conversion error: Set to default value for this argument */, pEntityID.ToString());
                    //    withBlock.AddField("sys_Entity", "NextID", FieldType.FLD_NUMBER, null/* Conversion error: Set to default value for this argument */, System.Convert.ToString(1));
                    //    withBlock.AddField("sys_Entity", "LastUPD", FieldType.FLD_DATETIME, null/* Conversion error: Set to default value for this argument */, System.Convert.ToString(DateAndTime.Today));
                    //}
                    //iNext = DB.ExecuteActionSQL(mSQLBuilder);
                }
                return iNext;
            }
            catch (Exception dEx)
            {
                throw dEx;
            }
        }

     
          
    }

    public static class SystemExtension
    {
        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
