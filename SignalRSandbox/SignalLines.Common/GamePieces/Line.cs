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
                return true;
            }
            return false;
        }
    }
}