using System;
using System.IO;

namespace TaskList.Classes
{
    class TaskListSaver
    {
        private EntryList _entryList;

        public TaskListSaver(EntryList entryList)
        {
            _entryList = entryList;
        }

        public void SaveToFile()
        {
            short lineCodeCounter = 0;
            var path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\Lists\"));
            path += _entryList.listName + ".txt";
            using (var writer = new StreamWriter(path))
            {
                for (int i = 0; i < _entryList.Count();)
                {
                    switch (lineCodeCounter)
                    {
                        case 0:
                            if (_entryList.GetEntry(i).IsPriority)
                                writer.WriteLine("P");
                            else
                                writer.WriteLine();
                            break;
                        case 1:
                            writer.WriteLine(_entryList.GetEntry(i).Content);
                            break;
                        case 2:
                            var date = _entryList.GetEntry(i).AddDate.Year.ToString() + " " + _entryList.GetEntry(i).AddDate.Month.ToString() + " " + _entryList.GetEntry(i).AddDate.Day.ToString();
                            writer.WriteLine(date);
                            break;
                        case 3:
                            date = _entryList.GetEntry(i).DueDate.Year.ToString() + " " + _entryList.GetEntry(i).DueDate.Month.ToString() + " " + _entryList.GetEntry(i).DueDate.Day.ToString();
                            writer.WriteLine(date);
                            break;
                    }
                    lineCodeCounter = (short)((lineCodeCounter + 1) % 4);
                    if (lineCodeCounter == 0)
                        i++;
                }
            }
            Console.Clear();
            Console.WriteLine("Successfully saved '{0}' to {1} ", _entryList.listName, path);
            Console.WriteLine("(Press any key to continue)");
            Console.ReadKey();
        }
    }
}
