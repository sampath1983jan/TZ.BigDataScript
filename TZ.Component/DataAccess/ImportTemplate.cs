using System;
using System.Data;
using Tech.Data;
using Tech.Data.Query;
using TZ.CompExtention.Builder.Data;
using TZ.CompExtention.Builder.Schema;
using TZ.Data;

namespace TZ.CompExtention.DataAccess
{
    public class ImportTemplate : DataBase
    {
        private DBDatabase db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public ImportTemplate(string conn)
        {
            InitDbs(conn);
            db = base.Database;
        }
        private CSVItem[] all;
        private CSVItem itm;
        private DBQuery insert;

        protected internal DataTable GetImport(int cid)
        {
            DBComparison client = DBComparison.Equal(DBField.Field(TalentozTemplate.ClientID.Name), DBConst.Int32(cid));
            DBQuery select = DBQuery.SelectAll().From(TalentozTemplate.Table).Where(client).OrderBy(TalentozTemplate.Name.Name, Order.Ascending);
            return db.GetDatatable(select);
        }

        protected internal DataTable GetImport(string importID,int cid)
        {
            DBComparison client = DBComparison.Equal(DBField.Field(TalentozTemplate.ClientID.Name), DBConst.Int32 (cid));
            DBComparison comp = DBComparison.Equal(DBField.Field(TalentozTemplate.TemplateID.Name), DBConst.String(importID));
            DBComparison code = DBComparison.Equal(DBField.Field(TalentozTemplate.TemplateCode.Name), DBConst.String(importID));
            DBQuery select = DBQuery.SelectAll().From(TalentozTemplate.Table).WhereAny(comp, code).And(client) .OrderBy(TalentozTemplate.Name.Name, Order.Ascending);
            return db.GetDatatable(select);
        }

        protected internal bool Remove(string template, int cid)
        {
            DBComparison client = DBComparison.Equal(DBField.Field(TalentozTemplate.ClientID.Name), DBConst.Int32(cid));
            DBComparison dbTemplate = DBComparison.Equal(DBField.Field(TalentozTemplate.TemplateID.Name), DBConst.String(template));
            DBQuery delete = DBQuery.DeleteFrom(TalentozTemplate.Table).WhereAll (dbTemplate, client);
            if (db.ExecuteNonQuery(delete) > 0)
            {
                return true;
            }
            else
                return false;

        }
        protected internal bool Update(int cid,string template, string name, string category, string code, string viewid, int type, string attributes, string pivotColumn)
        {
            DBComparison dbTemplate = DBComparison.Equal(DBField.Field(TalentozTemplate.TemplateID.Name), DBConst.String(template));
            DBComparison client = DBComparison.Equal(DBField.Field(TalentozTemplate.ClientID.Name), DBConst.Int32(cid));
            // DBComparison att = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.FieldID.Name), DBConst.String(attributeID));
            DBQuery update = DBQuery.Update(TalentozTemplate.Table)
                .Set(TalentozTemplate.Name.Name, DBConst.String(name))
                 .AndSet(TalentozTemplate.TemplateCode.Name, DBConst.String(code))
                    .AndSet(TalentozTemplate.Category.Name, DBConst.String(category))
                      .AndSet(TalentozTemplate.ViewID.Name, DBConst.String(viewid))
                 .AndSet(TalentozTemplate.PivotColumn.Name, DBConst.String(pivotColumn))
                         .AndSet(TalentozTemplate.ViewFields.Name, DBConst.String(attributes))
                .AndSet(TalentozTemplate.TemplateType.Name, DBConst.Int32(type)).WhereAll(dbTemplate, client);
            if (db.ExecuteNonQuery(update) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected internal string Save(int cid,string name, string category, string code, string viewid, int type, string attributes, string pivotColumn)
        {

            string templateid = Shared.generateID();
            Tech.Data.Query.DBQuery dBQuery = DBQuery.InsertInto(TalentozTemplate.Table)
                .Field(TalentozTemplate.ViewID.Name)
                .Field(TalentozTemplate.ClientID .Name)
                .Field(TalentozTemplate.TemplateID.Name)
                .Field(TalentozTemplate.Name.Name)
                .Field(TalentozTemplate.TemplateCode.Name)
                  .Field(TalentozTemplate.Category.Name)
                  .Field(TalentozTemplate.TemplateType.Name)
                  .Field(TalentozTemplate.ViewFields.Name)
                     .Field(TalentozTemplate.PivotColumn.Name)
                  .Field(TalentozTemplate.LastUPD.Name)
                .Values(DBConst.String(viewid),
                DBConst.Int32(cid),
                DBConst.String(templateid),
                DBConst.String(name),
                DBConst.String(code),
                DBConst.String(category),
                  DBConst.Const(System.Data.DbType.Int32, type),
                              DBConst.String(attributes),
                                     DBConst.String(pivotColumn),
                                  DBConst.Const(System.Data.DbType.DateTime, DateTime.Today)
                  );
            if (db.ExecuteNonQuery(dBQuery) > 0)
            {
                return templateid;
            }
            else
            {
                return "";
            }
        }
    }
}
