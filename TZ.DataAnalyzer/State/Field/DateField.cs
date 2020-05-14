using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.State.Field
{
   public class DateField:IField
    {
        public enum FunctionType
        {
            NONE,
            DAY,
            MONTH,
            YEAR,
            WEEK,
            HOUR,
            MIN,
            SEC,
            LAST,
            QUARTER,
            DATEADD,
            DATEDIFF,
            ADDMONTH,
            BETWEENMONTH,
            DATEFORMAT,
            TODATE,
            NEXT,
            TOTIME,
        }
        public int NumberOfDays { get; set; }
        public int NumberOfMonths { get; set; }
        public string Format { get; set; }
        public string CompareColumn { get; set; }
        public FunctionType Function { get; set; }
        public DateField() {
            this.FieldType = "date";
        }

        public DateField(string name, string alias)
        {
            this.FieldName = name;
            this.AliasName = alias;
            this.FieldType = "date";
            IsDerivedField = false;
            FunctionName = "";
            Function = FunctionType.NONE;
        }
    }
}
