using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using TZ.Leave.Data;

namespace TZ.Leave.DataAccess
{
   public class ExceptionLeavePeriod: DataBase
    {

        private DBDatabase db;
        
        public ExceptionLeavePeriod(string conn)
        {
            InitDbs(conn);
            db = base.Database;
        }

        public DataTable GetLeavePeriod(int clientID, int exceptionLeavePeriodID) {
            DataTable dt = new DataTable();

            return dt;
        }
        public DataTable GetCurrentLeavePeriod(int clientID, int UserID) {
            DataTable dt = new DataTable();

            return dt;
        }
        public int Save(int clientID,
            int userID,
            int type,
            DateTime startDate,
            DateTime endDate,
            bool isCurrentPeriod,
            bool isNextPeriod,
            int previousPeriodLevel,
            int actionBy,
            string LeaveType) {

            return -1;
        }

        public bool Update(int exceptionLeavePeriodID,
            int clientID,
            int userID,
            int type,
            DateTime startDate,
            DateTime endDate,
            bool isCurrentPeriod,
            bool isNextPeriod,
            int previousPeriodLevel,
            int actionBy,
            string LeaveType) {
            return true;

        }

        public bool Delete(int clientID,int exceptionLeavePeriodID) {
            return true;
        }


    }
}
