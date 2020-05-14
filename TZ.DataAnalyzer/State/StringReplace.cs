using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.State
{
   public class StringReplace
    {
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public string ReplaceValue { get; set; }
        public StringReplace()
        {
            ColumnName = "";
            Value = "";
            ReplaceValue = "";
        }
    }
}
