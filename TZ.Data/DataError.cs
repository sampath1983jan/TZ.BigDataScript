using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Data
{
   public class DataError
    {
        public enum ErrorType
        {
            ERROR,
            WARNING,
            NOERROR,
        }
        public string Message { get; set; }
        public ErrorType Type { get; set; }
    }
}
