using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.CompExtention.Builder;
namespace TZ.CompExtention.DataAccess
{
    public class ComponentDataHandler : IComponentDataAccess

    {
        private readonly string connection;
        public string Connection => connection;
        Builder.Data.ComponentBuilder cb;
        public ComponentDataHandler(string conn) {
            connection = conn;
            cb = new Builder.Data.ComponentBuilder(this.Connection);
        }
        public bool AddAttribute(string id, Attribute attr)
        {
            throw new NotImplementedException();
        }
        public List<Attribute> GetAttribute(string componentID,int clientID)
        {
           return ComponentDataHandler.GetAttributes(this.connection, componentID, clientID);
        }

        public List<Attribute> GetAllAttributes( int clientID)
        {
            return ComponentDataHandler.GetAttributes(this.connection,  clientID);
        }

        public Component GetComponent(string id)
        {
              cb = new Builder.Data.ComponentBuilder(this.connection);
            DataTable dt = new DataTable();
            dt = cb.GetComponent(id);
           /// List<Component> ComponentList = new List<Component>();
            foreach (DataRow dr in dt.Rows)
            {
                Component c = new Component();
                c.ID = dr[Builder.Schema.TalentozSchema.ComponentID.Name] != null ? dr[Builder.Schema.TalentozSchema.ComponentID.Name].ToString() : "";
                c.Name = dr[Builder.Schema.TalentozSchema.ComponentName.Name] != null ? dr[Builder.Schema.TalentozSchema.ComponentName.Name].ToString() : "";
                c.Type = dr[Builder.Schema.TalentozSchema.ComponentType.Name] != null ? (ComponentType)dr[Builder.Schema.TalentozSchema.ComponentType.Name] : ComponentType.core;
                c.TableName = dr[Builder.Schema.TalentozSchema.TableName.Name] != null ? dr[Builder.Schema.TalentozSchema.TableName.Name].ToString() : "";
                string s = dr[Builder.Schema.TalentozSchema.PrimaryKeys.Name] != null ? dr[Builder.Schema.TalentozSchema.PrimaryKeys.Name].ToString() : "";
                c.EntityKey = dr[Builder.Schema.TalentozSchema.EntityKey.Name] != null ? dr[Builder.Schema.TalentozSchema.EntityKey.Name].ToString() : "";
                //c. = dr[Builder.Schema.TalentozSchema.Title.Name] != null ? dr[Builder.Schema.TalentozSchema.Title.Name].ToString() : "";
                if (s != "")
                {
                    List<Attribute> keys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Attribute>>(s);
                    c.Keys = keys;
                }
                return c;
            }
            return null;
        }
        public List<Component> GetComponents()
        {
            throw new NotImplementedException();
        }
        public bool Remove(string ID)
        {
            throw new NotImplementedException();
        }
        public bool RemoveAttribute(string componentID, string attributeID)
        {
            throw new NotImplementedException();
        }
        public bool SaveComponent(Component component)
        {
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(this.Connection);
            component.ID= cb.SaveComponent(component.Name,"",Convert.ToInt32(component.Type),component.Name,
                Newtonsoft.Json.JsonConvert.SerializeObject(component.Keys),1,component.TableName,component.EntityKey);
            foreach (Attribute att in component.Attributes) {
                att.ComponentID = component.ID;
                if (att.LookupInstanceID == "") {
                    att.LookupInstanceID = "0";
                }
               att.ID = cb.SaveAttribute(att.Name, att.DisplayName, att.ComponentID, att.IsRequired,
                    att.IsUnique, att.IsCore, false, att.IsSecured, Convert.ToInt32(att.LookupInstanceID), Convert.ToInt32( att.Type), att.Length, att.DefaultValue, att.FileExtension, att.IsNullable,
                    att.IsKey, att.IsAuto,att.ComponentLookup, att.ComponentLookupDisplayField);
            }
            return true;
        }

        protected internal bool ImportComponent(Component component)
        {            
            component.ID = cb.ImportComponent (component.ID,component.Name, "", Convert.ToInt32(component.Type), component.Name,
                Newtonsoft.Json.JsonConvert.SerializeObject(component.Keys), 1, component.TableName, component.EntityKey);             
            return true;
        }
        protected internal bool ClearComponent() {
            cb.ClearComponent();
            return true;
        }

