using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.State
{
   public class With
    {
        public List<MultipleWhen> WhenItems { get; set; }
        public string Name { get; set; }
        public IList<string> Concat { get; set; }
        public With() {
            Name = "";
            WhenItems = new List<MultipleWhen>();
            Concat = new List<string>();
        }
    }
    public class MultipleWhen {
        public List<When> WhenItem { get; set; }
        public string AliasValue { get; set; }
        public MultipleWhen() {
            WhenItem = new List<When>();
            AliasValue = "";
        }
    }
    public class When { 
        public string FieldName { get; set; }
        public string Value { get; set; }    
        public string Logical { get; set; }
        public string MethodName { get; set; }
        public When() {
            FieldName = "";
            Value = "";
            Logical = "";
        }
    }
}
