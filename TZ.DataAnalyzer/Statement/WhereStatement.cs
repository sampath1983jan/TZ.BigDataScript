using Microsoft.Spark.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tz.DataAnalyzer.Statement
{
    public class WhereStatement 
    {
       public string Condition { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

    }
}
