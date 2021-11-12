using System;
using System.Xml.Serialization;

namespace todo.Models
{
    public class TodoItem
    {
        [XmlElement(Order = 1, ElementName = "Index")]
        public int Index;
        
        [XmlElement(Order = 2, ElementName = "Creation")]
        public DateTime Creation;
        
        [XmlElement(Order = 3, ElementName = "Completed")]
        public bool Completed;
        
        [XmlElement(Order = 4, ElementName = "Description")]
        public string Description;
    }
}