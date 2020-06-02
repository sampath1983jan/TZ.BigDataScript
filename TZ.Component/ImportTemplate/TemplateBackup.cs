using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.CompExtention.DataAccess;

namespace TZ.CompExtention.ImportTemplate
{
    public class TemplateBackup
    {
        public List<Component> Components { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<ComponentView> Views { get; set; }
        public List<Template> Templates { get; set; }
        /// <summary>
        /// 1 partial 2 full
        /// </summary>
        public int  BackupType {get;set;} //1-partial 2-full
        public TemplateBackup() { 
        
        }
        public TemplateBackup(int clientID,string connection,int type) {
            BackupType = type;
            Templates = Template.GetTemplates(clientID, connection);
            if (type == 2) {
                Components= ComponentDataHandler.GetComponents(connection);
                Attributes = CompExtention.ComponentManager.GetAllAttributes(clientID,
            new CompExtention.DataAccess.ComponentDataHandler(connection));
                Views = ComponentViewHandler.GetViews(connection );                
            }      
        }
    }
}
