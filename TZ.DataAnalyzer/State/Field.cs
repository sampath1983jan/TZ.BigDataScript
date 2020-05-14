using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.DataAnalyzer.State
{
    
   public class DataField : IField
    {     
        public DataField() {
            IsDerivedField = false;
        }
        public DataField(string name, string alias) {
            this.FieldName = name;
            this.AliasName = alias;
            this.FieldType = "string";
            IsDerivedField = false;
            FunctionName = "";
        }
    }

    public class IField {
        public string FieldName { get; set; }
        public string AliasName { get; set; }
        public string FieldType { get; set; }
        public bool IsDerivedField { get; set; }
        public string FunctionName { get; set; }
    }
}
