using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Schema;
using Tech.Data.Query;
using TZ.CompExtention.Builder.Schema;
using System.Data;
using TZ.Data;

namespace TZ.CompExtention.Builder.Data
{
    public class ComponentBuilder : DataBase
    {
        private DBDatabase db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public ComponentBuilder(string conn)
        {
            InitDbs(conn);
            db = base.Database;
        }
        private CSVItem[] all;
        private CSVItem itm;
        private DBQuery insert;
        private void prepareInsertStatement(string state, string tb, string pFields)
        {
            List<DBClause> cs = new List<DBClause>();
            CSVData data = CSVData.ParseString(string.Join(Environment.NewLine, state), true);
            all = data.Items;
            List<string> c = new List<string>();
            string[] flds;
            flds = pFields.Split(System.Convert.ToChar(","));
            foreach (string s in flds)
            {
                cs.Add(DBParam.ParamWithDelegate(() => itm[data.GetOffset(s)]));
                c.Add(s);
            }
            insert = DBQuery.InsertInto(tb).Fields(c.ToArray()).Values(cs.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetTables()
        {
            List<DBSchemaItemRef> tables;
            List<string> tbls = new List<string>();
            DBSchemaProvider provider = db.GetSchemaProvider();
            tables = provider.GetAllTables().ToList();
            foreach (DBSchemaItemRef s in tables)
            {
                tbls.Add(s.Name);
            }
            return tbls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        public DBSchemaTable GetTable(string tbName)
        {
            DBSchemaProvider provider = db.GetSchemaProvider();
            return provider.GetTable(tbName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="category"></param>
        /// <param name="schemaType"></param>
        /// <param name="title"></param>
        /// <param name="primarykeys"></param>
        /// <param name="state"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected internal string SaveComponent(string pName,
            string category,
            int schemaType,
            string title,
            string primarykeys,
            int state,
            string tableName,
            string entityKey
            )
        {
            string ComponentId = Shared.generateID();
            Tech.Data.Query.DBQuery dBQuery = DBQuery.InsertInto(TalentozSchema.Table)
                .Field(TalentozSchema.ComponentID.Name)
                 .Field(TalentozSchema.ComponentName.Name)
                  .Field(TalentozSchema.Category.Name)
                   .Field(TalentozSchema.ComponentType.Name)
                    .Field(TalentozSchema.Title.Name)
                      .Field(TalentozSchema.ComponentState.Name)
                      .Field(TalentozSchema.TableName.Name)
                       .Field(TalentozSchema.LastUPD.Name)
                        .Field(TalentozSchema.PrimaryKeys.Name)
                          .Field(TalentozSchema.EntityKey.Name)
                      .Values(DBConst.String(ComponentId),
                DBConst.String(pName),
                DBConst.String(category),
                DBConst.Int32(schemaType),
                DBConst.String(title),
                DBConst.Int32(state),
                DBConst.String(tableName),
                 DBConst.DateTime(DateTime.Today),
                    DBConst.String(primarykeys),
                    DBConst.String(entityKey));
            if (db.ExecuteNonQuery(dBQuery) > 0)
            {
                return ComponentId;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="componentID"></param>
        /// <param name="isRequired"></param>
        /// <param name="isUnique"></param>
        /// <param name="isCore"></param>
        /// <param name="isReadonly"></param>
        /// <param name="isSecured"></param>
        /// <param name="lookupId"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <param name="defaultValue"></param>
        /// <param name="fileExtension"></param>
        /// <param name="isNull"></param>
        /// <param name="isPrimary"></param>
        /// <param name="isAuto"></param>
        /// <returns></returns>
        protected internal string SaveAttribute(string name,string displayName, string componentID,
            bool isRequired, bool isUnique, bool isCore,
            bool isReadonly,
            bool isSecured,
            int lookupId,
            int type,
            int length,
            string defaultValue,
            string fileExtension,
            bool isNull,
            bool isPrimary,
            bool isAuto,
            string lookupComponent,
            string componentDisplayField)
        {
            string attID = Shared.generateID();
            Tech.Data.Query.DBQuery dBQuery = DBQuery.InsertInto(TalentozSchemaInfo.Table)
                .Field(TalentozSchemaInfo.ComponentID.Name).Field(TalentozSchemaInfo.FieldID.Name).Field(TalentozSchemaInfo.AttributeName.Name)
                  .Field(TalentozSchemaInfo.DisplayName .Name).Field(TalentozSchemaInfo.IsRequired.Name).Field(TalentozSchemaInfo.IsUnique.Name)
                      .Field(TalentozSchemaInfo.IsCore.Name).Field(TalentozSchemaInfo.IsReadOnly.Name).Field(TalentozSchemaInfo.IsSecured.Name)
                       .Field(TalentozSchemaInfo.LookUpID.Name).Field(TalentozSchemaInfo.AttributeType.Name).Field(TalentozSchemaInfo.Length.Name)                        
                       .Field(TalentozSchemaInfo.DefaultValue.Name).Field(TalentozSchemaInfo.FileExtension.Name)//  .Field(TalentozSchemaInfo.RegExp.Name)
                       .Field(TalentozSchemaInfo.IsNullable.Name)
                       .Field(TalentozSchemaInfo.ISPrimaryKey.Name)
                      .Field(TalentozSchemaInfo.IsAuto.Name)                        
                            .Field(TalentozSchemaInfo.LookupComponent.Name)
                               .Field(TalentozSchemaInfo.ComponentLookupDisplayName.Name)
                                .Field(TalentozSchemaInfo.LastUPD.Name)                                  
                      .Values(DBConst.String(componentID),
                DBConst.String(attID),
                DBConst.String(name),
                DBConst.String(displayName),
                DBConst.Const(System.Data.DbType.Boolean,isRequired),
                DBConst.Const(System.Data.DbType.Boolean,isUnique),
                DBConst.Const(System.Data.DbType.Boolean, isCore),
                DBConst.Const(System.Data.DbType.Boolean, isReadonly),
                 DBConst.Const(System.Data.DbType.Boolean, isSecured),
                      DBConst.Const(System.Data.DbType.Int32, lookupId),
            DBConst.Const(System.Data.DbType.Int32, type),
            DBConst.Const(System.Data.DbType.Int32, length),
            DBConst.Const(System.Data.DbType.String, defaultValue),
            DBConst.Const(System.Data.DbType.String, fileExtension),
            DBConst.Const(System.Data.DbType.Boolean, isNull),
            DBConst.Const(System.Data.DbType.Boolean, isPrimary),
            DBConst.Const(System.Data.DbType.Boolean, isAuto)    ,
               DBConst.Const(System.Data.DbType.String, lookupComponent),
                  DBConst.Const(System.Data.DbType.String, componentDisplayField),
                   DBConst.Const(System.Data.DbType.DateTime, DateTime.Today)                      
                  );
            if (db.ExecuteNonQuery(dBQuery) > 0)
            {
                return attID;

            }
            else
            {
                return "";
            }
        }

        protected internal string SaveClientAttribute(string id,string name, string displayName, string componentID,
            bool isRequired, bool isUnique, bool isCore,
            bool isReadonly,
            bool isSecured,
            int lookupId,
            int type,
            int length,
            string defaultValue,
            string fileExtension,
            bool isNull,
            bool isPrimary,
            bool isAuto,
            string lookupComponent,
            string componentDisplayField, int clientid)
        {
            string attID = id;
            Tech.Data.Query.DBQuery dBQuery = DBQuery.InsertInto(TalentozSchemaClientInfo.Table)
                .Field(TalentozSchemaClientInfo.ComponentID.Name).Field(TalentozSchemaClientInfo.FieldID.Name).Field(TalentozSchemaClientInfo.AttributeName.Name)
                  .Field(TalentozSchemaClientInfo.DisplayName.Name).Field(TalentozSchemaClientInfo.IsRequired.Name).Field(TalentozSchemaClientInfo.IsUnique.Name)
                      .Field(TalentozSchemaClientInfo.IsCore.Name).Field(TalentozSchemaClientInfo.IsReadOnly.Name).Field(TalentozSchemaClientInfo.IsSecured.Name)
                       .Field(TalentozSchemaClientInfo.LookUpID.Name).Field(TalentozSchemaClientInfo.AttributeType.Name).Field(TalentozSchemaClientInfo.Length.Name)
                       .Field(TalentozSchemaClientInfo.DefaultValue.Name).Field(TalentozSchemaClientInfo.FileExtension.Name)//  .Field(TalentozSchemaClientInfo.RegExp.Name)
                       .Field(TalentozSchemaClientInfo.IsNullable.Name)
                       .Field(TalentozSchemaClientInfo.ISPrimaryKey.Name)
                      .Field(TalentozSchemaClientInfo.IsAuto.Name)
                            .Field(TalentozSchemaClientInfo.LookupComponent.Name)
                               .Field(TalentozSchemaClientInfo.ComponentLookupDisplayName.Name)
                                .Field(TalentozSchemaClientInfo.LastUPD.Name)
                                   .Field(TalentozSchemaClientInfo.ClientID.Name)
                      .Values(DBConst.String(componentID),
                DBConst.String(attID),
                DBConst.String(name),
                DBConst.String(displayName),
                DBConst.Const(System.Data.DbType.Boolean, isRequired),
                DBConst.Const(System.Data.DbType.Boolean, isUnique),
                DBConst.Const(System.Data.DbType.Boolean, isCore),
                DBConst.Const(System.Data.DbType.Boolean, isReadonly),
                 DBConst.Const(System.Data.DbType.Boolean, isSecured),
                      DBConst.Const(System.Data.DbType.Int32, lookupId),
            DBConst.Const(System.Data.DbType.Int32, type),
            DBConst.Const(System.Data.DbType.Int32, length),
            DBConst.Const(System.Data.DbType.String, defaultValue),
            DBConst.Const(System.Data.DbType.String, fileExtension),
            DBConst.Const(System.Data.DbType.Boolean, isNull),
            DBConst.Const(System.Data.DbType.Boolean, isPrimary),
            DBConst.Const(System.Data.DbType.Boolean, isAuto),
               DBConst.Const(System.Data.DbType.String, lookupComponent),
                  DBConst.Const(System.Data.DbType.String, componentDisplayField),
                   DBConst.Const(System.Data.DbType.DateTime, DateTime.Today),
                     DBConst.Const(System.Data.DbType.Int32, clientid)
                  );
            if (db.ExecuteNonQuery(dBQuery) > 0)
            {
                return attID;

            }
            else
            {
                return "";
            }
        }


        protected internal bool UpdateComponent( string componentID, string name, int type,string title,string category,string primarykey,string entityKEy)
        {
         //   DBComparison client = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.ClientID.Name), DBConst.Int32(clientID));
           DBComparison comp = DBComparison.Equal(DBField.Field(TalentozSchema.ComponentID.Name), DBConst.String(componentID));
           // DBComparison att = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.FieldID.Name), DBConst.String(attributeID));

            DBQuery update = DBQuery.Update(TalentozSchema.Table)                                      
                .Set(TalentozSchema.ComponentName.Name, DBConst.String(name))
                 .AndSet(TalentozSchema.Title.Name, DBConst.String(title))
                    .AndSet(TalentozSchema.Category.Name, DBConst.String(category))
                     .AndSet(TalentozSchema.EntityKey.Name, DBConst.String(entityKEy))
                .AndSet(TalentozSchema.PrimaryKeys.Name, DBConst.String(primarykey)).WhereAll(comp);
            if (db.ExecuteNonQuery(update) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected internal bool UpdateComponentAttribute(string componentID,string attributeID,
           string displayName,
            bool isRequired, bool isUnique, bool isCore,
            bool isReadonly,
            bool isSecured,
            int lookupId,
            int type,            
            int length,
            string defaultValue,
            string fileExtension,             
            string lookupComponent,
            string componentDisplayField) {


             DBComparison comp = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.ComponentID.Name), DBConst.String(componentID));
            DBComparison att = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.FieldID.Name), DBConst.String(attributeID));

            DBQuery update = DBQuery.Update(TalentozSchemaInfo.Table).Set(DBField.Field(TalentozSchemaInfo.DisplayName.Name), DBConst.String(displayName))
                .AndSet(DBField.Field(TalentozSchemaInfo.IsRequired.Name), DBConst.Const(DbType.Boolean, isRequired))
                .AndSet(DBField.Field(TalentozSchemaInfo.IsUnique.Name), DBConst.Const( DbType.Boolean, isUnique))
                .AndSet(DBField.Field(TalentozSchemaInfo.IsCore.Name), DBConst.Const( DbType.Boolean, isCore))
                .AndSet(DBField.Field(TalentozSchemaInfo.IsReadOnly.Name), DBConst.Const( DbType.Boolean, isReadonly))
                .AndSet(DBField.Field(TalentozSchemaInfo.IsSecured.Name), DBConst.Const( DbType.Boolean, isSecured))
                .AndSet(DBField.Field(TalentozSchemaInfo.AttributeType.Name), DBConst.Int32(type))
                .AndSet(DBField.Field(TalentozSchemaInfo.LookUpID.Name), DBConst.Int32(lookupId))
                .AndSet(DBField.Field(TalentozSchemaInfo.DefaultValue.Name), DBConst.String(defaultValue))
                .AndSet(DBField.Field(TalentozSchemaInfo.LookupComponent.Name), DBConst.String(lookupComponent))
                .AndSet(DBField.Field(TalentozSchemaInfo.ComponentLookupDisplayName.Name), DBConst.String(componentDisplayField))
                       .AndSet(DBField.Field(TalentozSchemaInfo.FileExtension.Name), DBConst.String(fileExtension))
                            .AndSet(DBField.Field(TalentozSchemaInfo.Length.Name), DBConst.Int32(length))
                .WhereAll( comp, att);
            if (db.ExecuteNonQuery(update) > 0)
            {
                return true;
            }
            else
                return false;
        }

        protected internal bool UpdateComponentClientAttribute(int clientID, string componentID, string attributeID,
           string displayName,
            bool isRequired, bool isUnique, bool isCore,
            bool isReadonly,
            bool isSecured,
            int lookupId,
            int type,
            int length,
            string defaultValue,
            string fileExtension,
            string lookupComponent,
            string componentDisplayField)
        {

            DBComparison client = DBComparison.Equal(DBField.Field(TalentozSchemaClientInfo.ClientID.Name), DBConst.Int32(clientID));
            DBComparison comp = DBComparison.Equal(DBField.Field(TalentozSchemaClientInfo.ComponentID.Name), DBConst.String(componentID));
            DBComparison att = DBComparison.Equal(DBField.Field(TalentozSchemaClientInfo.FieldID.Name), DBConst.String(attributeID));

            DBQuery update = DBQuery.Update(TalentozSchemaClientInfo.Table).Set(DBField.Field(TalentozSchemaClientInfo.DisplayName.Name), DBConst.String(displayName))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.IsRequired.Name), DBConst.Const(DbType.Boolean, isRequired))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.IsUnique.Name), DBConst.Const(DbType.Boolean, isUnique))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.IsCore.Name), DBConst.Const(DbType.Boolean, isCore))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.IsReadOnly.Name), DBConst.Const(DbType.Boolean, isReadonly))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.IsSecured.Name), DBConst.Const(DbType.Boolean, isSecured))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.AttributeType.Name), DBConst.Int32(type))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.LookUpID.Name), DBConst.Int32(lookupId))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.DefaultValue.Name), DBConst.String(defaultValue))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.LookupComponent.Name), DBConst.String(lookupComponent))
                .AndSet(DBField.Field(TalentozSchemaClientInfo.ComponentLookupDisplayName.Name), DBConst.String(componentDisplayField))
                       .AndSet(DBField.Field(TalentozSchemaClientInfo.FileExtension.Name), DBConst.String(fileExtension))
                            .AndSet(DBField.Field(TalentozSchemaClientInfo.Length.Name), DBConst.Int32(length))
                .WhereAll(client, comp, att);
            if (db.ExecuteNonQuery(update) > 0)
            {
                return true;
            }
            else
                return false;
        }

        protected internal bool UpdateComponentLookUp(string componentID, string attributeID, string componentLookup, string displayName) {
       //     DBComparison client = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.ClientID.Name), DBConst.Int32(clientID));
            //DBComparison comp = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.ComponentID.Name), DBConst.String(componentID));
            DBComparison att = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.FieldID.Name), DBConst.String(attributeID));
            DBQuery update = DBQuery.Update(TalentozSchemaInfo.Table)
                .Set(TalentozSchemaInfo.LookupComponent.Name , DBConst.String(componentLookup))
                .AndSet(TalentozSchemaInfo.ComponentLookupDisplayName.Name, DBConst.String(displayName)).WhereAll( att);
            if (db.ExecuteNonQuery(update) > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        protected internal DataTable GetComponents() {
            DBQuery select = DBQuery.SelectAll().From(TalentozSchema.Table).OrderBy(TalentozSchema.ComponentName.Name,Order.Ascending) ;
          return  db.GetDatatable(select);
        }

        protected internal DataTable GetComponents(string compID)
        {
            string[] cid = compID.Split(',');
            List<DBClause> cids = new List<DBClause>();
            for (int i = 0; i < cid.Length; i++)
            {
                cids.Add(DBConst.String(cid[i]));
            }

            DBComparison comp = DBComparison.In(DBField.Field(TalentozSchema.ComponentID.Name), cids.ToArray());
            //DBComparison tb = DBComparison.Equal(DBField.Field(TalentozSchema.TableName.Name), DBConst.String(compID));
            //DBComparison cname = DBComparison.Equal(DBField.Field(TalentozSchema.ComponentName.Name), DBConst.String(compID));
            DBQuery select = DBQuery.SelectAll().From(TalentozSchema.Table).WhereAny(comp).OrderBy(TalentozSchema.ComponentName.Name, Order.Ascending);
            return db.GetDatatable(select);
        }

        protected internal DataTable GetComponent(string compID)
        {
            DBComparison comp = DBComparison.Equal(DBField.Field(TalentozSchema.ComponentID.Name), DBConst.String(compID));
            DBComparison tb = DBComparison.Equal(DBField.Field(TalentozSchema.TableName.Name), DBConst.String(compID));
            DBComparison cname = DBComparison.Equal(DBField.Field(TalentozSchema.ComponentName.Name), DBConst.String(compID));
            DBQuery select = DBQuery.SelectAll().From(TalentozSchema.Table).WhereAny(comp, tb, cname).OrderBy(TalentozSchema.ComponentName.Name, Order.Ascending);
            return db.GetDatatable(select);
        }
        protected internal DataTable GetAttribute( string componentID) {
            string[] cid = componentID.Split(',');
            List<DBClause> cids = new List<DBClause>();
            for (int i = 0; i < cid.Length; i++) {
                cids.Add(DBConst.String(cid[i]));
            }
          //  DBComparison client = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.ClientID.Name), DBConst.Int32(clientId));
            DBComparison comp = DBComparison.In(DBField.Field(TalentozSchemaInfo.ComponentID.Name), cids.ToArray());
            DBQuery select = DBQuery.SelectAll().From(TalentozSchemaInfo.Table).WhereAll(comp);
            return db.GetDatatable(select);
        }

        protected internal DataTable GetClientAttribute(int clientID,string componentID)
        {
            string[] cid = componentID.Split(',');
            List<DBClause> cids = new List<DBClause>();
            for (int i = 0; i < cid.Length; i++)
            {
                cids.Add(DBConst.String(cid[i]));
            }
             DBComparison client = DBComparison.Equal(DBField.Field(TalentozSchemaClientInfo.ClientID.Name), DBConst.Int32(clientID));
            DBComparison comp = DBComparison.In(DBField.Field(TalentozSchemaClientInfo.ComponentID.Name), cids.ToArray());
            DBQuery select = DBQuery.SelectAll().From(TalentozSchemaClientInfo.Table).WhereAll(client, comp);
            return db.GetDatatable(select);
        }
    }
}
