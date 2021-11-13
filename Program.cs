using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
using todo.Models;

namespace todo
{
    internal static class Program
    {
        private static bool _isFirstRun;
        private static TodoList _todoList;
        private static string _todoFile;
        
        private static void Main(string[] args)
        {
            _todoFile = Path.Combine(AppContext.BaseDirectory, "todo.file");
            _isFirstRun = !File.Exists(_todoFile);
            if (_isFirstRun)
                CreateTodo();
            else
                ReadFile();

            if (args.Length is 0) 
                ListItems();
            else if (args[0] is "add")
                TryAdd(args);
            else if (args[0] is "rm" && args.Length is 2)
                TryRem(args);
            
            Console.WriteLine("Completed!");
        }

        private static void CreateTodo()
        {
            _todoList = new TodoList();
            _todoList.AddItem("This is the initial item");
            _todoList.AddItem("This sentence is false");
            _todoList.EditItem(1, Operation.ChangeDone, true);
            SaveFile();
        }

        private static void ListItems()
        {
            for (int i = 0; i < _todoList.Count; i++)
                Console.WriteLine((_todoList[i].Completed ? "[x] " : "[ ] ") + _todoList[i].Description);
        }

        private static void TryAdd(string[] args)
        {
            if (args.Length is 1)
                Console.WriteLine(@"subcommand 'add' needs 'description' to add todo item!");
            else
            {
                StringBuilder sb = new();
                for (int i = 1; i < args.Length; i++) sb.AppendFormat(" {0} ", args[i]);
                _todoList.AddItem(sb.ToString().Trim());
                SaveFile();   
            }
        }

        private static void TryRem(string[] args)
        {
            throw new NotImplementedException();
        }

        private static void ReadFile()
        {
            XmlSerializer deserializer = new(typeof(TodoList));
            using FileStream fs = File.Open(_todoFile, FileMode.Open);
            _todoList = (TodoList) deserializer.Deserialize(fs);
        }

        private static void SaveFile()
        {
            XmlSerializer serializer = new(typeof(TodoList));
            using FileStream fs = File.Open(_todoFile, FileMode.OpenOrCreate);
            serializer.Serialize(fs, _todoList);
        }
    }
}
