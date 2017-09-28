using Caliburn.Micro;
using System;
using System.Collections.Generic;


namespace XLFezEditor.Files
{
    public class XLFDataViewModel : PropertyChangedBase
    {

        private IObservableCollection<string> _data;
        public IObservableCollection<string> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                NotifyOfPropertyChange(() => Data);
            }

            // linq XLF conversion reading and parsing to a list
        }
        public XLFDataViewModel()
        {
            List<string> strs = new List<string>();
            strs.Add("what");
            strs.Add("the");
            strs.Add("..");
        }
    }
}
