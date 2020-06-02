using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TZ.CompExtention.Builder.Schema;
using TZ.Data;

namespace TZ.CompExtention.ImportTemplate
{
    public class Template
    {
        public enum TemplateType { 
        DIRECT,
        PIVOT,
        }
        public int ClientID { get; set; }
        public string TemplateID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public TemplateType Type { get; set; }
        public string ViewID { get; set; }
        public List<TemplateField> TemplateFields {get;set;}
        public ComponentView View { get; set; }
        public string Code { get; set; }        
        private string Connection { get; set; }
        public Template(string templateID,int clientID,string conn) {
            Connection = conn;
            TemplateID = templateID;
            ClientID = clientID;
            TemplateFields = new List<TemplateField>();
            Load();
        }
        public Template() {
            Connection = "";
            TemplateID = "";
            TemplateFields = new List<TemplateField>();
        }
        public Template(string conn,int clientID)
        {
            ClientID = clientID;
            Connection = conn;
            TemplateID = "";
            TemplateFields = new List<TemplateField>();
        }
        private void Load() {
            DataTable dt = new DataTable();
            DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(Connection);
            dt=itemp.GetImport(this.TemplateID, ClientID);
            string att = "";
            foreach (DataRow dr in dt.Rows) {
                Name = dr[TalentozTemplate.Name.Name] == null ? "" : dr[TalentozTemplate.Name.Name].ToString();
                Category = dr[TalentozTemplate.Category.Name] == null ? "" : dr[TalentozTemplate.Category.Name].ToString();
                Code = dr[TalentozTemplate.TemplateCode.Name] == null ? "" : dr[TalentozTemplate.TemplateCode.Name].ToString();
                Type = dr[TalentozTemplate.TemplateType.Name] == null ?  TemplateType.DIRECT : (TemplateType)Convert.ToInt32(dr[TalentozTemplate.TemplateType.Name]);               
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
                this.TemplateID = itemp.Save(ClientID,this.Name, this.Category, this.Code, this.ViewID, (int)this.Type, att, "");
                if (this.TemplateID != "")
                    return true;
                else
                    return false;
            }
            else {
                DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(Connection);
                string att = Newtonsoft.Json.JsonConvert.SerializeObject(this.TemplateFields);
                if (itemp.Update(ClientID, this.TemplateID, this.Name, this.Category, this.Code, this.ViewID, (int)this.Type, att, ""))
                    return true;
                else
                    return false;
            }           
        }
        protected internal bool Import(string conn)
        {
            DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(conn);
            string att = Newtonsoft.Json.JsonConvert.SerializeObject(this.TemplateFields);
            this.TemplateID = itemp.Import (this.TemplateID,ClientID, this.Name, this.Category, this.Code, this.ViewID, (int)this.Type, att, "");
            if (this.TemplateID != "")
                return true;
            else
                return false;
        }
        public bool Remove() {
            DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(Connection);
         return   itemp.Remove(this.TemplateID, ClientID);
        }

        public void LoadView(int clientID) {
            ComponentViewManager cvm = new ComponentViewManager(this.ViewID, clientID, new TZ.CompExtention.DataAccess.ComponentViewHandler(this.Connection, clientID));
            cvm.LoadViewComponents();
            this.View = (ComponentView)cvm.View;
            if (this.Type == TemplateType.PIVOT) {
                Attribute PivotCol = new Attribute();
              var cField=  this.TemplateFields.Where(x => x.IsColumn == true).FirstOrDefault();
                if (cField != null) {
                    foreach (Component c in this.View.Components)
                    {
                        int attCount = c.Attributes.Count;
                        for(int i=0;i< attCount; i++)
                        {
                            var att = c.Attributes[i];
                            if (att.ID == cField.ID)
                            {
                                PivotCol = att;

                                if (PivotCol.Type == AttributeType._lookup)
                                {
                                    var dm = new DataManager();
                                    dm.tableName("sys_fieldinstancelookup");
                                    dm.SelectFields("fieldinstanceid", "sys_FieldInstanceLookup", FieldType._string);
                                    dm.SelectFields("LookUpID", "sys_FieldInstanceLookup", FieldType._string);
                                    dm.SelectFields("LookUpDescription", "sys_FieldInstanceLookup", FieldType._string);
                                    dm.SelectFields("LookUpCode", "sys_FieldInstanceLookup", FieldType._string);
                                    dm.Where("sys_FieldInstanceLookup", "clientid", "=", Convert.ToString(clientID));
                                    dm.Where("sys_FieldInstanceLookup", "fieldinstanceid", "=", Convert.ToString(PivotCol.LookupInstanceID));
                                    var dt = dm.GetData(Connection);

                                    int index = 0;
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        bool isRequired = false;
                                        if (index == 0) {
                                            isRequired = true;
                                        }
                                        c.AddAttribute(new Attribute()
                                        {
                                            ID = PivotCol.ID + "_" + dr["LookUpID"].ToString(),
                                            Name = dr["LookUpDescription"].ToString(),
                                            DisplayName = dr["LookUpDescription"].ToString(),
                                            Type = AttributeType._string,
                                            IsKey = false,
                                            IsRequired = isRequired,
                                            ClientID = clientID,
                                            ComponentID = c.ID,
                                            LookupInstanceID = PivotCol.LookupInstanceID,
                                        });
                                        index = index + 1;

                                        this.TemplateFields.Add(new TemplateField()
                                        {
                                            ID = PivotCol.ID + "_" + dr["LookUpID"].ToString(),
                                            IsColumn = true,
                                            IsKey = false,
                                            IsDefault = true,
                                            IsRequired = isRequired,
                                        });
                                    }
                                }

                            }
                        }

                        c.Attributes.Remove(PivotCol);
                        var pField = this.TemplateFields.Where(x => x.IsPivot == true).FirstOrDefault();
                        if (pField != null) {
                            c.Attributes.Remove(c.Attributes.Where(x=> x.ID == pField.ID).FirstOrDefault());
                            this.TemplateFields.Remove(pField);
                        }
                    }  
                }   
            }           
        }
        public static List<Template> GetTemplates(int ClientID,string Connection) {
            DataTable dt = new DataTable();
            DataAccess.ImportTemplate itemp = new DataAccess.ImportTemplate(Connection);
            dt = itemp.GetImport(ClientID);
            string att = "";
            List < Template > Ts= new List<Template>(); 
            foreach (DataRow dr in dt.Rows)
            {
                att = "";
                   Template t = new Template(Connection, ClientID);
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
