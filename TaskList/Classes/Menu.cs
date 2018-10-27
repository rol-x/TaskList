using System;
using System.Collections.Generic;
using System.IO;

namespace TaskList.Classes
{
    class Menu                          //create ShowOptionsMenu? too much bool-loop-do-swtich-while constructs
    {
        private ConsoleKeyInfo _userKey;
        private EntryList _currentList;

        public void ViewMenu()
        {
            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Hello! Welcome to TaskList task manager!\nWhat do you want to do?\n\n\t[1] Create a new list\n\t[2] Open a list\n\t[0] Exit the program\n");
                _userKey = Console.ReadKey();
                Console.Clear();
                switch (_userKey.KeyChar)
                {
                    case '1':
                        CreateList();
                        break;
                    case '2':
                        var listName = ShowAvailableLists();
                        if (!String.IsNullOrEmpty(listName))
                        {
                            _currentList = LoadFromFile(listName);
                            ViewList();
                        }
                        break;
                    case '0':
                        loop = false;
                        break;
                    default:
                        break;
                }
            } while (loop);
            Environment.Exit(0);
        }

        private string ShowAvailableLists()                 
        {
            bool loop = true;
            char fileNumber;
            string listName = "";
            string pathString = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Lists\"));
            string[] listNames = Directory.GetFiles(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Lists\")), "*.txt");
            for (int i = 0; i < listNames.Length; i++)
            {
                listNames[i] = listNames[i].Substring(pathString.Length);
                listNames[i] = listNames[i].Substring(0, listNames[i].Length - 4);
            }
            do
            {
                Console.Clear();
                for (int i = 0; i < listNames.Length; i++)
                    Console.WriteLine("[{0}] {1}", i + 1, listNames[i]);
                Console.WriteLine("[0] Go back");
                fileNumber = Console.ReadKey().KeyChar;
                if (fileNumber == '0')
                    loop = false;
                else if (fileNumber-48 > listNames.Length || fileNumber-48 < 0)
                {
                    Console.WriteLine("No list numbered '{0}'", fileNumber);
                    Console.ReadKey();
                }
                else
                {
                    loop = false;
                    listName = listNames[fileNumber - 48 - 1];
                }
            } while (loop);
            return listName;
        }

        public void CreateList()
        {
            _currentList = new EntryList();
            Console.WriteLine("Name your list: ");
            _currentList.listName = Console.ReadLine();
            ViewList();
        }

        public void ViewList()
        {
            bool loop = true;
            do
            {
                Console.Clear();
                _currentList.Sort();
                Console.WriteLine(_currentList);
                Console.WriteLine("\n[1] Modify\t[2] Delete\t[3] Save\t[0] Go back\n");
                _userKey = Console.ReadKey();
                switch (_userKey.KeyChar)
                {
                    case '1':
                        EditList();
                        break;
                    case '2':
                        if (GetDecision(string.Format("Are you sure you want to delete list '{0}'?", _currentList.listName)))
                        {
                            var path = Directory.GetCurrentDirectory();
                            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\Lists\"));
                            path += _currentList.listName + ".txt";
                            File.Delete(path);

                            Console.Clear();
                            Console.WriteLine("List '{0}' successfully deleted!", _currentList.listName);
                            Console.WriteLine("(Press any key to continue)");
                            Console.ReadKey();
                            Console.Clear();

                            _currentList = new EntryList();
                            loop = false;
                        }
                        break;
                    case '3':
                        SaveToFile();
                        break;
                    case '0':
                        if (!_currentList.Same(LoadFromFile(_currentList.listName)))            //todo (not working)
                            if (GetDecision(string.Format("Do you want to save '{0}'?", _currentList.listName)))
                                SaveToFile();
                        Console.Clear();
                        loop = false;
                        break;
                }
            } while (loop);
        }

        private void EditList()         //todo
        {
            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine(_currentList);
                Console.WriteLine("\n[1] Add a new entry\t[2] Modify existing entry\t[3] Delete an entry\t[0] Go back");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Type your entry here: ");
                        var newEntry = new Entry(Console.ReadLine());
                        if (GetDecision("Is is a priority?"))
                            newEntry.IsPriority = true;
                        Console.Clear();
                        Console.WriteLine("Provide a due date [YYYY MM DD] (otherwise type nothing)");
                        var date = Console.ReadLine().Split(" ");
                        if (date.GetLength(0) == 3)
                        {
                            newEntry.DueDate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                        }
                        _currentList.Add(newEntry);
                        Console.Clear();
                        Console.WriteLine("Entry added!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine(_currentList + "\nWhich entry to modify?");
                        ModifyEntry(_currentList.GetEntry(Console.ReadKey().KeyChar - 49));
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine(_currentList + "\nWhich entry to delete?");
                        _currentList.Remove(Console.ReadKey().KeyChar - 49);
                        break;
                    case '0':
                        loop = false;
                        Console.Clear();
                        break;
                }
            } while (loop);
        }

        private void ModifyEntry(Entry entry)
        {
            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine(entry + "\n\n[1] Change priority\t[2] Edit content\t[3] Set a due date\t[0] Go back");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        entry.IsPriority = GetDecision(String.Format("Current priority state: " + entry.IsPriority + "\nIs it a priority?"));
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("Current entry content: " + entry.Content + "\nType new content: ");
                        entry.Content = Console.ReadLine();
                        break;
                    case '3':
                        Console.Clear();
                        Console.Write("Current due date: ");
                        if (entry.DueDate.Date == DateTime.MaxValue.Date)
                            Console.Write("not set\n");
                        else
                            Console.Write(entry.DueDate.Date);
                        Console.WriteLine("Set new due date (YYYY MM DD): ");
                        var date =  Console.ReadLine().Split(" ");
                        if (date.GetLength(0) < 3)
                            break;
                        entry.DueDate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                        break;
                    case '0':
                        Console.Clear();
                        loop = false;
                        break;
                }
            } while (loop);
        }

        private bool GetDecision(string message)
        {
            var tempKey = new ConsoleKeyInfo();
            do
            {
                Console.Clear();
                Console.WriteLine(message + " (Y/N)");
                tempKey = Console.ReadKey();
            } while ((tempKey.KeyChar != 'y' && tempKey.KeyChar != 'Y') && (tempKey.KeyChar != 'n' && tempKey.KeyChar != 'N'));
            if (tempKey.KeyChar == 'y' || tempKey.KeyChar == 'Y')
                return true;
            else
                return false;
        }

        private void SaveToFile()
        {
            var listSaver = new TaskListSaver(_currentList);
            listSaver.SaveToFile();
        }

        private EntryList LoadFromFile(string listName)
        {
            var listLoader = new TaskListLoader(listName);
            return listLoader.LoadFromFile();
        }
    }
}
