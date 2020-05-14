using Microsoft.Spark.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tz.DataAnalyzer.Statement
{
    public class JoinStatement : IStatement
    {
        public string Source { get; set; }

        public string Target { get; set; }

        public string JoinType { get; set; }

        public string SourceField { get; set; }

        public string TargetField { get; set; }

        private DataFrame data;
        /// <summary>
        /// 
        /// </summary>
        public bool IsDataFrameStatement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="jointype"></param>
        /// <param name="sourceField"></param>
        /// <param name="targetField"></param>
        public JoinStatement(string source, string target, string jointype, string sourceField, string targetField) {
            this.Source = source;
            this.Target = target;
            this.JoinType = jointype;
            this.SourceField = sourceField;
            this.TargetField = targetField;             
        }
        /// <summary>
        /// 
        /// </summary>    
        public JoinStatement() {
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spark"></param>
        /// <returns></returns>
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
