using Microsoft.Spark.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.DataAnalyzer
{
    public enum SchemaType { 
    PARAM,
    VARIABLE,
    DATACUBE,
    }
    public class Schema
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<SchemaStructField> StructFields { get; set; }
        public DataFrame Data { get; set; }
        public SchemaType Type { get; set; }
        public string Value { get; set; }
        public Schema() {
            Type = SchemaType.DATACUBE;
            Environment.GetEnvironmentVariable("DOTNETBACKEND_PORT");
        }
    }
}
