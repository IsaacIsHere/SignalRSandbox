using System;

namespace SignalLines.Common.GamePieces
{
    public class Square : GamePiece
    {
        private Line _left;
        private Line _top;
        private Line _right;
        private Line _bottom;

        public delegate void SquareCompleteHandler(object sender, SquareCompletedEventArgs args);
        public event SquareCompleteHandler Completed;

        public Square(int row, int column)
            : base(row, column)
        {
        }

        private void OnCompleted(SquareCompletedEventArgs eventArgs)
        {
            if (Completed != null)
                Completed.Invoke(this, eventArgs);
        }

        public void SetLine(Line line, string position)
        {
            switch (position)
            {
                case "Left":
                    _left = line;
                    _left.Occupied += LineOccupied;
                    break;
                case "Right":
                    _right = line;
                    _right.Occupied += LineOccupied;
                    break;
                case "Top":
                    _top = line;
                    _top.Occupied += LineOccupied;
                    break;
                case "Bottom":
                    _bottom = line;
                    _bottom.Occupied += LineOccupied;
                    break;
            }
        }

        private void LineOccupied(object sender, EventArgs eventArgs)
        {
            if (_left.PlayerId != 0 && _right.PlayerId != 0 &&
                _top.PlayerId != 0 && _bottom.PlayerId != 0)
            {
                var line = sender as Line;
                if (line != null) OnCompleted(new SquareCompletedEventArgs{PlayerId = line.PlayerId});
            }
        }
    }

    public class SquareCompletedEventArgs   : EventArgs
    {
        public int PlayerId { get; set; }
    }
}