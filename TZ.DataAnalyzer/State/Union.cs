using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.DataAnalyzer.State
{
    public class Union
    {
        public string UnionData { get; set; }
        public bool IsAll { get; set; }
        public Union() {
            UnionData = "";
        }
    }
}
