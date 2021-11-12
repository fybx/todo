using System;

namespace todo.Models
{
    public class TodoItem
    {
        public int Index;
        public DateTime Creation;
        public bool Completed;
        public string Description;

        public TodoItem(string description, int index)
        {
            Index = index;
            Creation = DateTime.UtcNow;
            Completed = false;
            Description = description;
        }
    }
}