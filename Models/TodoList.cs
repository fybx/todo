using System.Collections.Generic;

namespace todo.Models
{
    public class TodoList
    {
        public List<TodoItem> Items { get; }

        public TodoList() => Items = new List<TodoItem>();
    }
}