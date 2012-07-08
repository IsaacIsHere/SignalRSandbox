using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SignalLines.Common;
using SignalLines.Common.GamePieces;

namespace SignalLines
{

    public partial class MainWindow
    {
        private readonly ConnectionManager _connectionManager;
        private GameModel _model;
        private Grid _gameGrid;

        public MainWindow()
        {
            InitializeComponent();

            _connectionManager = new ConnectionManager(new WpfDispatcher());
            _connectionManager.MessageReceived += ConnectionManagerOnMessageReceived;

            CreateWorld();
        }

        private void ConnectionManagerOnMessageReceived(object sender, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            //MessagesReceived.Text += messageReceivedEventArgs.Message;
        }

        private void CreateWorld()
        {
            _model = _connectionManager.JoinGame();

            _gameGrid = new Grid();

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
            }

            Board.Children.Add(_gameGrid);
        }

        private FrameworkElement GetElementForPiece(GamePiece piece)
        {
            if (piece is Dot)
            {
                var item = new Grid { Style = (Style)Resources["DotStyle"] };
                return item;
            }

            if (piece is Line)
            {
                var line = piece as Line;

                var button = new Button { Style = (Style)Resources["LineStyle"] };
                button.Click += LineClicked;
                return button;
            }

            var square = new Grid { Style = (Style)Resources["SquareStyle"] };
            return square;
        }

        private void LineClicked(object sender, RoutedEventArgs e)
        {

        }

        private void SetColumnsDefinitions()
        {
            for (var i = 0; i < _model.Width; i++)
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
            for (var i = 0; i < _model.Height; i++)
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
