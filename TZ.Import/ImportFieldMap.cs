using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Import
{
    public class ImportFieldMap
    {
        public string DataField { get; set; }
        public string FileFields { get; set; }
        public string ComponentID { get; set; }
        public bool IsKey { get; set; }
        public bool IsRequired { get; set; }
        public bool IsAutoCode { get; set; }
        /// <summary>
        /// <STRING> {INCRNO}
        /// </summary>
        public string AutoFormat { get; set; } 
        public string DefaultValue { get; set; }
        public string Value { get; set; }
    }
}
