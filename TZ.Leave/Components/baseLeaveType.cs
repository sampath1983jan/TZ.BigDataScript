using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Leave.Components
{
    public enum LeaveTypeCategory { 
    PAID,
    UNPAID,
    COMPOFF,
    RESERVEDHOLIDAY
    }
    public abstract class baseLeaveType
    {
        public int ClientID { get; set; }
        public int LeaveTypeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAllUnit { get; set; }
        public bool IsAllEmployee { get; set; }
        public string ColorCode { get; set; }
        public LeaveTypeCategory Category { get; set; }
        public bool IsDisplayInPayslip { get; set; }
        public int OrderNo { get; set; }
        LeaveConfiguration Configuration { get; set; }
        public List<int> EmployeeGroups { get; set; }
            
        public  void LoadConfiguration() { 
        
        }
        public void SaveConfiguration() { 
        
        }
        public void AddEmployeeGroup(int GroupID) {
            this.EmployeeGroups.Add(GroupID);
        }

    }
}
