using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using TZ.CompExtention;

namespace TZ.Import.Step
{
    //Step - 4
    public class DataValidation : BaseImportHandler
    {
        //private List<CustomValidation> _cv;
        private DataTable dt;
        private List<ImportError> impErrors;
        DataTable dtLk = new DataTable();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cv"></param>
        //public void SetCustomValidation(CustomValidation cv) {
        //    _cv = new List<CustomValidation>();
        //}
        //public void SetCustomValidation(CustomValidation[] cv)
        //{
        //    _cv = new List<CustomValidation>();
        //    _cv = cv.ToList();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nextHandler"></param>
        /// <param name="context"></param>
        public  DataValidation(BaseImportHandler nextHandler, ImportContext context) : base(nextHandler, context)
        {
            //_cv = new List<CustomValidation>();
        }
        /// <summary>
        ///  get data of component before validate like show data
        /// </summary>
        /// <param name="compID"></param>
        /// <param name="logPath"></param>
        /// <returns></returns>
        public DataTable GetComponentData(string compID, string logPath)
        {
            List<ImportError> ie = new List<ImportError>();           
            string strjson = File.ReadAllText(logPath + "/" + this.Context.ID + ".json");
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);
            this.Context.LoadComponentView();
            var CoreComponent = Context.View.Components.Where(x => x.Type == ComponentType.core).ToList();
            var comp = Context.View.Components.Where(x => x.ID == compID).FirstOrDefault();
            if (comp.Type == ComponentType.core)
            {
                ComponentData cd = new ComponentData(this.Context.ClientID, (Component)comp);
                cd.ClientID = this.Context.ClientID;
                cd.Component = (Component)comp;
                List<ImportFieldMap> compIFM = this.Context.ImportFields.Where(x => x.ComponentID == comp.ID).ToList();
                cd.ImportFields = compIFM;
                cd.ExtractDataFromSource(dt);                           
                return cd.GetProcessedData(logPath, this.Context.ID, false);              
            }
            else
            {
                ComponentData cd = new ComponentData(this.Context.ClientID, (Component)comp);
                cd.ClientID = this.Context.ClientID;
                cd.Component = (Component)comp;
                List<ImportFieldMap> compIFM = this.Context.ImportFields.Where(x => x.ComponentID == comp.ID).ToList();
                cd.ImportFields = compIFM;
                foreach (Component c in CoreComponent)
                {
                    var cRelation = Context.View.ComponentRelations.Where(x => (x.ComponentID == c.ID || x.ChildComponentID == c.ID)
                    && (x.ComponentID == comp.ID || x.ChildComponentID == comp.ID)).ToList();
                    if (cRelation.Count > 0)
                    {
                        var keyFields = this.Context.ImportFields.Where(x => x.ComponentID == c.ID && x.IsKey == true).ToList();
                        cd.ImportFields.AddRange(keyFields);
                    }
                }
                List<string> cols = new List<string>();
                foreach (ImportFieldMap ifm in compIFM)
                {
                    ifm.FileFields = ifm.FileFields.Replace(" ", "_");
                    cols.Add(ifm.FileFields);
                }
                foreach (DataColumn dc in dt.Columns)
                {
                    dc.ColumnName = dc.ColumnName.Replace(" ", "_");
                }
                cd.SourceData = dt.DefaultView.ToTable(true, cols.ToArray());
                return cd.GetProcessedData(logPath, this.Context.ID, false);
       
            }

        }
        /// <summary>
        /// load all component data from validated schema(Json)
        /// </summary>
        /// <param name="logPath"></param>
        public void GetComponentData(string logPath,bool withStatus =false) {
           
            this.Context.LoadComponentView();
            string strjson = File.ReadAllText(logPath + "/" + this.Context.ID + ".json");
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);
            var CoreComponent = Context.View.Components.Where(x => x.Type == ComponentType.core || x.Type == ComponentType.attribute || x.Type== ComponentType.link).ToList();
            foreach (IComponent comp in CoreComponent)
            {
                if (File.Exists(logPath + "/" + "cd" + "_" + comp.ID + "_" + this.Context.ID + ".json"))
                {
                    ComponentData cd = GetCache(logPath + "/" + "cd" + "_" + comp.ID + "_" + this.Context.ID + ".json");
                    cd.LoadLog(logPath, this.Context.ID);
                    cd.lookupPath = logPath + "/" + this.Context.ID + "_lookup.json";
                   
                     cd.GetProcessedData(logPath, this.Context.ID, withStatus); 
                    this.Context.ComponentData.Add(cd);
                }                              
            }
        }
        private DataTable GetLookups()
        {
           return Shared.GetLookup(this.Context.ClientID, this.Context.Connection);
        }
        private ComponentData ValidateLink(string logPath, List<ComponentCustomAction> customAction) {
            var CoreComponent = Context.View.Components.Where(x => x.Type == ComponentType.core).ToList();
            var LinkCompnent = Context.View.Components.Where(x => x.Type == ComponentType.link).FirstOrDefault();
            if (LinkCompnent != null)
            {
                ComponentData cd = new ComponentData(this.Context.ClientID, LinkCompnent);
                var caction = customAction.Where(x => x.ComponentName == LinkCompnent.TableName).FirstOrDefault();
                if (caction != null)
                {
                    cd.Validation = caction.CustomAction;
                    cd.Validation.Init();
                }
                List<ImportFieldMap> compIFM = this.Context.ImportFields.Where(x => x.ComponentID == LinkCompnent.ID).ToList();
                cd.ImportFields = compIFM;
                foreach (Component c in CoreComponent)
                {
                    var cRelation = Context.View.ComponentRelations.Where(x => (x.ComponentID == c.ID || x.ChildComponentID == c.ID)
                    && (x.ComponentID == LinkCompnent.ID || x.ChildComponentID == LinkCompnent.ID)).ToList();
                    if (cRelation.Count > 0)
                    {
                        var keyFields = this.Context.ImportFields.Where(x => x.ComponentID == c.ID && x.IsKey == true).ToList();
                        cd.ImportFields.AddRange(keyFields);
                        ComponentData coreCd = GetCache(logPath + "/" + "cd" + "_" + c.ID + "_" + this.Context.ID + ".json");
                        coreCd.Component = c;
                        cd.ParentComponentData.Add(coreCd);
                    }
                }

                List<string> cols = new List<string>();
                foreach (ImportFieldMap ifm in cd.ImportFields)
                {
                    ifm.FileFields = ifm.FileFields.Replace(" ", "_");
                    cols.Add(ifm.FileFields);
                }
                foreach (DataColumn dc in dt.Columns)
                {
                    dc.ColumnName = dc.ColumnName.Replace(" ", "_");
                }
                cd.SourceData = dt.DefaultView.ToTable(true, cols.ToArray());
                cd.LoadTargetData(this.Context.Connection);
                cd.SetLookupList(dtLk);
                cd.lookupPath = logPath + "/" + this.Context.ID + "_lookup.json";
                cd.ValidateData(this.Context.EnableUpdateDuplicate, this.Context.Connection,logPath,this.Context.ID);
                cd.ParentComponentData.Clear();
                cd.GetProcessedData(logPath, this.Context.ID);
                cd.Validation = null;
                cd.Errors = cd.Errors.Where(x => x.Type == ErrorType.ERROR).ToList();

                if (cd.Logs.Where(x=> x.Type==  ErrorType.ERROR).ToList().Count >0 || cd.Errors.Where(x=> x.Type == ErrorType.ERROR ).ToList().Count  > 0)
                {
                    cd.IsValid = false;
                }
                cd.lookupPath= "";
                //cd.Logs.Clear();
                CacheData(logPath + "/" + "cd" + "_" + LinkCompnent.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(cd));
                return (cd);
            }
            else
                return null;
                
        }
        private ComponentData ValidateAttribute( string logPath, List<ComponentCustomAction> customAction) {
            var comp = this.Context.View.Components.Where(x => x.Type == ComponentType.attribute).FirstOrDefault();
            //foreach (Component comp in MetaComponent)
            //{
            if (comp != null)
            {
                var caction = customAction.Where(x => x.ComponentName == comp.TableName).FirstOrDefault();
                ComponentData cd = new ComponentData(this.Context.ClientID, (Component)comp);
                cd.ImportFields = this.Context.ImportFields.Where(x => x.ComponentID == comp.ID).ToList();
                cd.lookupPath = logPath + "/" + this.Context.ID + "_lookup.json";
                cd.ExtractDataFromSource(dt);
                cd.LoadTargetData(this.Context.Connection);
                cd.SetLookupList(dtLk);
                if (caction != null)
                {
                    cd.Validation = caction.CustomAction;
                    cd.Validation.Init();

                }
                cd.ValidateData(this.Context.EnableUpdateDuplicate, this.Context.Connection, logPath, this.Context.ID);
                cd.Validation = null;
                cd.Errors = cd.Errors.Where(x => x.Type == ErrorType.ERROR).ToList();
                if (cd.Logs.Where(x => x.Type == ErrorType.ERROR).ToList().Count > 0 || cd.Errors.Where(x=> x.Type == ErrorType.ERROR ).ToList().Count  > 0)
                {
                    cd.IsValid = false;
                }

                cd.GetProcessedData(logPath, this.Context.ID);
                cd.lookupPath = "";
                //cd.Logs.Clear();
                CacheData(logPath + "/" + "cd" + "_" + comp.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(cd));
                return (cd);
            }
            else {
                return null;
            }
               
          ///  }
        }
        private ComponentData ValidateCore(Component comp, string logPath, ComponentCustomAction caction) {
            ComponentData cd = new ComponentData(this.Context.ClientID, (Component)comp);
            cd.ImportFields = this.Context.ImportFields.Where(x => x.ComponentID == comp.ID).ToList();
            cd.lookupPath = logPath + "/" + this.Context.ID + "_lookup.json";
            cd.ExtractDataFromSource(dt);
            cd.LoadTargetData(this.Context.Connection);
            cd.SetLookupList(dtLk);         
            if (caction != null) {
                cd.Validation = caction.CustomAction;
                cd.Validation.Init();
            }
            cd.ValidateData(this.Context.EnableUpdateDuplicate, this.Context.Connection, logPath, this.Context.ID);
            cd.Errors = cd.Errors.Where(x => x.Type == ErrorType.ERROR).ToList();
            cd.Validation = null;
            cd.lookupPath = "";
            cd.GetProcessedData(logPath, this.Context.ID);
          
            CacheData(logPath + "/" + "cd" + "_" + comp.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(cd));
            return (cd);        
        }
       /// <summary>
       /// validate component 
       /// </summary>
       /// <param name="compID"></param>
       /// <param name="logPath"></param>
       /// <returns></returns>
        public ImportContext ValidateComponent(string logPath, List<ComponentCustomAction> customAction) {
          
            dtLk = GetLookups();

            List<ImportError> ie = new List<ImportError>();
            this.Context.LoadComponentView();
            List<ComponentData> componentDataList = new List<ComponentData>();
            var lok = Global.GetLookup(this.Context.ClientID, GetLookup(), this.Context.Connection);
            File.WriteAllText(logPath + "/" + this.Context.ID + "_lookup.json", Newtonsoft.Json.JsonConvert.SerializeObject(lok));

            string strjson = File.ReadAllText(logPath + "/" + this.Context.ID + ".json");
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);

            var CoreComponent = Context.View.Components.Where(x => x.Type == ComponentType.core).ToList();
         
            foreach (Component comp in CoreComponent)
            {
                var custevent = customAction.Where(x => x.ComponentName == comp.TableName).FirstOrDefault();
            
                componentDataList.Add( ValidateCore(comp, logPath, custevent));              
            }
            var cdLink = ValidateLink(logPath, customAction);
            if (cdLink != null)
            {
                componentDataList.Add(cdLink);
            }
            var cdAtt=  ValidateAttribute(logPath, customAction);
            if (cdAtt != null) {
                componentDataList.Add(cdAtt);
            }
            ImportContext context = Newtonsoft.Json.JsonConvert.DeserializeObject<ImportContext>(Newtonsoft.Json.JsonConvert.SerializeObject(this.Context));
            context.ComponentData = componentDataList;
            this.Context.View = null;
            this.Context.ComponentData = new List<ComponentData>();
             this.Context.Template.TemplateFields = new List<CompExtention.ImportTemplate.TemplateField>();
            this.Context.Template.View = null;
            this.Context.DataLocation = "";

            //this.Context.ComponentData.Clear();
            Global.SaveImportContext(this.Context.ID, Newtonsoft.Json.JsonConvert.SerializeObject(this.Context), 4,
               this.Context.ActionBy,
               this.Context.Connection, this.Context.ClientID
               );
            return context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPath"></param>
        /// <returns></returns>
        public override ImportError HandleRequest(string logPath)
        {
            var lok = Global.GetLookup(this.Context.ClientID, GetLookup(), this.Context.Connection);
            File.WriteAllText(logPath + "/" + this.Context.ID + "_lookup.json", Newtonsoft.Json.JsonConvert.SerializeObject(lok));
            string strjson = File.ReadAllText(logPath + "/" + this.Context.ID + ".json");
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(strjson);
            this.Context.LoadComponentView();
            List<ImportError> ie = new List<ImportError>();
           var CoreComponent= Context.View.Components.Where(x => x.Type == ComponentType.core).ToList();
            foreach (IComponent comp in CoreComponent)
            {
                ComponentData cd = new ComponentData(this.Context.ClientID, (Component)comp);
            
                List<ImportFieldMap> compIFM = this.Context.ImportFields.Where(x => x.ComponentID == comp.ID).ToList();
                cd.ImportFields = compIFM;
                cd.ExtractDataFromSource(dt);
                cd.LoadTargetData(this.Context.Connection);
                cd.lookupPath = logPath + "/" + this.Context.ID + "_lookup.json";
                this.Context.ComponentData.Add(cd);
                CacheData(logPath + "/" + "cd" + "_" + comp.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(cd));
              //  ie.Add( cd.Validate(this.Context.EnableUpdateDuplicate,this.Context.Connection));
            }

            var linkComp = Context.View.Components.Where(x => x.Type == ComponentType.link).ToList();
            foreach (IComponent comp in linkComp)
            {               
                ComponentData cd = new ComponentData(this.Context.ClientID, (Component)comp);
                cd.ClientID = this.Context.ClientID;
                cd.Component = (Component)comp;
                List<ImportFieldMap> compIFM = this.Context.ImportFields.Where(x => x.ComponentID == comp.ID).ToList();                          
                cd.ImportFields = compIFM;
                foreach (Component c in CoreComponent)
                {
                    var cRelation = Context.View.ComponentRelations.Where(x => (x.ComponentID == c.ID || x.ChildComponentID == c.ID)
                    && (x.ComponentID == comp.ID || x.ChildComponentID == comp.ID)).ToList();
                    if (cRelation.Count > 0)
                    {
                        var keyFields = this.Context.ImportFields.Where(x => x.ComponentID == c.ID && x.IsKey == true).ToList();
                        cd.ImportFields.AddRange(keyFields);
                    }
                }
                List<string> cols = new List<string>();
                foreach (ImportFieldMap ifm in compIFM)
                {
                    ifm.FileFields = ifm.FileFields.Replace(" ", "_");
                    cols.Add(ifm.FileFields);
                }
                foreach (DataColumn dc in dt.Columns)
                {
                    dc.ColumnName = dc.ColumnName.Replace(" ", "_");
                }
                cd.SourceData = dt.DefaultView.ToTable(true, cols.ToArray());
                cd.LoadTargetData(this.Context.Connection);
                cd.lookupPath = logPath + "/" + this.Context.ID + "_lookup.json";
                this.Context.ComponentData.Add(cd);
                CacheData(logPath + "/" + "cd" + "_" + comp.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(cd));
           //     ie.Add(cd.Validate(this.Context.EnableUpdateDuplicate));
            }

                this.Context.View = null;
            this.Context.Template = null;
            Global.SaveImportContext(this.Context.ID, Newtonsoft.Json.JsonConvert.SerializeObject(this.Context), 4,
               this.Context.ActionBy,
               this.Context.Connection, this.Context.ClientID
               );                        
            return ie.Where(x=>x.Type != ErrorType.NOERROR).FirstOrDefault() ;
            // update data in the request;
        }

        public void CacheData(string path,string data) {
            File.WriteAllText(path, data);
        }
        private ComponentData GetCache(string path) {
           string s= File.ReadAllText(path);
         return   Newtonsoft.Json.JsonConvert.DeserializeObject<ComponentData>(s);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceField"></param>
        /// <returns></returns>
        public bool CheckFieldExist(string sourceField) {
            return false;
        }
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
        public override ImportError Validate()
        {            
            if (!dt.Columns.Contains("State")) {
                dt.Columns.Add(new DataColumn("State", typeof(string)));
            }
            if (!dt.Columns.Contains("message"))
            {
                dt.Columns.Add(new DataColumn("message", typeof(string)));
            }
            foreach (DataRow dr in dt.Rows) {
                //var ie = _cv.Validate(dr);
                //if (ie.Type == ErrorType.NOERROR)
                //{
                //    dr["state"] = "";
                //}
                //else {
                //    dr["state"] = "error";
                //    dr["message"] = ie.Message ;
                //}
            }
            if (dt.AsEnumerable().Where(x => x.IsNull("state") == true ? "" == "error" : x["state"].ToString() == "error").Count() > 0)
            {
                return new ImportError() { Type = ErrorType.ERROR };
            }
            else {
                return new ImportError() { Type = ErrorType.NOERROR  };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetLookup() {
            List<string> lookupid = new List<string>();
            foreach (IComponent comp in this.Context.View.Components) {
                foreach (IAttribute attr in comp.Attributes) {
                  var aa=  this.Context.Template.TemplateFields.Where(x => x.ID == attr.ID).FirstOrDefault();
                    if (aa != null) {
                        if (attr.Type == AttributeType._lookup)
                        {
                            lookupid.Add(attr.LookupInstanceID);
                        }
                    }                  
                }
            }
            return string.Join(",", lookupid.ToArray());
        }

    

    }
       
}



//if (comp.Type == ComponentType.core)
//{
//    ComponentData cd = new ComponentData();
//    cd.ClientID = this.Context.ClientID;
//    cd.Component = (Component)comp;
//    List<ImportFieldMap> compIFM = this.Context.ImportFields.Where(x => x.ComponentID == comp.ID).ToList();
//    cd.ImportFields = compIFM;
//    cd.ExtractDataFromSource(dt);
//    cd.LoadTargetData(this.Context.Connection);
//    cd.lookupPath = logPath + "/" + this.Context.ID + "_lookup.json";
//    this.Context.ComponentData.Add(cd);
//    cd.IsValidated = true;

//    ImportError ier = cd.Validate(this.Context.EnableUpdateDuplicate);
//    CacheData(logPath + "/" + "cd" + "_" + comp.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(cd));
//    DataTable Dt = cd.SourceData.Copy();

//    this.Context.ComponentData.Clear();
//    Global.SaveImportContext(this.Context.ID, Newtonsoft.Json.JsonConvert.SerializeObject(this.Context), 3,
// this.Context.ActionBy,
// this.Context.Connection
// );

//    return Dt;
//}
//else
//{
//    ComponentData cd = new ComponentData();
//    cd.ClientID = this.Context.ClientID;
//    cd.Component = (Component)comp;
//    List<ImportFieldMap> compIFM = this.Context.ImportFields.Where(x => x.ComponentID == comp.ID).ToList();
//    cd.ImportFields = compIFM;
//    foreach (Component c in CoreComponent)
//    {
//        var cRelation = Context.View.ComponentRelations.Where(x => (x.ComponentID == c.ID || x.ChildComponentID == c.ID)
//        && (x.ComponentID == comp.ID || x.ChildComponentID == comp.ID)).ToList();
//        if (cRelation.Count > 0)
//        {
//            var keyFields = this.Context.ImportFields.Where(x => x.ComponentID == c.ID && x.IsKey == true).ToList();
//            cd.ImportFields.AddRange(keyFields);
//        }
//    }
//    List<string> cols = new List<string>();
//    foreach (ImportFieldMap ifm in compIFM)
//    {
//        ifm.FileFields = ifm.FileFields.Replace(" ", "_");
//        cols.Add(ifm.FileFields);
//    }
//    foreach (DataColumn dc in dt.Columns)
//    {
//        dc.ColumnName = dc.ColumnName.Replace(" ", "_");
//    }
//    cd.SourceData = dt.DefaultView.ToTable(true, cols.ToArray());
//    cd.LoadTargetData(this.Context.Connection);
//    cd.lookupPath = logPath + "/" + this.Context.ID + "_lookup.json";
//    cd.IsValidated = true;
//    this.Context.ComponentData.Add(cd);

//    ImportError ier = cd.Validate(this.Context.EnableUpdateDuplicate);

//    CacheData(logPath + "/" + "cd" + "_" + comp.ID + "_" + this.Context.ID + ".json", Newtonsoft.Json.JsonConvert.SerializeObject(cd));

//    DataTable Dt = cd.SourceData.Copy();

//    this.Context.ComponentData.Clear();

//    Global.SaveImportContext(this.Context.ID, Newtonsoft.Json.JsonConvert.SerializeObject(this.Context), 3,
// this.Context.ActionBy,
// this.Context.Connection
// );
//    return Dt;
//}