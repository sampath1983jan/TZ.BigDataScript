
using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.DataAnalyzer
{
    /// <summary>
    /// 
    /// </summary>
   public class SchemaStructField
    {
        /// <summary>
        /// 
        /// </summary>
        public enum SchemaFieldType
        {
            NUMBER,
            STRING,
            DATE,
            BOOLEAN,
            ARRAY,
            OBJECT
        }
        /// <summary>
        /// 
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SchemaFieldType FieldType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fieldType"></param>
        /// <param name="alias"></param>
        public SchemaStructField(string name,SchemaFieldType fieldType,string alias ="") {
            this.FieldName = name;
            this.FieldType = fieldType;
            this.AliasName = alias;
        }
        public SchemaStructField() {
            this.FieldName = "";
            this.FieldType =  SchemaFieldType.STRING;
            this.AliasName = "";
        }
    }
}
