using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.State.Field
{
    public class NumberField:IField
    {
        public enum FunctionType {
            NONE,
            CEIL,
        FLOOR,
        ROUND,
        NUMBERFORMAT,       

        }
        public string Format { get; set; }
        public FunctionType Function { get; set; }
        public NumberField() {
            this.FieldType = "number";
        }
        public NumberField(string name, string alias)
        {
            this.FieldName = name;
            this.AliasName = alias;
            this.FieldType = "number";
            IsDerivedField = false;
            FunctionName = "";
            Function = FunctionType.NONE;
        }
    }
}
