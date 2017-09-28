using Caliburn.Micro;
using System;
using System.Windows;
using XLFezEditor.Files;

namespace XLFezEditor
{
    public class ShellViewModel : PropertyChangedBase
    {
        public IEventAggregator events;
        private XLFDataViewModel _xlfDataVM;
        public XLFDataViewModel XLFDataVM
        {
            get { return _xlfDataVM; }
            set
            {
                _xlfDataVM = value;
                NotifyOfPropertyChange(() => XLFDataVM);
            }
        }

        public ShellViewModel()
        {
            events = new EventAggregator();

            XLFDataVM = new XLFDataViewModel();
        }
        
    }
}
