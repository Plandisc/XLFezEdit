using Caliburn.Micro;
using System;

namespace XLFezEditor
{
    public class BrowseFileViewModel : PropertyChangedBase
    {
        private IEventAggregator _events;
        private string _filePath;
        public string FilePath {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                NotifyOfPropertyChange(() => FilePath);
            }
        }

        public BrowseFileViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public void BrowseForFile()
        {
        }

        public void BrowseForMasterFile()
        {
        }

        public void Button3()
        {
            //_events.PublishOnUIThread(new ChangeTextMessage("Button 3 Pressed"));
        }
    }
}
