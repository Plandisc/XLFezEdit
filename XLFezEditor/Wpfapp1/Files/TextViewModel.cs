using Caliburn.Micro;


namespace XLFezEditor
{
    public class TextViewModel : PropertyChangedBase
    {
        private string _text;

        private IEventAggregator _events;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public TextViewModel(IEventAggregator events)
        {
            _events = events;

            _events.Subscribe(this);

            Text = "No button pressed";
        }


    }
}
