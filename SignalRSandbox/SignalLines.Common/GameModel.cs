using System.Collections.Generic;
using SignalLines.Common.GamePieces;

namespace SignalLines.Common
{
    public class GameModel
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int NumRows { get; private set; }
        public int NumColumns { get; private set; }
        private readonly GamePiece[,] _grid;

        public GameModel(int height, int width)
        {
            Height = height;
            Width = width;
            NumRows = height * 2 - 1;
            NumColumns = width * 2 - 1;
            _grid = new GamePiece[NumRows, NumColumns];

            Initialise();
        }

        private void Initialise()
        {
            for (var i = 0; i <= _grid.GetUpperBound(0); i++)
            {
                for (var j = 0; j <= _grid.GetUpperBound(1); j++)
                {
                    _grid[i, j] = GetGamePiece(i, j);
                }
            }
        }

        private GamePiece GetGamePiece(int row, int column)
        {
            if (row.IsEven() && column.IsEven())
                return new Dot(row, column);
            if (row.IsOdd() && column.IsOdd())
                return new Square(row, column);
            return new Line(row, column);
        }

        public IEnumerable<GamePiece> GetAllElements()
        {
            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (var piece in _grid)
            // ReSharper restore LoopCanBeConvertedToQuery
            {
                if (piece != null)
                    yield return piece;
            }
        }

        public GamePiece GetElementAt(int row, int column)
        {
            return _grid[row, column];
        }
    }
}