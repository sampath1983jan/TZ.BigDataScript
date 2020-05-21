using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Leave.Components
{
   public class LeaveConfiguration
    {
        public int ClientID { get; set; }
        public int LeaveTypeID { get; set; }
        public int ConfiguationID { get; set; }
        public string  Name { get; set; }
        public int ConfigurationType { get; set; }
        public int GroupID { get; set; }
        public string Value { get; set; }
        public int DependentConfiguration { get; set; }
                      
        
    }
}
