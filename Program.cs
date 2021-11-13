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
            else if (args[0] is "rm")
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
            // i didn't use switch case here because if-else takes less lines and is more readable
            if (args.Length is 1)
                Console.WriteLine(@"todo: subcommand 'rm' needs 'index' to remove item!");
            else if (args.Length > 2)
                Console.WriteLine(@"todo: subcommand 'rm' doesn't take more than 1 arguments!");
            else if (int.TryParse(args[1], out int index))
            {
                if (index < 0 || index >= _todoList.Count)
                    Console.WriteLine($"todo: index was out of range, none removed. your todo list currently holds {_todoList.Count} items.");
                else
                {
                    _todoList.EditItem(index, Operation.Remove);
                    SaveFile();
                }
            }
            else
                Console.WriteLine($"todo: subcommand 'rm' doesn't expect '{args[1]}' for argument 'index'!");
        }

        private static void ReadFile()
        {
            XmlSerializer deserializer = new(typeof(TodoList));
            using FileStream fs = File.Open(_todoFile, FileMode.Open, FileAccess.Read);
            _todoList = (TodoList) deserializer.Deserialize(fs);
        }

        private static void SaveFile()
        {
            if (File.Exists(_todoFile))
                File.Delete(_todoFile);
            XmlSerializer serializer = new(typeof(TodoList));
            using FileStream fs = File.Open(_todoFile, FileMode.CreateNew, FileAccess.Write);
            serializer.Serialize(fs, _todoList);
        }
    }
}
