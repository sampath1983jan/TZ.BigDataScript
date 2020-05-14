using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.State.Field
{
    public class BooleanField:IField
    {
        public BooleanField() {
            this.FieldType = "bool";
        }

        public BooleanField(string name, string alias)
        {
            this.FieldName = name;
            this.AliasName = alias;
            this.FieldType = "bool";
            IsDerivedField = false;
            FunctionName = "";
        }
    }
}
