using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.State
{
   public class Load
    {
        public string Path { get; set; }
        public Load() { 
        
        }
        public Load(string path) {
            this.Path = path;
        }
    }
}
