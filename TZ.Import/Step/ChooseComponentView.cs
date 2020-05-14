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
            if (ie.Type == ErrorType.NOERROR )
            {
                this.Context.LoadComponentView();              
                this.Context.ID = Shared.generateID();
                this.Context.Status = ImportStatus.pending;

                this.Context.DataLocation = "";
                this.Context.View = null;
                this.Context.ComponentData = new List<ComponentData>();
                 this.Context.Template.TemplateFields = new List<CompExtention.ImportTemplate.TemplateField>();
                this.Context.Template.View = null;


                Global.SaveImportContext(this.Context.ID ,Newtonsoft.Json.JsonConvert.SerializeObject(this.Context), 1,                   
                    this.Context.ActionBy,
                    this.Context.Connection,this.Context.ClientID 
                    );
                return new ImportError() { Message ="No Error", Type=ErrorType.NOERROR };
            }
            else {
                return ie;
            }           
        }        
    }
}
