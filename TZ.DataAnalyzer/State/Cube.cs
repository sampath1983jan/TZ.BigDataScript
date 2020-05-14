using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.DataAnalyzer.State
{
    public class Cube
    {
        public List<IField> CubeFields { get; set; }
        public string Aggregate { get; set; }
        public List<Search> Conditions { get; set; }
        public List<SortBy> Orders { get; set; }

        public Cube() {
            CubeFields = new List<IField>();
            Aggregate = "";
            Conditions = new List<Search>();
            Orders = new List<SortBy>();
        }
        public Cube AddField(IField field)
        {
            this.CubeFields.Add(field);
            return this;
        }
        public Cube AddCondition(Search search) {
            this.Conditions.Add(search);
            return this;
        }
        public Cube AddOrder(SortBy order) {
            this.Orders.Add(order);
            return this;
        }
    }

}
