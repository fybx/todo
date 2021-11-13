using System;
using System.Xml.Serialization;

namespace todo.Models
{
    public class TodoItem
    {
        [XmlElement(Order = 1, ElementName = "Creation")]
        public DateTime Creation;
        
        [XmlElement(Order = 2, ElementName = "Completed")]
        public bool Completed;
        
        [XmlElement(Order = 3, ElementName = "Description")]
        public string Description;
    }
}