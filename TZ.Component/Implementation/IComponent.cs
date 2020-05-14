using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public interface IComponent
    {
        string ID { get; set; }
        List<Attribute> Attributes { get; set; }
        string Name { get; set; }
        string TableName { get; set; }
        List<Attribute> Keys { get; set; }
        ComponentType Type { get; set; }
        string Category { get; set; }
        string Title { get; set; }
       string EntityKey { get; set; }
    }
}
