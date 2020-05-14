using Microsoft.Spark.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tz.DataAnalyzer.Statement
{
    public interface IStatement
    {
        bool IsDataFrameStatement { get; set; }
        DataFrame ExcuteStatement(SparkSession spark);
        DataFrame ExecuteStatement(DataFrame df);
    }
}
