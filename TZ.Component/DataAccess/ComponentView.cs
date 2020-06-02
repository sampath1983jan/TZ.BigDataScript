using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.CompExtention.Builder;

namespace TZ.CompExtention.DataAccess
{
    public class ComponentViewHandler : IComponentViewDataAccess
    {
   
        private readonly string connection;
        public string Connection => connection;
        private int ClientID;
        public ComponentViewHandler(string conn, int clientID) {
            connection = conn;
            ClientID = clientID;
        }
        public CompExtention.ComponentView GetView(string viewid)
        {
            DataTable dt = new DataTable();
            Builder.Data.ComponentViewBuilder cv = new Builder.Data.ComponentViewBuilder(this.Connection);
            dt = cv.GetView(viewid);
            DataTable dtViewSchema = new DataTable();
            dtViewSchema = cv.GetViewSchema(viewid);
            List<ComponentView> views = new List<ComponentView>();
            foreach (DataRow dr in dt.Rows)
            {
                ComponentView v = new ComponentView();
                v.ID = dr[Builder.Schema.TalentozView.ViewID.Name] == null ? "" : dr[Builder.Schema.TalentozView.ViewID.Name].ToString();
                v.Name = dr[Builder.Schema.TalentozView.Name.Name] == null ? "" : dr[Builder.Schema.TalentozView.Name.Name].ToString();
                v.Category = dr[Builder.Schema.TalentozView.Catgory.Name] == null ? "" : dr[Builder.Schema.TalentozView.Catgory.Name].ToString();
                v.CoreComponent = dr[Builder.Schema.TalentozView.CoreComponent.Name] == null ? "" : dr[Builder.Schema.TalentozView.CoreComponent.Name].ToString();

               var dtVS= dtViewSchema.DefaultView.ToTable(true, "ViewID", Builder.Schema.TalentozViewSchema.ComponentID.Name ,
                    Builder.Schema.TalentozViewSchema.ChildComponentID.Name );

                foreach (DataRow drSch in dtVS.Rows) {
                    ComponentRelation vc = new ComponentRelation();
                    vc.ViewID = viewid;
                    vc.ComponentID = drSch[Builder.Schema.TalentozViewSchema.ComponentID.Name] == null ? "" : drSch[Builder.Schema.TalentozViewSchema.ComponentID.Name].ToString();
                    vc.ChildComponentID = drSch[Builder.Schema.TalentozViewSchema.ChildComponentID.Name] == null ? "" : drSch[Builder.Schema.TalentozViewSchema.ChildComponentID.Name].ToString();

                    dtViewSchema.DefaultView.RowFilter = 
                        Builder.Schema.TalentozViewSchema.ComponentID.Name  + " = '" + vc.ComponentID + "' AND " +
                        Builder.Schema.TalentozViewSchema.ChildComponentID.Name + " = '" + vc.ChildComponentID +"'" ;
                    var dtvsre = dtViewSchema.DefaultView.ToTable(true);
                    dtViewSchema.DefaultView.RowFilter = "";
                    vc.Relationship = new List<ViewRelation>();

                    foreach (DataRow drRel in dtvsre.Rows) {
                        var vr = new ViewRelation();
                        vr.ID = drRel[Builder.Schema.TalentozViewSchemaRelation.ViewSchemaRelation.Name] == null ? "" : 
                            drRel[Builder.Schema.TalentozViewSchemaRelation.ViewSchemaRelation.Name].ToString();

                        vr.Left = drRel[Builder.Schema.TalentozViewSchemaRelation.Parent.Name] == null ? "" : 
                            drRel[Builder.Schema.TalentozViewSchemaRelation.Parent.Name].ToString();

                        vr.LeftField = drRel[Builder.Schema.TalentozViewSchemaRelation.ParentField.Name] == null ? "" : 
                            drRel[Builder.Schema.TalentozViewSchemaRelation.ParentField.Name].ToString();

                        vr.Right = drRel[Builder.Schema.TalentozViewSchemaRelation.Child.Name] == null ? "" :
                         drRel[Builder.Schema.TalentozViewSchemaRelation.Child.Name].ToString();

                        vr.LeftField = drRel[Builder.Schema.TalentozViewSchemaRelation.RelatedField.Name] == null ? "" :
                            drRel[Builder.Schema.TalentozViewSchemaRelation.RelatedField.Name].ToString();
                        vc.Relationship.Add(vr);

                    }
                    v.ComponentRelations.Add(vc);

                }
                return v;
            }
            return null;
        }

