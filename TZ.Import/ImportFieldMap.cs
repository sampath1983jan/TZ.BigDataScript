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
        public int DateFormat { get; set; }

        //[ {key:"1", value :"MM-DD-YYYY"},
        //        {key:"2",value :"DD-MM-YYYY"},
        //        {key:"3",value :"MM-DD-YYYY"},
        //        { key: "4", value: "YYYY-MM-DD" },
        //        { key: "5", value: "YYYY-DD-MM" },
        //        { key: "6", value: "MM/DD/YYYY" },
        //        { key: "7", value: "DD/MM/YYYY" },
        //        { key: "8", value: "MM/DD/YYYY" },
        //        { key: "9", value: "YYYY/MM/DD" },
        //        { key: "10", value: "YYYY/DD/MM" }]
        //'' 1- MM-DD-YYYY, 2. MM/DD/YYYY 3. DD-MM-YYYY 4. MM-DD-YYYY 5. YYYY-MM-DD 6. YYYY-DD-MM
    }
}
