using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Leave.Components.LeaveType
{
   public class PaidLeaveType:baseLeaveType 
    {
        public PaidLeaveType()
        {
            this.Category = LeaveTypeCategory.PAID;
        }
    }
}