        public void LoadViewComponents(IComponentView view) {
            List<string> comp = new List<string>();
            comp.Add(view.CoreComponent);
            foreach (ComponentRelation vc in view.ComponentRelations)
            {
                if (view.CoreComponent != vc.ComponentID)
                {
                    comp.Add(vc.ComponentID);
                }
                if (view.CoreComponent != vc.ChildComponentID)
                {
                    comp.Add(vc.ChildComponentID);
                }
            }
            var ViewAttributes = CompExtention.ComponentManager.GetComponentAttributes(string.Join(",", comp.ToArray()), ClientID, new CompExtention.DataAccess.ComponentDataHandler(Connection));
           
            view.Components = CompExtention.DataAccess.ComponentDataHandler.GetComponents(string.Join(",", comp.ToArray()), Connection);
       
            foreach (Component c in view.Components)
            {
                var att = ViewAttributes.Where(x => x.ComponentID == c.ID).ToList();
                c.Attributes = att;
            }
        }
        public List<CompExtention.ComponentView> GetViews()
        {
            DataTable dt = new DataTable();
            Builder.Data.ComponentViewBuilder cv = new Builder.Data.ComponentViewBuilder(this.Connection);
            dt=cv.GetViews();
            List<ComponentView> views = new List<ComponentView>();
            foreach (DataRow dr in dt.Rows) {
                ComponentView v = new ComponentView();
                v.ID = dr[Builder.Schema.TalentozView.ViewID.Name] == null ? "" : dr[Builder.Schema.TalentozView.ViewID.Name].ToString();
                v.Name = dr[Builder.Schema.TalentozView.Name.Name] == null ? "" : dr[Builder.Schema.TalentozView.Name.Name].ToString();
                v.Category = dr[Builder.Schema.TalentozView.Catgory.Name] == null ? "" : dr[Builder.Schema.TalentozView.Catgory.Name].ToString();
                v.CoreComponent = dr[Builder.Schema.TalentozView.CoreComponent.Name] == null ? "" : dr[Builder.Schema.TalentozView.CoreComponent.Name].ToString();
                views.Add(v);
            }
            return views;
        }

