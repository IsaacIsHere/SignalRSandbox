using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SignalLines.Common;
using SignalLines.Common.GamePieces;

namespace SignalLines
{
    public partial class MainWindow
    {
        private readonly ConnectionManager _connectionManager;
        private GameModel _model;
        private Grid _gameGrid;
        private IList<Button> _lineButtons;

        public MainWindow()
        {
            InitializeComponent();

            _connectionManager = new ConnectionManager(new WpfDispatcher());
            _connectionManager.MessageReceived += ConnectionManagerOnMessageReceived;
            _connectionManager.LineClicked += ConnectionManagerOnLineClicked;

            CreateWorld();
        }

        private void ConnectionManagerOnLineClicked(object sender, LineClickedEventArgs e)
        {
            var piece = _model.GetElementAt(e.Row, e.Column) as Line;
            if (piece != null)
            {
                piece.Occupy(e.PlayerId);
                var button = FindButton(e.Row, e.Column);
                SetPieceColor(button);
            }
        }

        private void SetPieceColor(Button button)
        {
            var line = button.Tag as Line;
            if (line != null)
            {
                switch (line.PlayerId)
                {
                    case 0:
                        button.Background = Brushes.Transparent;
                        break;
                    default:
                        button.Background = Brushes.MediumBlue;
                        break;
                }
            }
        }

        private Button FindButton(int row, int column)
        {
            return _lineButtons
                .Select(x => new { Button = x, Line = x.Tag as Line })
                .FirstOrDefault(x => x.Line.Row == row && x.Line.Column == column)
                .Button;
        }

        private void ConnectionManagerOnMessageReceived(object sender, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            //MessagesReceived.Text += messageReceivedEventArgs.Message;
        }

        private void CreateWorld()
        {
            _model = _connectionManager.JoinGame();

            _gameGrid = new Grid();
            _lineButtons = new List<Button>();

            SetRowDefinitions();
            SetColumnsDefinitions();

            var lst = _model.GetAllElements().ToList();

            foreach (var piece in lst)
            {
                var element = GetElementForPiece(piece);
                element.SetValue(Grid.RowProperty, piece.Row);
                element.SetValue(Grid.ColumnProperty, piece.Column);
                element.Tag = piece;
                _gameGrid.Children.Add(element);

                if (element is Button)
                {
                    _lineButtons.Add(element as Button);
                }
            }

            Board.Children.Add(_gameGrid);

            foreach (var button in _lineButtons)
                SetPieceColor(button);

            foreach (var tuple in _model.LinesOccupied)
            {
                var line = _model.GetElementAt(tuple.Item1, tuple.Item2) as Line;
                if (line != null) line.Occupy(1);
                var button = FindButton(tuple.Item1, tuple.Item2);
                SetPieceColor(button);
            }
        }

        private FrameworkElement GetElementForPiece(GamePiece piece)
        {
            if (piece is Dot)
            {
                var item = new Grid { Style = (Style)Application.Current.FindResource("DotStyle")};
                return item;
            }

            if (piece is Line)
            {
                var button = new Button
                                 {
                                     Style = (Style)Application.Current.FindResource("LineStyle"),
                                     DataContext = piece
                                 };
                // Bind tag's PlayerId

                button.Click += LineClicked;
                return button;
            }

            var square = new Grid { Style = (Style)Application.Current.FindResource("SquareStyle") };
            return square;
        }

        private void LineClicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var piece = button.Tag as Line;
                if (piece != null) _connectionManager.ClickLine(piece.Row, piece.Column);
            }
        }

        private void SetColumnsDefinitions()
        {
            for (var i = 0; i < _model.NumColumns; i++)
            {
                if (i.IsEven())
                {
                    _gameGrid.ColumnDefinitions.Add(new ColumnDefinition
                                                        {
                                                            Width = new GridLength(1, GridUnitType.Star)
                                                        });
                }
                else
                {
                    _gameGrid.ColumnDefinitions.Add(new ColumnDefinition
                                                        {
                                                            Width = new GridLength(8, GridUnitType.Star)
                                                        });
                }
            }
        }

        private void SetRowDefinitions()
        {
            for (var i = 0; i < _model.NumRows; i++)
            {
                if (i.IsEven())
                {
                    _gameGrid.RowDefinitions.Add(new RowDefinition
                                                    {
                                                        Height = new GridLength(1, GridUnitType.Star)
                                                    });
                }
                else
                {
                    _gameGrid.RowDefinitions.Add(new RowDefinition
                                                    {
                                                        Height = new GridLength(8, GridUnitType.Star)
                                                    });
                }
            }
        }
    }


}
