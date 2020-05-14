using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
   public class ComponentRelation
    {
        public string ViewID { get; set; }
        public string ComponentID { get; set; }
        public string ChildComponentID { get; set; }
        public string ComponentAlias { get; set; }
        public string ChildCompnentAlias { get; set; }
        public  List<ViewRelation> Relationship { get; set; }

        public ComponentRelation() {
            ViewID = "";
            ComponentID = "";
            ChildComponentID = "";
            ComponentAlias = "";
            ChildCompnentAlias = "";
            Relationship = new List<ViewRelation>();
        }
    }
}
