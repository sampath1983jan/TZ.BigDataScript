using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.DataAnalyzer.State
{
    public class SortBy
    {
        public string SortType {get;set;}
        public string FieldName { get; set; }
        public SortBy(string sorttype,string name) {
            this.SortType = sorttype;
            this.FieldName = name;
        }
        public SortBy() { 
        
        }
    }
}
