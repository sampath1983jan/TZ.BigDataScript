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

            var pseudocore = this.Context.ComponentData.Where(x => x.Component.Type == ComponentType.pseudocore ).FirstOrDefault();
            if (pseudocore != null) {
                pseudocore.LoadLog(logPath, this.Context.ID);
 
                var ca = cAction.Where(x => x.ComponentName == pseudocore.Component.TableName).FirstOrDefault();
                if (ca != null)
                {
                    pseudocore.Validation = ca.CustomAction;
                }
                pseudocore.PushPseudo(this.Context.Connection, logPath, this.Context.ID);
                pseudocore.Validation = null;
                pseudocore.Errors = pseudocore.Errors.Where(x => x.Type == ErrorType.ERROR).ToList();

                if (pseudocore.Logs.Where(x => x.Type == ErrorType.ERROR).ToList().Count > 0 || pseudocore.Errors.Count > 0)
                {

                    isError = true;
                }
                pseudocore.IsCompleted = true;
                pseudocore.Logs.Clear();
                CacheData(logPath + "/" + "cd" + "_" + pseudocore.Component.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(pseudocore));

            }
            var metacore = this.Context.ComponentData.Where(x => x.Component.Type == ComponentType.meta).FirstOrDefault();
            if (metacore != null)
            {
                metacore.LoadLog(logPath, this.Context.ID);
                var ca = cAction.Where(x => x.ComponentName == pseudocore.Component.TableName).FirstOrDefault();
                if (ca != null)
                {
                    metacore.Validation = ca.CustomAction;
                }
                metacore.PushMeta(this.Context.Connection, logPath, this.Context.ID);
                metacore.Validation = null;
                metacore.Errors = metacore.Errors.Where(x => x.Type == ErrorType.ERROR).ToList();

                if (metacore.Logs.Where(x => x.Type == ErrorType.ERROR).ToList().Count > 0 || metacore.Errors.Count > 0)
                {

                    isError = true;
                }
                metacore.IsCompleted = true;
                metacore.Logs.Clear();
                CacheData(logPath + "/" + "cd" + "_" + metacore.Component.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(metacore));
            }

            var trasaction = this.Context.ComponentData.Where(x => x.Component.Type == ComponentType.transaction ).FirstOrDefault();
            if (trasaction != null)
            {
                var CoreComponent = Context.View.Components.Where(x => x.Type == ComponentType.core).ToList();
                trasaction.LoadLog(logPath, this.Context.ID);
                var ca = cAction.Where(x => x.ComponentName == trasaction.Component.TableName).FirstOrDefault();
                if (ca != null)
                {
                    trasaction.Validation = ca.CustomAction;
                }
                foreach (Component c in CoreComponent)
                {
                    var cRelation = Context.View.ComponentRelations.Where(x => (x.ComponentID == c.ID || x.ChildComponentID == c.ID)
                    && (x.ComponentID == trasaction.Component.ID || x.ChildComponentID == trasaction.Component.ID)).ToList();
                    if (cRelation.Count > 0)
                    {
                        ComponentData coreCd = GetCache(logPath + "/" + "cd" + "_" + c.ID + "_" + this.Context.ID + ".json");
                        coreCd.Component = c;
                        coreCd.LoadTargetData(this.Context.Connection);
                        trasaction.ParentComponentData.Add(coreCd);
                    }
                }
                trasaction.LoadTargetData(this.Context.Connection);
                trasaction.PushTrasaction(this.Context.Connection, logPath, this.Context.ID);
                trasaction.Validation = null;
                trasaction.Errors = trasaction.Errors.Where(x => x.Type == ErrorType.ERROR).ToList();

                if (trasaction.Logs.Where(x => x.Type == ErrorType.ERROR).ToList().Count > 0 || trasaction.Errors.Count > 0)
                {

                    isError = true;
                }
                trasaction.IsCompleted = true;
                trasaction.Logs.Clear();
                CacheData(logPath + "/" + "cd" + "_" + trasaction.Component.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(trasaction));
            }

            string strjson = File.ReadAllText(logPath + "/" + this.Context.ID + "_lookup.json");
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);
            if (dt.Columns.Contains("New"))
            {
                DataTable lok = dt.Select("New =1").CopyToDataTable();

                DataTableReader dr = lok.CreateDataReader();
                //clone original
                DataTable clonedDT = lok.Clone();                //add identity column   
                if (!clonedDT.Columns.Contains("ClientID")) {
                    clonedDT.Columns.Add(new DataColumn() { ColumnName = "ClientID", DataType = typeof(Int32), DefaultValue = this.Context.ClientID });
                }
               
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