using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Leave.LeavePeriod
{
    public enum LeavePeriodType { 
    ORGANIZE,
    EXCEPTIONAL
    }
   public interface ILeavePeriod
    {
         int ClientID { get; set; }
         int LeavePeriodID { get; set; }
         DateTime StartDate { get; set; }
         DateTime EndDate {get;set;}
        int Year { get; set; }
        bool IsCurrentPeriod { get; set; }
        bool IsNextPeriod { get; set; }
        int PreviousLeave { get; set; }
        LeavePeriodType Type { get; set; }
    
    }

}
