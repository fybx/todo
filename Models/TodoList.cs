using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

namespace todo.Models
{
    public class TodoList
    {
        public List<TodoItem> Items { get; }
        
        public int Count => Items.Count;

        public TodoItem this[int index] => Items[index];

        public TodoList() => Items = new List<TodoItem>();

        public void AddItem(string description) => Items.Add(new TodoItem {Completed = false, Creation = DateTime.UtcNow, Description = description, Index = Count});

        public void EditItem(int index, Operation operation, object parameter)
        {
            if (index < 0 || index >= Items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (parameter is null)
                throw new ArgumentNullException(nameof(parameter));
                
            if (operation == Operation.Remove)
                Items.RemoveAt(index);
            else if (operation == Operation.ChangeDescription)
                Items.ElementAt(index).Description = (string) parameter;
            else if (operation == Operation.ChangeDone) Items.ElementAt(index).Completed = (bool) parameter;
        }
    }
}