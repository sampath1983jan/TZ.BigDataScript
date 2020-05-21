using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
   public interface IAttribute
    {
         string Name { get; set; }
         string ID { get; set; }
         int Length { get; set; }
         bool IsRequired { get; set; }
         bool IsUnique { get; set; }
         bool IsCore { get; set; }
         bool IsKey { get; set; }
         string DisplayName { get; set; }
         string DefaultValue { get; set; }
         AttributeType Type { get; set; }
         bool IsSecured { get; set; }
         string FileExtension { get; set; }
         bool IsNullable { get; set; }
         bool IsAuto { get; set; }
         string LookupInstanceID { get; set; }
         string ComponentID { get; set; }
        int Min { get; set; }
        int Max { get; set; }
          string LookupDisplayField { get; set; }
          string ComponentLookupDisplayField { get; set; }
        bool IsCustomField { get; set; }
    }
}
