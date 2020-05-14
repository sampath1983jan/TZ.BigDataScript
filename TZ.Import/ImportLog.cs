using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Import
{
    public class ImportLog
    {
        public string ImportContext { get; set; }
        public int Step { get; set; }
        public string ID { get; set; }
        public int ClientID { get; set; }
        public DateTime DateonCreate { get; set; }
        public DateTime LastModified {get;set;}
        public string ActionBy { get; set; }
    }
}
