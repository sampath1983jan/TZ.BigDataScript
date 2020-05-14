using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.State.Field
{
    public class StringField:IField
    {
        public enum FunctionType { 
            NONE,
        LENGTH,
        FORMAT,
        LOWER,
        UPPER,
        TRIM,        
        ISNULL,
        ISNOTNULL,
        ISNAN,
        ISEMPTY,
        }
        public string Format { get; set; }
       public FunctionType Function { get; set; }
        public StringField() {
            this.FieldType = "string";
        }
        public StringField(string name, string alias)
        {
            this.FieldName = name;
            this.AliasName = alias;
            this.FieldType = "string";
            IsDerivedField = false;
            FunctionName = "";
            Function = FunctionType.NONE;
        }   
    }
}
