using System;
using System.IO;
using System.Xml.Serialization;
using todo.Models;

namespace todo
{
    internal static class Program
    {
        private static bool _isFirstRun;
        private static TodoList _todoList;
        
        private static void Main(string[] args)
        {
            string todoFile = Path.Combine(AppContext.BaseDirectory, "todo.file");
            _isFirstRun = !File.Exists(todoFile);

            if (_isFirstRun)
                CreateTodo(todoFile);
            else
                ReadFile(todoFile);
            
            ListItems();

            Console.WriteLine("Completed!");
        }

        private static void CreateTodo(string location)
        {
            _todoList = new TodoList();
            _todoList.AddItem("This is the initial item");
            _todoList.AddItem("This sentence is false");
            _todoList.EditItem(1, Operation.ChangeDone, true);
            SaveFile(location);
        }

        private static void ListItems()
        {
            for (int i = 0; i < _todoList.Count; i++)
                Console.WriteLine(_todoList[i].Completed ? "[x] " : "[ ] " + _todoList[i].Description);
        }

        private static void ReadFile(string location)
        {
            XmlSerializer deserializer = new(typeof(TodoList));
            using FileStream fs = File.Open(location, FileMode.Open);
            _todoList = (TodoList) deserializer.Deserialize(fs);
        }

        private static void SaveFile(string location)
        {
            XmlSerializer serializer = new(typeof(TodoList));
            using FileStream fs = File.Open(location, FileMode.OpenOrCreate);
            serializer.Serialize(fs, _todoList);
        }
    }
}
