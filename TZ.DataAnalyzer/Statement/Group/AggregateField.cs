using System;
using System.Collections.Generic;
using System.Text;

namespace Tz.DataAnalyzer.Statement.Group
{
    public enum AggregateType
    { 
    SUM,
    COUNT,
    DISTINCTCOUNT,
    AVG,
    MIN,
    MAX,
    MEAN,
    STANDARDDEV,
    SUMDISTINCT,
    }
   public class AggregateField
    {
        public string FieldName { get; set; }
        public AggregateType Aggregate { get; set; }
    }
}
