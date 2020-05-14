using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.State
{
   public class NullReplace
    {
        public string ColumnName { get; set; }
        public string Value { get; set; }

        public NullReplace() {
            ColumnName = "";
            Value = "";
        }
    }
}
