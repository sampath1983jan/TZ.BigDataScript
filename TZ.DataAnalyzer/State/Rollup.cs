using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.DataAnalyzer.State
{
    public class Rollup
    {
        public List<IField> Fields { get; set; }
        public string Aggregate { get; set; }
        public List<Search> Conditions { get; set; }
        public List<SortBy> Orders { get; set; }

        public Rollup()
        {
            Fields = new List<IField>();
            Aggregate = "";
            Conditions = new List<Search>();
            Orders = new List<SortBy>();
        }
        public Rollup AddField(IField field)
        {
            this.Fields.Add(field);
            return this;
        }
        public Rollup AddCondition(Search search)
        {
            this.Conditions.Add(search);
            return this;
        }
        public Rollup AddOrder(SortBy order)
        {
            this.Orders.Add(order);
            return this;
        }
    }
}
