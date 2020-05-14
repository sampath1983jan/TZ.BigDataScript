using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public enum JoinType { 
    inner,
    left,
    right,
    full,
    }
  public  class ViewRelation
    {
        public string ID { get; set; }
        public string Left { get; set; }
        public string LeftField { get; set; }
        public string Right { get; set; }
        public string RightField { get; set; }
        public JoinType Join { get; set; }

        public ViewRelation() {
            ID = "";
            Left = "";
            LeftField = "";
            Right = "";
            RightField = "";
            Join = JoinType.left;
        }
    }
}
