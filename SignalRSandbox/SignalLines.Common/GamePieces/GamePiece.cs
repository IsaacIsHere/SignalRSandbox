namespace SignalLines.Common.GamePieces
{
    public class GamePiece
    {
        public GamePiece(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}