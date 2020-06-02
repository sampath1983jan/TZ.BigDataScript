using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;
using Tech.Data.Schema;

namespace TZ.CompExtention.Builder.Data
{
 public   class Setup : DataBase
    {
        private IEnumerable<DBSchemaItemRef> tables;
        DBDatabase db;
        public Setup(string conn)
        {
            InitDbs(conn);
            db = base.Database;
           
        }
        public void Clear() {
            DBQuery[] all = new DBQuery[] {
            DBQuery.Drop.Table(base.Schema, Builder.Schema.TalentozSchema.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, Builder.Schema.TalentozSchemaInfo.Table).IfExists(),
                 DBQuery.Drop.Table(base.Schema, Builder.Schema.TalentozSchemaClientInfo.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, Builder.Schema.TalentozView.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, Builder.Schema.TalentozViewSchema.Table).IfExists(),
           DBQuery.Drop.Table(base.Schema, Builder.Schema.TalentozTemplate.Table).IfExists()     ,
             DBQuery.Drop.Table(base.Schema, Builder.Schema.TalentozViewSchemaRelation.Table).IfExists(),
             DBQuery.Drop.Table(base.Schema, Builder.Schema.TalentozImportLog.Table).IfExists()
             
            };
            foreach (DBQuery q in all)
            {
                try
                {
                    db.ExecuteNonQuery(q);
                }
                catch (System.Exception ex)
                {
                }
            }
        }       
        private void CreatePrimaryKeys(string pks, string table) {
            string[] keys = pks.Split(',');
            //if (this.PrimaryKey.Count == 0)
            //{
            //    return true;
            //}
            //ALTER TABLE `talentozdev`.`cf_fields` 
            //DROP PRIMARY KEY
            //, ADD PRIMARY KEY(`FieldID`);
            string tb = "ALTER TABLE `" + base.Schema + "`.`" + table + "`";
            //if (isdrop)
            //{
            //    tb = tb + " DROP PRIMARY KEY, ";
            //}
            tb = tb + " Add PRIMARY KEY(";
            int i = 0;
            string fields ="";
            foreach (string dc in keys)
            {
                fields = fields + ",`" + dc + "`";
            }
            if (fields.StartsWith(",")) {
                fields = fields.Substring(1);
            }
            tb = tb + fields;
            tb = tb + ")";
            try
            {
                int r = db.ExecuteNonQuery(tb);              
            }
            catch (System.Exception ex)
            {
               // throw new Exception.TableSchemaException(this.Table, fields, Exception.OperaitonType.addkey, ex.Message);
            }
        }
        public void Install() {
            DBSchemaProvider provider = db.GetSchemaProvider();
            tables = provider.GetAllTables();
            DBSchemaItemRef table = tables.Where(x => x.Name.ToLower() == Builder.Schema.TalentozSchema.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, Builder.Schema.TalentozSchema.Table)
                                        .Add(Builder.Schema.TalentozSchema.ComponentID)
                                        .Add(Builder.Schema.TalentozSchema.ComponentName)
                                        .Add(Builder.Schema.TalentozSchema.Category)
                                        .Add(Builder.Schema.TalentozSchema.ComponentType)
                                        .Add(Builder.Schema.TalentozSchema.Title)
                                        .Add(Builder.Schema.TalentozSchema.PrimaryKeys)
                                        .Add(Builder.Schema.TalentozSchema.TableName)
                                        .Add(Builder.Schema.TalentozSchema.IsGlobal)
                                        .Add(Builder.Schema.TalentozSchema.ComponentState)
                                           .Add(Builder.Schema.TalentozSchema.EntityKey)
                                        .Add(Builder.Schema.TalentozSchema.LastUPD);
                                        //.Add(TzAccount.Client.ServerID)    .Add(Builder.Schema.TalentozSchema.OrganizationName);
                db.ExecuteNonQuery(create);
                CreatePrimaryKeys(Builder.Schema.TalentozSchema.ComponentID.Name, Builder.Schema.TalentozSchema.Table);

            }
            else
            {
                throw new System.Exception(Builder.Schema.TalentozSchema.Table + " table Name exist");
            }
              table = tables.Where(x => x.Name.ToLower() == Builder.Schema.TalentozSchemaInfo .Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, Builder.Schema.TalentozSchemaInfo.Table)                                      
                                          .Add(Builder.Schema.TalentozSchemaInfo.ComponentID)
                                        .Add(Builder.Schema.TalentozSchemaInfo.FieldID)
                                        .Add(Builder.Schema.TalentozSchemaInfo.AttributeName)
                                             .Add(Builder.Schema.TalentozSchemaInfo.DisplayName)
                                        .Add(Builder.Schema.TalentozSchemaInfo.IsRequired)
                                        .Add(Builder.Schema.TalentozSchemaInfo.IsUnique)
                                        .Add(Builder.Schema.TalentozSchemaInfo.IsCore)
                                        .Add(Builder.Schema.TalentozSchemaInfo.IsReadOnly)
                                        .Add(Builder.Schema.TalentozSchemaInfo.IsSecured)
                                        .Add(Builder.Schema.TalentozSchemaInfo.IsNullable)
                                        .Add(Builder.Schema.TalentozSchemaInfo.ISPrimaryKey)
                                        .Add(Builder.Schema.TalentozSchemaInfo.IsAuto)
                                        .Add(Builder.Schema.TalentozSchemaInfo.LookUpID)
                                        .Add(Builder.Schema.TalentozSchemaInfo.AttributeType)
                                        .Add(Builder.Schema.TalentozSchemaInfo.Length)
                                        .Add(Builder.Schema.TalentozSchemaInfo.DefaultValue)
                                        .Add(Builder.Schema.TalentozSchemaInfo.FileExtension)
                                        .Add(Builder.Schema.TalentozSchemaInfo.LookupComponent )
                                        .Add(Builder.Schema.TalentozSchemaInfo.ComponentLookupDisplayName)
                                        .Add(Builder.Schema.TalentozSchemaInfo.RegExp)
                                        .Add(Builder.Schema.TalentozSchemaInfo.LastUPD);
                //.Add(TzAccount.Client.ServerID)    .Add(Builder.Schema.TalentozSchema.OrganizationName);
                db.ExecuteNonQuery(create);
                CreatePrimaryKeys (Builder.Schema.TalentozSchemaInfo.ComponentID.Name +","+ Builder.Schema.TalentozSchemaInfo.FieldID.Name
                    , Builder.Schema.TalentozSchemaInfo.Table);
            }
            else
            {
                throw new System.Exception(Builder.Schema.TalentozSchemaInfo.Table + "  table Name exist");
            }

