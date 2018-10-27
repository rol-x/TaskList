using System;

namespace TaskList.Classes
{
    class Entry                                     
    {

        public Entry(string content)
        {
            Content = content;
            IsPriority = false;
            AddDate = new DateTime();
            AddDate = DateTime.Today;
            DueDate = DateTime.MaxValue;
        }

        public string Content { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsPriority { get; set; }

        public bool Same(Entry entry)
        {
            if (entry.AddDate == AddDate &&
               entry.Content == Content &&
               entry.DueDate == DueDate &&
               entry.IsPriority == IsPriority)
                return true;
            else
                return false;
        }

        public Entry Copy()
        {
            var newEntry = new Entry(Content);
            newEntry.AddDate = AddDate;
            newEntry.DueDate = DueDate;
            newEntry.IsPriority = IsPriority;
            return newEntry;
        }

        public override string ToString()
        {
            string output = "";
            if (IsPriority)
                output += "[PRIORITY]";
            output += "\n" + Content + "\n(Added on: " + AddDate.ToLongDateString();

            if (!DateTime.Equals(DateTime.MaxValue.Date, DueDate.Date))
                output += "; Due date: " + DueDate.ToLongDateString();
            output += ")";
            return output;
        }
    }
}
