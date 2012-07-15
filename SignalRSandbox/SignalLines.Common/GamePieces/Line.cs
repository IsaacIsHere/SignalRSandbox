using System;
using System.ComponentModel;

namespace SignalLines.Common.GamePieces
{
    public class Line : GamePiece
    {
        public Line(int row, int column) : base(row, column)
        {
        }

        public int PlayerId { get; set; }

        public bool Occupy(int playerId)
        {
            if (PlayerId == 0)
            {
                PlayerId = playerId;
                OnChanged(new EventArgs());
                return true;
            }
            return false;
        }

        public void OnChanged(EventArgs e)
        {
            if (Occupied != null)
                Occupied.Invoke(this, e);
        }

        public event EventHandler Occupied;
    }
}