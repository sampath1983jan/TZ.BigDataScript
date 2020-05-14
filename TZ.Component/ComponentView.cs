using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public class ComponentView:IComponentView
    {
        public List<ComponentRelation> ComponentRelations { get; set; }
        public List<Component> Components { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string CoreComponent { get; set; }       
         public string Category { get; set; }
        public ComponentView() {
            ComponentRelations = new List<ComponentRelation>();
            Name = "";
            ID = "";
            Category = "";
            CoreComponent = "";             
        }
    }
}
