using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.Data;

namespace TZ.CompExtention.ImportTemplate
{
    public class TemplateRestore
    {
        public int ClientID {get;set;}
        public string Connection { get; set; }
        DataManager dm;
        public TemplateRestore(int cid,string conn) {
            ClientID = cid;
            Connection = conn;
            dm = new DataManager();
        }

        private bool PushTemplate(TemplateBackup tb)
        {
          //  dm = new DataManager();
          //  dm.tableName(TZ.CompExtention.Builder.Schema.TalentozTemplate.Table);
          //  List<string> fileContent = new List<string>();
          //  List<string> cols = new List<string>();
          //  cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozTemplate.TemplateID.Name.ToString() + "\"");
          //  cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozTemplate.ClientID.Name.ToString() + "\"");
          //  cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozTemplate.ViewID.Name.ToString() + "\"");
          //  cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozTemplate.TemplateCode.Name.ToString() + "\"");
          //  cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozTemplate.Name.Name.ToString() + "\"");
          //  cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozTemplate.Category.Name.ToString() + "\"");
          ////  cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozTemplate.PivotColumn.Name.ToString() + "\"");
          //  cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozTemplate.TemplateType.Name.ToString() + "\"");
          //  cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozTemplate.ViewFields.Name.ToString() + "\"");
          //  fileContent.Add(string.Join(",", cols.ToArray()));

            foreach (Template c in tb.Templates)
            {
                
                c.ClientID = ClientID;
                try
                {
                    c.Import(Connection);
                }
                catch (Exception ex) {
                 
                }               
             //   List<string> rows = new List<string>();
             //   rows.Add("\"" + c.TemplateID + "\"");
             //   rows.Add("\"" + ClientID + "\"");
             //   rows.Add("\"" + c.ViewID + "\"");
             //   rows.Add("\"" + c.Code + "\"");
             //   rows.Add("\"" + c.Name + "\"");
             //   rows.Add("\"" + c.Category + "\"");
             ////   rows.Add("\"" + c.pivo + "\"");
             //   rows.Add("'" + Newtonsoft.Json.JsonConvert.SerializeObject( c.TemplateFields).Trim()  + "'");
             //   fileContent.Add(string.Join(",", rows.ToArray()));
            }
            //dm.Data(fileContent.ToArray());
            //try
            //{
            //    dm.ExecuteNonQuery(Connection);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            return true;
        }

        private bool PushView(TemplateBackup tb) {

            dm = new DataManager();
            dm.tableName(TZ.CompExtention.Builder.Schema.TalentozView.Table);
            List<string> fileContent = new List<string>();
            List<string> cols = new List<string>();

            List<string> fileContent_vr = new List<string>();
            List<string> cols_vr = new List<string>();

            List<string> fileContent_vr_item = new List<string>();
            List<string> cols_vr_item = new List<string>();

            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozView.ViewID.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozView.CoreComponent.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozView.Name.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozView.Catgory.Name.ToString() + "\"");
            fileContent.Add(string.Join(",", cols.ToArray()));


