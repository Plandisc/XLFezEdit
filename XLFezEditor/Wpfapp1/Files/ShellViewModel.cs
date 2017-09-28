using Caliburn.Micro;
using System;

namespace XLFezEditor
{
    public class ShellViewModel : PropertyChangedBase
    {
        public IEventAggregator events;
        private BrowseFileViewModel _buttonsVM;
        public BrowseFileViewModel ButtonsVM {
            get { return _buttonsVM; }
            set
            {
                _buttonsVM = value;
                NotifyOfPropertyChange(() => ButtonsVM);
            }
        }
        private TextViewModel _textVM;
        public TextViewModel TextVM { get { return _textVM; }
            set
            {
                _textVM = value;
                NotifyOfPropertyChange(() => TextVM);
            }
        }

        public ShellViewModel()
        {
            events = new EventAggregator();

            ButtonsVM = new BrowseFileViewModel(events);
            TextVM = new TextViewModel(events);
        }
    }
}
