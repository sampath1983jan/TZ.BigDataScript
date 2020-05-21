using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public class ComponentJsonDataAccess : IComponentDataAccess

    {
        private readonly string folderName = "Component";     
        private readonly string connection;
        public string Connection { get => connection;  }
        private string GetFolder { get => connection + "/" + folderName; }
        private string ComponentPath { get =>   GetFolder + "/componentlist.json"; }
        public ComponentJsonDataAccess(string conn) {
            connection = conn;
            CheckFolderExist();            
        }
        private void CheckFolderExist() {
            if (!Directory.Exists(GetFolder + @"/")) {
                Directory.CreateDirectory(GetFolder);
            }
        }        
        public bool AddAttribute(string id, Attribute attr)
        {
            var compPath = GetFolder + "/" + id + ".json";
            if (!File.Exists(compPath)) {
                File.WriteAllText(compPath,"");
            }
            string jsonAttribute = File.ReadAllText(compPath);
            IComponent comp=  Newtonsoft.Json.JsonConvert.DeserializeObject<IComponent>(jsonAttribute);
            var compAttribute = comp.Attributes.Where(x => x.ComponentID == id && x.ID==attr.ID).FirstOrDefault();
            if (compAttribute == null) {
                attr.ID = Shared.generateID();
                comp.Attributes.Add((Attribute)attr);
            }
            File.WriteAllText(compPath, Newtonsoft.Json.JsonConvert.SerializeObject(comp));
            return true;        
        }
        public List<Attribute> GetAttribute(string componentID)
        {
            var compPath = GetFolder + "/" + componentID + ".json";
            if (!File.Exists(compPath))
            {
                throw new Exception("Component doesn't exist");
            }
            string jsonAttribute = File.ReadAllText(compPath);
            IComponent comp = Newtonsoft.Json.JsonConvert.DeserializeObject<IComponent>(jsonAttribute);
            return comp.Attributes;
        }
        public bool Remove(string ID)
        {
            var compPath = GetFolder + "/" + ID + ".json";
            if (File.Exists(compPath))
            {
                File.Delete(compPath);
            }
            string jsonAttribute = File.ReadAllText(ComponentPath);
            List<IComponent> comp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IComponent>>(jsonAttribute);
            comp.Remove(comp.Where(x => x.ID == ID).FirstOrDefault());
            File.WriteAllText(ComponentPath, Newtonsoft.Json.JsonConvert.SerializeObject(comp));
            return true;
        }
        public bool RemoveAttribute(string componentID, string attributeID)
        {
            var compPath = GetFolder + "/" + componentID + ".json";
            if (File.Exists(compPath))
            {
                string jsonAttribute = File.ReadAllText(compPath);
                IComponent comp = Newtonsoft.Json.JsonConvert.DeserializeObject <IComponent>(jsonAttribute);
                //  comp.Remove(comp.Where(x => x.ID == componentID).FirstOrDefault());
                comp.Attributes.Remove(comp.Attributes.Where(x => x.ID == attributeID).FirstOrDefault());
                File.WriteAllText(compPath, Newtonsoft.Json.JsonConvert.SerializeObject(comp));
                return true;
            }else
                return false;
        }
        public bool SaveComponent(Component component)
        {
            var compPath = ComponentPath;
            if (!File.Exists(compPath))
            {
                File.WriteAllText(compPath, "");               
            }
            string jsonAttribute = File.ReadAllText(compPath);
            List<Component> comp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Component>>(jsonAttribute);
            if (comp != null)
            {
                var compAttribute = comp.Where(x => x.Name == component.Name).FirstOrDefault();
                if (compAttribute == null)
                {
                    component.ID = Shared.generateID();
                    foreach (IAttribute att in component.Attributes) {
                        att.ComponentID = component.ID;
                        att.ID = Shared.generateID(); 
                    }
                    comp.Add(component);
                }
            }
            else {
                comp = new List<Component>();
                component.ID = Shared.generateID();
                foreach (IAttribute att in component.Attributes)
                {
                    att.ComponentID = component.ID;
                    att.ID = Shared.generateID();
                }
                comp.Add(component);
            }
           
          
            File.WriteAllText(compPath, Newtonsoft.Json.JsonConvert.SerializeObject(comp));
              compPath = GetFolder + "/" + component.ID + ".json";
            if (!File.Exists(compPath))
            {
                File.WriteAllText(compPath, Newtonsoft.Json.JsonConvert.SerializeObject(component));
            }
            return true;         
        }
        public bool UpdateComponent(Component component)
        {
            var compPath = GetFolder + "/" + component.ID + ".json";
            if (!File.Exists(compPath)) {
                throw new Exception("Component doesn't exist");
            }       

            string jsonAttribute = File.ReadAllText(ComponentPath);
            List<IComponent> comp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IComponent>>(jsonAttribute);
            if (comp.Where(x => x.Name == component.Name && x.ID != component.ID).FirstOrDefault() != null) {
                throw new Exception("Component Name already exist.");
            }
            for (int i =0; i< comp.Count; i++) {
                if (comp[i].ID == component.ID) {
                    comp[i] = component;
                }
            }          
              jsonAttribute = File.ReadAllText(compPath);
            IComponent c = Newtonsoft.Json.JsonConvert.DeserializeObject<IComponent>(jsonAttribute);
            c.Name = c.Name;
            c.Keys = c.Keys;
            File.WriteAllText(compPath, Newtonsoft.Json.JsonConvert.SerializeObject(c));
            return true;
        }
        public List<Component> GetComponents()
        {
           string jsonAttribute = File.ReadAllText(ComponentPath);
           return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Component>>(jsonAttribute);
        }
        public Component GetComponent(string id)
        {
            var compPath = GetFolder + "/" + id + ".json";
            if (!File.Exists(compPath))
            {
                File.WriteAllText(compPath, "");
            }
            string jsonAttribute = File.ReadAllText(compPath);
        return  Newtonsoft.Json.JsonConvert.DeserializeObject<Component>(jsonAttribute);
        }

        public Component NewComponent(string name, ComponentType type)
        {
            throw new NotImplementedException();
        }

        public bool UpdateComponentLookup(int clientID, string componentID, string attributeID, string componentLookUp, string displayName)
        {
            throw new NotImplementedException();
        }

        public List<Attribute> GetAttribute(string componentID, int clientID)
        {
            throw new NotImplementedException();
        }

        public bool SaveAttribute(string compID, Attribute att)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAttribute(string compID, Attribute att)
        {
            throw new NotImplementedException();
        }
    }
}
