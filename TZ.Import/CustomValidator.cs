using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace TZ.Import
{
    public abstract class BaseCustomAction
    {
     
        public abstract void Init();
        public abstract ImportError Validate(ComponentData componentData, DataRow dr);
        public abstract ImportError ValidateLink(ComponentData componentData, DataRow dr);
        public abstract void AfterSave(ComponentData componentData, DataTable dt);
        public abstract void AfterUpdate(ComponentData componentData, DataTable dt);
    }

    public class CustomAction : BaseCustomAction
    {
   

        public override void AfterSave(ComponentData componentData, DataTable dt)
        {
            throw new NotImplementedException();
        }

        public override void AfterUpdate(ComponentData componentData, DataTable dt)
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {

        }
        public override ImportError Validate(ComponentData componentData, DataRow dr)
        {
            return new ImportError() { Type = ErrorType.NOERROR, Message = "" };
        }

        public override ImportError ValidateLink(ComponentData componentData, DataRow dr)
        {
            throw new NotImplementedException();
        }
    }

    public class ComponentCustomAction {
        public string ComponentName { get; set; }
        public BaseCustomAction CustomAction { get; set; }
    }
}
