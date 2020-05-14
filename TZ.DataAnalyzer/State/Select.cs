using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.DataAnalyzer.State
{
    public class Select
    {
        public List<IField> Fields { get; set; }
        public string Aggregate { get; set; }
        public List<Search> Conditions { get; set; }
        public List<SortBy> Orders { get; set; }

        public Select()
        {
            Fields = new List<IField>();
            Aggregate = "";
            Conditions = new List<Search>();
            Orders = new List<SortBy>();
        }
        public Select AddField(IField field)
        {
            this.Fields.Add(field);
            return this;
        }
        public Select AddCondition(Search search)
        {
            this.Conditions.Add(search);
            return this;
        }
        public Select AddOrder(SortBy order)
        {
            this.Orders.Add(order);
            return this;
        }
    }
}
