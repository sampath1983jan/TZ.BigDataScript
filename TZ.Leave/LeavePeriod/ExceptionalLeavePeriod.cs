using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Leave.LeavePeriod
{
   public class ExceptionalLeavePeriod : ILeavePeriod
    {
        public int ClientID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int LeavePeriodID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime StartDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime EndDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Year { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsCurrentPeriod { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsNextPeriod { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int PreviousLeave { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public LeavePeriodType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }         
        public int ActionBy { get; set; }
        public string LeaveType { get; set; }
        public int UserID { get; set; }
    }
}
