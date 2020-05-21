using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
  public  interface IComponentDataAccess
    {
       string Connection { get; }
       bool SaveComponent(Component component);
        bool Remove(string ID);
        bool UpdateComponent(Component component );
        bool AddAttribute(string id,  Attribute attr);
        bool RemoveAttribute(string componentID, string attributeID);
        List<Attribute> GetAttribute(string componentID,int clientID);
        List<Component> GetComponents();
        Component GetComponent(string id);
        bool UpdateComponentLookup(int clientID, string componentID, string attributeID, string componentLookUp, string displayName);

        bool SaveAttribute(string compID, Attribute att);
        bool UpdateAttribute(string compID, Attribute att);
    }
}
