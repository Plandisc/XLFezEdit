﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

            // linq XLF conversion reading and parsing to a list
        }
        public XLFDataViewModel() {}

        public void bindList(List<TransUnit> Data)
        {
            BindableCollection<TransUnit> dataCollection = new BindableCollection<TransUnit>(Data);
            this.Data = dataCollection;
            Console.WriteLine("data bound");
        }
    }
}
