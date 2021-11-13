using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace todo.Models
{
    public class TodoList
    {
        [XmlElement(Order = 1, ElementName = "Items")]
        public List<TodoItem> Items { get; }
        
        public int Count => Items.Count;

        public TodoItem this[int index] => Items[index];

        public TodoList() => Items = new List<TodoItem>();

        public void AddItem(string description) => Items.Add(new TodoItem {Completed = false, Creation = DateTime.UtcNow, Description = description});

        public void EditItem(int index, Operation operation, object parameter = null)
        {
            if (index < 0 || index >= Items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (operation is not Operation.Remove && parameter is null)
                throw new ArgumentNullException(nameof(parameter));
                
            if (operation == Operation.Remove)
                Items.RemoveAt(index);
            else if (operation == Operation.ChangeDescription)
                Items.ElementAt(index).Description = (string) parameter;
            else if (operation == Operation.ChangeDone) Items.ElementAt(index).Completed = (bool) parameter;
        }
    }
}