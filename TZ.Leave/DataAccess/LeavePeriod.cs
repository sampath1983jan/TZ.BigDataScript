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
    public class LeavePeriod:DataBase
    {
        private DBDatabase db;

        public LeavePeriod(string conn)
        {
            InitDbs(conn);
            db = base.Database;
        }

        public DataTable GetLeavePeriod(int clientID, int exceptionLeavePeriodID)
        {
            DataTable dt = new DataTable();

            return dt;
        }
        public DataTable GetCurrentLeavePeriod(int clientID, int UserID)
        {
            DataTable dt = new DataTable();
            return dt;
        }
        public int Save(int clientID,
            DateTime startDate,
            DateTime endDate,
            bool isCurrentPeriod,
            bool isNextPeriod,
            int previousPeriodLevel,
            int Year)
        {

            return -1;
        }

        public bool Update(int exceptionLeavePeriodID,
            int clientID,
            DateTime startDate,
            DateTime endDate,
            bool isCurrentPeriod,
            bool isNextPeriod,
            int previousPeriodLevel, int Year)
        {
            return true;

        }

    
    }
}
