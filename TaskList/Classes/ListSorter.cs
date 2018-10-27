using System;
using System.Collections.Generic;

namespace TaskList.Classes
{
    class ListSorter            //not working in 100%
    {
        public List<Entry> _entryList;

        public ListSorter(List<Entry> entryList)
        {
            _entryList = entryList;
        }

        private void MoveList(int upperBound, int lowerBound)
        {
            var lowerBoundValue = _entryList[lowerBound];
            for (int i = lowerBound; i > upperBound; i--)
            {
                    _entryList[i] = _entryList[i - 1];
            }
            _entryList[upperBound] = lowerBoundValue;
        }

        public List<Entry> Sort()
        {
            for (int i = 0; i < _entryList.Count - 1; i++)
            {                   
                for (int j = i + 1; j < _entryList.Count; j++)
                {
                    if (DateTime.Compare(_entryList[i].DueDate, _entryList[j].DueDate) > 0)
                    {
                        var temp = _entryList[i];
                        _entryList[i] = _entryList[j];
                        _entryList[j] = temp;
                    }
                }
            }
            for (int i = 0; i < _entryList.Count - 1; i++)
            {
                for (int j = i + 1; j < _entryList.Count; j++)
                {
                    if(!_entryList[i].IsPriority && _entryList[j].IsPriority)
                    {
                        MoveList(i, j);
                        break;
                    }
                }
            }

                    return _entryList;
        }
    }
}
