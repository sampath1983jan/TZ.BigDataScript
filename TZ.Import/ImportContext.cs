using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.CompExtention;
 
using TZ.Import.Step;

namespace TZ.Import
{
    public enum ImportFileType { 
    csv,
    excel,
    excelx,
    json,
    }
    public enum ImportStatus { 
    pending,
    completed,
    error,
    }
    public class ImportContext
    {
        public List<ImportError> Errors;
        public int ClientID { get; set; }  
        /// <summary>
        /// File Type extension
        /// </summary>
        public ImportFileType FileType { get; set; }
        /// <summary>
        /// TemplateID for import file
        /// </summary>
        public string ImportTemplateID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CompExtention.ImportTemplate.Template Template {get;set;}
        /// <summary>
        /// 
        /// </summary>
        public string DataFileName { get; set; }
        /// <summary>
        ///  Path of the import file
        /// </summary>
        public string DataFilePath { get; set; }
        public string DataLocation { get; set; }
        public List<string> DataColumns { get; set; }
        /// <summary>
        /// Field Mapping with Target
        /// </summary>
        public List<ImportFieldMap> ImportFields { get; set; }
        /// <summary>
        /// Validation information
        /// </summary>
        //public Object ValidationPath { get; set; }
        /// <summary>
        /// ID of the Import log
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Status of the import process
        /// </summary>
        public ImportStatus Status { get; set; }
        /// <summary>
        /// Who done this action
        /// </summary>
        public string ActionBy { get; set; }
        /// <summary>
        /// Component View Object
        /// </summary>
        public ComponentView View { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ComponentData> ComponentData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal string Connection { get; set; }
        public List<string> ImportSelectedFields { get; set; }
        public bool EnableUpdateDuplicate { get; set; }
        public bool EnableCreateLookup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public ImportContext(string conn) {
            Connection = conn;
            ComponentData = new List<ComponentData>();
            Errors = new List<ImportError>();
            ImportSelectedFields = new List<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="con"></param>
        public void SetConnection(string con) {
            Connection = con;
        }
        private void LoadImportTemplate() {
        Template = new CompExtention.ImportTemplate.Template(this.ImportTemplateID, ClientID, this.Connection);
        }

        public   List<ImportDataStatus> GetComponentStatus(string LogPath) {
            List<ImportDataStatus> ids = new List<ImportDataStatus>();
            foreach (ComponentData cd in this.ComponentData) {
                cd.LoadImportStatus(LogPath, this.ID);
                ids.Add(cd.DataStatus);
            }
            return ids;
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadComponentView() {
            if (Template == null || Template.TemplateFields.Count ==0 ) {
                LoadImportTemplate();
                foreach (CompExtention.ImportTemplate.TemplateField te in Template.TemplateFields) {
                    if (ImportSelectedFields.Where(x => x == te.ID).FirstOrDefault() != null)
                    {
                        te.IsDefault = true;
                    }
                    else {
                        te.IsDefault = false;
                    }
                } 
            }
            Template.LoadView(ClientID);
            this.View = Template.View;
        // ComponentViewManager cv = new ComponentViewManager(Template.ViewID,ClientID, new CompExtention.DataAccess.ComponentViewHandler(this.Connection,this.ClientID));
        // this.View=(ComponentView)cv.View;
        //    Template.View = this.View;

        }
    }
}
