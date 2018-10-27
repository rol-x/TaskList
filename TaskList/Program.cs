using TaskList.Classes;
using System;
using System.Collections.Generic;

namespace TaskList
{
    class Program
    {
        static void Main(string[] args)             //add searching for existing list .txt files and showing their names for user to choose
        {                                           //add modification options for entries
                                                    //add exceptions for files, lists range, file creation etc
            #region manualAdding
            //var myList = new EntryList();
            //myList.listName = "myList";
            //myList.Add(new Entry("this is my new entry!"));
            //myList.GetEntry(0).DueDate = new DateTime(2018, 9, 11);
            //myList.GetEntry(0).IsPriority = true;
            //myList.GetEntry(0).Content = "this is my new entry! (first)";

            //var entryFromClass = new Entry("this entry is firstly created in main, then edited and added to list");
            //entryFromClass.DueDate = new DateTime(2018, 9, 9);
            //entryFromClass.Content = "this entry is firstly created in main, then edited and added to list (first from non-priority)";

            //var smallEntryList = new List<Entry>();
            //smallEntryList.Add(new Entry("long-term task that is a priority"));
            //smallEntryList[0].DueDate = new DateTime(2018, 11, 11);
            //smallEntryList[0].IsPriority = true;

            //var notImportantEntry = new Entry("this entry is not very important... (last)");
            //var habitEntry = new Entry("this entry reminds me of a habit I want to cultivate (last from priorities)");
            //habitEntry.IsPriority = true;

            //smallEntryList.Add(notImportantEntry);
            //smallEntryList.Add(habitEntry);

            //myList.Add(entryFromClass);
            //myList.AddMany(smallEntryList);

            //myList.Sort();
            //Console.WriteLine(myList);

            //Console.ReadKey();
            #endregion

            var menu = new Menu();                  //put some of the spaghetti into professional bowls
            menu.ViewMenu();                        //is EntryList good?
            
        }
    }
}


///Made by Karol Latos, IX 2018