            table = tables.Where(x => x.Name.ToLower() == Builder.Schema.TalentozSchemaClientInfo.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, Builder.Schema.TalentozSchemaClientInfo.Table)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.ClientID)
                                          .Add(Builder.Schema.TalentozSchemaClientInfo.ComponentID)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.FieldID)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.AttributeName)
                                             .Add(Builder.Schema.TalentozSchemaClientInfo.DisplayName)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.IsRequired)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.IsUnique)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.IsCore)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.IsReadOnly)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.IsSecured)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.IsNullable)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.ISPrimaryKey)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.IsAuto)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.LookUpID)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.AttributeType)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.Length)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.DefaultValue)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.FileExtension)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.LookupComponent)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.ComponentLookupDisplayName)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.RegExp)
                                        .Add(Builder.Schema.TalentozSchemaClientInfo.LastUPD);
                //.Add(TzAccount.Client.ServerID)    .Add(Builder.Schema.TalentozSchema.OrganizationName);
                db.ExecuteNonQuery(create);
                CreatePrimaryKeys(Builder.Schema.TalentozSchemaClientInfo.ClientID.Name + "," + Builder.Schema.TalentozSchemaClientInfo.ComponentID.Name + "," + Builder.Schema.TalentozSchemaClientInfo.FieldID.Name
                    , Builder.Schema.TalentozSchemaClientInfo.Table);
            }
            else
            {
                throw new System.Exception(Builder.Schema.TalentozSchemaClientInfo.Table + "  table Name exist");
            }

            table = tables.Where(x => x.Name.ToLower() == Builder.Schema.TalentozView.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, Builder.Schema.TalentozView.Table)
                                        .Add(Builder.Schema.TalentozView.ViewID)
                                        .Add(Builder.Schema.TalentozView.CoreComponent)
                                        .Add(Builder.Schema.TalentozView.Name)
                                        .Add(Builder.Schema.TalentozView.Catgory)                           
                                        .Add(Builder.Schema.TalentozView.LastUPD);
                //.Add(TzAccount.Client.ServerID)    .Add(Builder.Schema.TalentozSchema.OrganizationName);
                db.ExecuteNonQuery(create);
                CreatePrimaryKeys(Builder.Schema.TalentozView.ViewID.Name 
                   , Builder.Schema.TalentozView.Table);
            }
            else
            {
                throw new System.Exception(Builder.Schema.TalentozView.Table + "  table Name exist");
            }

            table = tables.Where(x => x.Name.ToLower() == Builder.Schema.TalentozViewSchema.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, Builder.Schema.TalentozViewSchema.Table)
                                        .Add(Builder.Schema.TalentozViewSchema.ViewID)
                                        .Add(Builder.Schema.TalentozViewSchema.ComponentID)
                                        .Add(Builder.Schema.TalentozViewSchema.ChildComponentID)
                                        .Add(Builder.Schema.TalentozViewSchema.ComponentAlias)
                                        .Add(Builder.Schema.TalentozViewSchema.LastUPD);
                //.Add(TzAccount.Client.ServerID)    .Add(Builder.Schema.TalentozSchema.OrganizationName);
                db.ExecuteNonQuery(create);
               // CreatePrimaryKeys(Builder.Schema.TalentozViewSchema.ViewID.Name
               //, Builder.Schema.TalentozView.Table);
            }
            else
            {
                throw new System.Exception(Builder.Schema.TalentozViewSchema.Table + "  table Name exist");
            }

            table = tables.Where(x => x.Name.ToLower() == Builder.Schema.TalentozViewSchemaRelation.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, Builder.Schema.TalentozViewSchemaRelation.Table)
                                        .Add(Builder.Schema.TalentozViewSchemaRelation.ViewID)
                                        .Add(Builder.Schema.TalentozViewSchemaRelation.ViewSchemaRelation)
                                        .Add(Builder.Schema.TalentozViewSchemaRelation.ParentField)
                                        .Add(Builder.Schema.TalentozViewSchemaRelation.RelatedField)
                                        .Add(Builder.Schema.TalentozViewSchemaRelation.Parent)
                                         .Add(Builder.Schema.TalentozViewSchemaRelation.Child)
                                        .Add(Builder.Schema.TalentozViewSchemaRelation.LastUPD);              
                db.ExecuteNonQuery(create);
                CreatePrimaryKeys(Builder.Schema.TalentozViewSchemaRelation.ViewID.Name + "," +
                    Builder.Schema.TalentozViewSchemaRelation.ViewSchemaRelation.Name  
               , Builder.Schema.TalentozViewSchemaRelation.Table);
            }
            else
            {
                throw new System.Exception(Builder.Schema.TalentozViewSchemaRelation.Table + "  table Name exist");
            }
            table = tables.Where(x => x.Name.ToLower() == Builder.Schema.TalentozTemplate.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, Builder.Schema.TalentozTemplate.Table)
                      .Add(Builder.Schema.TalentozTemplate.ClientID )
                                        .Add(Builder.Schema.TalentozTemplate.TemplateID)
                                          .Add(Builder.Schema.TalentozTemplate.Name)
                                        .Add(Builder.Schema.TalentozTemplate.TemplateCode)
                                        .Add(Builder.Schema.TalentozTemplate.ViewFields)
                                             .Add(Builder.Schema.TalentozTemplate.TemplateType)
                                              .Add(Builder.Schema.TalentozTemplate.PivotColumn)
                                               .Add(Builder.Schema.TalentozTemplate.Category)
                                        .Add(Builder.Schema.TalentozTemplate.ViewID)                                         
                                        .Add(Builder.Schema.TalentozTemplate.LastUPD);               
                db.ExecuteNonQuery(create);
                CreatePrimaryKeys(Builder.Schema.TalentozSchemaClientInfo.ClientID.Name + "," + Builder.Schema.TalentozTemplate.TemplateID.Name
                    , Builder.Schema.TalentozTemplate.Table);
            }
            else
            {
                throw new System.Exception(Builder.Schema.TalentozTemplate.Table + "  table Name exist");
            }

            table = tables.Where(x => x.Name.ToLower() == Builder.Schema.TalentozImportLog.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, Builder.Schema.TalentozImportLog.Table)
                                        .Add(Builder.Schema.TalentozImportLog.ImportID)
                                        .Add(Builder.Schema.TalentozImportLog.ClientID)
                                         .Add(Builder.Schema.TalentozImportLog.ImportSchema)
                                        .Add(Builder.Schema.TalentozImportLog.ActionBy)
                                        .Add(Builder.Schema.TalentozImportLog.Step)  
                                        .Add(Builder.Schema.TalentozImportLog.LastUPD);
                db.ExecuteNonQuery(create);
                CreatePrimaryKeys(Builder.Schema.TalentozSchemaClientInfo.ClientID.Name + "," + Builder.Schema.TalentozImportLog.ImportID.Name
                    , Builder.Schema.TalentozImportLog.Table);
            }
            else
            {
                throw new System.Exception(Builder.Schema.TalentozImportLog.Table + "  table Name exist");
            }

        }
    }
}
