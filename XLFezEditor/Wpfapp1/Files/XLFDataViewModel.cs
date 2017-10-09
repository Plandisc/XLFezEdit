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
            get
            {
                Console.WriteLine("Got data");
                return _data;
            }
            set
            {
                _data = value;
                NotifyOfPropertyChange(() => Data);
            }
        }
        public XLFDataViewModel() { }

        private BindableCollection<TransUnit> _filteredData;
        public BindableCollection<TransUnit> FilteredData
        {
            get
            {
                if (_filteredData != null)
                {
                    Console.WriteLine("Got FILTERED data");
                    return _filteredData;
                }
                return Data;

            }
            set
            {
                _filteredData = value;
                NotifyOfPropertyChange(() => FilteredData);
            }
        }

        private string _filterText = "";
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                _filterText = value;
                NotifyOfPropertyChange(() => FilterText);
            }
        }

        public void bindList(List<TransUnit> Data)
        {
            BindableCollection<TransUnit> dataCollection = new BindableCollection<TransUnit>(Data);
            this.Data = dataCollection;
            FilteredData = this.Data;
            Console.WriteLine("data bound");
        }
        public void bindFilteredList()
        {
            if (Data != null)
            {
                BindableCollection<TransUnit> filteredData = new BindableCollection<TransUnit>();
                foreach (var unit in Data)
                {
                    if (unit.Source.ToLower().Contains(FilterText.ToLower()))
                    {
                        filteredData.Add(unit);
                    }
                }
                FilteredData = filteredData;

                Console.WriteLine("FILTERED data bound");
            }
        }

        public void clear()
        {

        }

    }
}
