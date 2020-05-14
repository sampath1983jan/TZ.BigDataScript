using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public class ComponentViewDataAccess : IComponentViewDataAccess
    {
        private string conn;
        private readonly string folderName = "componentview";
        public string Connection => conn;
        private string GetFolder { get => conn + "/" + folderName; }
        private string ViewPath { get => conn + "/" + folderName + "/" + "viewlist.json"; }
        public ComponentViewDataAccess(string path)
        {
            conn = path;
            CheckFolderExist();
        }
        private void CheckFolderExist()
        {
            if (!Directory.Exists(GetFolder + @"/"))
            {
                Directory.CreateDirectory(GetFolder);
            }
        }
        public ComponentView GetView(string viewid)
        {
            string jsonAttribute = File.ReadAllText(ViewPath);
            var compview= Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComponentView>>(jsonAttribute);
           return compview.Where(x => x.ID == viewid).FirstOrDefault();
        }

        public List<ComponentView> GetViews()
        {
            string jsonAttribute = File.ReadAllText(ViewPath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComponentView>>(jsonAttribute);
        }

        public bool RemoveView(string viewID)
        {
            throw new NotImplementedException();
        }

        public bool SaveView(IComponentView view)
        {
            if (!File.Exists(ViewPath))
            {
                File.WriteAllText(ViewPath, "");
            }
            string jsonAttribute = File.ReadAllText(ViewPath);
            List<ComponentView> comp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComponentView>>(jsonAttribute);
            if (comp != null)
            {
                var compAttribute = comp.Where(x => x.Name == view.Name).FirstOrDefault();
                if (compAttribute == null)
                {
                    view.ID = Shared.generateID();
                    comp.Add((ComponentView)view);
                }
                else {
                    compAttribute = (ComponentView)view;
                }
            }
            else
            {
                comp = new List<ComponentView>();
                view.ID = Shared.generateID();                 
                comp.Add((ComponentView)view);
            }
            File.WriteAllText(ViewPath, Newtonsoft.Json.JsonConvert.SerializeObject(comp));            
            return true;
        }

        public bool UpdateView(IComponentView view)
        {
            throw new NotImplementedException();
        }

        public void LoadViewComponents(IComponentView view)
        {
            throw new NotImplementedException();
        }
    }
}