        public bool SaveAttribute(string compID,  Attribute att) {
            cb = new Builder.Data.ComponentBuilder(this.Connection);
            att.ID = cb.SaveClientAttribute(att.ID, att.Name, att.DisplayName, att.ComponentID, att.IsRequired,
                    att.IsUnique, att.IsCore, false, att.IsSecured, Convert.ToInt32(att.LookupInstanceID), Convert.ToInt32(att.Type), att.Length, att.DefaultValue, att.FileExtension, att.IsNullable,
                    att.IsKey, att.IsAuto, att.ComponentLookup, att.ComponentLookupDisplayField, att.ClientID);
            return true;
        }

        public static List<Component> GetComponents(string compIDs ,string conn)
        {
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(conn);
            DataTable dt = new DataTable();
            dt = cb.GetComponents(compIDs);
            List<Component> ComponentList = new List<Component>();
            foreach (DataRow dr in dt.Rows)
            {
                Component c = new Component();
                c.ID = dr[Builder.Schema.TalentozSchema.ComponentID.Name] != null ? dr[Builder.Schema.TalentozSchema.ComponentID.Name].ToString() : "";
                c.Name = dr[Builder.Schema.TalentozSchema.ComponentName.Name] != null ? dr[Builder.Schema.TalentozSchema.ComponentName.Name].ToString() : "";
                c.Type = dr[Builder.Schema.TalentozSchema.ComponentType.Name] != null ? (ComponentType)dr[Builder.Schema.TalentozSchema.ComponentType.Name] : ComponentType.core;
                c.TableName = dr[Builder.Schema.TalentozSchema.TableName.Name] != null ? dr[Builder.Schema.TalentozSchema.TableName.Name].ToString() : "";
                c.EntityKey = dr[Builder.Schema.TalentozSchema.EntityKey.Name] != null ? dr[Builder.Schema.TalentozSchema.EntityKey.Name].ToString() : "";
                string s = dr[Builder.Schema.TalentozSchema.PrimaryKeys.Name] != null ? dr[Builder.Schema.TalentozSchema.PrimaryKeys.Name].ToString() : "";
                //c. = dr[Builder.Schema.TalentozSchema.Title.Name] != null ? dr[Builder.Schema.TalentozSchema.Title.Name].ToString() : "";
                if (s != "")
                {
                    List<Attribute> keys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Attribute>>(s);
                    c.Keys = keys;
                }
                ComponentList.Add(c);
            }
            return ComponentList;
        }
        public static List<Component> GetComponents(string conn) {
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(conn);
            DataTable dt = new DataTable();
            dt= cb.GetComponents();
            List<Component> ComponentList = new List<Component>();
            foreach (DataRow dr in dt.Rows) {
                Component c = new Component();
                c.ID = dr[Builder.Schema.TalentozSchema.ComponentID.Name] != null ? dr[Builder.Schema.TalentozSchema.ComponentID.Name].ToString() : "";
                c.Name = dr[Builder.Schema.TalentozSchema.ComponentName.Name] != null ? dr[Builder.Schema.TalentozSchema.ComponentName.Name].ToString() : "";
                c.Type = dr[Builder.Schema.TalentozSchema.ComponentType.Name] != null ? (ComponentType)dr[Builder.Schema.TalentozSchema.ComponentType.Name] : ComponentType.core;
                c.TableName = dr[Builder.Schema.TalentozSchema.TableName.Name] != null ? dr[Builder.Schema.TalentozSchema.TableName.Name].ToString() : "";
                c.EntityKey = dr[Builder.Schema.TalentozSchema.EntityKey.Name] != null ? dr[Builder.Schema.TalentozSchema.EntityKey.Name].ToString() : "";
                string s= dr[Builder.Schema.TalentozSchema.PrimaryKeys.Name] != null ? dr[Builder.Schema.TalentozSchema.PrimaryKeys.Name].ToString() : "";
                //c. = dr[Builder.Schema.TalentozSchema.Title.Name] != null ? dr[Builder.Schema.TalentozSchema.Title.Name].ToString() : "";
                if (s != "") { 
                List<Attribute> keys= Newtonsoft.Json.JsonConvert.DeserializeObject<List<Attribute>>(s);
                    c.Keys = keys;
                }
                ComponentList.Add(c);
            }
            return ComponentList;
        }
        public static List<Attribute> GetAttributes(string conn, string compID, int pClientID) {
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(conn);
            DataTable dt = new DataTable();
            DataTable dtClientAtribute = new DataTable();

            dt = cb.GetAttribute( compID);
            dtClientAtribute = cb.GetClientAttribute(pClientID, compID);
            List<Attribute> atts = new List<Attribute>();
            for(int i = 0; i < dt.Rows.Count; i++) {
                //  foreach (DataRow dr in dt.Rows) {
                DataRow dr = dt.Rows[i];
                Attribute a = new Attribute();
                a.ClientID = 0;
                a.ComponentID = dr[Builder.Schema.TalentozSchemaInfo.ComponentID.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.ComponentID.Name].ToString() : "";
                a.ID = dr[Builder.Schema.TalentozSchemaInfo.FieldID.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.FieldID.Name].ToString() : "";
                if (dtClientAtribute.Rows.Count > 0) {
                    var clientRows = dtClientAtribute.Select("ComponentID='" + a.ComponentID + "' AND FieldID = '" + a.ID +"'");
                    if (clientRows.Count() > 0)
                    {
                        dr = clientRows[0];
                        a.ClientID = pClientID;
                    }
                }                      
               // a.ID = dr[Builder.Schema.TalentozSchemaInfo.ID.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.ID.Name].ToString() : "";
                a.Name = dr[Builder.Schema.TalentozSchemaInfo.AttributeName.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.AttributeName.Name].ToString() : "";
                a.DisplayName = dr[Builder.Schema.TalentozSchemaInfo.DisplayName.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.DisplayName.Name].ToString() : "";
            
                a.IsRequired = dr[Builder.Schema.TalentozSchemaInfo.IsRequired.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsRequired.Name]) : false;
                a.IsUnique = dr[Builder.Schema.TalentozSchemaInfo.IsUnique.Name] != null ?  Convert.ToBoolean( dr[Builder.Schema.TalentozSchemaInfo.IsUnique.Name] ): false;
                a.IsCore = dr[Builder.Schema.TalentozSchemaInfo.IsCore.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsCore.Name]) :false;
           //     a.Is = dr[Builder.Schema.TalentozSchemaInfo.IsReadOnly.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.IsReadOnly.Name].ToString() : "";
                a.IsSecured = dr[Builder.Schema.TalentozSchemaInfo.IsSecured.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsSecured.Name]) : false;
                a.IsNullable = dr[Builder.Schema.TalentozSchemaInfo.IsNullable.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsNullable.Name]) : false;
                a.IsKey = dr[Builder.Schema.TalentozSchemaInfo.ISPrimaryKey.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.ISPrimaryKey.Name]) : false;
                a.LookupInstanceID = dr[Builder.Schema.TalentozSchemaInfo.LookUpID.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.LookUpID.Name].ToString() : "";
                a.Type = dr[Builder.Schema.TalentozSchemaInfo.AttributeType.Name] != null ? (AttributeType)dr[Builder.Schema.TalentozSchemaInfo.AttributeType.Name] : AttributeType._string;
                a.Length = dr[Builder.Schema.TalentozSchemaInfo.Length.Name] != null ? Convert.ToInt32( dr[Builder.Schema.TalentozSchemaInfo.Length.Name]) : 0;
                a.DefaultValue = dr[Builder.Schema.TalentozSchemaInfo.DefaultValue.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.DefaultValue.Name].ToString() : "";
                a.FileExtension = dr[Builder.Schema.TalentozSchemaInfo.FileExtension.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.FileExtension.Name].ToString() : "";
                a.ComponentLookup = dr[Builder.Schema.TalentozSchemaInfo.LookupComponent.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.LookupComponent.Name].ToString() : "";
                a.ComponentLookupDisplayField = dr[Builder.Schema.TalentozSchemaInfo.ComponentLookupDisplayName.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.ComponentLookupDisplayName.Name].ToString() : "";
                a.IsAuto = dr[Builder.Schema.TalentozSchemaInfo.IsAuto.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsAuto.Name]) :false;
                atts.Add(a);
            }
            return atts;
        }

        public static List<Attribute> GetAttributes(string conn, int pClientID)
        {
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(conn);
            DataTable dt = new DataTable();
            dt = cb.GetAllAttributes();
            List<Attribute> atts = new List<Attribute>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //  foreach (DataRow dr in dt.Rows) {
                DataRow dr = dt.Rows[i];
                Attribute a = new Attribute();
                a.ClientID = 0;
                a.ComponentID = dr[Builder.Schema.TalentozSchemaInfo.ComponentID.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.ComponentID.Name].ToString() : "";
                a.ID = dr[Builder.Schema.TalentozSchemaInfo.FieldID.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.FieldID.Name].ToString() : "";              
                // a.ID = dr[Builder.Schema.TalentozSchemaInfo.ID.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.ID.Name].ToString() : "";
                a.Name = dr[Builder.Schema.TalentozSchemaInfo.AttributeName.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.AttributeName.Name].ToString() : "";
                a.DisplayName = dr[Builder.Schema.TalentozSchemaInfo.DisplayName.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.DisplayName.Name].ToString() : "";
                a.IsRequired = dr[Builder.Schema.TalentozSchemaInfo.IsRequired.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsRequired.Name]) : false;
                a.IsUnique = dr[Builder.Schema.TalentozSchemaInfo.IsUnique.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsUnique.Name]) : false;
                a.IsCore = dr[Builder.Schema.TalentozSchemaInfo.IsCore.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsCore.Name]) : false;
                //     a.Is = dr[Builder.Schema.TalentozSchemaInfo.IsReadOnly.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.IsReadOnly.Name].ToString() : "";
                a.IsSecured = dr[Builder.Schema.TalentozSchemaInfo.IsSecured.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsSecured.Name]) : false;
                a.IsNullable = dr[Builder.Schema.TalentozSchemaInfo.IsNullable.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsNullable.Name]) : false;
                a.IsKey = dr[Builder.Schema.TalentozSchemaInfo.ISPrimaryKey.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.ISPrimaryKey.Name]) : false;
                a.LookupInstanceID = dr[Builder.Schema.TalentozSchemaInfo.LookUpID.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.LookUpID.Name].ToString() : "";
                a.Type = dr[Builder.Schema.TalentozSchemaInfo.AttributeType.Name] != null ? (AttributeType)dr[Builder.Schema.TalentozSchemaInfo.AttributeType.Name] : AttributeType._string;
                a.Length = dr[Builder.Schema.TalentozSchemaInfo.Length.Name] != null ? Convert.ToInt32(dr[Builder.Schema.TalentozSchemaInfo.Length.Name]) : 0;
                a.DefaultValue = dr[Builder.Schema.TalentozSchemaInfo.DefaultValue.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.DefaultValue.Name].ToString() : "";
                a.FileExtension = dr[Builder.Schema.TalentozSchemaInfo.FileExtension.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.FileExtension.Name].ToString() : "";
                a.ComponentLookup = dr[Builder.Schema.TalentozSchemaInfo.LookupComponent.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.LookupComponent.Name].ToString() : "";
                a.ComponentLookupDisplayField = dr[Builder.Schema.TalentozSchemaInfo.ComponentLookupDisplayName.Name] != null ? dr[Builder.Schema.TalentozSchemaInfo.ComponentLookupDisplayName.Name].ToString() : "";
                a.IsAuto = dr[Builder.Schema.TalentozSchemaInfo.IsAuto.Name] != null ? Convert.ToBoolean(dr[Builder.Schema.TalentozSchemaInfo.IsAuto.Name]) : false;
                atts.Add(a);
            }
            return atts;
        }
        public bool UpdateComponent(Component component)
        {
            //
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(this.Connection);
            if (cb.UpdateComponent(component.ID, component.Name, (int)component.Type, component.Title, component.Category, 
                Newtonsoft.Json.JsonConvert.SerializeObject(component.Keys), component.EntityKey))
            {
                foreach (Attribute att in component.Attributes)
                {
                    if (att.ID != "")
                    {
                        cb.UpdateComponentAttribute( component.ID, att.ID, att.DisplayName, att.IsRequired, att.IsUnique, att.IsCore, 
                            false, att.IsSecured,
                     Convert.ToInt32(att.LookupInstanceID), (int)att.Type, att.Length, att.DefaultValue, att.FileExtension, att.ComponentLookup,
                     att.ComponentLookupDisplayField
                        );
                    }
                    else {
                        cb.SaveAttribute(att.Name, att.DisplayName, att.ComponentID, att.IsRequired, att.IsUnique, att.IsCore, false, 
                            att.IsSecured,Convert.ToInt32( att.LookupInstanceID),(int) att.Type, att.Length, att.DefaultValue, 
                            att.FileExtension, att.IsNullable, att.IsKey, att.IsAuto, att.ComponentLookup, att.ComponentLookupDisplayField);                         
                    }              

                }
                return true;
            }
            else {
                return false;
            }       
             
        }
        public bool UpdateAttribute(string compID,Attribute att) {
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(this.Connection);
           return cb.UpdateComponentClientAttribute(att.ClientID,compID, att.ID, att.DisplayName, att.IsRequired, att.IsUnique, att.IsCore,
                         false, att.IsSecured,
                  Convert.ToInt32(att.LookupInstanceID), (int)att.Type, att.Length, att.DefaultValue, att.FileExtension, att.ComponentLookup,
                  att.ComponentLookupDisplayField
                     );
        }

        public bool UpdateComponentLookup(int clientID,string componentID, string attributeID, string componentLookUp, string displayName)
        {
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(this.Connection);
            return cb.UpdateComponentLookUp(componentID, attributeID, componentLookUp, displayName);
        }
    }
}
