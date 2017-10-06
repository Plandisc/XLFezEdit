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

        private BindableCollection<TransUnit> OriginData;
        private bool OriginStored = false;

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

        public BindableCollection<TransUnit> VisibleData()
        {
            if (!String.IsNullOrWhiteSpace(FilterText))
            {
                FilteredData = null;
                foreach (var unit in Data)
                {
                    if(unit.Source.Contains(FilterText))
                    {
                        FilteredData.Add(unit);
                    }
                }
                return FilteredData;
            }
            else
            {
                return Data;
            }
        }

        private BindableCollection<TransUnit> _filteredData;
        public BindableCollection<TransUnit> FilteredData
        {
            get { return _filteredData; }
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

        private void bindFilteredList(string text)
        {

            if(!String.IsNullOrWhiteSpace(text))
            {
                foreach (var unit in Data)
                {

                }
            }
        }

        public void bindList(List<TransUnit> Data)
        {
            BindableCollection<TransUnit> dataCollection = new BindableCollection<TransUnit>(Data);
            this.Data = dataCollection;
            Console.WriteLine("data bound");
        }

    }
}
