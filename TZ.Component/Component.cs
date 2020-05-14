using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public enum ComponentType
    {
        /// <summary>
        /// Master component of the system
        /// </summary>
        core,
        /// <summary>
        /// additional information of the core
        /// </summary>
        attribute,
        /// <summary>
        /// meta information of the core or link
        /// </summary>
        meta,
        /// <summary>
        /// information of two or more core component
        /// </summary>
        link,
        /// <summary>
        /// transaction information of the system
        /// </summary>
        transaction,
        /// <summary>
        /// configuration of the system
        /// </summary>
        configuration,
        /// <summary>
        /// system information or global settings.
        /// </summary>
        system,
        /// <summary>
        /// it is not table
        /// </summary>
        none
    }
    public class Component : IComponent
    {
        private string componentid;
        private List<Attribute> attribute;
        private string name;
        private string tablename;
        private List<Attribute> keys;
        private ComponentType type;
        private string title;
        private string category;
        private string entityKey;
        public string ID { get => componentid; set => componentid=value; }
        public List<Attribute> Attributes { get => attribute; set => attribute=value; }
        public string Name { get => name; set => name = value; }
        public string TableName { get => tablename; set => tablename = value; }
        public List<Attribute> Keys { get => keys; set => keys = value; }
        public ComponentType Type { get => type; set => type = value; }
        public string Category { get => category; set => category=value; }
        public string Title { get => title; set => title=value; }
        public string EntityKey { get => entityKey; set => entityKey=value; }

        public Component(string id) {
            ID = id;
            attribute = new List<Attribute>();
            name = "";
            tablename = "";
            Keys = new List<Attribute>();
            Type = ComponentType.core;
            category = "";
            title = "";
            entityKey = "";
        }
        public Component(string name,
            ComponentType type)
        {
            this.Name = name;
            this.Type = type;
            attribute = new List<Attribute>();
            Keys = new List<Attribute>();
            this.ID = "";
            category = "";
            title = "";
            entityKey = "";
        }
        public Component()
        {
            ID = "";
            attribute = new List<Attribute>();
            name = "";
            tablename = "";
            Keys = new List<Attribute>();
            Type = ComponentType.core;
            this.ID = "";
            category = "";
            title = "";
            entityKey = "";

        }
        public Attribute NewAttribute(int clientID ) {
            return new Attribute() { ClientID = clientID};
        }
        public void AddAttribute(IAttribute att) {
            this.Attributes.Add((Attribute) att);
        }
       
    }
}

