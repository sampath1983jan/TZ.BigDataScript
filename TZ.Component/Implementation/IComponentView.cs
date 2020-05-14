using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public interface IComponentView
    {
        List<ComponentRelation> ComponentRelations { get; set; }
        string Name { get; set; }
        string ID { get; set; }
        string CoreComponent { get; set; }        
        string Category { get; set; }
          List<Component> Components { get; set; }
    }
}
