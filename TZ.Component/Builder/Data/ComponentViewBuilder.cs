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
    public class ComponentViewBuilder: DataBase
    {
        private DBDatabase db;
        public ComponentViewBuilder(string conn)
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

        protected internal bool UpdateView(string viewID,string viewName, string coreCompoent, string category)
        {
            //   DBComparison client = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.ClientID.Name), DBConst.Int32(clientID));
            DBComparison comp = DBComparison.Equal(DBField.Field(TalentozView.ViewID.Name), DBConst.String(viewID));
            // DBComparison att = DBComparison.Equal(DBField.Field(TalentozSchemaInfo.FieldID.Name), DBConst.String(attributeID));

            DBQuery update = DBQuery.Update(TalentozView.Table)
                .Set(TalentozView.Name.Name, DBConst.String(viewName))
                 .AndSet(TalentozView.CoreComponent.Name, DBConst.String(coreCompoent))
                    .AndSet(TalentozView.Catgory.Name, DBConst.String(category))
                .AndSet(TalentozView.LastUPD.Name, DBConst.DateTime(DateTime.Now)).WhereAll(comp);
            if (db.ExecuteNonQuery(update) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string SaveView(string viewName, string coreCompoent, string category) {
            string ComponentId = Shared.generateID();
            DBQuery insert = DBQuery.InsertInto(TalentozView.Table).
                    Field( (TalentozView.ViewID.Name )).
                     Field((TalentozView.Name.Name)).
                      Field((TalentozView.Catgory.Name)).
                       Field((TalentozView.CoreComponent.Name)).
                       Field((TalentozView.LastUPD.Name)).
                       Value(DBConst.String(ComponentId)).
                        Value(DBConst.String(viewName)).                      
                          Value(DBConst.String(category)).
                          Value(DBConst.String(coreCompoent)).
                           Value(DBConst.DateTime(DateTime.Now))
                    ;
            if (db.ExecuteNonQuery(insert)>0)
            {
                return ComponentId;
            }
            else {
                return "";
            }
        }

        public bool SaveViewSchema(string viewID, string compID, string childComp) { 
            DBQuery insert = DBQuery.InsertInto(TalentozViewSchema.Table).
                    Field((TalentozViewSchema.ViewID.Name)).
                     Field((TalentozViewSchema.ComponentID.Name)).
                      Field((TalentozViewSchema.ChildComponentID.Name)).
                       Field((TalentozViewSchema.ComponentAlias.Name)).
                       Field((TalentozViewSchema.LastUPD.Name)).
                       Value(DBConst.String(viewID)).
                        Value(DBConst.String(compID)).
                         Value(DBConst.String(childComp)).
                          Value(DBConst.String("")).
                           Value(DBConst.DateTime(DateTime.Now))
                    ;
            if (db.ExecuteNonQuery(insert) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetView(string viewID)
        {
            DBComparison dbView = DBComparison.Compare(DBField.Field(TalentozViewSchema.ViewID.Name), Compare.Equals, DBConst.String(viewID));
            DBQuery se = DBQuery.SelectAll().From(TalentozView.Table).Where(dbView);
            return db.GetDatatable(se);
        }

        public DataTable GetViews() {
            DBQuery se = DBQuery.SelectAll().From(TalentozView.Table);
            return db.GetDatatable(se);
        }

        public DataTable GetViewSchema(string viewid) {
            DBComparison dbView = DBComparison.Compare(DBField.Field(TalentozViewSchema.Table,TalentozViewSchema.ViewID.Name), Compare.Equals, DBConst.String(viewid));
            DBQuery se = DBQuery.Select()
                .Field(TalentozViewSchema.Table , TalentozViewSchema.ViewID.Name)
                .Field(TalentozViewSchema.Table, TalentozViewSchema.ComponentID.Name)
                .Field(TalentozViewSchema.Table, TalentozViewSchema.ChildComponentID.Name)
                .Field(TalentozViewSchemaRelation.Table, TalentozViewSchemaRelation.ViewSchemaRelation.Name)
                .Field(TalentozViewSchemaRelation.Table, TalentozViewSchemaRelation.ParentField.Name)
                .Field(TalentozViewSchemaRelation.Table, TalentozViewSchemaRelation.RelatedField.Name)
                .Field(TalentozViewSchemaRelation.Table, TalentozViewSchemaRelation.Parent.Name)
                .Field(TalentozViewSchemaRelation.Table, TalentozViewSchemaRelation.Child.Name)
          
                .From(TalentozViewSchema.Table).InnerJoin(TalentozViewSchemaRelation.Table)
                .On(TalentozViewSchema.Table, TalentozViewSchema.ComponentID.Name, Compare.Equals,TalentozViewSchemaRelation.Table, TalentozViewSchemaRelation.Parent.Name )
                .And(TalentozViewSchema.Table, TalentozViewSchema.ChildComponentID.Name , Compare.Equals,TalentozViewSchemaRelation.Table, TalentozViewSchemaRelation.Child.Name  )
                  .And(TalentozViewSchema.Table, TalentozViewSchema.ViewID.Name, Compare.Equals, TalentozViewSchemaRelation.Table, TalentozViewSchemaRelation.ViewID.Name)
                .Where(dbView);
            return db.GetDatatable(se);
        }

        public bool RemoveViewSchema(string ViewID) {
            DBComparison dbView = DBComparison.Compare(DBField.Field(TalentozViewSchema.ViewID.Name), Compare.Equals, DBConst.String(ViewID));
            DBQuery deleteViewSchema = DBQuery.DeleteFrom(TalentozViewSchema.Table).Where(dbView);
            DBQuery deleteViewSchemaRelation = DBQuery.DeleteFrom(TalentozViewSchemaRelation.Table).Where(dbView);
            db.ExecuteNonQuery(deleteViewSchema);
            db.ExecuteNonQuery(deleteViewSchemaRelation);
            return true;
        }
        public bool RemoveView(string ViewID) {
            DBComparison dbView = DBComparison.Compare(DBField.Field(TalentozView.ViewID.Name), 
                Compare.Equals, DBConst.String(ViewID));
            DBQuery deleteView = DBQuery.DeleteFrom(TalentozView.Table).Where(dbView);
            db.ExecuteNonQuery(deleteView);
            return true;
        }
        public string SaveViewSchemaRelation(string viewID,string parent,string parentField,string child,string childField ) {
            string viewSchema = Shared.generateID();
            DBQuery insert = DBQuery.InsertInto(TalentozViewSchemaRelation.Table).
                    Field((TalentozViewSchemaRelation.ViewID.Name)).
                     Field((TalentozViewSchemaRelation.ViewSchemaRelation.Name)).
                      Field((TalentozViewSchemaRelation.Parent.Name)).
                       Field((TalentozViewSchemaRelation.ParentField.Name)).
                         Field((TalentozViewSchemaRelation.Child.Name)).
                       Field((TalentozViewSchemaRelation.RelatedField.Name)).
                       Field((TalentozViewSchemaRelation.LastUPD.Name)).
                       Value(DBConst.String(viewID)).
                        Value(DBConst.String(viewSchema)).
                         Value(DBConst.String(parent)).
                          Value(DBConst.String(parentField)).
                       Value(DBConst.String(child)).
                          Value(DBConst.String(childField)).
                           Value(DBConst.DateTime(DateTime.Now))
                    ;
            if (db.ExecuteNonQuery(insert) > 0)
            {
                return viewSchema;
            }
            else
            {
                return "";
            }
        }
    }
}
