using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.DataAnalyzer.State
{
    public class Search
    {
        public string FieldName { get; set; }
        public string Operation { get; set; } 
        public string FieldValue { get; set; }

        public Search(string name, string operation, string value) {
            this.FieldName = name;
            this.Operation = operation;
            this.FieldValue = value;
        }
        public Search() {
            this.FieldName = "";
            this.Operation = "";
            this.FieldValue = "";
        }
    }
}
