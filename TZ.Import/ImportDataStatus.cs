using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Import
{

    public enum DataStatus
    {
        NOTSTARTED,
        STARTED,
        COMPLETED,
        ERROR
    }

    public  class ImportDataStatus
    {       
        public string ComponentID { get; set; }
        public string ComponentName { get; set; }
        public int DataProcessedCount { get; set; }
        public double PercentageToComplete { get; set; }
        public string Messge { get; set; }
        public DataStatus Status { get; set; }
    }
}
