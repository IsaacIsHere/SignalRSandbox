using System.ComponentModel;

namespace SignalLines.Common
{
    public class Player : INotifyPropertyChanged
    {
        public int PlayerId { get; set; }
        public string ConnectionId { get; set; }
        private int _score;
        public int Score
        {
            get { return _score; }
            set { _score = value;
            RaisePropertyChanged("Score");}
        }

        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}