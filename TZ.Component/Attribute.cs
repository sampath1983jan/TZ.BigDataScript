using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public enum AttributeType {
        _number,
        _decimal,
        _string,
        _longstring,
        _currency,
        _lookup,
        _componentlookup,
        _file,
        _picture,
        _date,
        _time,
        _datetime,
        _bit,
        _multilookup,
        
    }
    public class Attribute:IAttribute 
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public int Length { get; set; }
        public bool IsRequired { get; set; }
        public string EntityKey { get; set; }
        public bool IsUnique { get; set; }
        public bool IsCore { get; set; }
        public bool IsKey { get; set; }
        public string DisplayName { get; set; }
        public string DefaultValue { get; set; }
        public AttributeType Type { get; set; }
        public bool IsSecured { get; set; }
        public string FileExtension { get; set; }
        public bool IsNullable { get; set; }
        public bool IsAuto { get; set; }
        public string LookupInstanceID { get; set; }
        public string ComponentID { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string LookupDisplayField { get; set; }
        public string ComponentLookupDisplayField { get; set; }
        public string ComponentLookup { get; set; }
        protected internal int ClientID { get; set; }
        public bool IsCustomField { get; set ; }

        public Attribute() {
            EntityKey = "";
            ComponentID = "";
            LookupInstanceID = "0";
            IsAuto = false;
            IsNullable = true;
            IsSecured = false;
            FileExtension = "";
            Type = AttributeType._string;
            DefaultValue = "";
            DisplayName = "";
            IsKey = false;
            IsCore = false;
            IsUnique = false;
            IsRequired = false;
            Length = 0;
            Name = "";
            LookupDisplayField = "";
            ComponentLookupDisplayField = "";
            ComponentLookup = "";
        }

        public static AttributeType GetAttributeType(DbType type) {
            if (type == DbType.AnsiString)
            {
                return AttributeType._string;
            }
            else if (type == DbType.Int32 || type == DbType.Int16 || type == DbType.Int64 || type == DbType.UInt16 || type == DbType.UInt32 || type == DbType.UInt64 || type == DbType.UInt32)
            {
                return AttributeType._number;
            }
            else if (type == DbType.Double || type == DbType.Single)
            {
                return AttributeType._number;
            }
            else if (type == DbType.Boolean)
            {
                return AttributeType._bit;
            }
            else if (type == DbType.Decimal)
            {
                return AttributeType._decimal;

            }
            else if (type == DbType.Date)
            {
                return AttributeType._date;

            }
            else if (type == DbType.DateTime)
            {
                return AttributeType._datetime;

            }
            else {
                return AttributeType._string;
            }
        }
    }
}
