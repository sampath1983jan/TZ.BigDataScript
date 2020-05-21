using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention.ImportTemplate
{
    public class TemplateField
    {
        public Attribute Field { get; set; }
        public string ID { get; set; }
        public bool IsKey { get; set; }
        public bool IsPivot { get; set; }
        public bool IsDefault { get; set; }
        public bool IsRequired { get; set; }
        public bool IsRow { get; set; }
        public bool IsColumn { get; set; }
        
        public TemplateField() {
            Field = new Attribute();
            ID = "";
            IsKey = false;
            IsPivot = false;
            IsDefault = false;
            IsRequired = false;
            IsColumn = false;
            IsRow = false;
           

        }

    }
}
