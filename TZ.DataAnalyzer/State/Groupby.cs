using System;
using System.Collections.Generic;
using System.Text;
using TZ.DataAnalyzer.State;

namespace TZ.DataAnalyzer.State
{
    public class GroupBy
    {
        public List<IField> Fields { get; set; }

        public List<Search> Conditions { get; set; }
        public List<SortBy> Orders { get; set; }

        public List<AggregateField> AggregateFields { get; set; }
        public GroupBy()
        {
            AggregateFields = new List<AggregateField>();
            Orders = new List<SortBy>();
            Conditions = new List<Search>();
            Fields = new List<IField>();
        }
        public GroupBy AddField(IField field)
        {
            this.Fields.Add(field);
            return this;
        }
        public GroupBy AddCondition(Search search)
        {
            this.Conditions.Add(search);
            return this;
        }
        public GroupBy AddAgg(AggregateField agg)
        {
            this.AggregateFields.Add(agg);
            return this;
        }
        public GroupBy AddOrder(SortBy order)
        {
            this.Orders.Add(order);
            return this;
        }
    }
    public class AggregateField : DataField
    {
        public string AggregateType { get; set; }
        public AggregateField(string name, string alias) : base( name,  alias) { 
        
        }
        public AggregateField() : base() { 
        
        }
    }
}


