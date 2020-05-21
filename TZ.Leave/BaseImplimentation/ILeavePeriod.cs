using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.Leave.LeavePeriod;

namespace TZ.Leave.BaseImplimentation
{
    interface ILeavePeriod
    {
        int Save(LeavePeriod.ILeavePeriod leavePeriod );
        bool Update(LeavePeriod.ILeavePeriod leavePeriod);
        bool Delete(int clientID, int leavePeriodID);
        void Load(int clientID,int leaveperiodID);
    }
}
