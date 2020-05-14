using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.CompExtention;

namespace TZ.Import.Step
{

    //1. Choose component model
    //2. Upload data(csv, json, xslx, xls)
    //3. Load Field and mapping
    //4. Data Validation & Statistic
    //5. Import Schedule

    //Step -5
    public class DataImport : BaseImportHandler
    {
        public DataImport(BaseImportHandler nextHandler, ImportContext context) : base(nextHandler, context)
        {

        }
        public override ImportError HandleRequest(string logPath)
        {
            foreach (ComponentData comp in this.Context.ComponentData)
            {
              comp.Push(this.Context.Connection,logPath, this.Context.ID);

                string strjson = File.ReadAllText(logPath + "/" + this.Context.ID + "_lookup.json");
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);
                if (dt.Columns.Contains("New")) {
                    DataTable lok = dt.Select("New =1").CopyToDataTable();
                    ComponentData.PushLookUp(this.Context.Connection, lok);
                }
            }
            this.Context.View = null;
            this.Context.ComponentData = new List<ComponentData>();
             this.Context.Template.TemplateFields = new List<CompExtention.ImportTemplate.TemplateField>();
            this.Context.Template.View = null;

            this.Context.Status = ImportStatus.completed;
            Global.SaveImportContext(this.Context.ID, Newtonsoft.Json.JsonConvert.SerializeObject(this.Context), 5,
        this.Context.ActionBy,
        this.Context.Connection, this.Context.ClientID
        );

            return null;
        }
        public ImportContext ImportComponent(string logPath, List<ComponentCustomAction> cAction) {
            bool isError = false;
            var CoreComponentData = this.Context.ComponentData.Where(x => x.Component.Type == ComponentType.core || x.Component.Type == ComponentType.attribute).ToList();

            foreach (ComponentData cd in CoreComponentData) {
                if (cd.Component != null)
                    cd.LoadLog(logPath, this.Context.ID);
                {
                    var ca = cAction.Where(x => x.ComponentName == cd.Component.TableName).FirstOrDefault();
                    if (ca != null) {
                        cd.Validation = ca.CustomAction;
                    }
                    cd.Push(this.Context.Connection, logPath, this.Context.ID);
                    cd.Validation = null;
                    cd.Errors = cd.Errors.Where(x => x.Type == ErrorType.ERROR).ToList();
                    
                    if (cd.Logs.Where(x=> x.Type == ErrorType.ERROR ).ToList().Count > 0 || cd.Errors.Count >0) {
                        isError = true;
                    }
                    cd.Logs.Clear();
                    CacheData(logPath + "/" + "cd" + "_" + cd.Component.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(cd));                
                }
            }
            var LinkComponentData = this.Context.ComponentData.Where(x => x.Component.Type == ComponentType.link).FirstOrDefault();
            if (LinkComponentData != null)
            {
                LinkComponentData.LoadLog(logPath, this.Context.ID);
                var CoreComponent = Context.View.Components.Where(x => x.Type == ComponentType.core).ToList();
                var ca = cAction.Where(x => x.ComponentName == LinkComponentData.Component.TableName).FirstOrDefault();
                if (ca != null)
                {
                    LinkComponentData.Validation = ca.CustomAction;
                }

                foreach (Component c in CoreComponent)
                {
                    var cRelation = Context.View.ComponentRelations.Where(x => (x.ComponentID == c.ID || x.ChildComponentID == c.ID)
                    && (x.ComponentID == LinkComponentData.Component.ID || x.ChildComponentID == LinkComponentData.Component.ID)).ToList();
                    if (cRelation.Count > 0)
                    {                                 
                        ComponentData coreCd = GetCache(logPath + "/" + "cd" + "_" + c.ID + "_" + this.Context.ID + ".json");
                        coreCd.Component = c;
                        coreCd.LoadTargetData(this.Context.Connection);
                        LinkComponentData.ParentComponentData.Add(coreCd);
                    }
                }
                LinkComponentData.PushLink(this.Context.Connection, logPath, this.Context.ID);
                LinkComponentData.Validation = null;
                LinkComponentData.Errors = LinkComponentData.Errors.Where(x => x.Type == ErrorType.ERROR).ToList();

                if (LinkComponentData.Logs.Where(x => x.Type == ErrorType.ERROR).ToList().Count > 0 || LinkComponentData.Errors.Count > 0)
                {
                
                    isError = true;
                }
                LinkComponentData.IsCompleted = true;
                LinkComponentData.Logs.Clear();
                CacheData(logPath + "/" + "cd" + "_" + LinkComponentData.Component.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(LinkComponentData));
            }
            string strjson = File.ReadAllText(logPath + "/" + this.Context.ID + "_lookup.json");
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);
            if (dt.Columns.Contains("New"))
            {
                DataTable lok = dt.Select("New =1").CopyToDataTable();

                DataTableReader dr = lok.CreateDataReader();
                //clone original
                DataTable clonedDT = lok.Clone();                //add identity column               
                clonedDT.Columns.Add(new DataColumn() { ColumnName = "ClientID", DataType = typeof(Int32), DefaultValue = this.Context.ClientID });
                //load clone from reader, identity col will auto-populate with values
                clonedDT.Load(dr);
                lok = clonedDT;
                clonedDT.Dispose();

                ComponentData.PushLookUp(this.Context.Connection, lok);
                dt.Columns.Remove("New");
                File.WriteAllText(logPath + "/" + this.Context.ID + "_lookup.json", Newtonsoft.Json.JsonConvert.SerializeObject(dt));
            }            
            ImportContext context =  Newtonsoft.Json.JsonConvert.DeserializeObject<ImportContext>( Newtonsoft.Json.JsonConvert.SerializeObject( this.Context));
            if (isError)
            {
                context.Status = ImportStatus.error;
            }
            else {
                context.Status = ImportStatus.completed;
            }           
            this.Context.View = null;
            this.Context.ComponentData = new List<ComponentData>();
            this.Context.Template.TemplateFields = new List<CompExtention.ImportTemplate.TemplateField>();
            this.Context.Template.View = null;
            this.Context.Status = context.Status;
            this.Context.DataLocation = "";

            Global.SaveImportContext(this.Context.ID, Newtonsoft.Json.JsonConvert.SerializeObject(this.Context), 5,
        this.Context.ActionBy,
        this.Context.Connection, this.Context.ClientID);
            return context;
        }

        private ComponentData GetCache(string path)
        {
            string s = File.ReadAllText(path);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ComponentData>(s);

        }

        public void CacheData(string path, string data)
        {
            File.WriteAllText(path, data);
        }

       
        public override ImportError Validate()
        {
            throw new NotImplementedException();
        }
    }
}