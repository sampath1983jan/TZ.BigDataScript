using Microsoft.Spark.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tz.DataAnalyzer.Statement
{
    public class GroupStatement : IStatement
    {
        private DataFrame Data;

        public List<string> GroupFields { get; set; }
        public bool IsDataFrameStatement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DataFrame ExcuteStatement(SparkSession spark)
        {
            throw new NotImplementedException();
        }

        public GroupStatement() {           
        }
        public DataFrame ExecuteStatement(DataFrame d)
        {
            throw new NotImplementedException();
        }
    }
}
