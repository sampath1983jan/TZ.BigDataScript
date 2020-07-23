using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TZ.CompExtention;
namespace TZ.Import.Step
{
    //Step -1
    public class ChooseComponentView : BaseImportHandler
    {       
        public ChooseComponentView(BaseImportHandler nextHandler, ImportContext context) : base(nextHandler, context) { 
        
        }
        public override ImportError Validate()
        {
            //if (!(Directory.Exists(this.Context.ComponentViewPath)))
            //{
            //    return new ImportError() { Message = "Component View directory doest not exist. please enter validate folder path", 
            //        Type=ErrorType.ERROR  };
            //}
            if (this.Context.ImportTemplateID == "")
            {
                return new ImportError()
                {
                    Message = "Unable to process this import due to invalid template.",
                    Type = ErrorType.ERROR
                };
            }
            else {
                return new ImportError()
                {
                    Message = "",
                    Type = ErrorType.NOERROR
                };
            }
        }
        public override ImportError HandleRequest(string logPath)
        {
            ImportError ie = Validate();
            if (ie.Type == ErrorType.NOERROR) { 
                this.Context.LoadComponentView();

            if (this.Context.ID == null) {
                    this.Context.ID = Shared.generateID();
                }
                       
                
                this.Context.Status = ImportStatus.pending;

                var con = this.Context.Clone<ImportContext>();
                con.View = null;
                con.ComponentData = new List<ComponentData>();
                con.Template.TemplateFields = new List<CompExtention.ImportTemplate.TemplateField>();
                con.Template.View = null;
                con.DataLocation = "";

                Global.SaveImportContext(this.Context.ID, Newtonsoft.Json.JsonConvert.SerializeObject(con), 1,
                 this.Context.ActionBy,
                 this.Context.Connection, this.Context.ClientID
                 );
                return new ImportError() { Message ="No Error", Type=ErrorType.NOERROR };
            }
            else {
                return ie;
            }           
        }        
    }
}
