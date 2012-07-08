using System.ComponentModel;

namespace SignalLines.Common.GamePieces
{
    public class Line : GamePiece, INotifyPropertyChanged
    {
        public Line(int row, int column) : base(row, column)
        {
        }

        private int _playerId;
        public int PlayerId
        {
            get { return _playerId; }
            set
            {
                _playerId = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PlayerId"));
                }
            }
        }

        public bool Occupy(int playerId)
        {
            if (PlayerId == 0)
            {
                PlayerId = playerId;
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}