using System;
using System.Collections.Generic;

namespace SignalLines.Common
{
    public class GameState
    {
        private List<Tuple<int, int>> _occupiedLines = new List<Tuple<int, int>>();
        public List<Tuple<int, int>> OccupiedLines
        {
            get { return _occupiedLines; }
            set { _occupiedLines = value; }
        }

        public Tuple<int,int> Size { get; set; }
    }
}