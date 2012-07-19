using System;
using SignalLines.Common;
using SignalR.Client.Hubs;

namespace SignalLines
{
    public class ConnectionManager
    {
        private readonly IDispatcher _dispatcher;
        private readonly IHubProxy _gameHub;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<LineClickedEventArgs> LineClicked;
        public event EventHandler<PlayerJoinedEventArgs> PlayerJoined;
        public event EventHandler<PlayerLeftEventArgs> PlayerLeft;
        public event EventHandler<GameResetEventArgs> GameReset;

        public ConnectionManager(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            var hub = new HubConnection("http://signallines.apphb.com");
            
            _gameHub = hub.CreateProxy("GameHub");
            _gameHub.On("addMessage", ProcessMessage);
            _gameHub.On<int, int, int>("lineClicked", HandleLineClicked);
            _gameHub.On<Player>("newPlayerJoined", ProcessNewPlayerJoined);
            _gameHub.On<GameState>("resetGame", HandleResetGame);
            _gameHub.On<Player>("playerLeft", HandlePlayerLeft);

            hub.Start().Wait();
        }

        private void HandlePlayerLeft(Player player)
        {
            if (PlayerLeft != null)
                _dispatcher.Dispatch(() => PlayerLeft(this, new PlayerLeftEventArgs(player)));
        }

        private void HandleResetGame(GameState gameState)
        {
            if (GameReset != null)
                _dispatcher.Dispatch(() => GameReset(this, new GameResetEventArgs(gameState)));
        }

        private void ProcessNewPlayerJoined(Player player)
        {
            if(PlayerJoined != null)
                _dispatcher.Dispatch(() => PlayerJoined(this, new PlayerJoinedEventArgs(player)));
        }

        public GameState JoinGame(string name)
        {
            var task = _gameHub.Invoke<GameState>("JoinGame", name);
            task.Wait();
            var result = task.Result;
            return result;
        }

        private void ProcessMessage(dynamic message)
        {
            if (MessageReceived != null)
            {
                _dispatcher.Dispatch(() => MessageReceived(this, new MessageReceivedEventArgs(message)));
            }
        }

        public void SendMessage(string message)
        {
            _gameHub.Invoke("Send", message);
        }

        public void ResetGame()
        {
            _gameHub.Invoke("ResetGame");
        }

        public void HandleLineClicked(int row, int column, int playerId)
        {
            if (LineClicked != null)
            {
                _dispatcher.Dispatch(() => LineClicked(this, new LineClickedEventArgs(row, column, playerId)));
            }
        }

        public void ClickLine(int row, int column)
        {
            var task = _gameHub.Invoke("ClickLine", row, column);
            task.Wait();
        }

        public void LeaveGame()
        {
            _gameHub.Invoke("Leave");
        }
    }

    public class GameResetEventArgs : EventArgs
    {
        public GameState GameState { get; set; }

        public GameResetEventArgs(GameState gameState)
        {
            GameState = gameState;
        }
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        private readonly dynamic _message;

        public MessageReceivedEventArgs(dynamic message)
        {
            _message = message;
        }

        public dynamic Message
        {
            get { return _message; }
        }
    }

    public class PlayerJoinedEventArgs :EventArgs
    {
        public Player Player { get; set; }

        public PlayerJoinedEventArgs(Player player)
        {
            Player = player;
        }
    }

    public class PlayerLeftEventArgs : EventArgs
    {
        public Player Player { get; set; }

        public PlayerLeftEventArgs(Player player)
        {
            Player = player;
        }
    }

    public class LineClickedEventArgs : EventArgs
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int PlayerId { get; private set; }

        public LineClickedEventArgs(int row, int column, int playerId)
        {
            Row = row;
            Column = column;
            PlayerId = playerId;
        }
    }
}