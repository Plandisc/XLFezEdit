using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace XLFezEditor.Files
{
    public class XLFDataViewModel : PropertyChangedBase
    {

        private BindableCollection<TransUnit> _data;
        public BindableCollection<TransUnit> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                NotifyOfPropertyChange(() => Data);
            }
        }
        public XLFDataViewModel() { }

        public void bindList(List<TransUnit> Data)
        {
            BindableCollection<TransUnit> dataCollection = new BindableCollection<TransUnit>(Data);
            this.Data = dataCollection;
            Console.WriteLine("data bound");
        }

    }
}
