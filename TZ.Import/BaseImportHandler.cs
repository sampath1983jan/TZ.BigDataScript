using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Import
{
    public abstract class BaseImportHandler
    {
        public BaseImportHandler NextStep { get; private set; }
        public ImportContext Context { get; set; }
 
        public BaseImportHandler(BaseImportHandler nextHandler, ImportContext context)
        {
            NextStep = nextHandler;
            Context = context;          
        }
        public abstract ImportError Validate();
        public abstract ImportError HandleRequest(string LogPath);
    }
}