        public static List<CompExtention.ComponentView> GetViews(string conn) {
            DataTable dt = new DataTable();
            Builder.Data.ComponentViewBuilder cv = new Builder.Data.ComponentViewBuilder(conn);
            dt = cv.GetViews();
            DataTable dtViewSchema = new DataTable();
            
            List<ComponentView> views = new List<ComponentView>();
            foreach (DataRow dr in dt.Rows)
            {
                ComponentView v = new ComponentView();
                v.ID = dr[Builder.Schema.TalentozView.ViewID.Name] == null ? "" : dr[Builder.Schema.TalentozView.ViewID.Name].ToString();
                v.Name = dr[Builder.Schema.TalentozView.Name.Name] == null ? "" : dr[Builder.Schema.TalentozView.Name.Name].ToString();
                v.Category = dr[Builder.Schema.TalentozView.Catgory.Name] == null ? "" : dr[Builder.Schema.TalentozView.Catgory.Name].ToString();
                v.CoreComponent = dr[Builder.Schema.TalentozView.CoreComponent.Name] == null ? "" : dr[Builder.Schema.TalentozView.CoreComponent.Name].ToString();

                //var dtVS = dtViewSchema.DefaultView.ToTable(true, "ViewID", Builder.Schema.TalentozViewSchema.ComponentID.Name,
                //     Builder.Schema.TalentozViewSchema.ChildComponentID.Name);

                dtViewSchema = cv.GetViewSchema(v.ID);

                foreach (DataRow drSch in dtViewSchema.Rows)
                {
                    ComponentRelation vc = new ComponentRelation();
                    vc.ViewID = v.ID;
                    vc.ComponentID = drSch[Builder.Schema.TalentozViewSchema.ComponentID.Name] == null ? "" : drSch[Builder.Schema.TalentozViewSchema.ComponentID.Name].ToString();
                    vc.ChildComponentID = drSch[Builder.Schema.TalentozViewSchema.ChildComponentID.Name] == null ? "" : drSch[Builder.Schema.TalentozViewSchema.ChildComponentID.Name].ToString();

                    dtViewSchema.DefaultView.RowFilter =
                        Builder.Schema.TalentozViewSchema.ComponentID.Name + " = '" + vc.ComponentID + "' AND " +
                        Builder.Schema.TalentozViewSchema.ChildComponentID.Name + " = '" + vc.ChildComponentID + "'";
                    var dtvsre = dtViewSchema.DefaultView.ToTable(true);
                    dtViewSchema.DefaultView.RowFilter = "";
                    vc.Relationship = new List<ViewRelation>();
                    foreach (DataRow drRel in dtvsre.Rows)
                    {
                        var vr = new ViewRelation();
                        vr.ID = drRel[Builder.Schema.TalentozViewSchemaRelation.ViewSchemaRelation.Name] == null ? "" :
                            drRel[Builder.Schema.TalentozViewSchemaRelation.ViewSchemaRelation.Name].ToString();

                        vr.Left = drRel[Builder.Schema.TalentozViewSchemaRelation.Parent.Name] == null ? "" :
                            drRel[Builder.Schema.TalentozViewSchemaRelation.Parent.Name].ToString();

                        vr.LeftField = drRel[Builder.Schema.TalentozViewSchemaRelation.ParentField.Name] == null ? "" :
                            drRel[Builder.Schema.TalentozViewSchemaRelation.ParentField.Name].ToString();

                        vr.Right = drRel[Builder.Schema.TalentozViewSchemaRelation.Child.Name] == null ? "" :
                         drRel[Builder.Schema.TalentozViewSchemaRelation.Child.Name].ToString();

                        vr.LeftField = drRel[Builder.Schema.TalentozViewSchemaRelation.RelatedField.Name] == null ? "" :
                            drRel[Builder.Schema.TalentozViewSchemaRelation.RelatedField.Name].ToString();
                        vc.Relationship.Add(vr);
                    }
                    v.ComponentRelations.Add(vc);
                }
                views.Add(v);
            }
            return views;
        }

        public bool RemoveView(string viewID)
        {
            Builder.Data.ComponentViewBuilder cv = new Builder.Data.ComponentViewBuilder(this.Connection);
            if (cv.RemoveView(viewID) == true)
            {
                cv.RemoveViewSchema(viewID);
                return true;
            }
            else
                return false;
        }

        public bool SaveView(IComponentView view)
        {
            Builder.Data.ComponentViewBuilder cv = new Builder.Data.ComponentViewBuilder(this.Connection);
           view.ID= cv.SaveView(view.Name, view.CoreComponent, view.Category);
            if (view.ID != "") {

                foreach (ComponentRelation vc in view.ComponentRelations) {
                    cv.SaveViewSchema(view.ID, vc.ComponentID, vc.ChildComponentID);
                    foreach (ViewRelation vr in vc.Relationship)
                    {
                        cv.SaveViewSchemaRelation(view.ID, vr.Left, vr.LeftField, vr.Right, vr.RightField);
                    }
                }
                return true;
            }
            else
                return false;
        }
        public bool UpdateView(IComponentView view)
        {
            Builder.Data.ComponentViewBuilder cv = new Builder.Data.ComponentViewBuilder(this.Connection);
            cv.RemoveViewSchema(view.ID);       
            if (cv.UpdateView(view.ID, view.Name, view.CoreComponent, view.Category)==true)
            {
                foreach (ComponentRelation vc in view.ComponentRelations)
                {
                    cv.SaveViewSchema(view.ID, vc.ComponentID, vc.ChildComponentID);
                    foreach (ViewRelation vr in vc.Relationship)
                    {
                        cv.SaveViewSchemaRelation(view.ID, vr.Left, vr.LeftField, vr.Right, vr.RightField);
                    }
                }
                return true;
            }
            else
                return false;
        }
    }
}
