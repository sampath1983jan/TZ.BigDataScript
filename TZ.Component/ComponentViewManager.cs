using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public class ComponentViewManager
    {
        private IComponentViewDataAccess viewDataAccess;
        private IComponentViewDataAccess ViewDataAccess { get => viewDataAccess; }
        private IComponentView view;
        public string ViewID { get; set; }
        public IComponentView View { get => view; }

        public ComponentViewManager(string viewid,int clientid, IComponentViewDataAccess v) {
            this.ViewID = viewid;
            viewDataAccess = v;
            view = this.ViewDataAccess.GetView(this.ViewID);
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
        view.Components = CompExtention.DataAccess.ComponentDataHandler.GetComponents(string.Join(",", comp.ToArray()), v.Connection);
             var ViewAttributes = CompExtention.ComponentManager.GetComponentAttributes(string.Join(",", comp.ToArray()), clientid,
                 new CompExtention.DataAccess.ComponentDataHandler(v.Connection));
            foreach (Component c in view.Components)
            {
                var att = ViewAttributes.Where(x => x.ComponentID == c.ID).ToList();
                c.Attributes = att;
            }
        }
        public ComponentViewManager(IComponentViewDataAccess v) {
            viewDataAccess = v;
            ViewID = "";
        }
        public void LoadViewComponents() {
            this.ViewDataAccess.LoadViewComponents(this.View);
        }
        public IComponentView GetView() {         
            return view;
        }
        public IComponentView NewView(string name) {
            view = new ComponentView() { Name = name };
            return view;
        }
        public bool Save(IComponentView view) {
            if (view.ID == "")
                return this.ViewDataAccess.SaveView(view);
            else
                return this.ViewDataAccess.UpdateView(view);
        }
        public bool Remove() {
            return this.ViewDataAccess.RemoveView(this.view.ID);
        }
        public List<ComponentView> GetViews() {
            return this.ViewDataAccess.GetViews();
        }
        

    }
}
