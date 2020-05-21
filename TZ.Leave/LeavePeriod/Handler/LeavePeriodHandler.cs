using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Leave.LeavePeriod.Handler
{
    public class LeavePeriodHandler : BaseImplimentation.ILeavePeriod
    {
        private string Connection { get; set; }
        DataAccess.LeavePeriod exp_Data;
        public LeavePeriodHandler(string conn)
        {
            Connection = conn;
        }
        public bool Delete(int clientID, int leavePeriodID)
        {
            throw new NotImplementedException();
        }
        public void Load(int clientID, int leaveperiodID)
        {
            exp_Data = new DataAccess.LeavePeriod(Connection);
        }
        public int Save(ILeavePeriod leavePeriod)
        {
            exp_Data = new DataAccess.LeavePeriod(Connection);
            var orgLeavePeriod = (OrganizationLeavePeriod)leavePeriod;
            leavePeriod.LeavePeriodID = exp_Data.Save(orgLeavePeriod.ClientID,
                orgLeavePeriod.StartDate,
                orgLeavePeriod.EndDate,
                orgLeavePeriod.IsCurrentPeriod,
                orgLeavePeriod.IsNextPeriod,
                orgLeavePeriod.PreviousLeave,
                orgLeavePeriod.Year
                );
            if (leavePeriod.LeavePeriodID > 0)
            {
                return leavePeriod.LeavePeriodID;
            }
            else
            {
                return -1;
            }
        }
        public bool Update(ILeavePeriod leavePeriod)
        {
            exp_Data = new DataAccess.LeavePeriod(Connection);
               throw new NotImplementedException();
        }
    }
}
