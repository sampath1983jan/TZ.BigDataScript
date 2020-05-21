using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TZ.CompExtention;
using TZ.Data;
using System.IO;
using TZ.Import.Step;
using TZ.CompExtention.ImportTemplate;
/// <summary>
/// 
/// </summary>
namespace TZ.Import
{
    /// <summary>
    /// 
    /// </summary>
    public class ComponentData
    {
        /// <summary>
        /// 
        /// </summary>
        public int TotalImportRecord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ErrorRecord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SuccessRecord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalInserted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalUpdated { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lookupPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Component Component { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable SourceData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable TargetData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable ValidDataSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable ProcessedData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BaseCustomAction Validation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ImportFieldMap> ImportFields { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ComponentData> ParentComponentData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsValidated { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid { get => CheckValid(); set { } }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LookupComponentField> ComponentLookups { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable UnIdentifyLink { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private int AutoIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ImportError> Errors { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private DataTable LookupList { get; set; }

        public ImportDataStatus DataStatus { get; set; }

        public List<ComponentDataLog> Logs { get; set; }

        private string LogPath { get; set; }
        private string Context { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="comp"></param>
        public ComponentData(int clientID, CompExtention.Component comp)
        {
            this.ClientID = clientID;
            ComponentLookups = new List<LookupComponentField>();
            this.Component = comp;
            ParentComponentData = new List<ComponentData>();
            Errors = new List<ImportError>();
            UnIdentifyLink = new DataTable();
            Logs = new List<ComponentDataLog>();
            DataStatus = new ImportDataStatus();
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        protected internal void ExtractDataFromSource(DataTable dt)
        {
            List<ImportFieldMap> keys = ImportFields.Where(x => x.IsKey == true).ToList();
            List<string> cols = new List<string>();
            foreach (ImportFieldMap ifm in ImportFields)
            {
                if (ifm.IsAutoCode != true)
                {
                    ifm.FileFields = ifm.FileFields.Replace(" ", "_");
                    cols.Add(ifm.FileFields);
                }
            }
            foreach (DataColumn dc in dt.Columns)
            {
                dc.ColumnName = dc.ColumnName.Replace(" ", "_");
            }
            if (keys.Count == ImportFields.Count)
            {
                SourceData = dt.DefaultView.ToTable(false, cols.ToArray());
            }
            else {
                SourceData = dt.DefaultView.ToTable(true, cols.ToArray());
            }
            
        }
        protected internal DataTable ReshapeSource(DataTable dt,List<TemplateField> fields,
            List<CompExtention. Attribute> attributes,string conn) {
            List<string> cols = new List<string>();
            foreach (ImportFieldMap ifm in ImportFields)
            {
                if (ifm.IsAutoCode != true)
                {
                    ifm.FileFields = ifm.FileFields.Replace(" ", "_");
                    cols.Add(ifm.FileFields);
                }
            }
            foreach (DataColumn dc in dt.Columns)
            {
                dc.ColumnName = dc.ColumnName.Replace(" ", "_");
            }
           var dtUnique = dt.DefaultView.ToTable(true, cols.ToArray());
            DataTable dtShape = new DataTable();
            
            foreach (TemplateField a in fields) {
                var att = attributes.Where(x => x.ID == a.ID).FirstOrDefault();
                if (att != null) {
                    dtShape.Columns.Add(att.Name);
                }
            }
            var pcol = fields.Where(x => x.IsPivot == true).FirstOrDefault();
            var col = fields.Where(x => x.IsColumn == true).FirstOrDefault();
        


            var pcatt = attributes.Where(x => x.ID == pcol.ID).FirstOrDefault();
            var catt = attributes.Where(x => x.ID == col.ID).FirstOrDefault();

            Dictionary<int, string> colVals = new Dictionary<int, string>();
            if (catt.Type == AttributeType._lookup) {
                var item = GetLookup (catt.LookupInstanceID, conn);
                foreach (DataRow r in item.Rows) {
                    colVals.Add( Convert.ToInt32( r["LookupID"]),r["LookupDescription"].ToString());
                }
            }
            int totalRow = dtUnique.Rows.Count * colVals.Count;
            for (int i = 0; i < totalRow; i++) {
                dtShape.Rows.Add(dtShape.NewRow());
            }
            var rows = fields.Where(x => x.IsRow == true).ToList();
            List<string> rowFields = new List<string>();

            foreach (TemplateField tf in rows) {
                var imf = ImportFields.Where(x => x.DataField == tf.ID).FirstOrDefault();
                if (imf != null) {
                    rowFields.Add(imf.FileFields);
                }
            }
            int rowIndex = 0;
         

            foreach (DataRow dr in dtUnique.Rows)
            {
                foreach (KeyValuePair<int, string> s in colVals)
                {
                    if (dtUnique.Columns.Contains(s.Value.Replace(" ", "_")))
                    {
                        foreach (TemplateField a in rows)
                        {
                            var att = attributes.Where(x => x.ID == a.ID).FirstOrDefault();
                            var imf = ImportFields.Where(x => x.DataField == a.ID).FirstOrDefault();
                            if (att != null && imf != null)
                            {
                                dtShape.Rows[rowIndex][att.Name] = dr[imf.FileFields].ToString();
                            }
                        }
                        dtShape.Rows[rowIndex][catt.Name] = s.Key.ToString();
                        if (dr[s.Value.Replace(" ", "_")] != null)
                        {
                            dtShape.Rows[rowIndex][pcatt.Name] = dr[s.Value.Replace(" ", "_")].ToString();
                        }
                    }
                    rowIndex = rowIndex + 1;
                }
            }
            //string sfilter = "Isnull("+ pcatt.Name + ",'') <> ''";
            //string ssfilter = pcatt.Name + " IS NOT NULL";
            //dtShape.DefaultView.RowFilter = sfilter;
          return dtShape;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public void SetLookupList(DataTable dt)
        {
            LookupList = dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dt"></param>
        protected internal static void PushLookUp(string conn, DataTable dt)
        {
            DataManager dm = new DataManager();
           // List<string> columnNames = new List<string>();
            dt.Columns.Remove(dt.Columns["LastUPD"]);
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName != "New")
                {
                    dm.InsertInto(dc.ColumnName, FieldType._string);
                    //columnNames.Add(dc.ColumnName);
                }
            }
            dm.tableName("sys_fieldinstancelookup");

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(string.Join(",", columnNames));

            //foreach (DataRow row in dt.Rows)
            //{
            //    string[] fields = row.ItemArray.Select(field => field.ToString()).
            //                                    ToArray();
            //    sb.AppendLine(string.Join(",", fields));
            //}
            dm.Data(dt.GetCSVToArray());
            List<DataError> dataErrors = dm.ExecuteNonQuery(conn);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPath"></param>
        /// <param name="write"></param>
        /// <param name="context"></param>        
        protected internal void PushLink(string conn, string logPath, string contextid)
        {
            this.LogPath = logPath;
            this.Context = contextid;
            DataTable InsertData = new DataTable();
            TotalImportRecord = ValidDataSource.Rows.Count;

            InsertData = ValidDataSource.Copy();

            #region Prevalidation before push the link

            foreach (ImportFieldMap ifm in ImportFields)
            {
                var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                if (InsertData.Columns.Contains(ifm.FileFields))
                {
                    if (att != null)
                    {
                        InsertData.Columns[ifm.FileFields].ColumnName = att.Name;
                    }
                }
            }

            foreach (DataRow row in InsertData.Rows)
            {
                SetLinkKeys(row, true);
                int idx = Convert.ToInt32(row["index"]);
                var lg = this.Logs.Where(x => x.Index == idx).FirstOrDefault();
                //if (lg.IsExist == false)
                //{
                //    List<string> val = new List<string>();
                //    foreach (ComponentData cd in this.ParentComponentData)
                //    {
                //        List<ImportFieldMap> keys = new List<ImportFieldMap>();
                //        var keyFieldsOnly = ImportFields.Where(x => x.IsKey == true && x.ComponentID == cd.Component.ID).ToList();
                //        foreach (ImportFieldMap ifm in keyFieldsOnly)
                //        {
                //            val.Add(ifm.FileFields);
                //        }
                //    }
                //    if (lg != null)
                //    {
                //        lg.Message = "Link not exist.(" + string.Join(",", val.ToArray()) + ")";
                //        lg.Type = ErrorType.ERROR;
                //        lg.ImportingType = ComponentDataLog.ImportType.INSERT;
                //    }
                //}
                if (lg.Type != ErrorType.ERROR)
                {
                    if (Validation != null)
                    {
                        var err = Validation.ValidateLink(this, row);
                        if (err.Type == ErrorType.ERROR)
                        {
                            lg.Message = err.Message;
                            lg.Type = ErrorType.ERROR;
                            lg.ImportingType = ComponentDataLog.ImportType.INSERT;
                            continue;
                        }
                    }
                }
            }
            #endregion


            var Insertlog = this.Logs.Where(x => x.Type == ErrorType.NOERROR && x.ImportingType == ComponentDataLog.ImportType.INSERT && x.IsExist == false).ToList();
            List<int> indexList = Insertlog.Select(log => log.Index).ToList();
            if (Insertlog.Count > 0)
            {
                InsertData = InsertData.Select("Index In (" + string.Join(",", indexList.ToArray()) + ")").CopyToDataTable();
                // InsertData = InsertData.AsEnumerable().Where(x => x["Index"].ToString().Contains(string.Join(",", indexList.ToArray()))).AsDataView().ToTable(true);
                SuccessRecord = InsertData.Rows.Count;

                DataManager dm = new DataManager();
                DataTable dt = InsertData.Copy();
                if (dt.Columns.Contains("Index"))
                {
                    dt.Columns.Remove("Index");
                }
                List<string> cols = new List<string>();
                foreach (DataColumn dc in dt.Columns)
                {
                    bool isKeyCol = false;
                    foreach (ComponentData c in this.ParentComponentData)
                    {
                        var a = c.Component.Keys.Where(x => x.Name == dc.ColumnName).FirstOrDefault();
                        if (a != null)
                        {
                            isKeyCol = true;
                        }
                    }

                    foreach (CompExtention.Attribute att in this.Component.Attributes)
                    {
                        if (att.Name == dc.ColumnName)
                        {
                            isKeyCol = true;
                        }
                    }

                    if (isKeyCol == false)
                    {
                        cols.Add(dc.ColumnName);
                    }
                }

                foreach (string s in cols)
                {
                    dt.Columns.Remove(s);
                }

                List<string> columnNames = new List<string>();
                foreach (DataColumn dc in dt.Columns)
                {

                    if (dc.DataType == typeof(DateTime))
                    {
                        dm.InsertInto(dc.ColumnName, FieldType._datetime);
                    }
                    else if (dc.DataType == typeof(Boolean))
                    {
                        dm.InsertInto(dc.ColumnName, FieldType._bool);
                    }
                    else
                    {
                        dm.InsertInto(dc.ColumnName, FieldType._string);
                    }

                    columnNames.Add(dc.ColumnName);
                }

                dm.tableName(this.Component.TableName);
                //IEnumerable<string> columnNames = ImportFields.
                //                      Select(column => column.DataField);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Join(",", columnNames));

                //StringBuilder filter = new StringBuilder();
                //StringBuilder filternotin = new StringBuilder();
                //int index = 0;
                //foreach (ComponentData pc in ParentComponentData) {
                //    foreach (CompExtention.Attribute a in pc.Component.Keys) {
                //        if (a.Name.ToLower() != "clientid") {
                //            if (index == 0)
                //            {
                //                filter.Append(a.Name + "=-1");
                //                filternotin.Append(a.Name + "<>-1");
                //            }
                //            else {
                //                filter.Append(" AND " + a.Name + "=-1");
                //                filternotin.Append(" AND " + a.Name + "<>-1");
                //            }
                //            index = index + 1;
                //        }
                //    }
                //}
                //// validate here key value updated in link if not write errorr
                //if (filter.ToString() != "") {
                //    dt.DefaultView.RowFilter = filter.ToString();
                //    UnIdentifyLink = dt.DefaultView.ToTable(true);
                //    dt.DefaultView.RowFilter = "";
                //    dt.DefaultView.RowFilter = filternotin.ToString();
                //    dt = dt.DefaultView.ToTable(true);
                //    foreach (DataRow drr in UnIdentifyLink.Rows) {
                //        if (Errors == null)
                //        {
                //            Errors = new List<ImportError>();
                //        }
                //        Errors.Add(new ImportError()
                //        {
                //            Message = "Unable to identify links+ " + string.Join(",", drr.ItemArray.Select(field => field.ToString()).ToArray()),
                //            Type = ErrorType.ERROR,
                //        });
                //    }
                //}
                //   int Index = 0;
                //List<int> Errorindex = new List<int>();
                foreach (DataRow row in dt.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
                    sb.AppendLine(string.Join(",", fields));
                }
                dm.Data(sb.ToString());
                List<DataError> dataErrors = dm.ExecuteNonQuery(conn);
                int indexx = 0;
                foreach (DataError derr in dataErrors)
                {
                    var drow = InsertData.Rows[indexx];
                    var a = Convert.ToInt32(drow["Index"]);
                    var lg = Logs.Where(x => x.Index == a).FirstOrDefault();
                    if (lg != null)
                    {
                        lg.Message = derr.Message;
                        lg.ImportingType = ComponentDataLog.ImportType.INSERT;
                        if (derr.Type == DataError.ErrorType.ERROR)
                        {
                            lg.Type = ErrorType.ERROR;
                        }
                        else if (derr.Type == DataError.ErrorType.NOERROR)
                        {
                            lg.Type = ErrorType.NOERROR;
                        }
                    }
                }
                if (Validation != null)
                {
                    Validation.AfterSave(this, dt);
                }
             
                TotalInserted = Logs.Where(x => x.Type == ErrorType.NOERROR && x.ImportingType == ComponentDataLog.ImportType.INSERT).ToList().Count;
               
            }
            else
            {

                var updatelog = this.Logs.Where(x => x.Type == ErrorType.NOERROR && x.ImportingType == ComponentDataLog.ImportType.UPDATE && x.IsExist == true).ToList();
                List<int> updateindex = updatelog.Select(log => log.Index).ToList();
                // update behavior
                DataTable updateData = new DataTable();
                updateData = ValidDataSource.Select("Index In (" + string.Join(",", updateindex.ToArray()) + ")").CopyToDataTable();
                if (updateData.Rows.Count > 0)
                {
                    if (Validation != null)
                    {
                        foreach (DataRow row in updateData.Rows)
                        {
                            int idx = Convert.ToInt32(row["index"]);
                            var lg = this.Logs.Where(x => x.Index == idx).FirstOrDefault();
                            if (lg.Type != ErrorType.ERROR)
                            {
                                if (Validation != null)
                                {
                                    var err = Validation.ValidateLink(this, row);
                                    if (err.Type == ErrorType.ERROR)
                                    {
                                        lg.Message = err.Message;
                                        lg.Type = ErrorType.ERROR;
                                        lg.ImportingType = ComponentDataLog.ImportType.UPDATE;
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    this.Logs.Where(x => x.Type == ErrorType.NOERROR && x.ImportingType == ComponentDataLog.ImportType.UPDATE && x.IsExist == true).ToList();
                    updateindex = updatelog.Select(log => log.Index).ToList();
                    // update behavior

                    updateData = ValidDataSource.Select("Index In (" + string.Join(",", updateindex.ToArray()) + ")").CopyToDataTable();

                    if (updateData.Rows.Count > 0)
                    {
                        var dm = new DataManager();
                        List<string> cols = new List<string>();
                        // replace filefield name to component attribute name
                        foreach (ImportFieldMap ifm in ImportFields)
                        {
                            var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                            if (att != null)
                            {
                                if (ifm.FileFields != null)
                                {
                                    if (updateData.Columns.Contains(ifm.FileFields))
                                    {

                                        updateData.Columns[ifm.FileFields].ColumnName = att.Name;
                                    }
                                }
                            }
                        }

                        foreach (CompExtention.Attribute key in this.Component.Keys)
                        {
                            if (key.Name.ToLower() != "clientid" && key.ComponentLookup == "")
                            {
                                if (!updateData.Columns.Contains(key.Name))
                                {
                                    updateData.Columns["ExistKey"].ColumnName = key.Name;
                                }
                            }
                        }
                        DataTable dt = updateData.Copy();
                        // find key fields and component fields other than any field exist need to remove it
                        cols = new List<string>();
                        foreach (DataColumn dc in dt.Columns)
                        {
                            bool isKeyCol = false;
                            foreach (ComponentData c in this.ParentComponentData)
                            {
                                var a = c.Component.Keys.Where(x => x.Name == dc.ColumnName).FirstOrDefault();
                                if (a != null)
                                {
                                    isKeyCol = true;
                                }
                            }
                            foreach (CompExtention.Attribute att in this.Component.Attributes)
                            {
                                if (att.Name == dc.ColumnName)
                                {
                                    isKeyCol = true;
                                }
                            }
                            if (isKeyCol == false)
                            {
                                cols.Add(dc.ColumnName);
                            }
                        }
                        foreach (string s in cols)
                        {
                            dt.Columns.Remove(s);
                        }/////

                        dm = new DataManager();
                        dm.tableName(this.Component.TableName);
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.DataType == typeof(DateTime))
                            {
                                dm.UpdateInto(dc.ColumnName, FieldType._datetime, "");
                            }
                            else if (dc.DataType == typeof(Boolean))
                            {
                                dm.UpdateInto(dc.ColumnName, FieldType._bool, "");
                            }
                            else
                            {
                                dm.UpdateInto(dc.ColumnName, FieldType._string, "");
                            }
                        }
                        foreach (CompExtention.Attribute key in this.Component.Keys)
                        {
                            dm.Where(this.Component.TableName, key.Name, "=", "");
                        }
                        if (dm.GetSchema().Wheres.Count == 0)
                        {
                            Errors.Add(new ImportError()
                            {
                                Message = "No Key field set while update record",
                                Type = ErrorType.ERROR,
                            });
                        }
                        else
                        {
                            if (dm.GetSchema().Wheres.Where(x => x.FieldName.ToLower() == "clientid").FirstOrDefault() == null)
                            {
                                dm.Where(this.Component.TableName, "ClientID", "=", ClientID.ToString());
                            }
                        }
                        TotalUpdated = 0;
                        //}
                        var path = logPath + "/" + Guid.NewGuid() + ".csv";
                        File.WriteAllText(path, "");
                        dt.ToCSV(path);
                        var updateerr = dm.ExecuteNonQuery(path, conn);

                        int indexx = 0;
                        foreach (DataError derr in updateerr)
                        {
                            var drow = updateData.Rows[indexx];
                            var a = Convert.ToInt32(drow["Index"]);
                            var lg = Logs.Where(x => x.Index == a).FirstOrDefault();
                            if (lg != null)
                            {
                                lg.Message = derr.Message;
                                lg.ImportingType = ComponentDataLog.ImportType.UPDATE;
                                if (derr.Type == DataError.ErrorType.ERROR)
                                {
                                    lg.Type = ErrorType.ERROR;
                                }
                                else if (derr.Type == DataError.ErrorType.NOERROR)
                                {
                                    lg.Type = ErrorType.NOERROR;
                                }
                            }
                        }
                        if (Validation != null)
                        {
                            Validation.AfterUpdate(this, dt);
                        }
                        dt.Dispose();
                        TotalUpdated = Logs.Where(x => x.Type == ErrorType.NOERROR && x.ImportingType == ComponentDataLog.ImportType.UPDATE).ToList().Count;
                    }
                }
            }
            ErrorRecord = Logs.Where(x => x.Type == ErrorType.ERROR).ToList().Count;
        }
        public bool IsValidRow(DataRow dr)
        {
            int index = Convert.ToInt32(dr["Index"]);
            var a = this.Logs.Where(x => x.Index == index).FirstOrDefault();
            if (a != null)
            {
                if (a.Type == ErrorType.ERROR)
                {
                    return false;
                }
                else
                    return true;
            }
            else
                return false;
        }
        private void AddImportStatus(int DataCount,double percentageToComplete,string mss, DataStatus Status) {
            DataStatus = new ImportDataStatus();
            DataStatus.ComponentID = this.Component.ID;
            DataStatus.ComponentName = this.Component.Name;
            DataStatus.DataProcessedCount = DataCount;
            DataStatus.PercentageToComplete = percentageToComplete;
            DataStatus.Messge = mss;
            DataStatus.Status = Status;
            WriteImportStatus(LogPath,Newtonsoft.Json.JsonConvert.SerializeObject(DataStatus ), Context);
        }

        protected internal void PushPseudo(string conn, string logPath, string context) {
            this.LogPath = logPath;
            this.Context = context;
            AddImportStatus(0, 0, "Import Started", Import.DataStatus.STARTED);
            DataTable InsertData = new DataTable();
            DataTable dt = new DataTable();
            TotalImportRecord = ValidDataSource.Rows.Count;
            InsertData= ValidDataSource.Copy();
         
            AddImportStatus(0, 0, "Data perparation progress to import", Import.DataStatus.STARTED);

            DataManager dm = new DataManager();
            List<string> columnNames = new List<string>();

            dt = InsertData.Copy();

            foreach (ImportFieldMap ifm in ImportFields)
            {
                var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                if (ifm.FileFields != null)
                {
                    if (dt.Columns.Contains(ifm.FileFields) && (!dt.Columns.Contains(att.Name)))
                    {
                        dt.Columns[ifm.FileFields].ColumnName = att.Name;
                    }
                }
            }
                 

         
            if (dt.Columns.Contains("index"))
            {
                dt.Columns.Remove("index");
            }
            foreach (DataColumn dc in dt.Columns)
            {

                if (dc.DataType == typeof(DateTime))
                {
                    dm.InsertInto(dc.ColumnName, FieldType._datetime);
                }
                else if (dc.DataType == typeof(Boolean))
                {
                    dm.InsertInto(dc.ColumnName, FieldType._bool);
                }
                else
                {
                    dm.InsertInto(dc.ColumnName, FieldType._string);
                }
                columnNames.Add(dc.ColumnName);
            }
            dm.tableName(this.Component.TableName);

            dm.Data(dt.GetCSVToArray());

            dm.AfterProcessed += new EventHandler(AfterDataImported);
            List<DataError> dataErrors = dm.ExecuteNonQuery(conn);
            List<ImportError> er = new List<ImportError>();
            int index = 0;
            foreach (DataError dr in dataErrors)
            {
                if (InsertData.Rows.Count > index)
                {
                    var datarow = InsertData.Rows[index];
                    var dataindex = Convert.ToInt32(datarow["index"]);
                    var log = Logs.Where(x => x.Index == dataindex).FirstOrDefault();
                    if (log != null)
                    {
                        if (dr.Type == DataError.ErrorType.ERROR)
                        {
                            log.Message = dr.Message;
                            log.Type = ErrorType.ERROR;
                        }
                        else
                        {
                            log.Message = "Data Imported Successfully";
                            log.Type = ErrorType.NOERROR;
                        }
                    }
                    else
                    {
                        Logs.Add(new ComponentDataLog() { Type = ErrorType.ERROR, Message = "Unknow record found" + ": " + dataindex });
                    }

                    index = index + 1;
                }
                else
                {
                    Logs.Add(new ComponentDataLog() { Type = ErrorType.ERROR, Message = "Unknow record found" });
                }
            }
            if (dataErrors.Count <= dt.Rows.Count)
            {
                if (Validation != null)
                {
                    this.DataStatus.PercentageToComplete = this.DataStatus.PercentageToComplete + (this.DataStatus.PercentageToComplete * .12);
                    if (this.DataStatus.PercentageToComplete >= 100)
                    {
                        this.DataStatus.PercentageToComplete = 90;
                    }
                    AddImportStatus(this.DataStatus.DataProcessedCount, this.DataStatus.PercentageToComplete, "Processing support information", Import.DataStatus.STARTED);
                    // send error index
                    Validation.AfterSave(this, dt); // dt change to InsertData with error status based on that depended what to save
                }
            }

            TotalInserted = Logs.Where(x => x.Type == ErrorType.NOERROR && x.ImportingType == ComponentDataLog.ImportType.INSERT).ToList().Count();
            AddImportStatus(this.DataStatus.DataProcessedCount, 100, "Processing support information", Import.DataStatus.COMPLETED);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        protected internal void Push(string conn, string logPath, string context)
        {
            this.LogPath = logPath;
            this.Context = context;
            AddImportStatus( 0, 0, "Import Started", Import.DataStatus.STARTED);
            
            if (this.Component.Type == ComponentType.core || this.Component.Type == ComponentType.attribute)
            {
                DataTable InsertData = new DataTable();
                TotalImportRecord = ValidDataSource.Rows.Count;
                //DataTable dtLog = this.Logs.ToDataTable<ComponentDataLog>();
                var Insertlog = this.Logs.Where(x => x.Type == ErrorType.NOERROR && x.IsExist == false).ToList();
                List<int> indexList = Insertlog.Select(log => log.Index).ToList();
                if (Insertlog.Count > 0)
                {
                    InsertData = ValidDataSource.Select("Index In (" + string.Join(",", indexList.ToArray()) + ")").CopyToDataTable();               
                    SuccessRecord = ValidDataSource.Rows.Count;
                    int Index = Global.GetCurrentID(this.Component.EntityKey, conn);
                    Index = Index + 1;
                    if (Index == 0)
                    {
                        Index = 1;
                    }
                    int LastID = Global.GetNextID(this.Component.EntityKey, conn, InsertData.Rows.Count);                

                    string keyName = "";
                    if (this.Component.Type == ComponentType.core)
                    {
                        foreach (CompExtention.Attribute a in Component.Keys)
                        {
                            if (a.Name != "ClientID")
                            {
                                keyName = a.Name;
                            }
                        }
                    }
                    else if (this.Component.Type == ComponentType.attribute) {

                        foreach (CompExtention.Attribute a in Component.Keys)
                        {
                            if (a.Name != "ClientID")
                            {
                                if (this.Component.Attributes.Where(x => x.Name == a.Name && a.Type == AttributeType._componentlookup).ToList().Count == 0) {
                                    keyName = a.Name;
                                }                              
                            }
                        }
                    }       
                    
                    if (!InsertData.Columns.Contains(keyName))
                    {
                        // create a reader
                        DataTableReader dr = InsertData.CreateDataReader();
                        //clone original
                        DataTable clonedDT = InsertData.Clone();
                        //add identity column
                        clonedDT.Columns.Add(new DataColumn() { AutoIncrement = true, ColumnName = keyName, AutoIncrementStep = 1, DataType = typeof(Int32), AutoIncrementSeed = Index });
                        //load clone from reader, identity col will auto-populate with values
                        clonedDT.Load(dr);
                        InsertData = clonedDT;
                        clonedDT.Dispose();
                        ImportFields.Add(new ImportFieldMap() { ComponentID = this.Component.ID, DataField = keyName });
                    }
                    AddImportStatus( 0, 0, "Set Unique Keys to Dataset", Import.DataStatus.STARTED);
                    DataTable dt = InsertData.Copy();
                    if (dt.Columns.Contains("Index"))
                    {
                        dt.Columns.Remove("Index");
                    }
                    foreach (ImportFieldMap ifm in ImportFields)
                    {
                        var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                        if (ifm.FileFields != null)
                        {
                            if (dt.Columns.Contains(ifm.FileFields) && (!dt.Columns.Contains(att.Name)))
                            {
                                dt.Columns[ifm.FileFields].ColumnName = att.Name;
                            }
                        }
                    }
                    AddImportStatus( 0, 0, "Data perparation progress to import", Import.DataStatus.STARTED);
                    DataManager dm = new DataManager();
                    List<string> columnNames = new List<string>();
                    foreach (DataColumn dc in dt.Columns)
                    {

                        if (dc.DataType == typeof(DateTime))
                        {
                            dm.InsertInto(dc.ColumnName, FieldType._datetime);
                        }
                        else if (dc.DataType == typeof(Boolean))
                        {
                            dm.InsertInto(dc.ColumnName, FieldType._bool);
                        }
                        else
                        {
                            dm.InsertInto(dc.ColumnName, FieldType._string);
                        }
                        columnNames.Add(dc.ColumnName);
                    }
                    dm.tableName(this.Component.TableName);
                  
                    dm.Data(dt.GetCSVToArray());

                    dm.AfterProcessed += new EventHandler(AfterDataImported);
                    List<DataError> dataErrors = dm.ExecuteNonQuery(conn);
                    List<ImportError> er = new List<ImportError>();
                    int index = 0;
                    foreach (DataError dr in dataErrors)
                    {
                        if (InsertData.Rows.Count > index)
                        {
                            var datarow = InsertData.Rows[index];
                            var dataindex = Convert.ToInt32(datarow["index"]);
                            var log = Logs.Where(x => x.Index == dataindex).FirstOrDefault();
                            if (log != null)
                            {
                                if (dr.Type == DataError.ErrorType.ERROR)
                                {
                                    log.Message = dr.Message;
                                    log.Type = ErrorType.ERROR;
                                }
                                else
                                {
                                    log.Message = "Data Imported Successfully";
                                    log.Type = ErrorType.NOERROR;
                                }
                            }
                            else
                            {
                                Logs.Add(new ComponentDataLog() { Type = ErrorType.ERROR, Message = "Unknow record found" + ": " + dataindex });
                            }

                            index = index + 1;
                        }
                        else
                        {
                            Logs.Add(new ComponentDataLog() { Type = ErrorType.ERROR, Message = "Unknow record found" });
                        }
                    }
                    if (dataErrors.Count <= dt.Rows.Count)
                    {
                        if (Validation != null)
                        {
                            this.DataStatus.PercentageToComplete = this.DataStatus.PercentageToComplete + (this.DataStatus.PercentageToComplete * .12);
                            if (this.DataStatus.PercentageToComplete >= 100)
                            {
                                this.DataStatus.PercentageToComplete = 90;
                            }
                            AddImportStatus(this.DataStatus.DataProcessedCount, this.DataStatus.PercentageToComplete, "Processing support information", Import.DataStatus.STARTED);
                            // send error index
                            Validation.AfterSave(this, dt); // dt change to InsertData with error status based on that depended what to save
                        }
                    }
                   
                    TotalInserted = Logs.Where(x => x.Type == ErrorType.NOERROR && x.ImportingType == ComponentDataLog.ImportType.INSERT).ToList().Count();
                    AddImportStatus(this.DataStatus.DataProcessedCount, 100, "Processing support information", Import.DataStatus.COMPLETED);
                    //ErrorRecord = er.Count;
                }
                else
                {
                   // AddImportStatus(0, 0, "Import Completed", Import.DataStatus.STARTED);
                    var updatelog = this.Logs.Where(x => x.Type == ErrorType.NOERROR && x.IsExist == true).ToList();
                    List<int> updateindex = updatelog.Select(log => log.Index).ToList();

                    DataTable updateData = new DataTable();
                    List<string> columnNames = new List<string>();
                    int index = 0;
                    updateData = ValidDataSource.Select("Index In (" + string.Join(",", updateindex.ToArray()) + ")").CopyToDataTable();
                    //  updateData = ValidDataSource.AsEnumerable().Where(x => x["Index"].ToString().Contains(string.Join(",", updateindex.ToArray()))).AsDataView().ToTable(true);
                    if (updateData.Rows.Count > 0)
                    {
                        var dm = new DataManager();

                        DataTable dt = new DataTable();
                        dt = updateData.Copy();

                        if (dt.Columns.Contains("Index"))
                        {
                            dt.Columns.Remove("Index");
                        }

                        foreach (ImportFieldMap ifm in ImportFields)
                        {
                            var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                            if (ifm.FileFields != null)
                            {
                                if (dt.Columns.Contains(ifm.FileFields) && (! dt.Columns.Contains(att.Name)))
                                {
                                    dt.Columns[ifm.FileFields].ColumnName = att.Name;
                                }
                            }
                        }
                        foreach (CompExtention.Attribute key in this.Component.Keys)
                        {
                            if (key.Name.ToLower() != "clientid" && key.ComponentLookup == "")
                            {
                                dt.Columns.Add(new DataColumn(key.Name, typeof(int)));
                                int i = 0;
                                foreach (ComponentDataLog log in updatelog)
                                {
                                    dt.Rows[i][key.Name] = log.ExistKey;
                                    i = i + 1;
                                }
                            }
                        }


                        dm = new DataManager();
                        dm.tableName(this.Component.TableName);
                        foreach (DataColumn dc in dt.Columns)
                        {

                            string dval = "";
                            if (dc.DataType == typeof(DateTime))
                            {
                                //dval = Convert.ToDateTime(dr[dc.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss");
                                dm.UpdateInto(dc.ColumnName, FieldType._datetime, "");
                            }
                            else if (dc.DataType == typeof(Boolean))
                            {
                                dm.UpdateInto(dc.ColumnName, FieldType._bool, "");
                            }
                            else
                            {
                                dm.UpdateInto(dc.ColumnName, FieldType._string, "");
                            }

                            //}
                            columnNames.Add(dc.ColumnName);
                        }
                        foreach (CompExtention.Attribute key in this.Component.Keys)
                        {
                            dm.Where(this.Component.TableName, key.Name, "=", "");
                        }
                        if (dm.GetSchema().Wheres.Count == 0)
                        {
                            Errors.Add(new ImportError()
                            {
                                Message = "No Key field set while update record",
                                Type = ErrorType.ERROR,
                            });
                        }
                        else
                        {
                            if (dm.GetSchema().Wheres.Where(x => x.FieldName.ToLower() == "clientid").FirstOrDefault() == null)
                            {
                                dm.Where(this.Component.TableName, "ClientID", "=", ClientID.ToString());
                            }
                            //     bdata.Add(dm.GetSchema());
                        }
                        dm.AfterProcessed += new EventHandler(AfterDataImported);
                        TotalUpdated = 0;
                        //}
                        var path = logPath + "/" + Guid.NewGuid() + ".csv";
                        File.WriteAllText(path, "");
                        dt.ToCSV(path);
                        var updateerr = dm.ExecuteNonQuery(path, conn);
                        index = 0;
                        foreach (DataError drr in updateerr)
                        {
                            if (updateData.Rows.Count > index)
                            {
                                var datarow = updateData.Rows[index];
                                int dataindex = 0;
                                var ulog = Logs.Where(x => x.Index == dataindex).FirstOrDefault();
                                if (ulog != null)
                                {
                                    if (drr.Type == DataError.ErrorType.ERROR)
                                    {
                                        ulog.Message = drr.Message;
                                        ulog.Type = ErrorType.ERROR;
                                        ulog.ImportingType = ComponentDataLog.ImportType.UPDATE;

                                    }
                                    else
                                    {
                                        ulog.Message = "Data updated successfully";
                                        ulog.Type = ErrorType.NOERROR;
                                        ulog.ImportingType = ComponentDataLog.ImportType.UPDATE;
                                    }
                                }
                                index = index + 1;
                            }
                            //if (drr.Type == DataError.ErrorType.ERROR)
                            //{
                            //    Errors.Add(new ImportError()
                            //    {
                            //        Message = drr.Message,
                            //        Type = ErrorType.ERROR,
                            //    });
                            //}
                        }
                        TotalUpdated = updatelog.Where(x => x.Type == ErrorType.NOERROR).ToList().Count();
                        if (Validation != null)
                        {
                            try
                            {
                                this.DataStatus.PercentageToComplete = this.DataStatus.PercentageToComplete + (this.DataStatus.PercentageToComplete * .12);
                                if (this.DataStatus.PercentageToComplete > 100) {
                                    this.DataStatus.PercentageToComplete = 90;
                                }
                                AddImportStatus(this.DataStatus.DataProcessedCount, this.DataStatus.PercentageToComplete, "Processing support information", Import.DataStatus.STARTED);
                                Validation.AfterUpdate(this, dt);
                            }
                            catch (Exception ex)
                            {

                            }
                        }                     

                        ValidDataSource.Rows.Clear();
                        ValidDataSource.Merge(InsertData);
                        ValidDataSource.Merge(updateData);
                    }
                }
                AddImportStatus(this.DataStatus.DataProcessedCount, 100, "Import Completed", Import.DataStatus.COMPLETED);
                ErrorRecord = Logs.Where(x => x.Type == ErrorType.ERROR).ToList().Count();
            }
        }

        private void AfterDataImported(object sender, EventArgs e) {
             LoadImportStatus(LogPath, Context);
            DataStatus.DataProcessedCount = DataStatus.DataProcessedCount + 1;
            double a = Convert.ToDouble((DataStatus.DataProcessedCount * 100) / this.ValidDataSource.Rows.Count);
              a = (.75 * a);
            AddImportStatus( DataStatus.DataProcessedCount, a, "Data Importing", Import.DataStatus.STARTED);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetAutoIndex()
        {
            return AutoIndex = AutoIndex + 1;
        }

        private void BuildLog()
        {
            int count = ValidDataSource.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                Logs.Add(new ComponentDataLog() { Index = i + 1 });
            }
        }
        private void SetIndexing()
        {
            if (!ValidDataSource.Columns.Contains("index"))
            {
                DataTableReader dr = ValidDataSource.CreateDataReader();
                //clone original
                DataTable clonedDT = ValidDataSource.Clone();
                //add identity column
                clonedDT.Columns.Add(new DataColumn() { AutoIncrement = true, ColumnName = "Index", AutoIncrementStep = 1, DataType = typeof(Int32), AutoIncrementSeed = 1 });
                clonedDT.Columns.Add(new DataColumn() { ColumnName = "ClientID", DataType = typeof(Int32), DefaultValue = this.ClientID });
                //load clone from reader, identity col will auto-populate with values
                clonedDT.Load(dr);
                ValidDataSource = clonedDT;
                clonedDT.Dispose();
            }

        }
        /// <summary>
        /// check any duplicate record exist in the source data
        /// </summary>
        /// <returns></returns>
        private bool IsDuplidateInSource()
        {
            List<string> Key = new List<string>();
            string unstring = "";
            int indx = 0;
            foreach (ImportFieldMap ifm in ImportFields)
            {
                if (ifm.IsKey == true)
                {
                    Key.Add(ifm.FileFields);
                    if (indx == 0)
                    {
                        unstring = ifm.FileFields;
                    }
                    else
                    {
                        unstring = unstring + "+'#'+" + "IIF((ISNULL(" + ifm.FileFields + ",'0')='0'),'0'," + ifm.FileFields + ")";
                    }
                    indx = indx + 1;
                }
            }
            if (unstring.StartsWith("#"))
            {
                unstring = unstring.Substring(1);
            }
            if (!ValidDataSource.Columns.Contains("unikey"))
            {
                ValidDataSource.Columns.Add(new DataColumn("unikey", typeof(string), unstring) { DefaultValue = "" });
            }
            var duplicates = ValidDataSource.AsEnumerable().GroupBy(r => r.Field<string>("unikey")).Where(gr => gr.Count() > 1).ToList();
            foreach (IGrouping<string,DataRow> dr in duplicates)
            {
                var drDup = ValidDataSource.Select("unikey='" + dr.Key.Trim().Replace("'", "''") + "'");
                foreach (DataRow dr1 in drDup) {
                    var log = Logs.Where(x => x.Index ==  Convert.ToInt32( dr1["Index"])).FirstOrDefault();
                    log.Message = "Duplicate row.Same key value exist in the data (" + dr.Key.Replace("#",",") + ")" ;// + ":" + dr["unikey"].ToString().Replace("#", ",");
                    log.Type = ErrorType.ERROR;
                }
           //  DataRow ddr=   (System.Data.DataRow)(((System.Linq.Lookup<string, System.Data.DataRow>.Grouping)(dr)));
           //     int index = Convert.ToInt32(dr.Key["Index"]);
             

            }
            if (ValidDataSource.Columns.Contains("unikey"))
            {
                ValidDataSource.Columns.Remove("unikey");
            }
            if (duplicates.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        ///  check mapping field exist in the current component
        /// </summary>
        /// <returns></returns>
        private ImportError IsComponentMappingFieldValid()
        {
            string errorField = "";
            foreach (ImportFieldMap ifm in ImportFields)
            {
                var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                if (att == null)
                {
                    errorField = errorField + "," + ifm.FileFields;
                }
            }
            if (errorField != "")
            {
                return (new ImportError()
                {
                    Message = "Following mapping field not exist in the '" + this.Component.Name + " component', Fields are " + errorField + ""
                    ,
                    Type = ErrorType.ERROR
                });
            }
            else
            {
                return (new ImportError { Type = ErrorType.NOERROR });
            }
        }
        /// <summary>
        /// generate auto code based on field mapping information
        /// </summary>
        private void CreateAutoCodeField()
        {
            var autoField = ImportFields.Where(x => x.IsAutoCode == true).FirstOrDefault();
            if (autoField != null)
            {
                var attauto = this.Component.Attributes.Where(x => x.ID == autoField.DataField).FirstOrDefault();
                if (attauto != null)
                {
                    autoField.FileFields = attauto.Name;
                    if (!this.ValidDataSource.Columns.Contains(attauto.Name))
                    {
                        this.ValidDataSource.Columns.Add(new DataColumn(attauto.Name, typeof(string)) { DefaultValue = "" });
                    }
                }
            }
        }
        /// <summary>
        /// get mapping key fields and it data to check unique value in the source data
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetMappingKeyWithValues(DataRow dr)
        {
            var index = Convert.ToInt32(dr["index"]);
            Dictionary<string, string> sk = new Dictionary<string, string>();
            var items = ImportFields.Where(x => x.IsKey == true).ToList();
            foreach (ImportFieldMap ifm in items)
            {
                var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                if (att != null)
                {
                    if (att.Type == AttributeType._componentlookup)
                    {
                        if (ifm.IsAutoCode == false)
                        {

                            var val = GetComponentLookup(att.ComponentLookupDisplayField, dr[ifm.FileFields].ToString().Trim());
                            sk.Add(att.Name, val);
                            if (this.Component.Type == ComponentType.attribute)
                            {
                                if (val == "")
                                {
                                    var log = this.Logs.Where(x => x.Index == index).FirstOrDefault();
                                    log.Message = dr[ifm.FileFields].ToString() + " is invalid data in the column of " + ifm.FileFields;
                                    log.Type = ErrorType.ERROR;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ifm.IsAutoCode == false)
                        {
                            sk.Add(att.Name, dr[ifm.FileFields].ToString().Trim());
                        }
                    }
                }
            }
            return sk;
        }
        /// <summary>
        /// Set key and vlaue from the source data if it is exist based on this condition record is going to update or insert 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="enableUpdate"></param>
        private void SetKeyValueIfExist(DataRow dr, bool enableUpdate)
        {
            var index = Convert.ToInt32(dr["index"]);

            string key = IsKeyValid(GetMappingKeyWithValues(dr));
            if (key != "-1")
            {
                if (enableUpdate == true)
                {
                    var log = this.Logs.Where(x => x.Index == index).FirstOrDefault();
                    log.IsExist = true;
                    log.ExistKey = key;
                    log.ImportingType = ComponentDataLog.ImportType.UPDATE;

                }
            }
        }
        /// <summary>
        /// Mapping field exist in the component or not (only link component)
        /// </summary>
        /// <returns></returns>
        private ImportError IsLinkMappingFieldValid()
        {
            string errorField = "";
            foreach (ImportFieldMap ifm in ImportFields)
            {
                var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                var isExist = false;
                foreach (ComponentData cd in this.ParentComponentData)
                {
                    if (ifm.IsAutoCode == false)
                    {
                        if (cd.ValidDataSource.Columns.Contains(ifm.FileFields))
                        {
                            isExist = true;
                        }
                    }
                }
                if (att == null && isExist == false)
                {
                    errorField = errorField + "," + ifm.FileFields;
                }

            }
            if (errorField != "")
            {
                return (new ImportError()
                {
                    Message = "Following mapping field not exist in the '" + this.Component.Name + " component', Fields are " + errorField + ""
                    ,
                    Type = ErrorType.ERROR
                });
            }
            else
            {
                return (new ImportError { Type = ErrorType.NOERROR });
            }
        }

        public DataTable GetValidDataOnly(string logPath, string context  )
        {
            DataTableReader drr = SourceData.CreateDataReader();
            //clone original
            DataTable clonedDT = SourceData.Clone();
            //add identity column
            clonedDT.Columns.Add(new DataColumn() { AutoIncrement = true, ColumnName = "Index", AutoIncrementStep = 1, DataType = typeof(Int32), AutoIncrementSeed = 1 });
            clonedDT.Columns.Add(new DataColumn() { ColumnName = "Message", DataType = typeof(string) });
            clonedDT.Columns.Add(new DataColumn() { ColumnName = "Type", DataType = typeof(int) });
            //load clone from reader, identity col will auto-populate with values
            clonedDT.Load(drr);
            LoadLog(logPath, context);
            var errLog = this.Logs.Where(x => x.Type == ErrorType.NOERROR).ToList();
            List<int> indexList = errLog.Select(log => log.Index).ToList();

            clonedDT = clonedDT.Select("Index In (" + string.Join(",", indexList.ToArray()) + ")").CopyToDataTable();

            foreach (DataRow dr in clonedDT.Rows)
            {
                int indx = Convert.ToInt32(dr["index"]);
                var lg = this.Logs.Where(x => x.Index == indx).FirstOrDefault();
                var status = lg.Type;
                var itype = lg.ImportingType;                                      
                    dr["Message"] = lg.Message;
                    dr["Type"] = lg.Type;               
            }
            clonedDT.Columns.Remove("Index");
            return clonedDT;
        }

        public DataTable GetErrorList(string logPath, string context, bool includeState = false) {
            DataTableReader drr = SourceData.CreateDataReader();
            //clone original
            DataTable clonedDT = SourceData.Clone();
            //add identity column
            clonedDT.Columns.Add(new DataColumn() { AutoIncrement = true, ColumnName = "Index", AutoIncrementStep = 1, DataType = typeof(Int32), AutoIncrementSeed = 1 });
            clonedDT.Columns.Add(new DataColumn() { ColumnName = "Message", DataType = typeof(string) });
            clonedDT.Columns.Add(new DataColumn() { ColumnName = "Type", DataType = typeof(int) });
            //load clone from reader, identity col will auto-populate with values
            clonedDT.Load(drr);
            LoadLog(logPath, context);
            var errLog = this.Logs.Where(x => x.Type == ErrorType.ERROR).ToList();
            List<int> indexList = errLog.Select(log => log.Index).ToList();

            clonedDT = clonedDT.Select("Index In (" + string.Join(",", indexList.ToArray()) + ")").CopyToDataTable();

            foreach (DataRow dr in clonedDT.Rows)
            {
                int indx = Convert.ToInt32(dr["index"]);
                var lg = this.Logs.Where(x => x.Index == indx).FirstOrDefault();
                var status = lg.Type;
                var itype = lg.ImportingType;
                var stext = "";
                var itypetext = "";
                if (includeState)
                {
                    if (status == ErrorType.ERROR)
                    {
                        stext = "Error";
                    }
                    else
                    {
                        stext = "Valid";
                    }
                    if (itype == ComponentDataLog.ImportType.INSERT)
                    {
                        itypetext = "Create";
                    }
                    else
                    {
                        itypetext = "Update";
                    }

                    dr["Message"] = "Status : " + stext + " ; Message : " + lg.Message + "; Import Type:" + itypetext;
                    dr["Type"] = lg.Type;
                }
                else
                {
                    dr["Message"] = lg.Message;
                    dr["Type"] = lg.Type;
                }
            }
            clonedDT.Columns.Remove("Index");
            return clonedDT;
        }
        public DataTable GetProcessedData(string logPath, string context, bool includeState = false)
        {
            DataTableReader drr = SourceData.CreateDataReader();
            //clone original
            DataTable clonedDT = SourceData.Clone();
            //add identity column
            clonedDT.Columns.Add(new DataColumn() { AutoIncrement = true, ColumnName = "Index", AutoIncrementStep = 1, DataType = typeof(Int32), AutoIncrementSeed = 1 });
            clonedDT.Columns.Add(new DataColumn() { ColumnName = "Message", DataType = typeof(string) });
            clonedDT.Columns.Add(new DataColumn() { ColumnName = "Type", DataType = typeof(int) });
            //load clone from reader, identity col will auto-populate with values
            clonedDT.Load(drr);
            LoadLog(logPath, context);
            foreach (DataRow dr in clonedDT.Rows)
            {
                int indx = Convert.ToInt32(dr["index"]);
                var lg = this.Logs.Where(x => x.Index == indx).FirstOrDefault();
                var status = lg.Type;
                var itype = lg.ImportingType;
                var stext = "";
                var itypetext = "";
                if (includeState)
                {
                    if (status == ErrorType.ERROR)
                    {
                        stext = "Error";
                    }
                    else
                    {
                        stext = "Valid";
                    }
                    if (itype == ComponentDataLog.ImportType.INSERT)
                    {
                        itypetext = "Create";
                    }
                    else
                    {
                        itypetext = "Update";
                    }

                    dr["Message"] = "Status : " + stext + " ; Message : " + lg.Message + "; Import Type:" + itypetext;
                    dr["Type"] = lg.Type;
                }
                else
                {
                    dr["Message"] = lg.Message;
                    dr["Type"] = lg.Type;
                }
            }
            ProcessedData = clonedDT;
            return clonedDT;
        }

        protected internal void ValidatePivotData(bool enableUpdate, string conn, string logPath, string context, List<TemplateField> templateFields, List<CompExtention.Attribute> attributes) {
            var PivotData = ReshapeSource(this.SourceData, templateFields, attributes, conn);

            var pcol = templateFields.Where(x => x.IsPivot == true).FirstOrDefault();
            var col = templateFields.Where(x => x.IsColumn == true).FirstOrDefault();
            var rows = templateFields.Where(x => x.IsRow == true).ToList();

         
            var pcatt = attributes.Where(x => x.ID == pcol.ID).FirstOrDefault();
            var catt = attributes.Where(x => x.ID == col.ID).FirstOrDefault();


            AutoIndex = TargetData.Rows.Count;
            ValidDataSource = this.SourceData.Copy();

            //
            BuildLog();
            // Set Unique indexing in the Source to be validated
            SetIndexing();

            List<DataTable> dt = new List<DataTable>();
            LoadLookupComponent(conn);
            foreach (DataRow dr in ValidDataSource.Rows) { 
            
            }
         
            
        }
        /// <summary>
        /// Method to validate import data
        /// </summary>
        /// <returns></returns>
        /// 
        protected internal void ValidateData(bool enableUpdate, string conn, string logPath, string context)
        {
            AutoIndex = TargetData.Rows.Count;
            ValidDataSource = this.SourceData.Copy();
            List<DataTable> dt = new List<DataTable>();
            LoadLookupComponent(conn);

            if (this.Component.Type == ComponentType.link)
            {
                BuildLog();
                // Set Unique indexing in the Source to be validated
                SetIndexing();
                // validate mapping field is valid or not even validation
                Errors.Add(IsLinkMappingFieldValid());
                Errors = Errors.Where(x => x.Type == ErrorType.ERROR).ToList();
                if (Errors.Where(x => x.Type == ErrorType.ERROR).FirstOrDefault() == null)
                {
                    //BuildValidationSource();
                    foreach (DataRow dr in this.ValidDataSource.Rows)
                    {
                        try
                        {
                            ValidateLinkByRow(enableUpdate, dr);
                        }
                        catch (Exception ex)
                        {
                            var log = Logs.Where(x => x.Index == Convert.ToInt32(dr["Index"])).FirstOrDefault();
                            log.Message = ex.Message;
                            log.Type = ErrorType.ERROR;
                        }
                    }
                    WriteLog(logPath, Newtonsoft.Json.JsonConvert.SerializeObject(this.Logs), context);
                }
            }
            else if (this.Component.Type == ComponentType.core || this.Component.Type == ComponentType.attribute)
            {
                try
                {
                    //
                    BuildLog();
                    // Set Unique indexing in the Source to be validated
                    SetIndexing();
                    // validate mapping field is valid or not even validation
                    Errors.Add(IsComponentMappingFieldValid());
                    Errors = Errors.Where(x => x.Type == ErrorType.ERROR).ToList();
                    // after validation if any error found dont further proceed
                    if (Errors.Where(x => x.Type == ErrorType.ERROR).FirstOrDefault() == null)
                    {
                        // Check Unique record exist in the source to be validated
                        if (IsDuplidateInSource() == false)
                        {
                            // Create auto field if exist in the import fields
                            CreateAutoCodeField();
                            foreach (DataRow dr in this.ValidDataSource.Rows)
                            {
                                try
                                {
                                    SetKeyValueIfExist(dr, enableUpdate);
                                    ValidateCoreByRow(enableUpdate, dr);
                                }
                                catch (Exception ex)
                                {
                                    var log = Logs.Where(x => x.Index == Convert.ToInt32(dr["Index"])).FirstOrDefault();
                                    log.Message = ex.Message;
                                    log.Type = ErrorType.ERROR;
                                }
                            }
                            WriteLog(logPath, Newtonsoft.Json.JsonConvert.SerializeObject(this.Logs), context);
                            //Errors.Add(new ImportError() { Type = ErrorType.NOERROR });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Errors.Add(new ImportError() { Message = ex.Message, Type = ErrorType.ERROR });
                }
                // }             
            }
            else if (this.Component.Type == ComponentType.pseudocore) {
                BuildLog();
                // Set Unique indexing in the Source to be validated
                SetIndexing();               
                Errors = Errors.Where(x => x.Type == ErrorType.ERROR).ToList();
                if (Errors.Where(x => x.Type == ErrorType.ERROR).FirstOrDefault() == null)
                {
                    if (IsDuplidateInSource() == false) {
                        foreach (DataRow dr in this.ValidDataSource.Rows)
                        {
                            try
                            {                                
                                if (IsKeyValid(GetMappingKeyWithValues(dr)) !="-1") {
                                    var index = Convert.ToInt32(dr["index"]);
                                    var log = Logs.Where(x => x.Index == index).FirstOrDefault();
                                    log.Message = "This record aleady exist";
                                    log.Type = ErrorType.ERROR;
                                    continue;
                                }                              
                                ValidatePseudoCoreByRow(enableUpdate, dr);
                            }
                            catch (Exception ex)
                            {
                                var log = Logs.Where(x => x.Index == Convert.ToInt32(dr["Index"])).FirstOrDefault();
                                log.Message = ex.Message;
                                log.Type = ErrorType.ERROR;
                            }
                        }
                    }                      
                      
                    WriteLog(logPath, Newtonsoft.Json.JsonConvert.SerializeObject(this.Logs), context);
                }
            }
        }
        private string GetParentKey(string compID) {
            
         var a =   this.ParentComponentData.Where(x => x.Component.ID == compID).FirstOrDefault();
            foreach (CompExtention.Attribute atr in a.Component.Keys)
            {
                if (atr.Name.ToLower() != "clientid")
                {
                    return atr.Name;
                }
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifm"></param>
        /// <param name="compID"></param>
        /// <param name="drSource"></param>
        private void SetKeyByComponent(DataRow drSource, bool isPush = false)
        {
            StringBuilder sb = new StringBuilder();
            int dataindex = Convert.ToInt32(drSource["index"]);
            var log = Logs.Where(x => x.Index == dataindex).FirstOrDefault();
            int index = 0;
            List<CompExtention.Attribute> attkey = new List<CompExtention.Attribute>();
            List<ImportFieldMap> keys = new List<ImportFieldMap>();
            Dictionary<string, string> kv = new Dictionary<string, string>();

            foreach (ComponentData comp in this.ParentComponentData)
            {
                var keyFieldsOnly = ImportFields.Where(x => x.IsKey == true && x.ComponentID == comp.Component.ID).ToList();
                foreach (ImportFieldMap imf in keyFieldsOnly)
                {
                    imf.Value = drSource[imf.FileFields].ToString().Trim();
                    keys.Add(imf);
                    if (comp != null)
                    {
                        var datafield = comp.Component.Attributes.Where(x => x.ID == imf.DataField).FirstOrDefault();
                        var fval = "";
                        if (datafield.Type == AttributeType._componentlookup)
                        {
                            var clk = comp.ComponentLookups.Where(x => x.Attribute.ID == datafield.ComponentLookupDisplayField).FirstOrDefault();
                            var dt = (DataTable)clk.LookupData;
                            var keyName = "";
                            foreach (CompExtention.Attribute atr in clk.LookupComponent.Keys)
                            {
                                if (atr.Name.ToLower() != "clientid")
                                {
                                    keyName = atr.Name;
                                }
                            }
                            if (dt.Rows.Count > 0)
                            {
                                var drcomplk = dt.Select(clk.Attribute.Name + "='" + imf.Value.Replace("'", "''") + "'");
                                if (drcomplk.Count() > 0)
                                {
                                    fval = drcomplk[0][keyName].ToString();
                                }
                            }
                        }
                        else
                        {
                            fval = imf.Value;
                        }
                        if (comp.TargetData.Columns.Contains(datafield.Name))
                        {
                            if (index == 0)
                            {
                                if (datafield.Type == AttributeType._decimal || datafield.Type == AttributeType._number
                                    || datafield.Type == AttributeType._lookup || datafield.Type == AttributeType._currency || datafield.Type == AttributeType._componentlookup)
                                {
                                    if ((IsNumeric(fval) || IsDecimal(fval)) && fval != "")
                                    {
                                        sb.Append(datafield.Name + "=" + fval);
                                    }
                                    else
                                    {
                                        if (fval == "")
                                        {
                                            // no need to include if the value is null
                                            //  sb.Append("ISNULL(" + datafield.Name + ",0)=0");
                                            sb.Append("1=1");
                                        }
                                        else
                                        {
                                            sb.Append(datafield.Name + "='" + fval.Replace("'", "''") + "'");
                                        }
                                    }
                                }
                                else
                                {
                                    if (fval != "")
                                    {
                                        sb.Append(datafield.Name + "='" + fval.Replace("'", "''") + "'");
                                    }

                                }
                            }
                            else
                            {

                                if (datafield.Type == AttributeType._decimal || datafield.Type == AttributeType._number
                                    || datafield.Type == AttributeType._lookup || datafield.Type == AttributeType._currency || datafield.Type == AttributeType._componentlookup)
                                {
                                    if ((IsNumeric(fval) || IsDecimal(fval)) && fval != "")
                                    {
                                        // sb.Append(datafield.Name + "=" + fval);
                                        sb.Append(" AND " + datafield.Name + "=" + fval);
                                    }
                                    else
                                    {
                                        if (fval == "")
                                        {
                                            sb.Append(" AND 1=1 ");
                                            // no need to include if the value is null
                                            //   sb.Append(" AND ISNULL(" + datafield.Name + ",0)=0");
                                        }
                                        else
                                        {
                                            sb.Append(" AND " + datafield.Name + "='" + fval.Replace("'", "''") + "'");
                                        }

                                        // sb.Append(" AND " + datafield.Name + "='" + fval.Replace("'", "''") + "'");
                                    }
                                }
                                else
                                {
                                    if (fval != "")
                                    {
                                        sb.Append(" AND " + datafield.Name + "='" + fval.Replace("'", "''") + "'");
                                    }
                                }
                            }
                            index = index + 1;
                        }
                        attkey.Add(datafield);
                       
                    }

                    
                }

                string s = GetParentKey(comp.Component.ID);
                string ss = sb.ToString();
                comp.TargetData.DefaultView.RowFilter = ss;
                var drr = comp.TargetData.Select(ss);
                if (drr.Count() > 0)
                {
                    kv.Add(s, Convert.ToString(drr[0][s]));
                }
                else
                {
                    kv.Add(s, "-1");
                }
                sb = new StringBuilder();
                index = 0;
            }

            StringBuilder linkkey= new StringBuilder();
            int ki = 0;
            foreach (KeyValuePair<string,string> k in kv) {
                if (ki == 0)
                {
                    linkkey.Append(k.Key + " = " + k.Value);
                }
                else {
                    linkkey.Append(" AND " + k.Key + " = " + k.Value);
                }
                ki = ki + 1;
            }
        
            DataTable dtRow = new DataTable();
            if (linkkey.ToString() != "")
            {
                this.TargetData.DefaultView.RowFilter = linkkey.ToString();
                dtRow = this.TargetData.DefaultView.ToTable(true);
                this.TargetData.DefaultView.RowFilter = "";
            }           
            foreach (KeyValuePair<string,string> att in kv)
            {
                if (!this.ValidDataSource.Columns.Contains(att.Key))
                {
                    this.ValidDataSource.Columns.Add(new DataColumn(att.Key, typeof(int)) { DefaultValue = -1 });
                }
                //if (dtRow.Rows.Count > 0)
                //{
                    drSource[att.Key] = att.Value;
               // }
               // else { 
                
               // }               
            }
           
            if (dtRow.Rows.Count > 0)
            {
              //  log.ExistKey = dtRow.Rows[0][key.Name].ToString();
                if (isPush == false)
                {
                    log.IsExist = true;
                    log.ImportingType = ComponentDataLog.ImportType.UPDATE;
                   // drSource[key.Name] = dtRow.Rows[0][key.Name].ToString();
                }
            }
            else
            {
                log.ExistKey = "-1";
                log.IsExist = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckValid()
        {
            if (ValidDataSource == null)
            {
                return false;
            }
            if (this.Logs.Where(x => x.Type == ErrorType.ERROR).ToList().Count > 0 || this.Errors.Where(x => x.Type == ErrorType.ERROR).ToList().Count > 0)
            {
                return false;
            }
            else
                return true;

        }
        /// <summary>
        /// Get parent table keys and set in the link table
        /// </summary>
        /// <param name="dr"></param>
        protected internal void SetLinkKeys(DataRow dr, bool isPush = false)
        {
            SetKeyByComponent(dr, isPush);
        }

        private void ValidatePseudoCoreByRow(bool enableUpdate, DataRow dr) {
            foreach (ImportFieldMap ifm in ImportFields)
            {
                //if (ifm.IsKey != true)
                //{
                    var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                    ValidDataType(att, ifm, enableUpdate, dr);
                //}
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enableUpdate"></param>
        /// <param name="dr"></param>
        private void ValidateLinkByRow(bool enableUpdate, DataRow dr)
        {
            SetLinkKeys(dr);
            foreach (ImportFieldMap ifm in ImportFields)
            {
                if (ifm.IsKey != true)
                {
                    var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                    ValidDataType(att, ifm, enableUpdate, dr);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="enableUpdate"></param>
        /// <param name="dr"></param>
        private void ValidateCoreByRow(bool enableUpdate, DataRow dr)
        {
            foreach (ImportFieldMap ifm in ImportFields)
            {
                var att = this.Component.Attributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                ValidDataType(att, ifm, enableUpdate, dr);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="att"></param>
        /// <param name="ifm"></param>
        /// <param name="enableUpdate"></param>
        /// <param name="dr"></param>
        /// <param name="sk"></param>
        private void ValidDataType(CompExtention.Attribute att, ImportFieldMap ifm, bool enableUpdate, DataRow dr)
        {
            var index = Convert.ToInt32(dr["index"]);
            var log = Logs.Where(x => x.Index == index).FirstOrDefault();
            if (att != null)
            {
                if (enableUpdate == false)
                {
                    if (att.IsUnique)
                    {
                        if (ifm.IsAutoCode == false)
                        {
                            if (!IsUnique(att.Name, dr[ifm.FileFields].ToString().Trim()))
                            {
                                log.Message = " duplicate value '" + dr[ifm.FileFields].ToString().Trim() + "' exist in the result";
                                log.Type = 0;
                            }
                        }
                    }
                }
                if (ifm.IsAutoCode == true)
                {
                    var s = GetKeyValue(GetMappingKeyWithValues(dr), att.Name);
                    if (s == "")
                    {
                        dr[ifm.FileFields] = ifm.AutoFormat + "" + GetAutoIndex();
                    }
                    else
                    {
                        dr[ifm.FileFields] = s;
                    }
                }
                if (att.Type == AttributeType._number)
                {
                    if (att.IsRequired == true)
                    {
                        if (IsEmpty(dr[ifm.FileFields]))
                        {
                            log.Message = ifm.FileFields + " is required field";
                            log.Type = 0;
                        }
                        else if (!IsNumeric(dr[ifm.FileFields].ToString()))
                        {
                            log.Message = ifm.FileFields + " invalid data";
                            log.Type = 0;

                            // error here;
                        }
                    }
                    else
                    {
                        if ((!IsEmpty(dr[ifm.FileFields])))
                        {
                            if (!IsNumeric(dr[ifm.FileFields].ToString()))
                            {
                                log.Message = ifm.FileFields + " invalid data";
                                log.Type = 0;
                            }
                        }
                    }
                }
                else if (att.Type == AttributeType._lookup)
                {
                    if (att.IsRequired == true)
                    {
                        if (IsEmpty(dr[ifm.FileFields]))
                        {
                            log.Message = ifm.FileFields + " is required field";
                            log.Type = 0;
                        }
                        else
                        {
                            string lktext = dr[ifm.FileFields].ToString();
                            int lk = GetLookUp(dr[ifm.FileFields].ToString(), att.LookupInstanceID);
                            if (lk == -1)
                            {
                                if (IsCoreLookup(Convert.ToInt32(att.LookupInstanceID)))
                                {
                                    log.Message = lktext + " not exist in the lookup item.Only valid lookup item accepted.";
                                    log.Type = 0;
                                }
                                else
                                {
                                    lk = AddLookup(lktext, att.LookupInstanceID);
                                }
                            }

                            if (lk != -1)
                            {
                                dr[ifm.FileFields] = lk;
                            }
                            else
                            {
                                dr[ifm.FileFields] = lktext;
                            }
                        }
                    }
                    else
                    {
                        if ((!IsEmpty(dr[ifm.FileFields])))
                        {
                            string lktext = dr[ifm.FileFields].ToString();
                            int lk = GetLookUp(dr[ifm.FileFields].ToString(), att.LookupInstanceID);
                            if (lk == -1)
                            {
                                if (IsCoreLookup(Convert.ToInt32(att.LookupInstanceID)))
                                {
                                    log.Message = lktext + " not exist in the lookup item.Only valid lookup item accepted.";
                                    log.Type = 0;
                                    //dr["state"] = "error";
                                    //dr["message"] = lktext + " not exist in the lookup item.Only valid lookup item accepted.";
                                    //dr["type"] = 0;
                                }
                                else
                                {
                                    lk = AddLookup(lktext, att.LookupInstanceID);
                                }
                            }
                            if (lk != -1)
                            {
                                dr[ifm.FileFields] = lk;
                            }
                            else
                            {
                                dr[ifm.FileFields] = lktext;
                            }
                        }
                    }
                }
                else if (att.Type == AttributeType._decimal || att.Type == AttributeType._currency)
                {
                    if (att.IsRequired == true)
                    {
                        if (IsEmpty(dr[ifm.FileFields]))
                        {
                            log.Message = ifm.FileFields + " is required field.";
                            log.Type = 0;
                            //dr["state"] = "error";
                            //dr["message"] = ifm.FileFields + " is required field.";
                            //dr["type"] = 0;
                        }
                        else if (!IsDecimal(dr[ifm.FileFields].ToString()))
                        {
                            log.Message = ifm.FileFields + " invalid data";
                            log.Type = 0;
                        }
                    }
                    else
                    {
                        if ((!IsEmpty(dr[ifm.FileFields])))
                        {
                            if (!IsDecimal(dr[ifm.FileFields].ToString()))
                            {
                                log.Message = ifm.FileFields + " invalid data";
                                log.Type = 0;
                                //dr["state"] = "error";
                                //dr["message"] = att.DisplayName + " invalid data";
                                //dr["type"] = 0;
                            }
                        }
                    }
                }
                else if (att.Type == AttributeType._componentlookup)
                {

                    if ((!IsEmpty(dr[ifm.FileFields])))
                    {
                        var s = GetComponentLookup(att.ComponentLookupDisplayField, dr[ifm.FileFields].ToString());
                        if (s == "")
                        {
                            if (att.IsRequired == true)
                            {
                                log.Message = ifm.FileFields + " is required field. " + dr[ifm.FileFields].ToString() + " is not exist.";
                                log.Type = 0;                                
                            }                          
                        }
                        else
                        {
                            dr[ifm.FileFields] = s;
                        }
                    }
                    else
                    {
                        if (att.IsRequired == true)
                        {
                            log.Message = ifm.FileFields + " is required field.";
                            log.Type = 0;
                            //dr["state"] = "error";
                            //dr["message"] = ifm.FileFields + " is required field.";
                            //dr["type"] = 0;
                        }
                    }


                }
                else if (att.Type == AttributeType._date || att.Type == AttributeType._datetime || att.Type == AttributeType._time)
                {
                    if (att.IsRequired == true)
                    {
                        if (IsEmpty(dr[ifm.FileFields]))
                        {
                            log.Message = ifm.FileFields + " is required field.";
                            log.Type = 0;
                            //dr["state"] = "error";
                            //dr["message"] = ifm.FileFields + " is required field.";
                            //dr["type"] = 0;
                        }
                        else if (!IsDate(dr[ifm.FileFields].ToString()))
                        {
                            log.Message = ifm.FileFields + " invalid date.";
                            log.Type = 0;

                            //dr["state"] = "error";
                            //dr["message"] = ifm.FileFields + " invalid date";
                            //dr["type"] = 0;
                        }
                        else {
                            try
                            {
                                dr[ifm.FileFields] = DateTime.Parse(dr[ifm.FileFields].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            catch (Exception e) { 
                            
                            }
                        }
                    }
                    else
                    {
                        if (!IsEmpty(dr[ifm.FileFields]))
                        {
                            if (!IsDate(dr[ifm.FileFields].ToString()))
                            {
                                //dr["state"] = "error";
                                //dr["message"] = ifm.FileFields + " invalid date";
                                //dr["type"] = 0;
                                log.Message = ifm.FileFields + " invalid date.";
                                log.Type = 0;
                            }
                            else {
                                try
                                {
                                    dr[ifm.FileFields] = DateTime.Parse(dr[ifm.FileFields].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                catch (Exception e)
                                {

                                }
                            }
                        }
                    }
                }
                else if (att.Type == AttributeType._bit)
                {
                    if (att.IsRequired == true)
                    {
                        if (IsEmpty(dr[ifm.FileFields]))
                        {
                            log.Message = ifm.FileFields + " is required field.";
                            log.Type = 0;
                            //dr["state"] = "error";
                            //dr["message"] = ifm.FileFields + " is required field.";
                            //dr["type"] = 0;
                        }
                        if (!IsBoolean(dr[ifm.FileFields].ToString()))
                        {
                            log.Message = ifm.FileFields + " is required field. value must be 'True/False' .";
                            log.Type = 0;

                            //dr["state"] = "error";
                            //dr["message"] = ifm.FileFields + " is required field. value must be 'True/False' ";
                            //dr["type"] = 0;
                        }
                    }
                    else
                    {
                        if (!IsEmpty(dr[ifm.FileFields]))
                        {
                            if (!IsBoolean(dr[ifm.DataField].ToString()))
                            {
                                log.Message = ifm.FileFields + " is required field. value must be 'True/False' .";
                                log.Type = 0;
                                //dr["state"] = "error";
                                //dr["message"] = ifm.FileFields + " is required field. value must be 'True/False' ";
                                //dr["type"] = 0;
                            }
                        }
                    }
                }
                if (Validation != null)
                {
                    var im = Validation.Validate(this, dr);
                    if (im.Type == ErrorType.ERROR)
                    {
                        log.Message = im.Message;
                        log.Type = 0;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookupid"></param>
        /// <returns></returns>
        private bool IsCoreLookup(int lookupid)
        {
            DataRow dr = LookupList.Select("FieldInstanceID =" + lookupid).FirstOrDefault();
            if (dr != null)
            {
                if (dr["Type"].ToString() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected bool IsDate(String date)
        {
            try
            {

                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal bool IsNumeric(string value)
        {
            if (value != "")
            {
                return value.All(char.IsNumber);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal bool IsBoolean(string value)
        {
            bool flag;
            if (Boolean.TryParse(value, out flag))
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        protected internal bool IsEmpty(object data)
        {
            if (data == null)
            {
                return true;
            }
            else
            {
                if (data.ToString() == "" || data.ToString() == "-1")
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="type"></param>
        /// <param name="dt"></param>
        private void AddColumn(string colName, System.Type type, DataTable dt)
        {
            if (!dt.Columns.Contains(colName))
            {
                dt.Columns.Add(new DataColumn(colName, type));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookuptext"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private int AddLookup(string lookuptext, string id)
        {
            string strjson = File.ReadAllText(lookupPath);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);
            if (!dt.Columns.Contains("New"))
            {
                dt.Columns.Add("New", typeof(int));
            }
            AddColumn("FieldInstanceID", typeof(int), dt);
            AddColumn("Lookupdescription", typeof(string), dt);
            AddColumn("LookUpID", typeof(int), dt);
            AddColumn("LookUpOrder", typeof(int), dt);
            AddColumn("ClientID", typeof(int), dt);
            AddColumn("LookupCode", typeof(string), dt);
            var _dt = dt.AsEnumerable().Where(x => x["FieldInstanceID"].ToString() == id).AsDataView().ToTable();
            int lookup = 0;
            int lookuporder = 0;
            if (_dt.Rows.Count > 0)
            {
                lookup = Convert.ToInt32(_dt.Compute("max(LookUpID)", ""));
                lookuporder = Convert.ToInt32(_dt.Compute("max(LookUpOrder)", ""));
            }
            lookup = lookup + 1;
            DataRow dr = dt.NewRow();
            dr["FieldInstanceID"] = id;
            dr["Lookupdescription"] = lookuptext;
            dr["LookUpID"] = lookup;
            dr["LookUpOrder"] = lookuporder + 1;
            dr["ClientID"] = this.ClientID;
            dr["New"] = 1;
            dt.Rows.Add(dr);
            File.WriteAllText(lookupPath, Newtonsoft.Json.JsonConvert.SerializeObject(dt));
            return lookup;

        }

        private DataTable GetLookup(string Id,string conn) {
            string strjson = File.ReadAllText(lookupPath);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);
            if (dt.Rows.Count > 0)
            {
                return dt.AsEnumerable().Where(x => x["FieldInstanceID"].ToString() == Id).CopyToDataTable();
            }
            else {
              return  Global.GetLookup(ClientID, Id, conn);
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookuptext"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected int GetLookUp(string lookuptext, string id)
        {

            string strjson = File.ReadAllText(lookupPath);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);
            var item = dt.AsEnumerable().Where(x => x["FieldInstanceID"].ToString() == id && x["Lookupdescription"].ToString().ToLower() == lookuptext.ToLower()).ToList();
            if (item.Count > 0)
            {
                return Convert.ToInt32(item[0]["LookupID"]);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetComponentLookup(string field, string value)
        {
            var clk = ComponentLookups.Where(x => x.Attribute.ID == field).FirstOrDefault();
            if (clk.LookupData != null)
            {
                DataTable dt = (DataTable)clk.LookupData;
                dt.DefaultView.RowFilter = clk.Attribute.Name + " = '" + value.Trim().Replace("'", "''") + "'";
                var finaldt = dt.DefaultView.ToTable(true);
                dt.DefaultView.RowFilter = "";
                if (finaldt.Rows.Count > 0)
                {
                    foreach (DataColumn dc in finaldt.Columns)
                    {
                        if (dc.ColumnName.ToLower() != "clientid" && dc.ColumnName.ToLower() != clk.Attribute.Name.ToLower())
                            return finaldt.Rows[0][dc.ColumnName] == null ? "" : finaldt.Rows[0][dc.ColumnName].ToString().Trim();
                    }
                    return "";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        protected bool IsDecimal(string val)
        {
            decimal outputVal;
            return decimal.TryParse(val, out outputVal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected bool IsUnique(string fieldname, string data) //,string keyfield,string keyval
        {
            var item = TargetData.AsEnumerable().Where(x => x[fieldname].ToString() == data).ToList();
            if (item.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        protected string IsKeyValid(Dictionary<string, string> keyValues)
        {
            List<DataRow> drs = TargetData.AsEnumerable().ToList();
            foreach (KeyValuePair<string, string> i in keyValues)
            {
                if (i.Value != "")
                {
                    drs = drs.AsEnumerable().Where(x => x[i.Key].ToString() == i.Value.Trim()).ToList();
                }
            }
            if (drs.Count > 0)
            {
                var key = this.Component.Keys.Where(x => x.Name.ToLower() != "clientid" && x.ComponentLookup == "").FirstOrDefault();
                if (key != null)
                {
                    return drs[0][key.Name].ToString();
                }
                return "-1";
            }
            else
            {
                return "-1";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="keyColumn"></param>
        /// <returns></returns>
        private string GetKeyValue(Dictionary<string, string> keyValues, string keyColumn)
        {
            List<DataRow> drs = TargetData.AsEnumerable().ToList();
            foreach (KeyValuePair<string, string> i in keyValues)
            {
                if (i.Value != "")
                {
                    drs = drs.AsEnumerable().Where(x => x[i.Key].ToString() == i.Value).ToList();
                }
            }
            if (drs.Count > 0)
            {
                if (!drs[0].IsNull(keyColumn))
                {
                    return drs[0][keyColumn].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        protected internal void LoadTargetData(string conn)
        {
            DataManager dm = new DataManager();
            List<string> fields = new List<string>();
            var keys = Component.Attributes.Where(x => x.IsKey == true).ToList();
            foreach (IAttribute att in keys)
            {
                if (att.IsKey == true)
                {
                    fields.Add(att.Name);
                }
            }
            foreach (ImportFieldMap f in ImportFields)
            {
                var att = Component.Attributes.Where(x => x.ID == f.DataField).FirstOrDefault();
                if (att != null)
                {
                    fields.Add(att.Name);
                }
            }
            dm.SelectFields(fields.ToArray(), Component.TableName).From(Component.TableName);
            dm.Where(Component.TableName, "clientid", "=", this.ClientID.ToString());
            TargetData = dm.GetData(conn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="templateFields"></param>
        /// <param name="attributes"></param>
        protected internal void LoadTargetPivotData(string conn,List<TemplateField> templateFields, List<CompExtention.Attribute> attributes) {
            DataManager dm = new DataManager();
            List<string> fields = new List<string>();
 
            foreach (CompExtention.Attribute f in attributes)
            {
                var att = Component.Attributes.Where(x => x.ID == f.ID).FirstOrDefault();
                if (att != null)
                {
                    fields.Add(att.Name);
                }
            }
            dm.SelectFields(fields.ToArray(), Component.TableName).From(Component.TableName);
            dm.Where(Component.TableName, "clientid", "=", this.ClientID.ToString());
            TargetData = dm.GetData(conn);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        protected internal void LoadLookupComponent(string conn)
        {
            ComponentLookups = new List<LookupComponentField>();
            foreach (ImportFieldMap field in ImportFields)
            {

                var aa = Component.Attributes.Where(x => x.ID == field.DataField).FirstOrDefault();
                if (aa != null)
                {
                    if (aa.Type == AttributeType._componentlookup)
                    {
                        var cm = new ComponentManager(ClientID, aa.ComponentLookup, new CompExtention.DataAccess.ComponentDataHandler(conn));
                        var comp = cm.GetComponent();
                        cm.LoadAttributes();
                        List<string> fields = new List<string>();
                        foreach (CompExtention.Attribute att in comp.Keys)
                        {
                            fields.Add(att.Name);
                        }
                        var lkdf = comp.Attributes.Where(x => x.ID == aa.ComponentLookupDisplayField).FirstOrDefault();
                        if (lkdf != null)
                        {
                            fields.Add(lkdf.Name);
                        }
                        DataManager dm = new DataManager();
                        dm.SelectFields(fields.ToArray(), comp.TableName).From(comp.TableName);
                        dm.Where(comp.TableName, "clientid", "=", this.ClientID.ToString());
                        ComponentLookups.Add(new LookupComponentField() { Attribute = lkdf, LookupComponent = (Component)comp, LookupData = dm.GetData(conn) });
                    }
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        protected internal void LoadUniqueKeys(string conn)
        {
            DataManager dm = new DataManager();
            List<string> fields = new List<string>();
            var keys = Component.Attributes.Where(x => x.IsKey == true).ToList();
            foreach (IAttribute att in keys)
            {
                if (att.IsKey == true)
                {
                    fields.Add(att.Name);
                }
            }
            foreach (ImportFieldMap f in ImportFields)
            {
                var att = Component.Attributes.Where(x => x.ID == f.DataField).FirstOrDefault();
                if (att != null)
                {
                    fields.Add(att.Name);
                }
            }
            dm.SelectFields(fields.ToArray(), Component.TableName).From(Component.TableName);
            dm.Where(Component.TableName, "clientid", "=", this.ClientID.ToString());
            TargetData = dm.GetData(conn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="msg"></param>
        /// <param name="err"></param>
        public void InsertLog(DataRow dr, string msg, ErrorType err)
        {
            if (!dr.Table.Columns.Contains("Index"))
            {
                int index = Convert.ToInt32(dr["Index"].ToString());
                var log = this.Logs.Where(x => x.Index == index).FirstOrDefault();
                if (log != null)
                {
                    log.Message = msg;
                    log.Type = err;
                    log.ImportingType = ComponentDataLog.ImportType.INSERT;
                }
                else
                {
                    this.Logs.Add(new ComponentDataLog()
                    {
                        ImportingType = ComponentDataLog.ImportType.INSERT,
                        UniqueKeys = "",
                        IsExist = false,
                        Index = index,
                        Message = msg,
                        Type = err
                    });
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="msg"></param>
        /// <param name="err"></param>
        public void UpdateLog(DataRow dr, string msg, ErrorType err)
        {
            if (!dr.Table.Columns.Contains("Index"))
            {
                int index = Convert.ToInt32(dr["Index"].ToString());
                var log = this.Logs.Where(x => x.Index == index).FirstOrDefault();
                if (log != null)
                {
                    log.Message = msg;
                    log.Type = err;
                    log.ImportingType = ComponentDataLog.ImportType.UPDATE;
                }
                else
                {
                    this.Logs.Add(new ComponentDataLog()
                    {
                        ImportingType = ComponentDataLog.ImportType.UPDATE,
                        UniqueKeys = "",
                        IsExist = false,
                        Index = index,
                        Message = msg,
                        Type = err
                    });
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPath"></param>
        /// <param name="write"></param>
        /// <param name="context"></param>
        private void WriteLog(string logPath, string write, string context)
        {
            string path = logPath + "/" + context + "/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(path + this.Component.ID + ".json", write);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPath"></param>
        /// <param name="write"></param>
        /// <param name="context"></param>
        private void WriteImportStatus(string logPath, string write, string context) {
            string path = logPath + "/" + context + "/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(path + this.Component.ID + "_status.json", write);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPath"></param>
        /// <param name="context"></param>
        public void LoadImportStatus(string logPath, string context)
        {
            string path = logPath + "/" + context + "/";
            path = path + this.Component.ID + "_status.json";
            if (System.IO.File.Exists(path))
            {
                string s = File.ReadAllText(path);
                this.DataStatus  = Newtonsoft.Json.JsonConvert.DeserializeObject<ImportDataStatus >(s);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPath"></param>
        /// <param name="context"></param>
        public void LoadLog(string logPath, string context)
        {
            string path = logPath + "/" + context + "/";
            path = path + this.Component.ID + ".json";
            if (System.IO.File.Exists(path))
            {
                string s = File.ReadAllText(path);
                this.Logs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComponentDataLog>>(s);
            }
        }
    }


    public class ComponentDataLog
    {
        public enum ImportType
        {
            INSERT,
            UPDATE
        }
        public int Index { get; set; }
        public string Message { get; set; }
        public ErrorType Type { get; set; }
        public bool IsExist { get; set; }
        public string UniqueKeys { get; set; }
        public string ExistKey { get; set; }
        public ImportType ImportingType { get; set; }

        public ComponentDataLog()
        {
            Message = "";
            Type = ErrorType.NOERROR;
            IsExist = false;
            UniqueKeys = "";
            ImportingType = ImportType.INSERT;
        }

    }
}
