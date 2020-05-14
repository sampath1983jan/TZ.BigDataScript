using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention.Builder
{
   public class TalentozComponent
    {
        public int ClientID { get; set; }
        public int ComponentID { get; set; }
        public string ComponentName { get; set; }
        public string TableName { get; set; }
        public string IDField1Name { get; set; }
        public string IDField2Name { get; set; }
        public string IDField3Name { get; set; }
        public string IDField4Name { get; set; }
        public string TitleField { get; set; }
        public string TitlePattern { get; set; }
        public List<FieldElement> TitleFields { get; set; }
        public int ComponentType { get; set; }
    
        public string TitleFieldTable { get; set; }
        public string LD { get; set; }
        public string VLD { get; set; }
        public string WLD { get; set; }
        public int TagID { get; set; }
        public string ComboField { get; set; }
    }
}
