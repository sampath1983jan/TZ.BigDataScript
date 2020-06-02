using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data.Query;
using TZ.CompExtention.Builder.Data;

namespace TZ.CompExtention
{
    public static class Shared
    {
        public static string generateID()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = String.Format("{0:d4}", (DateTime.Now.Ticks / 10) % 1000000000);

            return Guid.NewGuid().ToString("N") + number;
        }

        public static void ExecuteSetup(string conn) {
            Builder.Data.Setup s = new Builder.Data.Setup(conn);         
            s.Install();
        }

        public static void ClearSchema(string conn) {
            Builder.Data.Setup s = new Builder.Data.Setup(conn);
            s.Clear();
        }
        public static void ConvertoImportComponent(string connection,int ClientID) {
          var  dtComponentInstance = CompExtention.Shared.GetComponentList(ClientID, connection);
            var TZComponents = new List<CompExtention.Builder.TalentozComponent>();
            var dtComp = dtComponentInstance.DefaultView.ToTable(true, "CompType", "CompAttribute");
             
            foreach (DataRow s in dtComp.Rows)
            {
                var ft = s["CompType"];
                TZ.CompExtention.Builder.TalentozComponent comp =
                     Newtonsoft.Json.JsonConvert.DeserializeObject<TZ.CompExtention.Builder.TalentozComponent>(s["CompAttribute"].ToString());
                comp.ComponentID = Convert.ToInt32(ft);
                TZComponents.Add(comp);
            
            }

            DataTable dtLookUpComponent = new DataTable();
            List<CompExtention.Builder.FieldElement> TalentozComponentFields = new List<CompExtention.Builder.FieldElement>();
            List<int> LookUpComponent = new List<int>();
            List<CompExtention.Builder.FieldElement> LookupComponentDisplayField = new List<CompExtention.Builder.FieldElement>();
            TZ.CompExtention.ComponentManager cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));

            foreach (DataRow dr in dtComponentInstance.Rows)
            {
                CompExtention.Builder.FieldElement f = new CompExtention.Builder.FieldElement();
                f = Newtonsoft.Json.JsonConvert.DeserializeObject<CompExtention.Builder.FieldElement>(dr["FieldAttribute"].ToString());
                f.FieldInstanceID = Convert.ToInt32(dr["FieldInstanceID"].ToString());
                f.FieldDescription = (dr["FieldDescription"].ToString());
                f.ComponentID = Convert.ToInt32(dr["CompType"].ToString());
                f.FieldTypeID = Convert.ToInt32(dr["FieldTypeID"].ToString());
                if (f.FieldTypeID == 22)
                {
                    if (f.FieldComponent != "")
                    {
                        LookUpComponent.Add(Convert.ToInt32(f.ComponentID));
                        var fComponent = TZComponents.Where(x => x.ComponentID == Convert.ToInt32(f.FieldComponent)).FirstOrDefault();
                        if (fComponent != null) {
                            f.FieldHelp = fComponent.TitleField;
                        }                    

                        LookupComponentDisplayField.Add(f);
                    }
                }
                TalentozComponentFields.Add(f);
            }
            LookUpComponent = LookUpComponent.Distinct().ToList();
            Dictionary<int, string> tzComponentWithImportComp = new Dictionary<int, string>();



            foreach (CompExtention.Builder.TalentozComponent comp in TZComponents)
            {

                TZ.CompExtention.Component Comp = new CompExtention.Component(comp.ComponentName, GetType(comp.ComponentType));
                if (comp != null)
                {
                    Comp.TableName = comp.TableName;
                    dtComponentInstance.DefaultView.RowFilter = "FieldTypeID = " + comp.ComponentID;
                    DataTable dtAtt = dtComponentInstance.DefaultView.ToTable(true);
                    dtComponentInstance.DefaultView.RowFilter = "";
                    //CompExtention.Builder.FieldElement f = new CompExtention.Builder.FieldElement();

                    CompExtention.Attribute _att_id1 = Comp.NewAttribute(ClientID);
                    _att_id1.IsKey = true;
                    _att_id1.Name = comp.IDField1Name;
                    _att_id1.DisplayName = comp.IDField1Name;
                    _att_id1.Type = CompExtention.AttributeType._number;
                    _att_id1.IsNullable = false;
                    Comp.AddAttribute(_att_id1);
                    Comp.Keys.Add(_att_id1);

                    CompExtention.Attribute _att_id2 = Comp.NewAttribute(ClientID);
                    _att_id2.IsKey = true;
                    _att_id2.Name = comp.IDField2Name;
                    _att_id2.DisplayName = comp.IDField2Name;
                    _att_id2.Type = CompExtention.AttributeType._number;
                    _att_id2.IsNullable = false;
                    Comp.AddAttribute(_att_id2);
                    Comp.Keys.Add(_att_id2);

                    if (comp.IDField3Name != "")
                    {
                        CompExtention.Attribute _att_id3 = Comp.NewAttribute(ClientID);
                        _att_id3.IsKey = true;
                        _att_id3.Name = comp.IDField3Name;
                        _att_id3.DisplayName = comp.IDField3Name;
                        _att_id3.Type = CompExtention.AttributeType._number;
                        _att_id3.IsNullable = false;
                        Comp.AddAttribute(_att_id3);
                        Comp.Keys.Add(_att_id3);
                    }
                    if (comp.IDField4Name != "")
                    {
                        CompExtention.Attribute _att_id4 = Comp.NewAttribute(ClientID);
                        _att_id4.IsKey = true;
                        _att_id4.Name = comp.IDField4Name;
                        _att_id4.DisplayName = comp.IDField4Name;
                        _att_id4.Type = CompExtention.AttributeType._number;
                        _att_id4.IsNullable = false;
                        Comp.AddAttribute(_att_id4);
                        Comp.Keys.Add(_att_id4);
                    }

                    var flist = TalentozComponentFields.Where(x => x.ComponentID == comp.ComponentID).ToList();

                    foreach (CompExtention.Builder.FieldElement f in flist)
                    {
                        CompExtention.Attribute _att = Comp.NewAttribute(ClientID);
                        _att.Name = "F_" + f.FieldInstanceID;
                        _att.DisplayName = f.FieldDescription;
                        _att.DefaultValue = f.DefaultValue;
                        _att.FileExtension = f.FileExtension;
                        _att.LookupInstanceID = f.FieldInstanceLookUpID.ToString();
                        _att.Length = f.MaxLength;
                        _att.IsUnique = f.isUnique;
                        _att.IsRequired = f.isRequired;
                        _att.IsNullable = true;
                        _att.Type = GetAttributeType(f.FieldTypeID);
                        _att.IsCore = f.isCore;
                        if (f.FieldTypeID == 22)
                        {


                        }

                        _att.IsAuto = false;
                        _att.IsSecured = false;
                        Comp.AddAttribute(_att);
                    }
                    cm.Save(Comp);
                    tzComponentWithImportComp.Add(comp.ComponentID, cm.Component.ID); // mapping talentoz component to import component ids.
                    var compLookup_imp = Comp.Attributes.Where(x => x.Type == CompExtention.AttributeType._componentlookup);
                    foreach (CompExtention.Attribute a in compLookup_imp)
                    {
                        // LookupComponentDisplayField.Add(Convert.ToInt32(f.FieldComponent), f);
                        var cdfield = LookupComponentDisplayField.Where(x => x.FieldDescription == a.DisplayName && x.ComponentID == comp.ComponentID).FirstOrDefault();
                        if (cdfield != null) {
                            cdfield.FieldValue = a.ID;
                        }                       
                        //   cdfield.Value.FileExtension = a.ComponentID;
                    }

                    foreach (CompExtention.Builder.FieldElement a in LookupComponentDisplayField)
                    {
                        var atc = Comp.Attributes.Where(x => x.Name == a.FieldHelp).FirstOrDefault();
                        if (atc != null)
                        {
                            a.FieldExpression = atc.ID;
                        }
                    }
                }
            }
            foreach (CompExtention.Builder.FieldElement a in LookupComponentDisplayField)
            {
                var k = tzComponentWithImportComp.Where(x => x.Key == Convert.ToInt32(a.FieldComponent)).FirstOrDefault();

                cm.UpdateComponentLookup(ClientID, "", a.FieldValue, k.Value, a.FieldExpression);
            }
        }
        public static DataTable GetClientList (string conn)
        {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery select;
            string[] a = { "ClientID", "CustomerName"};
            select = DBQuery.Select().Fields(a).From("sys_client").Distinct();
            return db.Database.GetDatatable(select);
        }
        public static bool IsValidConnection(string conn) {
            try
            {
                DataBase db = new DataBase();
                db.InitDbs(conn);
             var dbqu=   DBQuery.SelectAll().From("sys_client").TopN(1) ;
                db.Database.GetDatatable(dbqu);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public static DataTable GetComponentList(int clientid,string conn) {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBComparison dbClient = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(clientid));
            DBComparison dbclientZero = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(0));
            DBComparison ft = DBComparison.In (DBField.Field("CompType"), DBConst.Int32(320000),DBConst.Int32(300000));
            
            DBQuery select;
            select = DBQuery.Select().Field("sys_FieldInstance", "CompType")
                .Field("sys_FieldType", "CompAttribute")
                 .Field("sys_FieldInstance", "FieldInstanceID")
                 .Field("sys_FieldInstance", "FieldTypeID")
                  .Field("sys_FieldInstance", "FieldAttribute")
                  .Field("sys_FieldInstance", "FieldDescription")
                .From("sys_FieldType").InnerJoin("sys_FieldInstance").On("sys_FieldType", "FieldTypeID", Tech.Data.Compare.Equals, "sys_FieldInstance", "CompType").WhereAll( dbClient);
            return db.Database.GetDatatable(select);
        }
        public static string[] GetCSVToArray(this DataTable dataTable) {
            List<string> fileContent = new List<string>();
            List<string> cols= new List<string>();

            foreach (var col in dataTable.Columns)
            {
                cols.Add(("\"" + col.ToString() + "\""));
            }
            fileContent.Add(string.Join(",", cols.ToArray()));
           
            foreach (DataRow dr in dataTable.Rows)
            {
                List<string> rows = new List<string>();
                foreach (var column in dr.ItemArray)
                {
                    rows.Add("\"" + column.ToString() + "\"");
                }
                fileContent.Add(string.Join(",", rows.ToArray()));           
            }
            return fileContent.ToArray();
        }
        public static string GetCSV(this DataTable dataTable)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            return fileContent.ToString();
        }

        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        public static DataTable GetLookup(int clientid,string conn)
        {
            DBComparison dbClient = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(clientid));
            DBComparison dbclientZero = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(0));
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery select;
            string[] a = { "FieldInstanceID", "Name", "Type" };
            select = DBQuery.Select().Fields(a).From("sys_lookup").WhereAny(dbClient, dbclientZero).Distinct();
            return db.Database.GetDatatable(select);
        }

        public static CompExtention.AttributeType GetAttributeType(int tzFieldID)
        {
            if (tzFieldID == 0)
            {
                return CompExtention.AttributeType._date;

            }
            else if (tzFieldID == 1)
            {
                return CompExtention.AttributeType._datetime;
            }
            else if (tzFieldID == 22)
            {
                return CompExtention.AttributeType._componentlookup;
            }
            else if (tzFieldID == 13)
            {
                return CompExtention.AttributeType._decimal;
            }
            else if (tzFieldID == 12)
            {
                return CompExtention.AttributeType._decimal;
            }
            else if (tzFieldID == 20)
            {
                return CompExtention.AttributeType._file;
            }
            else if (tzFieldID == 19)
            {
                return CompExtention.AttributeType._string;
            }
            else if (tzFieldID == 6)
            {
                return CompExtention.AttributeType._longstring;
            }
            else if (tzFieldID == 14)
            {
                return CompExtention.AttributeType._multilookup;
            }
            else if (tzFieldID == 1)
            {
                return CompExtention.AttributeType._number;
            }
            else if (tzFieldID == 18)
            {
                return CompExtention.AttributeType._picture;
            }
            else if (tzFieldID == 8)
            {
                return CompExtention.AttributeType._bit;
            }
            else if (tzFieldID == 3)
            {
                return CompExtention.AttributeType._lookup;
            }
            else if (tzFieldID == 2)
            {
                return CompExtention.AttributeType._string;
            }
            else if (tzFieldID == 21)
            {
                return CompExtention.AttributeType._datetime;
            }
            else
            {
                return CompExtention.AttributeType._string;
            }

        }
        public static CompExtention.ComponentType GetType(int type)
        {
            if (type == 1)
            {
                return CompExtention.ComponentType.core;
            }
            else if (type == 2)
            {
                return CompExtention.ComponentType.link;
            }
            else if (type == 3)
            {
                return CompExtention.ComponentType.attribute;
            }
            else if (type == 4)
            {
                return CompExtention.ComponentType.core;
            }
            else
            {
                return CompExtention.ComponentType.core;
            }
        }

    }


}
