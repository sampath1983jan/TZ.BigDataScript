using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tech.Data.Query;
using TZ.CompExtention.Builder;
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

        public static void ConvertEmployeePositionPayAsia(string connection, int ClientID) {

         
            TZ.CompExtention.ComponentManager employee = new CompExtention.ComponentManager(ClientID, "sys_user", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            employee.LoadAttributes();

            TZ.CompExtention.ComponentManager position = new CompExtention.ComponentManager(ClientID, "position", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            position.LoadAttributes();

            //var cm = new CompExtention.ComponentManager();
            //cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            //cm.NewComponent(ClientID, "Employee_Position", (CompExtention.ComponentType.link));
            //var component = (CompExtention.Component)cm.Component;
            //component.TableName = "Employee_Position";
            //TZ.CompExtention.ComponentBuilder cb = new CompExtention.ComponentBuilder(connection);
            //component.Attributes = cb.GetTableFields("Employee_Position", ClientID);

            //foreach (Attribute att in component.Attributes)
            //{
            //    if (att.Name.ToLower() == "EffectiveDate".ToLower())
            //    {
            //        att.DisplayName = "Effective Date";
            //    }

            //}
            //cm.Save(component);

            string formatString_ep = " {0,15:" + "00000000" + "}";

            string code = string.Format(formatString_ep, 123).Trim();

            var tev = new CompExtention.ImportTemplate.Template(code, ClientID, connection);
            string viewid = tev.ViewID;

            

            CompExtention.ComponentViewManager cvmep = new CompExtention.ComponentViewManager(viewid,ClientID ,new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));

            //var view_ep = cvmep.NewView("Employee Position");

            //var vr_ep = new CompExtention.ComponentRelation();
            //var vritem_ep = new ViewRelation();
            //vr_ep.ComponentID = employee.Component.ID;
            //vr_ep.ChildComponentID = Convert.ToString(component.ID);
            //vr_ep.Relationship.Add(new ViewRelation() { Left = vr_ep.ComponentID, Right = vr_ep.ChildComponentID, LeftField = "UserID", RightField = "UserID" });
            //vr_ep.Relationship.Add(new ViewRelation() { Left = vr_ep.ComponentID, Right = vr_ep.ChildComponentID, LeftField = "ClientID", RightField = "ClientID" });
            //view_ep.CoreComponent = vr_ep.ComponentID;
            //view_ep.ComponentRelations.Add(vr_ep);

            //vr_ep = new CompExtention.ComponentRelation();
            //vritem_ep = new ViewRelation();
            //vr_ep.ComponentID = position.Component.ID;
            //vr_ep.ChildComponentID = Convert.ToString(component.ID);
            //vr_ep.Relationship.Add(new ViewRelation() { Left = vr_ep.ComponentID, Right = vr_ep.ChildComponentID, LeftField = "PositionID", RightField = "PositionID" });
            //vr_ep.Relationship.Add(new ViewRelation() { Left = vr_ep.ComponentID, Right = vr_ep.ChildComponentID, LeftField = "ClientID", RightField = "ClientID" });
            //view_ep.ComponentRelations.Add(vr_ep);

            //cvmep.Save(view_ep);
            cvmep.LoadViewComponents();
            CompExtention.ImportTemplate.Template.Remove("Employee Position", connection, ClientID);



            var positionField = "F_360045,F_360025,F_360035,F_360105,F_360050,F_360040,F_360005,F_360020,F_360010,F_360115,F_360105,F_360030".Split(',');

            var empField = "F_200005,F_200305,F_200310,F_200145,";
               empField = empField + "F_200530,F_200030,F_200115,F_200255,F_200140,";
            empField = empField + "F_200200,F_200205,F_200390,F_200225,F_200210,";
            empField = empField + "F_200080,F_200090,F_200085,F_200105,";
            empField = empField + "F_200015,F_200170,F_200040,F_200025,F_200045,F_200010,";
                empField = empField + "F_200405,F_200410,F_200795,F_200155,F_200160";

            var eFields = empField.Split(',');


            var tmp_ep = new CompExtention.ImportTemplate.Template(connection, ClientID);

            string ss = " {0,15:" + "00000000" + "}";
            tmp_ep.Name = "Employee Position";
            tmp_ep.Code = string.Format(ss, 123).Trim();
            tmp_ep.Category = "Employee";
            tmp_ep.ViewID = cvmep.View.ID;

            foreach (Component c in cvmep.View.Components)
            {

                foreach (Attribute att in c.Attributes)
                {
                    if (c.ID == position.Component.ID )
                    {
                        if (positionField.Where(x => x == att.Name).FirstOrDefault() != null)
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            if (att.Name == "F_360025" || att.Name == "F_360035" || att.Name == "F_360050" || att.Name == "F_360010" || att.Name == "F_360030")
                            {
                                te.ID = att.ID;
                                te.IsKey = true;
                                te.IsDefault = true;
                                if (att.Name == "F_360050")
                                {
                                    te.IsRequired = false;
                                }
                                else
                                {
                                    te.IsRequired = true;
                                }
                            }
                            else
                            {
                                te.ID = att.ID;
                                te.IsKey = false;
                                te.IsDefault = false;
                                te.IsRequired = false;
                            }
                            tmp_ep.TemplateFields.Add(te);
                        }
                    }
                    else if (c.ID == employee.Component.ID )
                    {
                        if (eFields.Where(x => x == att.Name).FirstOrDefault() != null)
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.ID = att.ID;
                            if (att.Name == "F_200005")
                            {
                                te.IsKey = true;
                                te.IsDefault = true;
                                te.IsRequired = true;
                                tmp_ep.TemplateFields.Add(te);
                            }
                            else
                            {
                                te.IsKey = false;
                                te.IsDefault = true;
                                te.IsRequired = false;
                                tmp_ep.TemplateFields.Add(te);
                            }
                        }
                    }
                    else
                    {
                        if (att.Name.ToLower() == "EffectiveDate".ToLower())
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.ID = att.ID;
                            te.IsKey = false;
                            te.IsDefault = true;
                            te.IsRequired = true;
                            tmp_ep.TemplateFields.Add(te);
                        }
                    }
                }
            }
            tmp_ep.Save();



        }
        public static void ConvertEmployeePosition(string connection, int ClientID, Dictionary<int, string>  tzComponentWithImportComp) {
            /// Employee Position
            /// 
            var cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Employee_Position", (CompExtention.ComponentType.link));
            var component = (CompExtention.Component)cm.Component;
            component.TableName = "Employee_Position";
            TZ.CompExtention.ComponentBuilder cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("Employee_Position", ClientID);

            foreach (Attribute att in component.Attributes)
            {
                if (att.Name.ToLower() == "EffectiveDate".ToLower())
                {
                    att.DisplayName = "Effective Date";
                }

            }
            cm.Save(component);



            CompExtention.ComponentViewManager cvmep = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
            var view_ep = cvmep.NewView("Employee Position");

            var vr_ep = new CompExtention.ComponentRelation();
            var vritem_ep = new ViewRelation();
            vr_ep.ComponentID = tzComponentWithImportComp.Where(x => x.Key == 30000).FirstOrDefault().Value;
            vr_ep.ChildComponentID = Convert.ToString(component.ID);
            vr_ep.Relationship.Add(new ViewRelation() { Left = vr_ep.ComponentID, Right = vr_ep.ChildComponentID, LeftField = "UserID", RightField = "UserID" });
            vr_ep.Relationship.Add(new ViewRelation() { Left = vr_ep.ComponentID, Right = vr_ep.ChildComponentID, LeftField = "ClientID", RightField = "ClientID" });
            view_ep.CoreComponent = vr_ep.ComponentID;
            view_ep.ComponentRelations.Add(vr_ep);

            vr_ep = new CompExtention.ComponentRelation();
            vritem_ep = new ViewRelation();
            vr_ep.ComponentID = tzComponentWithImportComp.Where(x => x.Key == 360000).FirstOrDefault().Value;
            vr_ep.ChildComponentID = Convert.ToString(component.ID);
            vr_ep.Relationship.Add(new ViewRelation() { Left = vr_ep.ComponentID, Right = vr_ep.ChildComponentID, LeftField = "PositionID", RightField = "PositionID" });
            vr_ep.Relationship.Add(new ViewRelation() { Left = vr_ep.ComponentID, Right = vr_ep.ChildComponentID, LeftField = "ClientID", RightField = "ClientID" });
            view_ep.ComponentRelations.Add(vr_ep);

            cvmep.Save(view_ep);
            cvmep.LoadViewComponents();


            var tmp_ep = new CompExtention.ImportTemplate.Template(connection, ClientID);

            string formatString_ep = " {0,15:" + "00000000" + "}";
            tmp_ep.Name = "Employee Position";
            tmp_ep.Code = string.Format(formatString_ep, 123).Trim();
            tmp_ep.Category = "Employee";
            tmp_ep.ViewID = cvmep.View.ID;

            var position = tzComponentWithImportComp.Where(x => x.Key == 360000).FirstOrDefault().Value;
            var employee = tzComponentWithImportComp.Where(x => x.Key == 30000).FirstOrDefault().Value;
            var positionField = "F_360045,F_360025,F_360035,F_360105,F_360050,F_360040,F_360005,F_360020,F_360010,F_360115,F_360030".Split(',');
            var empField = "F_200005,F_200305,F_200310,F_200145,F_200530,F_200030,F_200115,F_200255,F_200015,F_200170,F_200040,F_200025,F_200045,F_200010,F_200405,F_200410,F_200795,F_200155,F_200160".Split(',');
            foreach (Component c in cvmep.View.Components)
            {

                foreach (Attribute att in c.Attributes)
                {
                    if (c.ID == position)
                    {
                        if (positionField.Where(x => x == att.Name).FirstOrDefault() != null)
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            if (att.Name == "F_360025" || att.Name == "F_360035" || att.Name == "F_360050" || att.Name == "F_360010" || att.Name == "F_360030")
                            {
                                te.ID = att.ID;
                                te.IsKey = true;
                                te.IsDefault = true;
                                if (att.Name == "F_360050")
                                {
                                    te.IsRequired = false;
                                }
                                else
                                {
                                    te.IsRequired = true;
                                }
                            }
                            else
                            {
                                te.ID = att.ID;
                                te.IsKey = false;
                                te.IsDefault = false;
                                te.IsRequired = false;
                            }
                            tmp_ep.TemplateFields.Add(te);
                        }
                    }
                    else if (c.ID == employee)
                    {
                        if (empField.Where(x => x == att.Name).FirstOrDefault() != null)
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.ID = att.ID;
                            if (att.Name == "F_200005")
                            {
                                te.IsKey = true;
                                te.IsDefault = true;
                                te.IsRequired = true;
                                tmp_ep.TemplateFields.Add(te);
                            }
                            else
                            {
                                te.IsKey = false;
                                te.IsDefault = false;
                                te.IsRequired = false;
                                tmp_ep.TemplateFields.Add(te);
                            }
                        }
                    }
                    else
                    {
                        if (att.Name.ToLower() == "EffectiveDate".ToLower())
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.ID = att.ID;
                            te.IsKey = false;
                            te.IsDefault = true;
                            te.IsRequired = true;
                            tmp_ep.TemplateFields.Add(te);
                        }
                    }
                }
            }
            tmp_ep.Save();

            // Employee Position End
        }
        public static void ConvertoImportComponent(string connection,int ClientID) {
          var  dtComponentInstance = CompExtention.Shared.GetComponentList(ClientID, connection);
            var TZComponents = new List<CompExtention.Builder.TalentozComponent>();
            var dtComp = dtComponentInstance.DefaultView.ToTable(true, "CompType", "CompAttribute");
            string[] NoComps = "31000,200000,210000,360200,360300,400000,30600,30800,31000,410000,430000,420000,500000,600000,610000,700000,800000,900000,1000000".Split(',');
            DataTable dtRelated = GetRelatedComponent(ClientID, connection);

            foreach (DataRow s in dtComp.Rows)
            {
                var ft = s["CompType"];
                if (NoComps.Where(x => x == ft.ToString()).Count() == 0) {
                    TZ.CompExtention.Builder.TalentozComponent comp =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<TZ.CompExtention.Builder.TalentozComponent>(s["CompAttribute"].ToString());
                    comp.ComponentID = Convert.ToInt32(ft);
                    TZComponents.Add(comp);
                }                        
            }

            string KeyFields = "[{'ID':'10000','KeyField':'F_10025','entity':'competency'},{'ID':'30000','KeyField':'F_200005','entity':'employee'},{'ID':'30100','KeyField':'F_30105','entity':'emergency_contact'},{'ID':'30200','KeyField':'F_30205','entity':'EDUCATION_QUALIFICATION'},{'ID':'30300','KeyField':'F_30305,F_30325','entity':'PAST_WORK_EXPERIENCE'},{'ID':'30400','KeyField':'F_30405','entity':'DEPENDENT_DETAILS'},{'ID':'30500','KeyField':'F_30520','entity':'EMPLOYEE_VISA_DETAILS'},{'ID':'30600','KeyField':'','entity':'EMPLOYEE_DOCUMENT'},{'ID':'30700','KeyField':'F_30705','entity':'EMPLOYEE_LIC_DETAILS'},{'ID':'30800','KeyField':'','entity':'EMPLOYEE_LIC_DETAILS'},{'ID':'30900','KeyField':'F_30905','entity':'AWARD_DETAILS'},{'ID':'300000','KeyField':'F_300015','entity':'BUSINESS_UNIT'},{'ID':'310000','KeyField':'F_310035','entity':'WORK_LOCATION'},{'ID':'320000','KeyField':'F_320005','entity':'DEPARTMENT'},{'ID':'340000','KeyField':'F_340010','entity':'BANDS_AND_GRADE'},{'ID':'350000','KeyField':'F_350015','entity':'JOB'},{'ID':'360000','KeyField':'F_360010,F_360025,F_360030,F_360035,F_360050','entity':'position'}]";
            var baseKey = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TZBaseKeyField>>(KeyFields);

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
                f.FieldGroupID  = Convert.ToInt32(dr["FieldGroupID"].ToString());
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


            // component only 
            foreach (CompExtention.Builder.TalentozComponent comp in TZComponents.Where(x => x.ComponentType == 1 || x.ComponentType == 4).ToList())
            {

                TZ.CompExtention.Component Comp = new CompExtention.Component(comp.ComponentName, GetType(comp.ComponentType));
                if (comp != null)
                {
                    Comp.TableName = comp.TableName;
                    if (baseKey.Where(x => x.ID == Convert.ToString(comp.ComponentID)).FirstOrDefault() != null) {
                        Comp.EntityKey = baseKey.Where(x => x.ID == Convert.ToString(comp.ComponentID)).FirstOrDefault().entity;
                    }
                    
                    //dtComponentInstance.DefaultView.RowFilter = "FieldTypeID = " + comp.ComponentID;
                    //DataTable dtAtt = dtComponentInstance.DefaultView.ToTable(true);
                    //dtComponentInstance.DefaultView.RowFilter = "";
                    //CompExtention.Builder.FieldElement f = new CompExtention.Builder.FieldElement();

                    CompExtention.Attribute _att_id1 = Comp.NewAttribute(ClientID);
                    _att_id1.IsKey = true;
                    _att_id1.Name = comp.IDField1Name;
                    _att_id1.DisplayName = comp.IDField1Name;
                    _att_id1.Type = CompExtention.AttributeType._number;
                    _att_id1.IsNullable = false;
                    _att_id1.ComponentLookup = "";
                    _att_id1.ComponentLookupDisplayField  = "";
                    Comp.AddAttribute(_att_id1);
                    Comp.Keys.Add(_att_id1);
                   
                    CompExtention.Attribute _att_id2 = Comp.NewAttribute(ClientID);
                    
                    _att_id2.IsKey = true;
                    _att_id2.Name = comp.IDField2Name;
                    _att_id2.DisplayName = comp.IDField2Name;
                    _att_id2.Type = CompExtention.AttributeType._number;
                    _att_id2.ComponentLookup = "";
                    _att_id2.ComponentLookupDisplayField = "";
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
                        _att_id3.ComponentLookup = "";
                        _att_id3.ComponentLookupDisplayField = "";
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
                        _att_id4.ComponentLookup = "";
                        _att_id4.ComponentLookupDisplayField = "";
                        _att_id4.IsNullable = false;
                        Comp.AddAttribute(_att_id4);
                        Comp.Keys.Add(_att_id4);
                    }

                    var flist = TalentozComponentFields.Where(x => x.ComponentID == comp.ComponentID).ToList();

                    foreach (CompExtention.Builder.FieldElement f in flist)
                    {
                       
                        CompExtention.Attribute _att = Comp.NewAttribute(ClientID);
                        _att.Name = "F_" + f.FieldInstanceID;
                        _att.DisplayName = f.FieldDescription.Replace("(", "").Replace(")","");
                        _att.DefaultValue = f.DefaultValue;
                        _att.FileExtension = f.FileExtension;
                        _att.LookupInstanceID = f.FieldInstanceLookUpID.ToString();
                        _att.Length = f.MaxLength;
                        _att.IsUnique = f.isUnique;
                        _att.ComponentLookup = "";
                        _att.ComponentLookupDisplayField = "";
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


            var e = tzComponentWithImportComp.Where(x => x.Key == 30000).FirstOrDefault();
            TZ.CompExtention.ComponentManager cms = new CompExtention.ComponentManager(ClientID, e.Value, new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cms.LoadAttributes();

            // component attributes only 
            foreach (CompExtention.Builder.TalentozComponent comp in TZComponents.Where(x=> x.ComponentType ==3).ToList())
            {

                TZ.CompExtention.Component Comp = new CompExtention.Component(comp.ComponentName, GetType(comp.ComponentType));
                if (comp != null)
                {
                    Comp.TableName = comp.TableName;
                    if (baseKey.Where(x => x.ID == Convert.ToString(comp.ComponentID)).FirstOrDefault() != null)
                    {
                        Comp.EntityKey = baseKey.Where(x => x.ID == Convert.ToString(comp.ComponentID)).FirstOrDefault().entity;
                    }

                    CompExtention.Attribute _att_id1 = Comp.NewAttribute(ClientID);
                    _att_id1.IsKey = true;
                    _att_id1.Name = comp.IDField1Name;
                    _att_id1.DisplayName = comp.IDField1Name;
                    _att_id1.Type = CompExtention.AttributeType._number;
                    _att_id1.ComponentLookup = "";
                    _att_id1.ComponentLookupDisplayField = "";
                    _att_id1.IsNullable = false;
                    Comp.AddAttribute(_att_id1);
                    Comp.Keys.Add(_att_id1);

                    CompExtention.Attribute _att_id2 = Comp.NewAttribute(ClientID);
                    if (comp.ComponentType == 3)
                    {
                        if (comp.IDField2Name == "UserID")
                        {
                            _att_id2.IsKey = true;
                            _att_id2.Name = comp.IDField2Name;
                            _att_id2.DisplayName = comp.IDField2Name;
                            _att_id2.ComponentLookup = "";
                            _att_id2.ComponentLookupDisplayField = "";

                            _att_id2.Type = CompExtention.AttributeType._componentlookup;                          
                            _att_id2.IsNullable = false;
                            _att_id2.DisplayName = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().DisplayName;
                            _att_id2.ComponentLookup = e.Value;
                            _att_id2.ComponentLookupDisplayField = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().ID;
                            Comp.AddAttribute(_att_id2);
                            Comp.Keys.Add(_att_id2);
                        }
                        else {
                            _att_id2.IsKey = true;
                            _att_id2.Name = comp.IDField2Name;
                            _att_id2.DisplayName = comp.IDField2Name;
                            _att_id2.ComponentLookup = "";
                            _att_id2.ComponentLookupDisplayField = "";
                            _att_id2.Type = CompExtention.AttributeType._number;
                            _att_id2.IsNullable = false;
                            Comp.AddAttribute(_att_id2);
                            Comp.Keys.Add(_att_id2);
                        }
                    }
                    else {
                        _att_id2.IsKey = true;
                        _att_id2.Name = comp.IDField2Name;
                        _att_id2.DisplayName = comp.IDField2Name;
                        _att_id2.ComponentLookup = "";
                        _att_id2.ComponentLookupDisplayField = "";
                        _att_id2.Type = CompExtention.AttributeType._number;
                        _att_id2.IsNullable = false;
                        Comp.AddAttribute(_att_id2);
                        Comp.Keys.Add(_att_id2);
                    }
                   

                    if (comp.IDField3Name != "")
                    {
                        CompExtention.Attribute _att_id3 = Comp.NewAttribute(ClientID);
                        _att_id3.IsKey = true;
                        _att_id3.Name = comp.IDField3Name;
                        _att_id3.DisplayName = comp.IDField3Name;
                        _att_id3.ComponentLookup = "";
                        _att_id3.ComponentLookupDisplayField = "";
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
                        _att_id4.ComponentLookup = "";
                        _att_id4.ComponentLookupDisplayField = "";
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
                        _att.DisplayName = f.FieldDescription.Replace("(", "").Replace(")", ""); ;
                        _att.DefaultValue = f.DefaultValue;
                        _att.FileExtension = f.FileExtension;
                        _att.LookupInstanceID = f.FieldInstanceLookUpID.ToString();
                        _att.Length = f.MaxLength;
                        _att.ComponentLookup = "";
                        _att.ComponentLookupDisplayField = "";
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
                        if (cdfield != null)
                        {
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

            


            List<TZComponentView> tzview = new List<TZComponentView>();
            foreach (KeyValuePair<int, string> comp in tzComponentWithImportComp) {
                TZComponentView tzv = new TZComponentView(); 
                var com = TZComponents.Where(x => x.ComponentID == comp.Key).FirstOrDefault();
                if (com != null) {
                    if (com.ComponentType == 1 || com.ComponentType ==3)
                    {
                        CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                        var view = cvm.NewView(com.ComponentName);
                        view.CoreComponent = comp.Value;
                        cvm.Save(view);
                        tzv.TZComponentID = com.ComponentID;
                        tzv.ComponentID = comp.Value;
                        tzv.ViewID = cvm.View.ID;
                        tzview.Add(tzv);
                    }
                    else if (  com.ComponentType == 4) {
                        if (com.ComponentID == 360100)
                        {
                            List<CompExtention.ViewRelation> vrlist = new List<CompExtention.ViewRelation>();                            
                            
                            CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                            var view = cvm.NewView(com.ComponentName);
                        
                            var vr = new CompExtention.ComponentRelation();
                            var vritem = new ViewRelation();
                            vr.ComponentID = tzComponentWithImportComp.Where(x => x.Key == 30000).FirstOrDefault().Value;
                            vr.ChildComponentID = Convert.ToString(comp.Value);
                            vr.Relationship.Add(new ViewRelation() { Left = vr.ComponentID, Right = vr.ChildComponentID, LeftField = "UserID", RightField = "UserID" });
                            vr.Relationship.Add(new ViewRelation() { Left = vr.ComponentID, Right = vr.ChildComponentID, LeftField = "ClientID", RightField = "ClientID" });
                            view.CoreComponent = vr.ComponentID;
                            view.ComponentRelations.Add(vr);

                            vr = new CompExtention.ComponentRelation();
                            vritem = new ViewRelation();
                            vr.ComponentID = tzComponentWithImportComp.Where(x => x.Key == 360000).FirstOrDefault().Value;
                            vr.ChildComponentID = Convert.ToString(comp.Value );
                            vr.Relationship.Add(new ViewRelation() { Left = vr.ComponentID, Right = vr.ChildComponentID, LeftField = "PositionID", RightField = "PositionID" });
                            vr.Relationship.Add(new ViewRelation() { Left = vr.ComponentID, Right = vr.ChildComponentID, LeftField = "ClientID", RightField = "ClientID" });
                            view.ComponentRelations.Add(vr);

                            cvm.Save(view);
                            tzv.TZComponentID = com.ComponentID;
                            tzv.ComponentID = comp.Value;
                            tzv.ViewID = cvm.View.ID;
                            tzview.Add(tzv);
                            //vr.Left = tzComponentWithImportComp.Where(x => x.Key == 30000).FirstOrDefault();
                        }
                        else if (com.ComponentID == 360200)
                        {

                        }
                        else if (com.ComponentID == 360300)
                        {

                        }
                        else {
                            CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                            var view = cvm.NewView(com.ComponentName);
                            view.CoreComponent = comp.Value;
                            cvm.Save(view);
                            tzv.TZComponentID = com.ComponentID;
                            tzv.ComponentID = comp.Value;
                            tzv.ViewID = cvm.View.ID;
                            tzview.Add(tzv);
                        }                        
                    }
                }                
            }
            int index = 1;
            // Employee Component
            var emp = tzview.Where(x => x.TZComponentID == 30000).FirstOrDefault();
            if (emp != null)
            {
                //TZ.CompExtention.ComponentManager cms = new CompExtention.ComponentManager(ClientID, emp.ComponentID, new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
                //cms.LoadAttributes();
              //  var com = TZComponents.Where(x => x.ComponentID == 30000).FirstOrDefault();
                var flist = TalentozComponentFields.Where(x => x.ComponentID == 30000).ToList();
                DataTable dtGroup = GetGroups(ClientID, connection);
               
                foreach (DataRow dr in dtGroup.Rows)
                {

                   var name = Convert.ToString(dr["FieldGroupName"]);
                   var gpFields= flist.Where(x => x.FieldGroupID == Convert.ToInt32(dr["FieldGroupID"])).ToList();
                    if (Convert.ToInt32(dr["FieldGroupID"]) ==20 || Convert.ToInt32(dr["FieldGroupID"]) == 100000 ||   Convert.ToInt32(dr["FieldGroupID"]) == 29) {
                        continue;
                    }
                    // add employee number to this field;
                   var tmp = new CompExtention.ImportTemplate.Template(connection, ClientID);
                    string fmt = "00000000";
                    string formatString = " {0,15:" + fmt + "}";
                    tmp.Name = name;
                    tmp.Code = string.Format(formatString, index);
                    tmp.Category = "Employee";
                    tmp.ViewID = emp.ViewID;
                    // 200005
                    foreach (FieldElement field in gpFields) {
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        var ifield =cms.Component.Attributes.Where(x => x.Name == "F_" + field.FieldInstanceID).FirstOrDefault();
                        if (ifield != null) {
                            if (ifield.DisplayName.EndsWith("ID")) {
                                continue;
                            }
                            te.ID = ifield.ID;
                            if (ifield.Type == AttributeType._file || ifield.Type == AttributeType._picture) {
                                continue;
                            }
                            if (field.FieldInstanceID == 200005)
                            {

                                te.IsKey = true;
                                te.IsRequired = true;
                                te.IsDefault = true;
                            }
                            else {
                                te.IsKey = ifield.IsKey ;
                                te.IsRequired = ifield.IsRequired;
                                te.IsDefault = true;
                                //if (te.IsRequired == true)
                                //{
                                //    te.IsDefault = true;
                                //}
                                //else {
                                //    te.IsDefault = false;
                                //}                              
                            }
                            tmp.TemplateFields.Add(te);
                        }                        
                    }
                    CompExtention.ImportTemplate.TemplateField teempno = new CompExtention.ImportTemplate.TemplateField();
                    var enofield = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault();
                    teempno.ID = enofield.ID;
                    teempno.IsKey = true;
                    teempno.IsRequired = true;
                    teempno.IsDefault = true;
                    tmp.TemplateFields.Add(teempno);

                    index = index + 1;
                    try
                    {
                        tmp.Save();
                    }
                    catch (Exception ex) { 
                    
                    }                
                }
            }


            ConvertEmployeePosition(connection, ClientID, tzComponentWithImportComp);

            // Employee Related Component
            foreach (DataRow dr in dtRelated.Rows) {
                var relComp = Convert.ToInt32(dr["RelatedComponent"]);
             var tzView=   tzview.Where(x => x.TZComponentID == relComp).FirstOrDefault();
                if (tzView != null) {
                    TZ.CompExtention.ComponentManager cmanager = new CompExtention.ComponentManager(ClientID, tzView.ComponentID, new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
                    cmanager.LoadAttributes();
                    var com = TZComponents.Where(x => x.ComponentID == tzView.TZComponentID).FirstOrDefault();
                    var tmp = new CompExtention.ImportTemplate.Template(connection, ClientID);
                    string fmt = "00000000";
                    string formatString = " {0,15:" + fmt + "}";
                    tmp.Name = com.ComponentName;
                    tmp.Code = string.Format(formatString, index).Trim();
                    tmp.Category = "Employee";
                    tmp.ViewID = tzView.ViewID;

                    foreach (Attribute att in cmanager.Component.Attributes )
                    {
                        if (att.Type == AttributeType._file || att.Type == AttributeType._picture)
                        {
                            continue;
                        }
                        if (att.DisplayName.EndsWith("ID"))
                        {
                            continue;
                        }
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        var bk = baseKey.Where(x => x.ID == Convert.ToString(tzView.TZComponentID)).FirstOrDefault();
                        if (bk != null)
                        {
                            if (bk.KeyField != "")
                            {
                                if (bk.KeyField.Split(',').Where(x => x == (att.Name)).FirstOrDefault() != null)
                                {
                                  
                                    te.IsKey = true;
                                    te.IsRequired = true;
                                    te.ID = att.ID;
                                    te.IsDefault = true;
                                    
                                }
                                else
                                {
                                     
                                    te.IsKey = att.IsKey;
                                    te.IsRequired = att.IsRequired;
                                    te.ID = att.ID;
                                    te.IsDefault = true;
                                   
                                }
                            }
                            else {
                               
                                te.IsKey = att.IsKey;
                                te.IsRequired = att.IsRequired;
                                te.ID = att.ID;
                                te.IsDefault = true;
                                 
                            }                            
                        }
                        else {
                           
                            te.IsKey = att.IsKey;
                            te.IsRequired = att.IsRequired;
                            te.ID = att.ID;
                            te.IsDefault = true;
                           
                        }

                        tmp.TemplateFields.Add(te);
                        //if (te.IsRequired == true)
                        //    {
                        //        te.IsDefault = true;
                        //    }
                        //    else
                        //    {
                        //        te.IsDefault = false;
                        //    }


                    }
                    index = index + 1;

                    try
                    {
                        tmp.Save();
                    }
                    catch (Exception ex)
                    {

                    }
                }
               
            }

            var otherViews = tzview.Where(x => x.TZComponentID != 30000).ToList();

            foreach (TZComponentView tz in otherViews) {
               var tzc=  TZComponents.Where(x => (x.ComponentType == 1 || x.ComponentType ==4)  && x.ComponentID == tz.TZComponentID).FirstOrDefault();
                if (tzc != null) {
                    if (tz.ComponentID == "") {
                        continue;
                    }
                    TZ.CompExtention.ComponentManager cmanager = new CompExtention.ComponentManager(ClientID, tz.ComponentID, new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
                    cmanager.LoadAttributes();

                    var com = TZComponents.Where(x => x.ComponentID == tz.TZComponentID).FirstOrDefault();
                    var tmp = new CompExtention.ImportTemplate.Template(connection, ClientID);
                    string fmt = "00000000";
                    string formatString = " {0,15:" + fmt + "}";
                    tmp.Name = com.ComponentName;
                    tmp.Code = string.Format(formatString, index).Trim();
                    tmp.Category =com.ComponentName;
                    tmp.ViewID = tz.ViewID;
                    foreach (Attribute att in cmanager.Component.Attributes)
                    {
                        if (att.Type == AttributeType._file || att.Type == AttributeType._picture)
                        {
                            continue;
                        }
                        if (att.Name.EndsWith("ID"))
                        {
                            continue;
                        }

                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        var bk = baseKey.Where(x => x.ID == Convert.ToString(tz.TZComponentID)).FirstOrDefault();
                        if (bk != null)
                        {
                            if (bk.KeyField != "")
                            {
                                if (bk.KeyField.Split(',').Where(x => x == (att.Name)).FirstOrDefault() != null)
                                {

                                    te.IsKey = true;
                                    te.IsRequired = true;
                                    te.ID = att.ID;
                                    te.IsDefault = true;

                                }
                                else
                                {

                                    te.IsKey = att.IsKey;
                                    te.IsRequired = att.IsRequired;
                                    te.ID = att.ID;
                                    te.IsDefault = true;

                                }
                            }
                            else
                            {

                                te.IsKey = att.IsKey;
                                te.IsRequired = att.IsRequired;
                                te.ID = att.ID;
                                te.IsDefault = true;

                            }
                        }
                        else
                        {

                            te.IsKey = att.IsKey;
                            te.IsRequired = att.IsRequired;
                            te.ID = att.ID;
                            te.IsDefault = true;

                        }
                        tmp.TemplateFields.Add(te);

                       
                        //tmp.TemplateFields.Add(te);

                    }
                    index = index + 1;

                    try
                    {
                        tmp.Save();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

                     



            // Employee Relation

            cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Employee Relation", (CompExtention.ComponentType.pseudocore ));
            var component = (CompExtention.Component)cm.Component;
            component.TableName = "employee_relation";
             var cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("employee_relation", ClientID);

            foreach (Attribute att in component.Attributes)
            {
                if (att.Name.ToLower() == "UserID".ToLower())
                {
                    att.DisplayName = "Employee No";
                    att.Type = AttributeType._componentlookup;
                    att.DisplayName = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().DisplayName;
                    att.ComponentLookup = e.Value;
                    att.ComponentLookupDisplayField = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().ID;
                }
                else if (att.Name.ToLower() == "RelatedUserID".ToLower())
                {
                    att.DisplayName = "Manager No";
                    att.Type = AttributeType._componentlookup;                     
                    att.ComponentLookup = e.Value;
                    att.ComponentLookupDisplayField = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().ID;
                }
                else if (att.Name.ToLower() == "RelationType".ToLower())
                {
                    att.Type = AttributeType._lookup;
                    att.LookupInstanceID = "125";
                }

            }
            cm.Save(component);

            CompExtention.ComponentViewManager cvm_er = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
            var viewer = cvm_er.NewView(component.Name);
            viewer.CoreComponent = component.ID;
            cvm_er.Save(viewer);

           // var com = TZComponents.Where(x => x.ComponentID == cm.Component.ID ).FirstOrDefault();
            var tmp_er = new CompExtention.ImportTemplate.Template(connection, ClientID);
           
            string formatString_er = " {0,15:" + "00000000" + "}";
            tmp_er.Name = cm.Component.Name;
            tmp_er.Code = string.Format(formatString_er, index).Trim();
            tmp_er.Category = "Employee";
            tmp_er.ViewID = cvm_er.View.ID;

            foreach (Attribute att in cm.Component.Attributes) {
                if (att.Name == "UserID" || att.Name == "RelatedUserID" || att.Name == "RelationType") {
                    CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                    te.IsKey = true;
                    te.IsRequired = true;
                    te.ID = att.ID;
                    te.IsDefault = true;
                    tmp_er.TemplateFields.Add(te);
                }
            }
            tmp_er.Save();

            ConvertDependantDetail_Malaysia(ClientID, connection);
            ConvertLeaveTransaction(ClientID, connection);
            ConvertCustomFields(ClientID, connection);
            ConvertClaims(ClientID, connection);

            ConvertToEmployeeCostCenter(ClientID, connection);
            ConvertToHolidays(ClientID, connection);
            ConvertToPassword(ClientID, connection);
            ConvertToLearningProgram(ClientID, connection);
            ConvertToEmployeeSchedule(ClientID, connection);
        }
        private static bool ConvertClaims(int ClientID, string connection)
        {
            TZ.CompExtention.ComponentManager employee = new CompExtention.ComponentManager(ClientID, "sys_user", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            employee.LoadAttributes();

            //TZ.CompExtention.ComponentManager employee = new CompExtention.ComponentManager(ClientID, "sys_user", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            //employee.LoadAttributes();

            // Set up currency
            var cm_currency = new CompExtention.ComponentManager();
            cm_currency.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm_currency.NewComponent(ClientID, "Currency", (CompExtention.ComponentType.core));
            var compCurrency = (CompExtention.Component)cm_currency.Component;
            compCurrency.EntityKey = "currencyid";
            compCurrency.TableName = "currency";
            var cb_currency = new CompExtention.ComponentBuilder(connection);
            compCurrency.Attributes = cb_currency.GetTableFields("currency", ClientID);
            if (compCurrency.Attributes.Count > 0)
            {
                foreach (Attribute att in compCurrency.Attributes)
                {
                    if (att.Name == "CurrencyID")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        compCurrency.Keys.Add(att);
                    }                     
                    else
                    {
                        att.DisplayName = SplitCamalCase(att.DisplayName);
                    }
                }
                cm_currency.Save(compCurrency);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            // Set up claim type
            var cm_claimtype = new CompExtention.ComponentManager();
            cm_claimtype.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm_claimtype.NewComponent(ClientID, "Claim Type", (CompExtention.ComponentType.core));
            var comp_claimtype = (CompExtention.Component)cm_claimtype.Component;
            comp_claimtype.EntityKey = "ClaimTypeid";
            comp_claimtype.TableName = "claims_claimtype";
            var cb_claimtype = new CompExtention.ComponentBuilder(connection);
            comp_claimtype.Attributes = cb_claimtype.GetTableFields("claims_claimtype", ClientID);
            if (comp_claimtype.Attributes.Count > 0)
            {
                foreach (Attribute att in comp_claimtype.Attributes)
                {
                    if (att.Name == "ClaimTypeID")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        comp_claimtype.Keys.Add(att);
                    }
                    else if (att.Name == "ClientID")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        comp_claimtype.Keys.Add(att);
                    }
                    else
                    {
                        att.DisplayName = SplitCamalCase(att.DisplayName);
                    }
                }
                cm_claimtype.Save(comp_claimtype);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            // Claim Component ////////////////////////////////////////////////////////////////////////////////
            var cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Claims", (CompExtention.ComponentType.core));
            var component = (CompExtention.Component)cm.Component;
            component.TableName = "claim_claimrequest";
            component.EntityKey = "requestID";
            var cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("claim_claimrequest", ClientID);
            if (component.Attributes.Count > 0)
            {
                foreach (Attribute att in component.Attributes)
                {
                    if (att.Name == "ClientID")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        component.Keys.Add(att);
                    }
                    if (att.Name == "RequestID")
                    {
                        att.IsRequired = true;
                        att.IsKey = true;
                        att.IsCore = true;
                        component.Keys.Add(att);
                    }
                    if (att.Name == "UserID")
                    {
                        att.IsKey = false;
                        att.IsRequired = true;
                        att.DisplayName = "Employee No";
                        att.IsCore = true;
                        att.Type = AttributeType._componentlookup;
                        att.ComponentLookup = employee.Component.ID;
                        att.ComponentLookupDisplayField = employee.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().ID;
                       // component.Keys.Add(att);
                    }
                    if (att.Name == "CurrencyID")
                    {
                        att.DisplayName = "Currency";
                        att.Type = AttributeType._componentlookup;
                        att.ComponentLookup = compCurrency.ID;
                        att.ComponentLookupDisplayField = compCurrency.Attributes.Where(x => x.Name == "CurrencyShortName").FirstOrDefault().ID;
                        // set component lookup here currency
                    }
                    else
                    {
                        att.DisplayName = SplitCamalCase(att.DisplayName);
                    }
                }
                cm.Save(component);
            }

            // Claim Request Items Component
            var citem = new CompExtention.ComponentManager();
            citem.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            citem.NewComponent(ClientID, "Claim Items", (CompExtention.ComponentType.transaction));
            component = (CompExtention.Component)citem.Component;
            component.EntityKey = "ClaimUserItemID";
            component.TableName = "claim_claimrequestitems";
            var cbitems = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cbitems.GetTableFields("claim_claimrequestitems", ClientID);
            if (component.Attributes.Count > 0)
            {
                var att = component.Attributes.Where(x => x.Name == "ClientID").FirstOrDefault();

               // if (att.Name == "ClientID")
                //{
                    att.IsKey = true;
                    att.IsRequired = true;
                    att.IsCore = true;
                    component.Keys.Add(att);
               // }
                att = component.Attributes.Where(x => x.Name == "RequestID").FirstOrDefault();
             
                    att.IsRequired = true;
                    att.IsKey = true;
                    att.IsCore = true;
                
                att = component.Attributes.Where(x => x.Name == "ItemID").FirstOrDefault();
                
                    att.IsKey = true;
                    att.IsRequired = true;
                    att.IsCore = true;
                    component.Keys.Add(att);
                

                foreach (Attribute attt in component.Attributes)
                {
                    
                      if (attt.Name == "ClaimType")
                    {
                        attt.IsKey = false;
                        attt.IsRequired = true;
                        attt.IsCore = true;
                        attt.Type = AttributeType._componentlookup;
                        attt.ComponentLookup = cm_claimtype.Component.ID;
                        attt.ComponentLookupDisplayField = cm_claimtype.Component.Attributes.Where(x=> x.Name =="ClaimType").FirstOrDefault().ID ;
                    }
                    else
                    {
                        attt.DisplayName = SplitCamalCase(attt.DisplayName);
                    }

                }
                citem.Save(component);
            }
            /// create view link between claim request and its items
            List<CompExtention.ViewRelation> vrlist = new List<CompExtention.ViewRelation>();
            CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
            var view = cvm.NewView(((CompExtention.Component)cm.Component).Name);            
            var vr = new CompExtention.ComponentRelation();
            var vritem = new ViewRelation();
            vr.ComponentID =  cm.Component.ID ;
            vr.ChildComponentID = Convert.ToString(((CompExtention.Component)citem.Component ).ID);
            vr.Relationship.Add(new ViewRelation() { Left = vr.ComponentID, Right = vr.ChildComponentID, LeftField = "RequestID", RightField = "RequestID" });
            vr.Relationship.Add(new ViewRelation() { Left = vr.ComponentID, Right = vr.ChildComponentID, LeftField = "ClientID", RightField = "ClientID" });
            view.CoreComponent = vr.ComponentID;
            view.ComponentRelations.Add(vr);     
            cvm.Save(view);
            cvm.LoadViewComponents();
            ////////////////////////////////////////////////////////////////////////////
            //////////////Template for claim////////////////////////////////////////////
            var tmp_ep = new CompExtention.ImportTemplate.Template(connection, ClientID);

            string formatString_ep = " {0,15:" + "00000000" + "}";
            tmp_ep.Name = "Employee Claims";
            tmp_ep.Code = string.Format(formatString_ep , 234442).Trim();
            tmp_ep.Category = "Claim";
            tmp_ep.ViewID = cvm.View.ID;

   
            var claimFields = "programtitle,StartDate,Enddate,CurrencyID,userid".Split(',');
            var claimItemFields = "ClaimType,Date,Description,Amount".Split(',');
            foreach (Component c in cvm.View.Components)
            {
                foreach (Attribute att in c.Attributes)
                {
                    if (c.ID == cm.Component.ID )
                    {
                        if (claimFields.Where(x => x.ToLower () == att.Name.ToLower()).FirstOrDefault() != null)
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            if (att.Name.ToLower () == "programtitle".ToLower())
                            {
                                te.ID = att.ID;
                                te.IsKey = true;
                                te.IsDefault = true;
                                te.IsRequired = true;
                            }
                            else if (att.Name.ToLower() == "userid".ToLower())
                            {
                                te.ID = att.ID;
                                te.IsKey = true;
                                te.IsDefault = true;
                                te.IsRequired = true;
                            }
                            else if (att.Name.ToLower() == "Description".ToLower()) {
                                te.ID = att.ID;
                                te.IsKey = false;
                                te.IsDefault = true;
                                te.IsRequired = false;
                            }
                            else
                            {
                                te.ID = att.ID;
                                te.IsKey = false;
                                te.IsDefault = true;
                                te.IsRequired = true;
                            }
                            tmp_ep.TemplateFields.Add(te);
                        }
                    }
                    else if (c.ID == citem.Component.ID)
                    {
                        if (claimItemFields.Where(x => x.ToLower() == att.Name.ToLower()).FirstOrDefault() != null)
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.ID = att.ID;
                            if (att.Name.ToLower() == "ClaimType".ToLower())
                            {
                                te.IsKey = true;
                                te.IsDefault = true;
                                te.IsRequired = true;
                                tmp_ep.TemplateFields.Add(te);
                            }
                            else if (att.Name.ToLower() == "date".ToLower())
                            {
                                te.IsKey = true;
                                te.IsDefault = true;
                                te.IsRequired = true;
                                tmp_ep.TemplateFields.Add(te);
                            }
                            else
                            {
                                te.IsKey = false;
                                te.IsDefault = true;
                                te.IsRequired = true;
                                tmp_ep.TemplateFields.Add(te);
                            }
                        }
                    }                   
                }
            }
            tmp_ep.Save();
            //////////////
            return true;

        }
        private static void ConvertDependantDetail_Malaysia(int ClientID,string connection) {

            TZ.CompExtention.ComponentManager cms = new CompExtention.ComponentManager(ClientID, "sys_user", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cms.LoadAttributes();

            var  cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Employee Dependents - Malaysia", (CompExtention.ComponentType.attribute));
           var  component = (CompExtention.Component)cm.Component;
            component.TableName = "employee_dependentdetails_malaysia";

           var  cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("employee_dependentdetails_malaysia", ClientID);
            if (component.Attributes.Count > 0) {
                foreach (Attribute att in component.Attributes)
                {
                    if (att.Type == AttributeType._picture || att.Type == AttributeType._file)
                    {
                        continue;
                    }
                    if (att.Name == "LastUPD")
                    {
                        continue;
                    }
                    if (att.Name == "ClientID")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        component.Keys.Add(att);
                    }
                    else if (att.Name == "DependentID")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        component.Keys.Add(att);
                    }
                    else if (att.Name == "Name") {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        att.DisplayName = SplitCamalCase(att.DisplayName);
                        component.Keys.Add(att);
                    }
                    else if (att.Name == "UserID")
                    {
                        att.DisplayName = "Employee No";
                        att.IsRequired = true;
                        att.IsCore = true;
                        att.Type = AttributeType._componentlookup;
                        // att.DisplayName = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().DisplayName;
                        att.ComponentLookup = cms.Component.ID;
                        att.ComponentLookupDisplayField = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().ID;
                        component.Keys.Add(att);
                    }
                    else if (att.Name.ToLower() == "gender")
                    {
                        att.LookupInstanceID = "102";
                        att.Type = AttributeType._lookup;
                    }
                    else if (att.Name.ToLower() == "relationship")
                    {
                        att.LookupInstanceID = "255";
                        att.Type = AttributeType._lookup;
                    }
                    else if (att.Name.ToLower() == "childEducation".ToLower())
                    {
                        att.LookupInstanceID = "259";
                        att.DisplayName = SplitCamalCase(att.DisplayName);
                        att.Type = AttributeType._lookup;
                    }
                    else
                    {
                        att.DisplayName = SplitCamalCase(att.DisplayName);
                    }
                }
                cms.Save(component);

                CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                var view = cvm.NewView(component.Name);
                view.CoreComponent = component.ID;
                cvm.Save(view);

                var tmp_er = new CompExtention.ImportTemplate.Template(connection, ClientID);

                string formatString_er = " {0,15:" + "00000000" + "}";
                tmp_er.Name = cm.Component.Name;
                tmp_er.Code = string.Format(formatString_er, new Random().Next(1, 200000)).Trim();
                tmp_er.Category = "Employee";
                tmp_er.ViewID = cvm.View.ID;

                foreach (Attribute att in cm.Component.Attributes)
                {
                    if (att.Name != "DependentID" && att.Name != "ClientID" && att.Name != "ActionBy" && att.Name != "FileName" && att.Name != "NewFileName")
                    {
                        if (att.Name == "UserID" || att.Name == "Name")
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.IsKey = true;
                            te.IsRequired = true;
                            te.ID = att.ID;
                            te.IsDefault = true;
                            tmp_er.TemplateFields.Add(te);
                        }
                        else
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.IsKey = false;
                            te.IsRequired = att.IsRequired;
                            te.ID = att.ID;
                            te.IsDefault = att.IsRequired;
                            tmp_er.TemplateFields.Add(te);
                        }
                    }
                }
                tmp_er.Save();
            }
            


        }
        private static void ConvertToEmployeeCostCenter(int ClientID, string connection) {
            TZ.CompExtention.ComponentManager cms = new CompExtention.ComponentManager(ClientID, "sys_user", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cms.LoadAttributes();

            TZ.CompExtention.ComponentManager ccenter = new CompExtention.ComponentManager(ClientID, "CostCenter", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            ccenter.LoadAttributes();

            var citem = new CompExtention.ComponentManager();
            citem.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            citem.NewComponent(ClientID, "Employee Cost Center", (CompExtention.ComponentType.transaction));
            var component = (CompExtention.Component)citem.Component;
            component.EntityKey = "UserCostCenterID";
            component.TableName = "employee_costcenter";
            var cbitems = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cbitems.GetTableFields("employee_costcenter", ClientID);

            if (component.Attributes.Count > 0)
            {
                var att = component.Attributes.Where(x => x.Name == "ClientID").FirstOrDefault();

                // if (att.Name == "ClientID")
                //{
                att.IsKey = true;
                att.IsRequired = true;
                att.IsCore = true;
                component.Keys.Add(att);
                // }
                att = component.Attributes.Where(x => x.Name == "CostCenterID").FirstOrDefault();

                att.IsRequired = true;
                att.IsKey = true;
                att.IsCore = true;
               // component.Keys.Add(att);

                att = component.Attributes.Where(x => x.Name == "UserID").FirstOrDefault();

                att.IsRequired = true;
                att.IsKey = true;
                att.IsCore = true;
               // component.Keys.Add(att);

                att = component.Attributes.Where(x => x.Name == "UserCostCenterID").FirstOrDefault();
                att.IsKey = true;
                att.IsRequired = true;
                att.IsCore = true;
                 component.Keys.Add(att);


                foreach (Attribute attt in component.Attributes)
                {

                    if (attt.Name == "UserID")
                    {
                        attt.DisplayName = "Employee No";
                        attt.IsKey = false;
                        attt.IsRequired = true;
                        attt.IsCore = true;
                        attt.Type = AttributeType._componentlookup;
                        attt.ComponentLookup = cms.Component.ID;
                        attt.ComponentLookupDisplayField = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().ID;
                    }
                    else if (attt.Name == "CostCenterID") {
                        attt.DisplayName = "Cost Center";
                        attt.IsKey = false;
                        attt.IsRequired = true;
                        attt.IsCore = true;
                        attt.Type = AttributeType._componentlookup;
                        attt.ComponentLookup = ccenter.Component.ID;
                        attt.ComponentLookupDisplayField = ccenter.Component.Attributes.Where(x => x.Name == "F_370010").FirstOrDefault().ID;
                    }
                    else
                    {
                        attt.DisplayName = SplitCamalCase(attt.DisplayName);
                    }

                }
                citem.Save(component);

                CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                var view = cvm.NewView(component.Name);
                view.CoreComponent = component.ID;
                cvm.Save(view);

                var tmp_er = new CompExtention.ImportTemplate.Template(connection, ClientID);

                string formatString_er = " {0,15:" + "00000000" + "}";
                tmp_er.Name = citem.Component.Name;
                tmp_er.Code = string.Format(formatString_er, new Random().Next(1, 200000)).Trim();
                tmp_er.Category = "Employee";
                tmp_er.ViewID = cvm.View.ID;

                foreach (Attribute attt in citem.Component.Attributes)
                {
                    if ( attt.Name != "ClientID" && attt.Name != "UserCostCenterID" && attt.Name !="LastUPD")
                    {
                        if (attt.Name == "UserID" || attt.Name == "CostCenterID" || attt.Name =="EffectiveDate")
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.IsKey = true;
                            te.IsRequired = true;
                            te.ID = attt.ID;
                            te.IsDefault = true;
                            tmp_er.TemplateFields.Add(te);
                        }
                        else
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.IsKey = false;
                            if (attt.Name == "EndDate")
                            {
                                te.IsRequired = false;
                            }
                            else {
                                te.IsRequired = true;
                            }                            
                            te.ID = attt.ID;
                            te.IsDefault = true;
                            tmp_er.TemplateFields.Add(te);
                        }
                    }
                }
                tmp_er.Save();


            }
        }
        private static void ConvertToHolidays(int ClientID, string connection) {
            CreateHolidayLookUp(ClientID, connection);
            TZ.CompExtention.ComponentManager wl = new CompExtention.ComponentManager(ClientID, "worklocation", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            wl.LoadAttributes();

            var cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Holidays", (CompExtention.ComponentType.core));
            var component = (CompExtention.Component)cm.Component;
            component.TableName = "lms_holidays";
            component.EntityKey = "HolidayID";
            var cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("lms_holidays", ClientID);

            if (component.Attributes.Count > 0)
            {
                var att = component.Attributes.Where(x => x.Name == "ClientID").FirstOrDefault();

                // if (att.Name == "ClientID")
                //{
                att.IsKey = true;
                att.IsRequired = true;
                att.IsCore = true;
                component.Keys.Add(att);
                // }
                att = component.Attributes.Where(x => x.Name == "HolidayID").FirstOrDefault();

                att.IsRequired = true;
                att.IsKey = true;
                att.IsCore = true;
                component.Keys.Add(att);

      

                foreach (Attribute attt in component.Attributes)
                {
                    if (attt.Name.ToLower() == "IsAllWorklocation".ToLower())
                    {
                        attt.DisplayName = "Worklocation";
                        attt.IsKey = false;
                        attt.IsRequired = true;
                        attt.IsCore = true;
                        attt.Type = AttributeType._componentlookup;
                        attt.ComponentLookup = wl.Component.ID;
                        attt.ComponentLookupDisplayField = wl.Component.Attributes.Where(x => x.Name == "F_310035").FirstOrDefault().ID;
                    }
                    else if (attt.Name == "HolidayType") {
                        attt.DisplayName = "Holiday Type";
                        attt.IsKey = false;
                        attt.IsRequired = true;
                        attt.IsCore = true;
                        attt.Type = AttributeType._lookup;
                        attt.LookupInstanceID ="1001";
                       
                    }
                    else
                    {
                        attt.DisplayName = SplitCamalCase(attt.DisplayName);
                    }

                }
                cm.Save(component);

                CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                var view = cvm.NewView(component.Name);
                view.CoreComponent = component.ID;
                cvm.Save(view);

                var tmp_er = new CompExtention.ImportTemplate.Template(connection, ClientID);

                string formatString_er = " {0,15:" + "00000000" + "}";
                tmp_er.Name = cm.Component.Name;
                tmp_er.Code = string.Format(formatString_er, new Random().Next(1, 200000)).Trim();
                tmp_er.Category = "LMS";
                tmp_er.ViewID = cvm.View.ID;

                foreach (Attribute attt in cm.Component.Attributes)
                {
                    if (attt.Name != "ClientID" && attt.Name != "HolidayID" && attt.Name != "LastUPD" && attt.Name != "NoofAccurance" && attt.Name != "GroupID" && attt.Name != "IsNational")
                    {
                        if (attt.Name == "HolidayName")
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.IsKey = true;
                            te.IsRequired = true;
                            te.ID = attt.ID;
                            te.IsDefault = true;
                            tmp_er.TemplateFields.Add(te);
                        }
                        else
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.IsKey = false;
                            te.IsRequired = true;
                            te.ID = attt.ID;
                            te.IsDefault = true;
                            tmp_er.TemplateFields.Add(te);
                        }
                    }
                }
                tmp_er.Save();


            }

        }

        private static void ConvertToPassword(int ClientID, string connection) {
            TZ.CompExtention.ComponentManager cms = new CompExtention.ComponentManager(ClientID, "sys_user", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cms.LoadAttributes();
            var tmp_er = new CompExtention.ImportTemplate.Template(connection, ClientID);

            CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
            var view = cvm.NewView(cms.Component.Name);
            view.CoreComponent = cms.Component.ID;
            cvm.Save(view);

            string formatString_er = " {0,15:" + "00000000" + "}";
            tmp_er.Name = "Change Password";
            tmp_er.Code = string.Format(formatString_er, new Random().Next(1, 200000)).Trim();
            tmp_er.Category = "Employee";
            tmp_er.ViewID = cvm.View.ID;

            foreach (Attribute att in cms.Component.Attributes)
            {
                if (att.Name == "F_200005" || att.Name == "F_200245" )
                {                    
                    
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                    if (att.Name == "F_200005")
                    {
                        te.IsKey = true;
                    }
                    else {
                        te.IsKey = false;
                    }
                        te.IsRequired = true;
                        te.ID = att.ID;
                        te.IsDefault = true;
                        tmp_er.TemplateFields.Add(te);                    
                }
            }
            tmp_er.Save();

        }
        private static void ConvertToLearningProgram(int ClientID, string connection) {
            //lookupids  category =172,171
            TZ.CompExtention.ComponentManager cms = new CompExtention.ComponentManager(ClientID, "businessunit", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cms.LoadAttributes();

            var cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Learning Program", (CompExtention.ComponentType.core));
            var component = (CompExtention.Component)cm.Component;
            component.TableName = "ld_program";
            component.EntityKey = "PROGRAMID";
            var cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("ld_program", ClientID);

            if (component.Attributes.Count > 0)
            {
                var att = component.Attributes.Where(x => x.Name == "ClientID").FirstOrDefault();

                // if (att.Name == "ClientID")
                //{
                att.IsKey = true;
                att.IsRequired = true;
                att.IsCore = true;
                component.Keys.Add(att);
                // }
                att = component.Attributes.Where(x => x.Name.ToLower() == "programid").FirstOrDefault();

                att.IsRequired = true;
                att.IsKey = true;
                att.IsCore = true;
                component.Keys.Add(att);



                foreach (Attribute attt in component.Attributes)
                {
                    if (attt.Name.ToLower() == "BusinessUnitID".ToLower())
                    {
                        attt.DisplayName = "Business Unit";
                        attt.IsKey = false;
                        attt.IsRequired = true;
                        attt.IsCore = true;
                        attt.Type = AttributeType._componentlookup;
                        attt.ComponentLookup = cms.Component.ID;
                        attt.ComponentLookupDisplayField = cms.Component.Attributes.Where(x => x.Name == "F_300015").FirstOrDefault().ID;
                    }
                    else if (attt.Name == "Type")
                    {
                        attt.DisplayName = "Program Type";
                        attt.IsKey = false;
                        attt.IsRequired = false;
                        attt.IsCore = true;
                        attt.Type = AttributeType._lookup;
                        attt.LookupInstanceID = "171";

                    }
                    else if (attt.Name == "Category")
                    {
                        attt.DisplayName = "Category";
                        attt.IsKey = false;
                        attt.IsRequired = false;
                        attt.IsCore = true;
                        attt.Type = AttributeType._lookup;
                        attt.LookupInstanceID = "172";
                    }
                    else if (attt.Name == "Title") {
                        attt.IsKey = false;
                        attt.IsRequired = true;
                        attt.IsCore = true;
                        attt.DisplayName = SplitCamalCase(attt.DisplayName);
                    }
                    else
                    {
                        attt.IsKey = false;
                        attt.IsRequired = false;
                        attt.IsCore = true;
                        attt.DisplayName = SplitCamalCase(attt.DisplayName);
                    }
                }
                cm.Save(component);

                CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                var view = cvm.NewView(component.Name);
                view.CoreComponent = component.ID;
                cvm.Save(view);

                var tmp_er = new CompExtention.ImportTemplate.Template(connection, ClientID);

                string formatString_er = " {0,15:" + "00000000" + "}";
                tmp_er.Name = cm.Component.Name;
                tmp_er.Code = string.Format(formatString_er, new Random().Next(1, 200000)).Trim();
                tmp_er.Category = "LMS";
                tmp_er.ViewID = cvm.View.ID;

                foreach (Attribute attt in cm.Component.Attributes)
                {
                    if (attt.Name != "ClientID" && attt.Name != "ProgramID" && attt.Name != "LastUPD" && attt.Name != "UnPlannedProgramID" && attt.Name != "Status" && attt.Name != "IsFromUnplannedRequest")
                    {
                        if (attt.Name == "Title")
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.IsKey = true;
                            te.IsRequired = true;
                            te.ID = attt.ID;
                            te.IsDefault = true;
                            tmp_er.TemplateFields.Add(te);
                        }
                        else if (attt.Name == "BusinessUnitID")
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.IsKey = true;
                            te.IsRequired = true;
                            te.ID = attt.ID;
                            te.IsDefault = true;
                            tmp_er.TemplateFields.Add(te);
                        }
                        else
                        {
                            CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                            te.IsKey = false;
                            te.IsRequired = false;
                            te.ID = attt.ID;
                            te.IsDefault = true;
                            tmp_er.TemplateFields.Add(te);
                        }
                    }
                }
                tmp_er.Save();


            }



           

        }

        private static CompExtention.ComponentManager ConvertToSchedule(int ClientID, string connection)
        {
            var cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Schedule", (CompExtention.ComponentType.core));
            var component = (CompExtention.Component)cm.Component;
            component.TableName = "lms_schedule";
            component.EntityKey = "ScheduleID";
            var cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("lms_schedule", ClientID);

            if (component.Attributes.Count > 0)
            {
                var att = component.Attributes.Where(x => x.Name == "ClientID").FirstOrDefault();

                // if (att.Name == "ClientID")
                //{
                att.IsKey = true;
                att.IsRequired = true;
                att.IsCore = true;
                component.Keys.Add(att);
                // }
                att = component.Attributes.Where(x => x.Name == "ScheduleID").FirstOrDefault();

                att.IsRequired = true;
                att.IsKey = true;
                att.IsCore = true;
                component.Keys.Add(att);



                foreach (Attribute attt in component.Attributes)
                {                     
                        attt.IsKey = false;
                        attt.IsRequired = false;
                        attt.IsCore = true;
                        attt.DisplayName = SplitCamalCase(attt.DisplayName);                    
                }
                cm.Save(component);
            }
            return cm;
        }
        private static void ConvertToEmployeeSchedule(int ClientID, string connection) {
            var schedule = new CompExtention.ComponentManager();
            schedule = ConvertToSchedule(ClientID, connection);
            TZ.CompExtention.ComponentManager cms = new CompExtention.ComponentManager(ClientID, "sys_user", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cms.LoadAttributes();

            var cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Employee Schedule", (CompExtention.ComponentType.transaction));
            var component = (CompExtention.Component)cm.Component;
            component.TableName = "lms_employeeschedule";
            
            var cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("lms_employeeschedule", ClientID);

            if (component.Attributes.Count > 0)
            {
                var att = component.Attributes.Where(x => x.Name == "ClientID").FirstOrDefault();

                // if (att.Name == "ClientID")
                //{
                att.IsKey = true;
                att.IsRequired = true;
                att.IsCore = true;
                component.Keys.Add(att);
                // }
                att = component.Attributes.Where(x => x.Name.ToLower() == "UserID".ToLower()).FirstOrDefault();

                att.IsRequired = true;
                att.IsKey = true;
                att.IsCore = true;
                att.Type = AttributeType._componentlookup;
                component.Keys.Add(att);
                att = component.Attributes.Where(x => x.Name.ToLower() == "ScheduleID".ToLower()).FirstOrDefault();

                att.IsRequired = true;
                att.IsKey = true;
                att.IsCore = true;
                att.Type = AttributeType._componentlookup;
                component.Keys.Add(att);


                foreach (Attribute attt in component.Attributes)
                {
                    if (attt.Name.ToLower() == "ScheduleID".ToLower())
                    {
                        att.IsRequired = true;
                        att.IsKey = true;
                        att.IsCore = true;
                        att.Type = AttributeType._componentlookup;
                        att.ComponentLookup = schedule.Component.ID;
                        att.ComponentLookupDisplayField = schedule.Component.Attributes.Where(x => x.Name == "ScheduleCode").FirstOrDefault().ID;
                    }
                    else if (attt.Name.ToLower() == "userid")
                    {
                        att.IsRequired = true;
                        att.IsKey = true;
                        att.IsCore = true;
                        att.Type = AttributeType._componentlookup;
                        att.ComponentLookup = cms.Component.ID;
                        att.ComponentLookupDisplayField = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().ID;
                    }
                    else {
                        attt.IsKey = false;
                        attt.IsRequired = false;
                        attt.IsCore = true;                        
                        attt.DisplayName = SplitCamalCase(attt.DisplayName);
                    }                  
                }
                cm.Save(component);


                CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                var view = cvm.NewView(component.Name);
                view.CoreComponent = component.ID;
                cvm.Save(view);

                var tmp_er = new CompExtention.ImportTemplate.Template(connection, ClientID);

                string formatString_er = " {0,15:" + "00000000" + "}";
                tmp_er.Name = cm.Component.Name;
                tmp_er.Code = string.Format(formatString_er, new Random().Next(1, 200000)).Trim();
                tmp_er.Category = "LMS";
                tmp_er.ViewID = cvm.View.ID;

                foreach (Attribute attt in cm.Component.Attributes)
                {

                    if (attt.Name == "UserID")
                    {
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        te.IsKey = true;
                        te.IsRequired = true;
                        te.ID = attt.ID;
                        te.IsDefault = true;
                        tmp_er.TemplateFields.Add(te);
                    }
                    else if (attt.Name == "ScheduelID")
                    {
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        te.IsKey = true;
                        te.IsRequired = true;
                        te.ID = attt.ID;
                        te.IsDefault = true;
                        tmp_er.TemplateFields.Add(te);
                    }
                    else if (attt.Name == "StartDate")
                    {
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        te.IsKey = false;
                        te.IsRequired = true;
                        te.ID = attt.ID;
                        te.IsDefault = true;
                        tmp_er.TemplateFields.Add(te);
                    }
                    else if (attt.Name == "EndDate")
                    {
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        te.IsKey = false;
                        te.IsRequired = false;
                        te.ID = attt.ID;
                        te.IsDefault = true;
                        tmp_er.TemplateFields.Add(te);
                    }


                }
                tmp_er.Save();
            }

        }

        private static bool CheckLookupExist(int clientid, string conn,int instanceid) {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBComparison dbClient = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(clientid));
            DBComparison dbfield = DBComparison.Compare(DBField.Field("FieldInstanceID"), Tech.Data.Compare.Equals, DBConst.Int32(instanceid));

            DBQuery select;
            select = DBQuery.Select().Field("sys_lookup", "FieldInstanceID")
                .Field("sys_lookup", "Name")
                .From("sys_lookup").WhereAll(dbClient, dbfield);
            if (db.Database.GetDatatable(select).Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public static bool CreateHalfDayLookup(int clientid,string conn) {
            if (CheckLookupExist(clientid, conn,1000) == true) {
                return true;
            }
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery insert = DBQuery.InsertInto("sys_lookup").
                       Field(("FieldInstanceID")).
                        Field(("Name")).
                         Field(("Type")).
                          Field(("ClientID")).
                          Value(DBConst.String("1000")).
                           Value(DBConst.String("Half Day Type")).
                            Value(DBConst.String("1")).
                             Value(DBConst.Int32(clientid ))
                       ;
            if (db.Database.ExecuteNonQuery(insert) > 0)
            {
                // 0 - None,1 - Morning,2 - AfterNoon
                insert = DBQuery.InsertInto("sys_Fieldinstancelookup").
                    Field(("FieldInstanceID")).
                     Field(("LookupID")).
                      Field(("LookupDescription")).
                       Field(("ClientID")).
                        Field(("LookupOrder")).
                       Value(DBConst.String("1000")).
                        Value(DBConst.String("0")).
                         Value(DBConst.String("None")).
                          Value(DBConst.Int32(clientid)).
                            Value(DBConst.Int32(1))
                    ;
                db.Database.ExecuteNonQuery(insert);

                insert = DBQuery.InsertInto("sys_Fieldinstancelookup").
                 Field(("FieldInstanceID")).
                  Field(("LookupID")).
                   Field(("LookupDescription")).
                    Field(("ClientID")).
                     Field(("LookupOrder")).
                    Value(DBConst.String("1000")).
                     Value(DBConst.String("1")).
                      Value(DBConst.String("Morning")).
                       Value(DBConst.Int32(clientid)).
                         Value(DBConst.Int32(2))
                 ;
                db.Database.ExecuteNonQuery(insert);

                insert = DBQuery.InsertInto("sys_Fieldinstancelookup").
                  Field(("FieldInstanceID")).
                   Field(("LookupID")).
                    Field(("LookupDescription")).
                     Field(("ClientID")).
                      Field(("LookupOrder")).
                     Value(DBConst.String("1000")).
                      Value(DBConst.String("2")).
                       Value(DBConst.String("AfterNoon")).
                        Value(DBConst.Int32(clientid)).
                          Value(DBConst.Int32(3))
                  ;
                db.Database.ExecuteNonQuery(insert);
                return true;
            }
            else {
                return false;
            }
        }
        public static bool CreateHolidayLookUp(int clientid, string conn)
        {
            if (CheckLookupExist(clientid, conn,1001) == true)
            {
                return true;
            }
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery insert = DBQuery.InsertInto("sys_lookup").
                       Field(("FieldInstanceID")).
                        Field(("Name")).
                         Field(("Type")).
                          Field(("ClientID")).
                          Value(DBConst.String("1001")).
                           Value(DBConst.String("Holiday Type")).
                            Value(DBConst.String("1")).
                             Value(DBConst.Int32(clientid))
                       ;
            if (db.Database.ExecuteNonQuery(insert) > 0)
            {
                // 0 - None,1 - Morning,2 - AfterNoon
                insert = DBQuery.InsertInto("sys_Fieldinstancelookup").
                    Field(("FieldInstanceID")).
                     Field(("LookupID")).
                      Field(("LookupDescription")).
                       Field(("ClientID")).
                        Field(("LookupOrder")).
                       Value(DBConst.String("1001")).
                        Value(DBConst.String("1")).
                         Value(DBConst.String("National")).
                          Value(DBConst.Int32(clientid)).
                            Value(DBConst.Int32(1))
                    ;
                db.Database.ExecuteNonQuery(insert);

                insert = DBQuery.InsertInto("sys_Fieldinstancelookup").
                 Field(("FieldInstanceID")).
                  Field(("LookupID")).
                   Field(("LookupDescription")).
                    Field(("ClientID")).
                     Field(("LookupOrder")).
                    Value(DBConst.String("1001")).
                     Value(DBConst.String("2")).
                      Value(DBConst.String("Festival")).
                       Value(DBConst.Int32(clientid)).
                         Value(DBConst.Int32(2))
                 ;
                db.Database.ExecuteNonQuery(insert);

                
               
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void ConvertLeaveTransaction(int ClientID, string connection)
        {
            CreateHalfDayLookup(ClientID, connection);
            TZ.CompExtention.ComponentManager cms = new CompExtention.ComponentManager(ClientID, "sys_user", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cms.LoadAttributes();

            // Leave Type component
            var cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Leave Type", (CompExtention.ComponentType.attribute));
            var component = (CompExtention.Component)cm.Component;
            component.TableName = "lms_leave_type";
           
            var cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("lms_leave_type", ClientID);

            if (component.Attributes.Count > 0)
            {
                foreach (Attribute att in component.Attributes)
                {
                    if (att.Name == "ClientID")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        component.Keys.Add(att);
                    }                   
                    if (att.Name == "UserID")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        component.Keys.Add(att);
                    }
                    if (att.Name == "LeaveTypeID") {
                        att.IsRequired = true;
                        att.IsKey = true;
                        att.IsCore = true;
                        component.Keys.Add(att);
                    }
                                
                }

              
                cm.Save(component);
            }
            string leavetype = cm.Component.ID;
            IComponent ltype = cm.Component;

                cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.NewComponent(ClientID, "Leave Transaction", (CompExtention.ComponentType.attribute));
              component = (CompExtention.Component)cm.Component;
            component.TableName = "lms_leave_request";
            component.EntityKey = "RequestID";
            cb = new CompExtention.ComponentBuilder(connection);
            component.Attributes = cb.GetTableFields("lms_leave_request", ClientID);
            if (component.Attributes.Count > 0)
            {
                foreach (Attribute att in component.Attributes)
                {
                    if (att.Type == AttributeType._picture || att.Type == AttributeType._file)
                    {
                        continue;
                    }
                    if (att.Name == "LastUPD")
                    {
                        continue;
                    }
                    if (att.Name == "ClientID")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        component.Keys.Add(att);
                    }
                    if (att.Name == "IsPartialDay")
                    {
                        att.Type  = AttributeType._bit;
                     
                        //att.DisplayName = SplitCamalCase(att.DisplayName);             
                    }

                    if (att.Name == "StartDate")
                    {
                        att.IsKey = true;
                        att.IsRequired = true;
                        att.IsCore = true;
                        att.DisplayName = SplitCamalCase(att.DisplayName);
                        component.Keys.Add(att);
                    }

                    else if (att.Name == "StartDuration") {
                        att.DisplayName = "Half Day Type";
                        att.IsRequired = false;
                        att.IsCore = true;
                        att.Type = AttributeType._lookup;
                        // att.DisplayName = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().DisplayName;
                        att.LookupInstanceID ="1000";
                        //t.ComponentLookupDisplayField = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().ID;
                        component.Keys.Add(att);
                    }
                    else if (att.Name == "UserID")
                    {
                        att.DisplayName = "Employee No";
                        att.IsRequired = true;
                        att.IsCore = true;
                        att.Type = AttributeType._componentlookup;
                        // att.DisplayName = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().DisplayName;
                        att.ComponentLookup = cms.Component.ID;
                        att.ComponentLookupDisplayField = cms.Component.Attributes.Where(x => x.Name == "F_200005").FirstOrDefault().ID;
                        component.Keys.Add(att);
                    }
                    else if (att.Name.ToLower() == "type")
                    {
                        att.Type = AttributeType._componentlookup;
                        att.ComponentLookup = leavetype;
                        att.ComponentLookupDisplayField = ltype.Attributes.Where(x => x.Name == "LeaveType").FirstOrDefault().ID;
                    }
                    else
                    {
                        att.DisplayName = SplitCamalCase(att.DisplayName);
                    }
                }

                var attt = component.Attributes.Where(x => x.Name == "RequestID").FirstOrDefault();
                   if (attt.Name == "RequestID")
                {
                    attt.IsKey = true;
                    attt.IsRequired = true;
                    attt.IsCore = true;
                    component.Keys.Add(attt);
                }

                cm.Save(component);

                CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                var view = cvm.NewView(component.Name);
                view.CoreComponent = component.ID;
                cvm.Save(view);

                // var com = TZComponents.Where(x => x.ComponentID == cm.Component.ID ).FirstOrDefault();
                var tmp_er = new CompExtention.ImportTemplate.Template(connection, ClientID);

                string formatString_er = " {0,15:" + "00000000" + "}";
                tmp_er.Name = cm.Component.Name;
                tmp_er.Code = string.Format(formatString_er, new Random().Next(1, 200000)).Trim();
                tmp_er.Category = "Leave";
                tmp_er.ViewID = cvm.View.ID;

                foreach (Attribute att in cm.Component.Attributes)
                {
                    if (att.Name == "UserID" || att.Name == "RequestedID" || att.Name == "Type" || att.Name == "StartDate" || att.Name =="EndDate" || att.Name == "StartDuration")
                    {
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        te.IsKey = true;
                        te.IsRequired = true;
                        te.ID = att.ID;
                        te.IsDefault = true;
                        tmp_er.TemplateFields.Add(te);
                    }
                    else if (att.Name == "StartDate" || att.Name == "EndDate"
                        || att.Name == "IsPartialDay"                         
                        || att.Name == "Description"
                        || att.Name == "NoOfDays"
                        || att.Name == "StartDuration"
                       ) {
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        te.IsKey = false;                      
                        te.ID = att.ID;
                         
                        if (att.Name == "Description")
                        {
                            te.IsRequired = false;
                        }
                        else {
                            te.IsRequired = true;
                        }
                        te.IsDefault = true;
                        tmp_er.TemplateFields.Add(te);
                    }
                }
                tmp_er.Save();
            }
        }
        public static string SplitCamalCase(string name) {
            string[] words = Regex.Matches(name, "(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")
                .OfType<Match>()
             .Select(m => m.Value)
                    .ToArray();
            string result = string.Join(" ", words);
            return result;
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
        public static DataTable GetMoreFieldInstance(string conn,int clientid)
        {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBQuery select;
             DBComparison dbClient = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(clientid));
            DBComparison dbfield = DBComparison.Compare(DBField.Field("FieldGroupID"), Tech.Data.Compare.Equals, DBConst.Int32(20));

        
            select = DBQuery.SelectAll().From("sys_fieldinstance").WhereAll(dbfield, dbClient);
            return db.Database.GetDatatable(select);
        }
        public static void ConvertCustomFields(int ClientID, string connection) {
            TZ.CompExtention.ComponentManager employeeManager = new CompExtention.ComponentManager(ClientID, "sys_user", new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            employeeManager.LoadAttributes();
            DataTable dtMoreFieldInstance = GetMoreFieldInstance(connection, ClientID);

          DataTable dtClients = GetClientList(connection);
           // foreach (DataRow dr in dtClients.Rows) {
                var tbName = "sys_user_custom_client_" + ClientID;

             //   dtMoreFieldInstance.DefaultView.RowFilter = "ClientID =" + Convert.ToInt32(dr["clientid"]);
               // var dtMoreFieldByClient = dtMoreFieldInstance.DefaultView.ToTable();
                if (dtMoreFieldInstance.Rows.Count > 0) {

                    var cm = new CompExtention.ComponentManager();
                    cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
                    cm.NewComponent(ClientID, "Employee more Information", (CompExtention.ComponentType.meta));
                    var component = (CompExtention.Component)cm.Component;
                    component.TableName = tbName;

                    List<CompExtention.Builder.FieldElement> TalentozComponentFields = new List<CompExtention.Builder.FieldElement>();

                    foreach (DataRow dRow in dtMoreFieldInstance.Rows)
                    {
                        CompExtention.Builder.FieldElement f = new CompExtention.Builder.FieldElement();
                        f = Newtonsoft.Json.JsonConvert.DeserializeObject<CompExtention.Builder.FieldElement>(dRow["FieldAttribute"].ToString());
                        f.FieldInstanceID = Convert.ToInt32(dRow["FieldInstanceID"].ToString());
                        f.FieldDescription = (dRow["FieldDescription"].ToString());
                        f.ComponentID = Convert.ToInt32(dRow["CompType"].ToString());
                        f.FieldTypeID = Convert.ToInt32(dRow["FieldTypeID"].ToString());
                        f.FieldGroupID = Convert.ToInt32(dRow["FieldGroupID"].ToString());
                        
                        TalentozComponentFields.Add(f);
                    }

                    CompExtention.Attribute _att_id1 = component.NewAttribute(ClientID);
                    _att_id1.IsKey = true;
                    _att_id1.Name = "ClientID";
                    _att_id1.DisplayName = "ClientID";
                    _att_id1.Type = CompExtention.AttributeType._number;
                    _att_id1.IsNullable = false;
                    _att_id1.ComponentLookup = "";
                    _att_id1.ComponentLookupDisplayField = "";
                    component.AddAttribute(_att_id1);
                    component.Keys.Add(_att_id1);

                    CompExtention.Attribute _att_id2 = component.NewAttribute(ClientID);

                    _att_id2.IsKey = true;
                    _att_id2.Name = "UserID";
                    _att_id2.DisplayName = "Employee No";
                    _att_id2.Type = CompExtention.AttributeType._componentlookup ;
                    _att_id2.ComponentLookup = employeeManager.Component.ID ;
                    _att_id2.ComponentLookupDisplayField = employeeManager.Component.Attributes.Where(x=>x.Name =="F_200005").FirstOrDefault().ID;
                    _att_id2.IsNullable = false;
                    component.AddAttribute(_att_id2);
                    component.Keys.Add(_att_id2);

                    foreach (CompExtention.Builder.FieldElement f in TalentozComponentFields)
                    {

                        CompExtention.Attribute _att = component.NewAttribute(ClientID);
                        _att.Name = "F_" + f.FieldInstanceID;
                        _att.DisplayName = f.FieldDescription.Replace("(", "").Replace(")", "");
                        _att.DefaultValue = f.DefaultValue;
                        _att.FileExtension = f.FileExtension;
                        _att.LookupInstanceID = f.FieldInstanceLookUpID.ToString();
                        _att.Length = f.MaxLength;
                        _att.IsUnique = f.isUnique;
                        _att.ComponentLookup = "";
                        _att.ComponentLookupDisplayField = "";
                        _att.IsRequired = f.isRequired;
                        _att.IsNullable = true;
                        _att.Type = GetAttributeType(f.FieldTypeID);
                        _att.IsCore = f.isCore;
                        
                        _att.IsAuto = false;
                        _att.IsSecured = false;
                        component.AddAttribute(_att);
                    }
                    cm.Save(component);
                    cm.LoadAttributes();

                    CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection, ClientID));
                    var view = cvm.NewView(component.Name );
                    view.CoreComponent = component.ID;
                    cvm.Save(view);

                    var tmp = new CompExtention.ImportTemplate.Template(connection, ClientID);
                    string fmt = "00000000";
                    string formatString = " {0,15:" + fmt + "}";
                    tmp.Name = component.Name ;
                    tmp.Code = string.Format(formatString, 1012);
                    tmp.Category = "Employee";
                    tmp.ViewID = view.ID;
                    // 200005
                    foreach (Attribute field in component.Attributes )
                    {
                        CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                        var ifield = field;
                        if (ifield != null)
                        {
                            if (ifield.DisplayName.EndsWith("ID"))
                            {
                                continue;
                            }
                            te.ID = ifield.ID;
                            if (ifield.Type == AttributeType._file || ifield.Type == AttributeType._picture)
                            {
                                continue;
                            }
                            if (field.Name  == "UserID")
                            {

                                te.IsKey = true;
                                te.IsRequired = true;
                                te.IsDefault = true;
                            }
                            else
                            {
                                te.IsKey = ifield.IsKey;
                                te.IsRequired = ifield.IsRequired;
                                te.IsDefault = false;                                                           
                            }
                            tmp.TemplateFields.Add(te);
                        }
                    }
                    tmp.Save();

                    // TZ.CompExtention.Component Comp = new CompExtention.Component(comp.ComponentName, GetType(comp.ComponentType));
                }

             
               
           // }
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
                   .Field("sys_FieldInstance", "FieldGroupID")
                .From("sys_FieldType").InnerJoin("sys_FieldInstance").On("sys_FieldType", "FieldTypeID", Tech.Data.Compare.Equals, "sys_FieldInstance", "CompType").WhereAll( dbClient);
            return db.Database.GetDatatable(select);
        }

        public static DataTable GetGroups(int clientid, string conn) {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBComparison dbClient = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(clientid));
            DBComparison dbcomp = DBComparison.Compare(DBField.Field("ComponentID"), Tech.Data.Compare.Equals, DBConst.Int32(30000));


            DBQuery select;
            select = DBQuery.Select().Field("sys_fieldgroup", "FieldGroupID")
                .Field("sys_fieldgroup", "FieldGroupName")
                .From("sys_fieldgroup").WhereAll(dbClient, dbcomp);
            return db.Database.GetDatatable(select);
        }
        public static DataTable GetRelatedComponent(int clientid, string conn)
        {
            DataBase db = new DataBase();
            db.InitDbs(conn);
            DBComparison dbClient = DBComparison.Compare(DBField.Field("ClientID"), Tech.Data.Compare.Equals, DBConst.Int32(clientid));
       
            

            DBQuery select;
            select = DBQuery.Select().Field("sys_relatedcomponent", "ComponentID")
                .Field("sys_relatedcomponent", "RelatedComponent")                
                .From("sys_relatedcomponent").WhereAll(dbClient);
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
            else if (tzFieldID == 7)
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

    public class TZComponentView { 
    public string ComponentID { get; set; }
        public string ViewID { get; set; }
        public int TZComponentID { get; set; }
    }

    public class TZBaseKeyField { 
    public string ID { get; set; }
        public string KeyField { get; set; }
        public string entity { get; set; }
    }

}
