using Microsoft.Spark.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tz.DataAnalyzer.Statement
{
    public class SelectStatement : IStatement
    {
        private DataFrame data;
        public List<SchemaStructField> Fields { get; set; }
        
        public List<WhereStatement> Where { get; set; }
        public bool IsDataFrameStatement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DataFrame ExcuteStatement(SparkSession spark)
        {
            throw new NotImplementedException();
        }

        public DataFrame ExecuteStatement(DataFrame df)
        {
            throw new NotImplementedException();
        }
    }
}
