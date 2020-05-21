using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Leave.LeavePeriod.Handler
{
    public class ExceptionLeavePeriodHandler : BaseImplimentation.ILeavePeriod
    {
        private string Connection { get; set; }
        DataAccess.ExceptionLeavePeriod exp_Data;
        public ExceptionLeavePeriodHandler(string conn) {
            Connection = conn;
        }
        public bool Delete(int clientID, int leavePeriodID)
        {
            exp_Data=  new DataAccess.ExceptionLeavePeriod(Connection);
            return exp_Data.Delete(clientID, leavePeriodID);  
        }
        public void Load(int clientID, int leaveperiodID)
        {
            
        }
        public int Save(ILeavePeriod leavePeriod)
        {
            exp_Data = new DataAccess.ExceptionLeavePeriod(Connection);
           var ExceptionLeavePeriod = (ExceptionalLeavePeriod)leavePeriod;
            leavePeriod.LeavePeriodID = exp_Data.Save(ExceptionLeavePeriod.ClientID,
                ExceptionLeavePeriod.UserID,
                (int)ExceptionLeavePeriod.Type,
                ExceptionLeavePeriod.StartDate,
                ExceptionLeavePeriod.EndDate,
                ExceptionLeavePeriod.IsCurrentPeriod,
                ExceptionLeavePeriod.IsNextPeriod,
                ExceptionLeavePeriod.PreviousLeave,
                ExceptionLeavePeriod.ActionBy,
                ExceptionLeavePeriod.LeaveType
                );
            if (leavePeriod.LeavePeriodID > 0)
            {
                return leavePeriod.LeavePeriodID;
            }
            else {
                return -1;
            }
        }
        public bool Update(ILeavePeriod leavePeriod)
        {
            exp_Data = new DataAccess.ExceptionLeavePeriod(Connection);
            var ExceptionLeavePeriod = (ExceptionalLeavePeriod)leavePeriod;            
            var result = exp_Data.Update(ExceptionLeavePeriod.LeavePeriodID, ExceptionLeavePeriod.ClientID,
                ExceptionLeavePeriod.UserID,
                (int)ExceptionLeavePeriod.Type,
                ExceptionLeavePeriod.StartDate,
                ExceptionLeavePeriod.EndDate,
                ExceptionLeavePeriod.IsCurrentPeriod,
                ExceptionLeavePeriod.IsNextPeriod,
                ExceptionLeavePeriod.PreviousLeave,
                ExceptionLeavePeriod.ActionBy,
                ExceptionLeavePeriod.LeaveType
                );
            return result;
        }
    }
}
