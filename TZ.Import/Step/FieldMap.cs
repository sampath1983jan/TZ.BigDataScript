using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.CompExtention;

namespace TZ.Import.Step
{
    //Step -3
   public class FieldMap: BaseImportHandler
    {
        public FieldMap(BaseImportHandler nextHandler, ImportContext context) : base(nextHandler, context)
        {

        }
        public override ImportError HandleRequest(string logPath)
        {
            var IE = Validate();
            if (IE.Type == ErrorType.NOERROR) {
                this.Context.Status = ImportStatus.pending;


                this.Context.View = null;
                this.Context.ComponentData = new List<ComponentData>();
                 this.Context.Template.TemplateFields = new List<CompExtention.ImportTemplate.TemplateField>();
                this.Context.Template.View = null;
                this.Context.DataLocation = "";

                Global.SaveImportContext(this.Context.ID, Newtonsoft.Json.JsonConvert.SerializeObject(this.Context), 3,
                 this.Context.ActionBy,
                 this.Context.Connection, this.Context.ClientID
                 );             
                return new ImportError() { Message = "No Error", Type = ErrorType.NOERROR };
            }
            else {
                return IE;
            }
            // update data in the request;
        }
        public override ImportError Validate()
        {
            var context = this.Context;
            List<ComponentRelation > components = context.View.ComponentRelations;
            var errorstring = "";
            List<string> comp = new List<string>();
            comp.Add(context.View.CoreComponent);
            foreach (ComponentRelation vc in context.View.ComponentRelations)
            {
                if (context.View.CoreComponent != vc.ComponentID)
                {
                    comp.Add(vc.ComponentID);
                }
                if (context.View.CoreComponent != vc.ChildComponentID)
                {
                    comp.Add(vc.ChildComponentID);              
                }
            }
         var   ViewAttributes = CompExtention.ComponentManager.GetComponentAttributes(string.Join(",", comp.ToArray()), this.Context.ClientID, new CompExtention.DataAccess.ComponentDataHandler(this.Context.Connection));

            foreach (ImportFieldMap ifm in this.Context.ImportFields)
            {

                var component = components.Where(x => x.ComponentID == ifm.ComponentID || x.ChildComponentID == ifm.ComponentID).FirstOrDefault();
                if (component != null)
                {
                    var att = ViewAttributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                    if (att == null)
                    {
                        errorstring = errorstring + "," + ifm.DataField + " ";
                    }
                }  
                else
                {
                    var att = ViewAttributes.Where(x => x.ID == ifm.DataField).FirstOrDefault();
                    if (att == null)
                    {
                        errorstring = errorstring + "," + ifm.DataField + " ";
                    }
                    else {
                      //  throw new Exception("Mapping field component dosenot exist in your schema. contact adminstrator");
                    }
                  
                }
            }
            if (errorstring != "")
            {
                return new ImportError() { Message = "Following data field  dosenot exist in import schema. Fields are "+ errorstring + ".", Type = ErrorType.ERROR };
            }
            else {
                return new ImportError() { Message = "No Error", Type = ErrorType.NOERROR };
            }
        }
    }
}
