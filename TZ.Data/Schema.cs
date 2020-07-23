using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;


namespace TZ.Data
{
    internal class Schema:DataBase
    {

        private CSVItem[] all;
        private CSVItem item;
        /// <summary>
        /// 
        /// </summary>
        private readonly DBSelectQuery select;
        private   DBUpdateQuery update;
        private   DBQuery insert;
        private DBCreateTableQuery create;
        private string tempTableName { get; set; }
        private DataSchema DataSchema { get; set; }

        public event EventHandler AfterProcessed;

        public Schema(DataSchema ds) :base() {
            select = DBQuery.Select();
            DataSchema = ds;
        }
      
        /// <summary>
        /// 
        /// </summary>
        private void prepareStatement() {

            foreach (TableField tf in DataSchema.Fields)
            {
                if (tf.FieldAlias != "")
                {
                    select.Field(tf.Table, tf.Field).As(tf.FieldAlias);
                }
                else {
                    select.Field(tf.Table, tf.Field).As(tf.Field);
                }             
            }
            if (DataSchema.Joins.Count == 0) {
                select.From(DataSchema.Table);
            }            
            int index = 0;
            foreach (Join j in DataSchema.Joins) {
                if (index == 0)
                {
                    select.InnerJoin(j.TableName.Trim()).As(j.TableName.Trim()).On(j.TableName.Trim(), j.JoinField.Trim(), Compare.Equals, j.JoinTable.Trim(), j.JoinField.Trim());
                }
                else {
                    select.And(j.TableName.Trim(), j.Field.Trim(), Compare.Equals, j.JoinTable.Trim(), j.JoinField.Trim());
                }
            }
            index = 0;
            foreach (WhereStatement ws in DataSchema.Wheres)
            {
                if (index == 0)
                {
                    index = 1;
                    select.WhereField(ws.TableName.Trim(), ws.FieldName.Trim(), GetCompare(ws.Operator), DBConst.String(ws.FieldValue.Trim()));
                }
                else {
                    select.AndWhere(ws.TableName.Trim(), ws.FieldName.Trim(), GetCompare(ws.Operator), DBConst.String(ws.FieldValue.Trim()));
                }
            }
        }
        private  string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        private void CreateTempTable() {
            tempTableName = RandomString(8, true);

            create = DBQuery.Create.Table(tempTableName);
            foreach (TableField tf in DataSchema.Fields)
            {
                if (tf.Type == FieldType._string)
                {
                    create.Add(tf.Field, DbType.String, 555,DBColumnFlags.Nullable);
                }
                else if (tf.Type == FieldType._datetime)
                {
                    create.Add(tf.Field, DbType.DateTime, DBColumnFlags.Nullable);
                }
                else if (tf.Type == FieldType._number)
                {
                    create.Add(tf.Field, DbType.String, 555, DBColumnFlags.Nullable);
                }
                else if (tf.Type == FieldType._bool) {
                    create.Add(tf.Field, DbType.Boolean, DBColumnFlags.Nullable);
                }            
               
            }
            foreach (WhereStatement ws in DataSchema.Wheres) {
                var aa = DataSchema.Fields.Where(x => x.Field == ws.FieldName).FirstOrDefault();
                if (aa == null) {
                    create.Add(ws.FieldName, DbType.String);
                }               
            }
            base.Database.ExecuteNonQuery(create);
        }
        private void PrepareUpdateStatement(string path) {
         
            MySqlConnection conn = new MySqlConnection(base.Database.ConnectionString);

            MySqlBulkLoader bl = new MySqlBulkLoader(conn) ;
            bl.Local = true;
            bl.TableName = tempTableName;
            bl.FieldTerminator = ",";
            bl.LineTerminator = "\n";
            bl.FileName = path;
            bl.NumberOfLinesToSkip = 1;
            try {
                conn.Open();

                // Upload data from file
                int count = bl.Load();
            }
            catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }            

