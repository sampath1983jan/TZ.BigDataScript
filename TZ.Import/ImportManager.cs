using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Import
{
    public class ImportManager
    {
        private int step;
        /// <summary>
        /// 
        /// </summary>
        public string Connection { get; set; }     
        /// <summary>
        /// 
        /// </summary>
        public string LogPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ImportContext Context { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public int Step { get { return step; } }
        /// <summary>
        /// Import ID
        /// </summary>
        private string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ImportManager() { 
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public ImportManager(string id, string connection,string lgPath) {
            this.ID = id;
            this.LogPath = lgPath;
            Connection = connection;
            ImportLog log;
        System.Data.DataTable dt= Global.GetImportContext(id, connection);
            if (dt.Rows.Count > 0) {
                foreach (DataRow dr in dt.Rows) { 
                Context = Newtonsoft.Json.JsonConvert.DeserializeObject<ImportContext>(dr["ImportSchema"].ToString());             
                    Context.SetConnection(connection);
                    step = Convert.ToInt32(dr["Step"].ToString());
                    Context.LoadComponentView(); 
                }
            }
            if (dt.Rows.Count == 0) {
                throw new Exception("Import log not available.Contact administrator");
            }           
        }

        public void InitState() {
            if ( step ==3 || step == 4 || step == 5)
            {
                var k = new Step.DataImport(null, Context);
                var dd = new Step.DataValidation(null, Context);
                if (step == 5)
                {
                    dd.GetComponentData(LogPath, true);
                }
                else {
                    dd.GetComponentData(LogPath);
                }         
            }
            else if (step == 2)
            {
                this.Context.DataColumns = GetSourceColumns();
            }
            else if (step == 1) { 
            
            }
        }
        public void MoveTo(int id) {
            this.step = id;
        }
        public bool Remove() {
            return Global.RemoveContext(this.ID, Connection);
           
        }

        public List<ImportDataStatus> GetStatus() {
            var k = new Step.DataImport(null, Context);
            var dd = new Step.DataValidation(null, Context);
            if (step == 5)
            {
                dd.GetComponentData(LogPath, true);
            }
            else
            {
                dd.GetComponentData(LogPath);
            }
            return  this.Context.GetComponentStatus(this.LogPath );           
        }
        public static List<ImportLog> GetImportList( int clientID,int actionby,string connection)
        {
            
            List<ImportLog> logs = new List<ImportLog>();
            System.Data.DataTable dt = Global.GetImportContext(clientID, actionby, connection);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ImportLog log = new ImportLog();                   
                    log.ImportContext = dr["ImportSchema"].ToString();
                    log.ID = dr["importID"].ToString();
                    log.Step = Convert.ToInt32(dr["Step"].ToString());
                    log.LastModified = Convert.ToDateTime(dr["LastUPD"].ToString());
                    logs.Add(log);
                }
            }
          
            return logs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<String> GetSourceColumns() {
            List<String> Cols = new List<string>();
            if (System.IO.File.Exists(LogPath + "/" + this.Context.ID + ".json")) {
                var s = System.IO.File.ReadAllText(LogPath + "/" + this.Context.ID + ".json");
                var dt = new System.Data.DataTable();
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(s);
               
                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    Cols.Add(dc.ColumnName);
                }
            }
       
            return Cols;
        }
        public System.Data.DataTable GetComponentDataSource(string cID) {
            var componentData =Context.ComponentData.Where(x => x.Component.ID == cID).FirstOrDefault();
         return   componentData.SourceData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ImportManager New(string connection,string compPath,string templateID) {
            Context = new ImportContext(connection);     
            Context.ImportTemplateID = templateID;
            //Context.ComponentViewPath = compPath;             
            Connection = connection;         
            this.ID = Context.ID;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        private BaseImportHandler GetStep(int step) {
            if (step == 1)
            {
                var imfs= new Step.ImportFile(null, Context);
                return new Step.ChooseComponentView(imfs, Context);
            }
            else if (step == 2)
            {
                var fm= new Step.FieldMap(null, Context);
                return new Step.ImportFile(fm, Context);
            }
            else if (step == 3)
            {
                var vad = new Step.DataValidation(null, Context);
                return new Step.FieldMap(vad, Context);
            }
            else if (step == 4)
            {
                var di= new Step.DataImport(null, Context);
                return new Step.DataValidation(di, Context);
            }
            else if (step == 5)
            {
                return new Step.DataImport(null, Context);
            }
            else {
                return null;
            }
        }

        public DataTable GetComponentData(string compID) {
            var k = new Step.DataValidation(null, Context);
            return k.GetComponentData(compID, LogPath);
        }
        public ImportContext Validate(bool enableRecordUpdate,bool enableCreateLookup,List<ComponentCustomAction> ca ) {
            //var di = new Step.DataImport(null, Context);
            this.Context.EnableCreateLookup = enableCreateLookup;
            this.Context.EnableUpdateDuplicate = enableRecordUpdate;
             var k= new Step.DataValidation(null, Context);
           return  k.ValidateComponent(LogPath, ca);
        }
        public ImportContext ImportData(List<ComponentCustomAction> ca) {
            if (this.Context.Status == ImportStatus.pending)
            {
                var k = new Step.DataImport(null, Context);
                var dd = new Step.DataValidation(null, Context);
                dd.GetComponentData(LogPath);
                return k.ImportComponent(LogPath, ca);
            }
            else {
                return this.Context;
            }                  
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BaseImportHandler GetCurrentStep() {
            return GetStep(this.Step);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BaseImportHandler GetNextStep() {
            step =   this.Step + 1;
            return GetStep(this.Step );
        }
        public ImportError HandleNextStep() {
          return  GetNextStep().HandleRequest(LogPath);
        }
    }
}
