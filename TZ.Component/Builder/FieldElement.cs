using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention.Builder
{
    public class FieldElement
    {
        public int FieldInstanceID { get; set; }
        public int FieldTypeID { get; set; }
        public int FieldInstanceLookUpID { get; set; }
        public int ComponentID { get; set; }
        public string FieldDescription { get; set; }
        public bool isRequired { get; set; }
        public bool isCore { get; set; }
        public bool isUnique { get; set; }
        public string DefaultValue { get; set; }
        public int FieldGroupID { get; set; }
        public string FieldDisplayOrder { get; set; }
        public string FileExtension { get; set; }
        public string FieldHelp { get; set; }
        public string FieldValue { get; set; }
        public string FieldLookUpValue { get; set; }
        public int ClientID { get; set; }
        public bool IsCR { get; set; }
        public bool IsDependant { get; set; } = false;
        public string ParentField { get; set; } = "";
        public string ChildField { get; set; } = "";
        public string FieldExpression { get; set; }
        public string ExpressionFields { get; set; }
        public bool EnableContentLimit { get; set; } = false;
        public int MaxLength { get; set; }
        public int NoOfDecimalPlaces { get; set; }
        public string AutoFieldFormat { get; set; }
        public string AutoIndex { get; set; }
        public bool isReadOnly { get; set; }
        public string FieldComponent { get; set; } = "";
        public string ViewSource { get; set; } = "";
        public string OldFieldValue { get; set; } = "";
        public string OldTextValue { get; set; } = "";
        public string NewTextValue { get; set; } = "";
        public FieldElement()
        {
            this.FieldInstanceID = 0;
            this.FieldTypeID = 0;
            this.isReadOnly = false;
            this.FieldInstanceLookUpID = -1;
            this.ComponentID = -1;
            this.FieldDescription = "";
            this.isRequired = false;
            this.isCore = false;
            this.isUnique = false;
            this.DefaultValue = "";
            this.FieldGroupID = -1;
            this.FieldDisplayOrder = "0";
            this.FileExtension = "";
            this.FieldHelp = "";
            this.EnableContentLimit = false;
            this.MaxLength = 0;
            this.NoOfDecimalPlaces = 0;
        }
 
    }

}
