using System;
using System.Diagnostics;
using System.IO;

namespace TaskList.Classes
{
    class TaskListLoader
    {
        private readonly StreamReader _taskListFile;
        private string _listName;

        public TaskListLoader(string listName)
        {
            _listName = listName;
            var path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\Lists\"));
            path += _listName + ".txt";
            if (!File.Exists(path))
            {
                try
                {
                    File.Create(path).Dispose();
                }
                catch (AccessViolationException ave)
                {
                    Debug.Write(ave);
                }
            }               //throw vs Debug.Write() vs Console.Write() vs ???
            try
            {
                _taskListFile = new StreamReader(path);
            }
            catch (FileLoadException fle)
            {

                Debug.Write(fle);
            }
        }

        public EntryList LoadFromFile()
        {
            EntryList loadedList = new EntryList();
            Entry loadedEntry = new Entry("");
            short lineCodeCounter = 0;

            if (_taskListFile.Peek() != -1)
            {
                using (_taskListFile)
                {
                    while (!_taskListFile.EndOfStream)
                    {
                        var line = _taskListFile.ReadLine();
                        switch (lineCodeCounter)
                        {
                            case 0:                         //first line contains P or nothing, declaring whether entry is a priority
                                if(line.StartsWith("P") && line.Length == 1)
                                    loadedEntry.IsPriority = true;
                                break;
                            case 1:                         //next line contains entry content
                                loadedEntry.Content = line;
                                break;
                            case 2:                         //next one contains add date
                                var dateCode = line.Split(" ");
                                loadedEntry.AddDate = new DateTime(int.Parse(dateCode[0]), int.Parse(dateCode[1]), int.Parse(dateCode[2]));
                                break;
                            case 3:                         //and last one contains (or does not contain) a due date
                                if (line.Length > 1)
                                {
                                    dateCode = line.Split(" ");
                                    loadedEntry.DueDate = new DateTime(int.Parse(dateCode[0]), int.Parse(dateCode[1]), int.Parse(dateCode[2]));
                                }
                                break;
                        }
                        lineCodeCounter = (short)((lineCodeCounter + 1) % 4);
                        if (lineCodeCounter == 0)
                        {
                            var completeEntry = loadedEntry.Copy();
                            loadedList.Add(completeEntry);
                            loadedEntry.IsPriority = false;
                            loadedEntry.DueDate = DateTime.MaxValue;
                        }
                    }
                }
            }
            loadedList.listName = _listName;
            loadedList.Sort();
            _taskListFile.Dispose();
            return loadedList;
        }
    }
}
