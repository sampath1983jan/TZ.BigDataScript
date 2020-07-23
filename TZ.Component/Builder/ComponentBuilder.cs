using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using TZ.CompExtention.DataAccess;

namespace TZ.CompExtention
{
    public class ComponentBuilder
    {
        private string Connection { get; set; }
        public ComponentBuilder(string conn) {
            Connection = conn;
        }

        public List<CompExtention.Component> GetTableAsComponent()
        {
            List<string> Tables = new List<string>();
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(this.Connection);
            List<CompExtention.Component> CompList;
                CompList = ComponentDataHandler.GetComponents(this.Connection);            
            Tables= cb.GetTables();
 
            foreach (string s in Tables) {
                var c =CompList.Where(x => x.TableName == s).FirstOrDefault();
                if (c == null) {
                    var comp = new Component(s, ComponentType.none);
                    comp.TableName = s;
                    CompList.Add(comp);
                }
            }            
            return CompList.OrderBy(x => x.TableName).ToList();
        }

        public List<Attribute> GetTableFields(string tbName,int clientID) {
            Builder.Data.ComponentBuilder cb = new Builder.Data.ComponentBuilder(this.Connection);
            
          Tech.Data.Schema.DBSchemaTable  tbl =  cb.GetTable(tbName);
            List<Attribute> atts = new List<Attribute>();
            try {
              
                foreach (Tech.Data.Schema.DBSchemaTableColumn c in tbl.Columns)
                {
                    Attribute at = new Attribute();
                    at.Name = c.Name;
                    at.ClientID = clientID;
                    at.DisplayName = c.Name;
                    at.IsNullable = c.Nullable;
                    at.IsKey = c.PrimaryKey;
                    at.IsRequired = !c.Nullable;
                    at.IsUnique = c.PrimaryKey;
                    if (c.Size > 0)
                    {
                        at.Length = c.Size;
                    }
                    at.Max = c.Size;
                    at.DefaultValue = c.DefaultValue;

                    at.Type = Attribute.GetAttributeType(c.DbType);
                    atts.Add(at);
                }
            }
            catch (Exception ex) {
                 
            }

            return atts;

        }

     
    }
}
