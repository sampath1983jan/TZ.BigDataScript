using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TZ.CompExtention.Builder.Schema;

namespace TZ.CompExtention.ImportTemplate
{
    public class Template
    {
        public enum TemplateType { 
        DIRECT,
        PIVOT,
        }
        public string TemplateID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public TemplateType Type { get; set; }
        public string ViewID { get; set; }
        public List<TemplateField> TemplateFields {get;set;}
        public ComponentView View { get; set; }
        public string Code { get; set; }        
        private string Connection { get; set; }
        public Template(string templateID,string conn) {
            Connection = conn;
            TemplateID = templateID;
            TemplateFields = new List<TemplateField>();
            Load();
        }
        public Template() {
            Connection = "";
            TemplateID = "";
            TemplateFields = new List<TemplateField>();
        }
        public Template(string conn)
        {
            Connection = conn;
            TemplateID = "";
            TemplateFields = new List<TemplateField>();
        }
        private void Load() {
            DataTable dt = new DataTable();
            DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(Connection);
            dt=itemp.GetImport(this.TemplateID);
            string att = "";
            foreach (DataRow dr in dt.Rows) {
                Name = dr[TalentozTemplate.Name.Name] == null ? "" : dr[TalentozTemplate.Name.Name].ToString();
                Category = dr[TalentozTemplate.Category.Name] == null ? "" : dr[TalentozTemplate.Category.Name].ToString();
                Code = dr[TalentozTemplate.TemplateCode.Name] == null ? "" : dr[TalentozTemplate.TemplateCode.Name].ToString();
                Type = dr[TalentozTemplate.TemplateType.Name] == null ?  TemplateType.DIRECT : (TemplateType)Convert.ToInt32(dr[TalentozTemplate.TemplateType.Name]);
              //  PivotColumn = dr[TalentozTemplate.PivotColumn.Name] == null ? "" : dr[TalentozTemplate.PivotColumn.Name].ToString();
                ViewID = dr[TalentozTemplate.ViewID.Name] == null ? "" : dr[TalentozTemplate.ViewID.Name].ToString();
                att = dr[TalentozTemplate.ViewFields.Name] == null ? "" : dr[TalentozTemplate.ViewFields.Name].ToString();
                TemplateFields = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateField>>(att);
            }
        }
        public bool Save() {
            if (this.TemplateID == "")
            {
                DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(Connection);
                string att = Newtonsoft.Json.JsonConvert.SerializeObject(this.TemplateFields);
                this.TemplateID = itemp.Save(this.Name, this.Category, this.Code, this.ViewID, (int)this.Type, att, "");
                if (this.TemplateID != "")
                    return true;
                else
                    return false;
            }
            else {
                DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(Connection);
                string att = Newtonsoft.Json.JsonConvert.SerializeObject(this.TemplateFields);
                if (itemp.Update(this.TemplateID, this.Name, this.Category, this.Code, this.ViewID, (int)this.Type, att, ""))
                    return true;
                else
                    return false;
            }           
        }
        public bool Remove() {
            DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(Connection);
         return   itemp.Remove(this.TemplateID);
        }

        public void LoadView(int clientID) {
            ComponentViewManager cvm = new ComponentViewManager(this.ViewID, clientID, new TZ.CompExtention.DataAccess.ComponentViewHandler(this.Connection, clientID
 ));
            cvm.LoadViewComponents();
            this.View = (ComponentView)cvm.View;
        }
        public static List<Template> GetTemplates(string Connection) {
            DataTable dt = new DataTable();
            DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(Connection);
            dt = itemp.GetImport();
            string att = "";
            List < Template > Ts= new List<Template>(); 
            foreach (DataRow dr in dt.Rows)
            {
                att = "";
                   Template t = new Template(Connection);
                t.TemplateID = dr[TalentozTemplate.TemplateID.Name] == null ? "" : dr[TalentozTemplate.TemplateID.Name].ToString();
                t.Name = dr[TalentozTemplate.Name.Name] == null ? "" : dr[TalentozTemplate.Name.Name].ToString();
                t.Category = dr[TalentozTemplate.Category.Name] == null ? "" : dr[TalentozTemplate.Category.Name].ToString();
                t.Code = dr[TalentozTemplate.TemplateCode.Name] == null ? "" : dr[TalentozTemplate.TemplateCode.Name].ToString();
                t.Type = dr[TalentozTemplate.TemplateType.Name] == null ? TemplateType.DIRECT : (TemplateType)Convert.ToInt32(dr[TalentozTemplate.TemplateType.Name]);
                t.ViewID = dr[TalentozTemplate.ViewID.Name] == null ? "" : dr[TalentozTemplate.ViewID.Name].ToString();
                att = dr[TalentozTemplate.ViewFields.Name] == null ? "" : dr[TalentozTemplate.Name.Name].ToString();              
                att = dr[TalentozTemplate.ViewFields.Name] == null ? "" : dr[TalentozTemplate.ViewFields.Name].ToString();
                t.TemplateFields = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateField>>(att);
                Ts.Add(t);
            }
            return Ts;
        }
        
    }
}
