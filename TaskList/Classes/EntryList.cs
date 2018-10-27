using System;
using System.Collections.Generic;

namespace TaskList.Classes
{
    class EntryList
    {
        private List<Entry> _entryList;
        public string listName/* = new String("")*/;

        public EntryList()
        {
            _entryList = new List<Entry>();
        }

        public void Add(Entry newEntry)
        {
            _entryList.Add(newEntry);
        }

        public void Remove(int index)
        {
            _entryList.Remove(_entryList[index]);
        }

        public int Count()
        {
            return _entryList.Count;
        }

        public void EditContent(int index, string newContent)
        {
            _entryList[index].Content = newContent;
        }

        public void EditDueDate(int index, DateTime newDueDate)
        {
            _entryList[index].DueDate = newDueDate;
        }

        public void Sort()
        {
            var sorter = new ListSorter(_entryList);
            _entryList = sorter.Sort();
        }

        public void AddMany(IEnumerable<Entry> entries)
        {
            _entryList.AddRange(entries);
        }

        public Entry GetEntry(int index)
        {
            return _entryList[index];
        }

        public bool Same(EntryList entryList)
        {
            if (Count() != entryList.Count())
                return false;
            for (int i = 0; i < Count(); i++)
            {
                if (!entryList.GetEntry(i).Same(GetEntry(i)))
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            string output = string.Format("List '{0}'\n", listName);
            if (_entryList.Count == 0)
            {
                output += "Your list is empty!";
            }
            else
            {
                int index = 1;
                foreach (var entry in _entryList)
                {
                    output += index + ".\t" + entry.ToString() + "\n\n";
                    index++;
                }
            }
            return output;
        }
    }
}
