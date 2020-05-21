using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Leave.Components.LeaveType
{
    public class UnPaidLeaveType:baseLeaveType 
    {
        public UnPaidLeaveType()
        {
            this.Category = LeaveTypeCategory.UNPAID;
        }
    }
}