            cols_vr.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchema.ViewID.Name.ToString() + "\"");
            cols_vr.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchema.ComponentID.Name.ToString() + "\"");
            cols_vr.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchema.ChildComponentID.Name.ToString() + "\"");
            cols_vr.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchema.ComponentAlias.Name.ToString() + "\"");
            fileContent_vr.Add(string.Join(",", cols_vr.ToArray()));


            cols_vr_item.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.ViewID.Name.ToString() + "\"");
            cols_vr_item.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.ViewSchemaRelation.Name.ToString() + "\"");
            cols_vr_item.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.ParentField.Name.ToString() + "\"");
            cols_vr_item.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.RelatedField.Name.ToString() + "\"");
            cols_vr_item.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.Parent.Name.ToString() + "\"");
            cols_vr_item.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.Child.Name.ToString() + "\"");

            fileContent_vr_item.Add(string.Join(",", cols_vr_item.ToArray()));

            foreach (ComponentView v in tb.Views) {
                List<string> rows = new List<string>();
               
                rows.Add("\"" + v.ID + "\"");
                rows.Add("\"" + v.CoreComponent + "\"");
                rows.Add("\"" +v.Name + "\"");
                rows.Add("\"" + v.Category + "\"");
                fileContent.Add(string.Join(",", rows.ToArray()));


                foreach (ComponentRelation  vr in v.ComponentRelations) {
                    vr.ViewID = v.ID;
                    rows = new List<string>();
                    rows.Add("\"" + vr.ViewID + "\"");
                    rows.Add("\"" + vr.ComponentID + "\"");
                    rows.Add("\"" + vr.ChildComponentID  + "\"");
                    rows.Add("\"" + vr.ComponentAlias  + "\"");
                    fileContent_vr.Add(string.Join(",", rows.ToArray()));

                    foreach (ViewRelation vritem in vr.Relationship) {
                        rows = new List<string>();

                        rows.Add("\"" + vr.ViewID + "\"");
                        rows.Add("\"" + vritem.ID + "\"");
                        rows.Add("\"" + vritem.LeftField  + "\"");
                        rows.Add("\"" + vritem.RightField  + "\"");
                        rows.Add("\"" + vritem.Left + "\"");
                        rows.Add("\"" + vritem.Right  + "\"");

                        fileContent_vr_item.Add(string.Join(",", rows.ToArray()));

                    }

                }
            }

            dm.InsertInto (  TZ.CompExtention.Builder.Schema.TalentozView.ViewID.Name.ToString()  , FieldType._string );
            dm.InsertInto( TZ.CompExtention.Builder.Schema.TalentozView.CoreComponent.Name.ToString()  , FieldType._string);
            dm.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozView.Name.Name.ToString()  , FieldType._string);
            dm.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozView.Catgory.Name.ToString()  , FieldType._string);

            dm.Data(fileContent.ToArray());
            try
            {
                dm.ExecuteNonQuery(Connection);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (fileContent_vr.Count > 1) {
                var dm1 = new DataManager();

                dm1.tableName(TZ.CompExtention.Builder.Schema.TalentozViewSchema.Table);

                dm1.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchema.ViewID.Name.ToString(), FieldType._string);
                dm1.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchema.ComponentID.Name.ToString(), FieldType._string);
                dm1.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchema.ChildComponentID.Name.ToString(), FieldType._string);
                dm1.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchema.ComponentAlias.Name.ToString(), FieldType._string);

                dm1.Data(fileContent_vr.ToArray());
                try
                {
                    dm1.ExecuteNonQuery(Connection);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            if (fileContent_vr_item.Count > 1) {
               var dm2 = new DataManager();
                dm2.tableName(TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.Table);
                dm2.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.ViewID.Name.ToString()  , FieldType._string);
                dm2.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.ViewSchemaRelation.Name.ToString()  , FieldType._string);
                dm2.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.ParentField.Name.ToString()  , FieldType._string);
                dm2.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.RelatedField.Name.ToString()  , FieldType._string);
                dm2.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.Parent.Name.ToString()  , FieldType._string);
                dm2.InsertInto(  TZ.CompExtention.Builder.Schema.TalentozViewSchemaRelation.Child.Name.ToString()  , FieldType._string);

                dm2.Data(fileContent_vr_item.ToArray());
                try
                {
                    dm2.ExecuteNonQuery(Connection);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }          
            return true;
        }

        private bool PushComponentAttribute(TemplateBackup tb)
        {
            dm = new DataManager();
            dm.tableName(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.Table);

            List<string> fileContent = new List<string>();
            List<string> cols = new List<string>();
                        
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.AttributeName.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.DisplayName.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.ComponentID.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.FieldID.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsRequired.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsUnique.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsCore.Name.ToString() + "\"");
            //cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsReadOnly.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsSecured.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.LookUpID.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.AttributeType.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.Length.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.DefaultValue.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.FileExtension.Name.ToString() + "\"");
         //   cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.RegExp.Name.ToString() + "\"");           
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsNullable.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.ISPrimaryKey.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.LookupComponent.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.ComponentLookupDisplayName.Name.ToString() + "\"");
            cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsAuto.Name.ToString() + "\"");
            fileContent.Add(string.Join(",", cols.ToArray()));


            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.AttributeName.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.DisplayName.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.ComponentID.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.FieldID.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsRequired.Name.ToString(), FieldType._bool);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsUnique.Name.ToString(), FieldType._bool);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsCore.Name.ToString(), FieldType._bool);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsSecured.Name.ToString(), FieldType._bool);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.LookUpID.Name.ToString(), FieldType._bool);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.AttributeType.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.Length.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.DefaultValue.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.FileExtension.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsNullable.Name.ToString(), FieldType._bool);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.ISPrimaryKey.Name.ToString(), FieldType._bool);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.LookupComponent.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.ComponentLookupDisplayName.Name.ToString(), FieldType._string);
            dm.InsertInto(TZ.CompExtention.Builder.Schema.TalentozSchemaInfo.IsAuto.Name.ToString(), FieldType._bool);
            

            foreach (Attribute c in tb.Attributes)
            {
                List<string> rows = new List<string>();               
                rows.Add("\"" + c.Name + "\"");
                rows.Add("\"" + c.DisplayName + "\"");
                rows.Add("\"" + c.ComponentID + "\"");
                rows.Add("\"" + c.ID + "\"");
                rows.Add("\"" + c.IsRequired + "\"");
                rows.Add("\"" + c.IsUnique + "\"");
                rows.Add("\"" + c.IsCore + "\"");
                rows.Add("\"" + c.IsSecured  + "\"");
                rows.Add("\"" + c.LookupInstanceID + "\"");
                rows.Add("\"" + Convert.ToInt32(c.Type) + "\"");
                rows.Add("\"" + c.Length + "\"");
                rows.Add("\"" + c.DefaultValue + "\"");
                rows.Add("\"" + c.FileExtension  + "\"");
                rows.Add("\"" + c.IsNullable + "\"");
                rows.Add("\"" + c.IsKey + "\"");
                rows.Add("\"" + c.ComponentLookup + "\"");
                rows.Add("\"" + c.ComponentLookupDisplayField + "\"");
                rows.Add("\"" + c.IsAuto + "\"");                 
                fileContent.Add(string.Join(",", rows.ToArray()));
            }
            dm.Data(fileContent.ToArray());
            try
            {
                dm.ExecuteNonQuery(Connection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
        private bool PushComponent(TemplateBackup tb) {
            //dm = new DataManager();
            //dm.tableName(TZ.CompExtention.Builder.Schema.TalentozSchema.Table);

            //List<string> fileContent = new List<string>();
            //List<string> cols = new List<string>();

            //foreach (var col in tb.Components )
            //{
            //    cols.Add(("\"" + col.ToString() + "\""));
            //}
             
            //cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.ComponentID.Name.ToString() + "\"");
            //cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.ComponentName.Name.ToString() + "\"");
            //cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.Category.Name.ToString() + "\"");
            //cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.ComponentType.Name.ToString() + "\"");
            //cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.Title.Name.ToString() + "\"");
            //cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.PrimaryKeys.Name.ToString() + "\"");
            //cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.TableName.Name.ToString() + "\"");
            ////cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.IsGlobal.Name.ToString() + "\"");
            ////cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.ComponentState.Name.ToString() + "\"");
            //cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.EntityKey.Name.ToString() + "\"");
            ////cols.Add("\"" + TZ.CompExtention.Builder.Schema.TalentozSchema.ComponentID.Name.ToString() + "\"");
            //fileContent.Add(string.Join(",", cols.ToArray()));

            TZ.CompExtention.DataAccess.ComponentDataHandler d = new DataAccess.ComponentDataHandler(Connection);

            foreach (Component c in tb.Components)
            {
                try
                {
                    d.ImportComponent(c);
                }
                catch (Exception ex) { 
                
                }


                //List<string> rows = new List<string>();
                ////foreach (var column in dr.ItemArray)
                ////{
                //    rows.Add("\"" + c.ID + "\"");
                //rows.Add("\"" + c.Name + "\"");
                //rows.Add("\"" + c.Category + "\"");
                //rows.Add("\"" + Convert.ToInt32( c.Type )+ "\"");
                //rows.Add("\"" + c.Title + "\"");
                //rows.Add("\"" + c.Keys + "\"");
                //rows.Add("\"" + c.TableName  + "\"");
                //rows.Add("\"" + c.EntityKey  + "\"");
                ////rows.Add("\"" + c.ID + "\"");
                ////rows.Add("\"" + c.ID + "\"");
                ////rows.Add("\"" + c.ID + "\"");
                ////rows.Add("\"" + c.ID + "\"");
                /////}
                //fileContent.Add(string.Join(",", rows.ToArray()));
            }
            //dm.Data(fileContent.ToArray());
            //try
            //{
            //    dm.ExecuteNonQuery(Connection);
            //}
            //catch (Exception ex) {
            //    throw new Exception(ex.Message);
            //}
            return true;
        }
        public bool Restore(TemplateBackup tbk) {
            TZ.CompExtention.Builder.Data.ComponentBuilder db = new Builder.Data.ComponentBuilder(Connection);
            
            if (tbk.BackupType  == 1)
            {
                try
                {
                    db.ClearTemplate();
                    PushTemplate(tbk);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else {
                db.ClearComponent();
                try
                {
                    PushComponent(tbk);
                }
                catch (Exception ex) {
                    throw ex;
                }

                try
                {
                    PushComponentAttribute(tbk);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                try
                {
                    PushView(tbk);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                try
                {
                    PushTemplate(tbk);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
                
            }

            return true;
        }
    }
}
