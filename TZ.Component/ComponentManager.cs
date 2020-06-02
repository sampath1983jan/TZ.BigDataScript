using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
   public class ComponentManager
    {
        private IComponent component;
        private IComponentDataAccess actions;
        private int clientID;
        /// <summary>
        /// 
        /// </summary>
        public IComponent Component { get => component; }
        /// <summary>
        /// 
        /// </summary>
        public IComponentDataAccess ComponentActions { get => actions; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public ComponentManager(int pClientID, string id, IComponentDataAccess access) {
            actions = access;
            clientID = pClientID;
            component = actions.GetComponent(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="access"></param>
        public void Set(IComponentDataAccess access) {
            actions = access;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ComponentManager() { 
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComponent NewComponent(int clientID,string name, ComponentType type) {
            component = new Component(name, type);
            return component;
        }
        public bool Save(IComponent _component) {
            component = _component;
            if (this.Component.ID != "")
            {
                return this.actions.UpdateComponent((Component)component);
            }
            else {
                if (this.actions.GetComponent(this.Component.TableName) ==null)
                {
                    return this.actions.SaveComponent((Component)component);
                }
                else {
                    return false;
                }
            }        
        }
        public bool SaveAttribute(int clientID, CompExtention.Attribute att) {
            att.ComponentID = this.Component.ID;           
            if (this.component.Attributes.Where(x => x.ID == att.ID && x.ClientID == clientID).ToList().Count > 0)
            {
                return this.actions.UpdateAttribute(this.component.ID, att);
            }
            else {
                att.ClientID = clientID;
                return this.actions.SaveAttribute(this.component.ID, att);
            }         
        }

        public static List<Attribute> GetAllAttributes(int clientID, IComponentDataAccess access) {
            return access.GetAllAttributes(clientID);
        }
        public static List<Attribute> GetComponentAttributes(string components,int clientID, IComponentDataAccess access) {
          return access.GetAttribute(components, clientID);
        }
        public void LoadAttributes() {
            this.Component.Attributes =this.actions.GetAttribute(component.ID,clientID);
        }
        public bool UpdateComponentLookup(int clientID,string componentID, string attributeID, string componentLookup, string displayfield) {
            return this.actions.UpdateComponentLookup(clientID, componentID, attributeID, componentLookup, displayfield);
        }
        public bool Remove() {
            return this.actions.Remove(this.Component.ID);
        }
        public bool AddAttribute(IAttribute attribute) {
            return this.actions.AddAttribute(this.Component.ID, (Attribute)attribute);
        }
        public bool RemoveAttribute(string attributeID)
        {
            return this.actions.RemoveAttribute(this.Component.ID, attributeID);
        }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public IComponent GetComponent() {
            return this.component;
          
        }
    }
}
