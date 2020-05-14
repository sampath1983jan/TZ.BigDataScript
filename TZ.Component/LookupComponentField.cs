using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public class LookupComponentField
    {
        public Component LookupComponent {get;set;}
        public Attribute Attribute { get; set; }
        public DataTable LookupData { get; set; }

    }
}
