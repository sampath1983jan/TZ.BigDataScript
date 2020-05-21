using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Leave.Components.LeaveType
{
    public class CompOffLeaveType:baseLeaveType
    {
        public CompOffLeaveType() {
            this.Category = LeaveTypeCategory.COMPOFF;
        }

    }
}
