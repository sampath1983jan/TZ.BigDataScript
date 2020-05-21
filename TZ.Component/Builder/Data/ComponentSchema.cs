using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;

namespace TZ.CompExtention.Builder
{
    namespace Schema
    {
        public class TalentozSchema
        {
            public const string Table = "tz_schema";
            public static readonly DBColumn ComponentID = DBColumn.Column("componentId", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentName = DBColumn.Column("ComponentName", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn Category = DBColumn.Column("Category", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ComponentType = DBColumn.Column("ComponentType", System.Data.DbType.Int32, DBColumnFlags.Nullable);
            public static readonly DBColumn Title = DBColumn.Column("Title", System.Data.DbType.String, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn PrimaryKeys = DBColumn.Column("PrimaryKeys", System.Data.DbType.AnsiString, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn TableName = DBColumn.Column("TableName", System.Data.DbType.String, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn IsGlobal = DBColumn.Column("IsGlobal", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn ComponentState = DBColumn.Column("ComponentState", System.Data.DbType.Int32, DBColumnFlags.Nullable);
            public static readonly DBColumn EntityKey = DBColumn.Column("EntityKey", System.Data.DbType.String, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
        public class TalentozSchemaInfo
        {
            public const string Table = "tz_schemainfo";   
            public static readonly DBColumn AttributeName = DBColumn.Column("AttributeName", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn DisplayName = DBColumn.Column("DisplayName", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ComponentID = DBColumn.Column("ComponentID", System.Data.DbType.String, 255);
            public static readonly DBColumn FieldID = DBColumn.Column("FieldID", System.Data.DbType.String, 255);
            public static readonly DBColumn IsRequired = DBColumn.Column("IsRequired", System.Data.DbType.Boolean, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsUnique = DBColumn.Column("IsUnique", System.Data.DbType.Boolean, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsCore = DBColumn.Column("IsCore", System.Data.DbType.Boolean, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsReadOnly = DBColumn.Column("IsReadOnly", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn IsSecured = DBColumn.Column("IsSecured", System.Data.DbType.Boolean, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn LookUpID = DBColumn.Column("LookUpID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn AttributeType = DBColumn.Column("AttributeType", System.Data.DbType.Int32, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn Length = DBColumn.Column("Length", System.Data.DbType.Int32, 100000000, DBColumnFlags.Nullable);
            public static readonly DBColumn DefaultValue = DBColumn.Column("DefaultValue", System.Data.DbType.String, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn FileExtension = DBColumn.Column("FileExtension", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn RegExp = DBColumn.Column("RegExp", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsNullable = DBColumn.Column("IsNullable", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn ISPrimaryKey = DBColumn.Column("ISPrimaryKey", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn LookupComponent = DBColumn.Column("LookupComponentID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ComponentLookupDisplayName = DBColumn.Column("ComponentLookupDisplayName", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsAuto = DBColumn.Column("IsAuto", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
        public class TalentozSchemaClientInfo
        {
            public const string Table = "tz_schemaclientinfo";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.Int32, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn AttributeName = DBColumn.Column("AttributeName", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn DisplayName = DBColumn.Column("DisplayName", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ComponentID = DBColumn.Column("ComponentID", System.Data.DbType.String, 255);
            public static readonly DBColumn FieldID = DBColumn.Column("FieldID", System.Data.DbType.String, 255);
            public static readonly DBColumn IsRequired = DBColumn.Column("IsRequired", System.Data.DbType.Boolean, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsUnique = DBColumn.Column("IsUnique", System.Data.DbType.Boolean, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsCore = DBColumn.Column("IsCore", System.Data.DbType.Boolean, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsReadOnly = DBColumn.Column("IsReadOnly", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn IsSecured = DBColumn.Column("IsSecured", System.Data.DbType.Boolean, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn LookUpID = DBColumn.Column("LookUpID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn AttributeType = DBColumn.Column("AttributeType", System.Data.DbType.Int32, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn Length = DBColumn.Column("Length", System.Data.DbType.Int32, 100000000, DBColumnFlags.Nullable);
            public static readonly DBColumn DefaultValue = DBColumn.Column("DefaultValue", System.Data.DbType.String, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn FileExtension = DBColumn.Column("FileExtension", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn RegExp = DBColumn.Column("RegExp", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsNullable = DBColumn.Column("IsNullable", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn ISPrimaryKey = DBColumn.Column("ISPrimaryKey", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn LookupComponent = DBColumn.Column("LookupComponentID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ComponentLookupDisplayName = DBColumn.Column("ComponentLookupDisplayName", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsAuto = DBColumn.Column("IsAuto", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
        public class TalentozView
        {
            public const string Table = "tz_view";           
            public static readonly DBColumn ViewID = DBColumn.Column("ViewID", System.Data.DbType.String, 255);
            public static readonly DBColumn CoreComponent = DBColumn.Column("CoreComponent", System.Data.DbType.String, 255,DBColumnFlags.Nullable);
            public static readonly DBColumn Name = DBColumn.Column("Name", System.Data.DbType.String, 255,DBColumnFlags.Nullable);
            public static readonly DBColumn Catgory = DBColumn.Column("Catgory", System.Data.DbType.String, 255,DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
        public class TalentozViewSchema
        {
            public const string Table = "tz_viewschema";
            public static readonly DBColumn ViewID = DBColumn.Column("ViewID", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentID = DBColumn.Column("ComponentID", System.Data.DbType.String, 255,DBColumnFlags.Nullable);
            public static readonly DBColumn ChildComponentID = DBColumn.Column("ChildComponentID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ComponentAlias = DBColumn.Column("ComponentAlias", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
        public class TalentozViewSchemaRelation {
            public const string Table = "tz_viewschemarelation";
            public static readonly DBColumn ViewID = DBColumn.Column("ViewID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ViewSchemaRelation = DBColumn.Column("ViewSchemaRelationID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ParentField = DBColumn.Column("ParentField", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn RelatedField = DBColumn.Column("RelatedField", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn Parent = DBColumn.Column("Parent", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn Child = DBColumn.Column("Child", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
        public class TalentozTemplate
        {
            
            public const string Table = "tz_importTemplate";
            public static readonly DBColumn TemplateID = DBColumn.Column("TemplateID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.Int32, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ViewID = DBColumn.Column("ViewID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn TemplateCode = DBColumn.Column("TemplateCode", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn  Name = DBColumn.Column("Name", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn Category = DBColumn.Column("Category", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn PivotColumn = DBColumn.Column("pivotcolumn", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn TemplateType = DBColumn.Column("TemplateType", System.Data.DbType.Int32, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ViewFields = DBColumn.Column("ViewFields", System.Data.DbType.AnsiString , 255000, DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
        public class TalentozImportLog
        {
            public const string Table = "tz_import_log";
            public static readonly DBColumn ImportID = DBColumn.Column("ImportID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ImportSchema = DBColumn.Column("ImportSchema", System.Data.DbType.AnsiString, 5343555, DBColumnFlags.Nullable);
            public static readonly DBColumn ActionBy = DBColumn.Column("ActionBy", System.Data.DbType.Int32, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn Step = DBColumn.Column("Step", System.Data.DbType.Int32, 255, DBColumnFlags.Nullable); 
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
    }
}