            //update = DBQuery.Update(DataSchema.Table);
            //int index = 0;
            //foreach (TableField tf in DataSchema.Fields)
            //{
            //    if (index == 0)
            //    {
            //        update.Set(tf.Field, DBConst.String(tf.FieldValue));
            //    }
            //    else {
            //        update.AndSet(tf.Field, DBConst.String(tf.FieldValue));
            //    }
            //    index = index = 1;
            //}
            //index = 0;
            //foreach (WhereStatement ws in DataSchema.Wheres)
            //{
            //    if (index == 0)
            //    {
            //        index = 1;
            //        update.WhereField(ws.TableName.Trim(), ws.FieldName.Trim(), GetCompare(ws.Operator), DBConst.String(ws.FieldValue.Trim()));
            //    }
            //    else
            //    {
            //        update.AndWhere(ws.TableName.Trim(), ws.FieldName.Trim(), GetCompare(ws.Operator), DBConst.String(ws.FieldValue.Trim()));
            //    }
            //}

        }
        private void prepareInsertStatement(string state, string tableName)
        {
            List<DBClause> cs = new List<DBClause>();
            CSVData data = CSVData.ParseString(string.Join(Environment.NewLine, state), true);
            all = data.Items;
            List<string> c = new List<string>();
            foreach (TableField tf in DataSchema.Fields)
            {
                cs.Add(DBParam.ParamWithDelegate(delegate {
                    if (tf.Type == FieldType._datetime)
                    {
                        if (item[data.GetOffset(tf.Field)] != "")
                        {
                            return Convert.ToDateTime(item[data.GetOffset(tf.Field)]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else if (tf.Type == FieldType._bool) {
                        if (item[data.GetOffset(tf.Field)] != "")
                        {
                            if (item[data.GetOffset(tf.Field)].ToString().ToLower() == "true")
                            {
                                return 1;
                            }
                            else {
                                return 0;
                            }
                           // return Convert.ToDateTime(item[data.GetOffset(tf.Field)]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (item[data.GetOffset(tf.Field)] != "")
                        {
                            return item[data.GetOffset(tf.Field)].Trim();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }));
                c.Add(tf.Field);
            }
            insert = DBQuery.InsertInto(tableName)
              .Fields(c.ToArray())
                                      .Values(cs.ToArray());
        }

        private void prepareInsertStatement(string[] state,string tableName) {
            List<DBClause> cs = new List<DBClause>();
            CSVData data = CSVData.ParseString(state, true);
         all=   data.Items;
            List<string> c = new List<string>();
            foreach (TableField tf in DataSchema.Fields)
            {
                cs.Add(DBParam.ParamWithDelegate(delegate {
                    if (tf.Type == FieldType._datetime)
                    {
                        if (item[data.GetOffset(tf.Field)] != "")
                        {
                            return Convert.ToDateTime(item[data.GetOffset(tf.Field)]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else if (tf.Type == FieldType._bool) {
                        if (item[data.GetOffset(tf.Field)] != "")
                        {
                            if (item[data.GetOffset(tf.Field)] == "true" || item[data.GetOffset(tf.Field)] == "True" || item[data.GetOffset(tf.Field)]=="1")
                            {
                                return true;
                            }
                            else {
                                return false;
                            }
                        //    return Convert.ToDateTime(item[data.GetOffset(tf.Field)]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else {
                            return false;
                        }
                    }
                    else
                    {
                        if (item[data.GetOffset(tf.Field)] != "")
                        {
                            return item[data.GetOffset(tf.Field)].Trim();
                        }
                        else
                        {
                            return null;
                        }
                    }                  
                }));
                c.Add(tf.Field);
            } 
              insert = DBQuery.InsertInto(tableName)
                .Fields(c.ToArray())
                                        .Values(cs.ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetStatement() {
           return select.ToSQLString(Database);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetData(string conn) {
 
                base.InitDbs(conn);
                prepareStatement();
            var statement = select.ToSQLString(Database);
            bool isCustomFunction = false;
            foreach (TableField tf in DataSchema.Fields)
            {
                //`lms_Schedule_yeardays`.`scheduleno` AS `scheduleno`
                if (tf.DFName != "" && tf.DFName !=null) {
                 
                    string st = "`" + tf.Table + "`.`" + tf.Field ;
                    string stwithalias = st + "` AS `" + tf.FieldAlias + "`";
                    statement = statement.Replace(stwithalias, tf.DFName + "(" + st + "`" + ") AS `" + tf.FieldAlias +"`");

                  var tt=  DataSchema.Wheres.
                        Where(x => x.TableName.ToLower() == tf.Table.ToLower() && x.FieldName.ToLower() == tf.FieldAlias.ToLower()).FirstOrDefault();
                    if (tt != null) {
                        st = "`" + tt.TableName + "`.`" + tf.FieldAlias + "`";
                       var  atst = "`" + tt.TableName + "`.`" + tf.Field + "`";

                        statement = statement.Replace(st, tf.DFName + "(" + atst + ")");
                    }
                    isCustomFunction = true;
                }
            }
            if (isCustomFunction)
            {
                DataSet ds = new DataSet();
                DataTable dt = ds.Tables.Add("output");
                Database.PopulateDataSet(ds, statement);
                ds.AcceptChanges();
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }

            }
            else {
                var data = Database.GetDatatable(select);
                return data;
            }           
              
          //  }
        }

        //protected Internal void UpdateData<T>(DataTable dt, string TableName)
        //{



        //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolSoulDataEntitiesForReport"].ConnectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand("", conn))
        //        {
        //            try
        //            {
        //                conn.Open();

        //                //Creating temp table on database
        //                command.CommandText = "CREATE TABLE #TmpTable(...)";
        //                command.ExecuteNonQuery();

        //                //Bulk insert into temp table
        //                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
        //                {
        //                    bulkcopy.BulkCopyTimeout = 660;
        //                    bulkcopy.DestinationTableName = "#TmpTable";
        //                    bulkcopy.WriteToServer(dt);
        //                    bulkcopy.Close();
        //                }

        //                // Updating destination table, and dropping temp table
        //                command.CommandTimeout = 300;
        //                command.CommandText = "UPDATE T SET ... FROM " + TableName + " T INNER JOIN #TmpTable Temp ON ...; DROP TABLE #TmpTable;";
        //                command.ExecuteNonQuery();
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle exception properly
        //            }
        //            finally
        //            {
        //                conn.Close();
        //            }
        //        }
        //    }
        //}

        private string  PrepareUpdateStatement() {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + DataSchema.Table);
            sb.Append(" INNER JOIN " + tempTableName);
            int index = 0;
            foreach (WhereStatement ws in DataSchema.Wheres) {
                if (index == 0)
                {
                    sb.Append(" ON  " + DataSchema.Table + "." + ws.FieldName + "=" + tempTableName + "." + ws.FieldName);
                }
                else {
                    sb.Append(" AND  " + DataSchema.Table + "." + ws.FieldName + "=" + tempTableName + "." + ws.FieldName);
                }
                index = +1;


            }
            index = 0;
            foreach (TableField tf in DataSchema.Fields) {
                if (index == 0)
                {
                    sb.Append(" SET " + tf.Table + "." + tf.Field + " = " + tempTableName + "." + tf.Field);
                }
                else {
                    sb.Append(" , " + tf.Table + "."+ tf.Field + " = " + tempTableName + "." + tf.Field);
                }
                index = index + 1;
            }

            return sb.ToString();
        }
         
        public List<DataError> UpdateScript(string path,string conn) {
            base.InitDbs(conn);
            DataError ers = new DataError();
            CreateTempTable();
            string contentcsv=  System.IO.File.ReadAllText(path);
            prepareInsertStatement(contentcsv,tempTableName);
            List<DataError> erss = new List<DataError>();

            using (DbTransaction trans = base.Database.BeginTransaction())
            {
                int sum = 0;
                // base.Database.CreateConnection().Open();
                for (int i = 0; i < all.Length; i++)
                {
                    item = all[i];
                    try
                    {
                        sum += base.Database.ExecuteNonQuery(trans, insert);
                        erss.Add(new DataError() { Type = DataError.ErrorType.NOERROR, Message = "Import done" });
                    }
                    catch (Exception ex)
                    {
                        erss.Add(new DataError() { Type = DataError.ErrorType.ERROR, Message = "Error:" + ex.Message + " and  values (" + string.Join(",", item.ToArray()) + ")" });
                    }
                    finally
                    {
                        this.AfterProcessed(null, null);
                    }
                }
                trans.Commit();
            }
            try
            {
                string s = PrepareUpdateStatement();
                int count = base.Database.ExecuteNonQuery(s);
                if (count == 0)
                {
                    foreach (DataError dr in erss)
                    {
                        dr.Type = DataError.ErrorType.NOERROR;
                        dr.Message = "Import done";
                    }                  
                }                 
                base.Database.ExecuteNonQuery("Drop table " + tempTableName + ";");
            }
            catch (Exception ex)
            {
                foreach (DataError dr in erss)
                {
                    dr.Type = DataError.ErrorType.ERROR;
                    dr.Message = ex.Message;
                }           
            }
            finally {
               
            }
            return erss;
        }

        public List<DataError> ExecuteNonQuery(string statement, string conn)
        {
            base.InitDbs(conn);
            List<DataError> ers = new List<DataError>();
            prepareInsertStatement(statement, DataSchema.Table);
            using (DbTransaction trans = base.Database.BeginTransaction())
            {
                int sum = 0;
                // base.Database.CreateConnection().Open();
                for (int i = 0; i < all.Length; i++)
                {
                    item = all[i];
                    try
                    {
                        sum += base.Database.ExecuteNonQuery(trans, insert);
                        ers.Add(new DataError() { Type = DataError.ErrorType.NOERROR, Message = "Import done" });                       
                    }
                    catch (Exception ex)
                    {
                        ers.Add(new DataError() { Type = DataError.ErrorType.ERROR, Message = "Error:" + ex.Message + " and  values (" + string.Join(",", item.ToArray()) + ")" });
                    }
                    finally
                    {
                        this.AfterProcessed(null, null);
                    }
                }
                trans.Commit();
            }
            return ers;
        }

        public List<DataError> ExecuteNonQuery( string[] statement, string conn) {
            base.InitDbs(conn);
            List<DataError> ers = new List<DataError>();
            prepareInsertStatement(statement, DataSchema.Table);
           using (DbTransaction trans = base.Database.BeginTransaction())
             {             
                int sum = 0;
               // base.Database.CreateConnection().Open();
                for (int i = 0; i < all.Length; i++)
                {
                    item = all[i];
                    try
                    {                        
                        sum += base.Database.ExecuteNonQuery(trans,insert);
                        ers.Add(new DataError() { Type = DataError.ErrorType.NOERROR , Message = "Import done" });
                    }
                    catch (Exception ex)
                    {
                        ers.Add(new DataError() { Type = DataError.ErrorType.ERROR, Message = "Error:" +ex.Message + " and  values (" + string.Join("," ,item.ToArray()) + ")" });
                    }
                    finally {
                        this.AfterProcessed(null, null);
                    }
                }
                trans.Commit();                 
            }           
            return ers;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opr"></param>
        /// <returns></returns>
        private Tech.Data.Compare GetCompare(string opr)
        {
            if (opr == "=")
            {
                return Compare.Equals;
            }
            switch (opr)
            {
                case "=":
                    return Compare.Equals;
                case "!=":
                    return Compare.NotEqual;
                case "in":
                    return Compare.In;
                case "notin":
                    return Compare.NotIn;
                case "start":
                    return Compare.Like;
                case "end":
                    return Compare.Like;
                case ">":
                    return Compare.GreaterThan;
                case ">=":
                    return Compare.GreaterThanEqual;
                case "<":
                    return Compare.LessThan;
                case "<=":
                    return Compare.LessThanEqual;
                case "%":
                    return Compare.Like;
                default:
                    return Compare.Equals;
            }
        }

    }
   
}
