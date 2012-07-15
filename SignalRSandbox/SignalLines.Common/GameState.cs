using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SignalLines.Common.GamePieces;

namespace SignalLines.Common
{
    public class GameState
    {
        private ObservableCollection<Line> _occupiedLines = new ObservableCollection<Line>();
        public ObservableCollection<Line> OccupiedLines
        {
            get { return _occupiedLines; }
            set { _occupiedLines = value; }
        }

        public Tuple<int,int> Size { get; set; }

        private IList<Player> _players = new List<Player>();
        public IList<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }
    }
}