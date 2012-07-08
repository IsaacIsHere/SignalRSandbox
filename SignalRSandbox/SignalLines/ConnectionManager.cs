using System;
using SignalLines.Common;
using SignalR.Client.Hubs;

namespace SignalLines
{
    public class ConnectionManager
    {
        private readonly IDispatcher _dispatcher;
        private readonly IHubProxy _chat;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<LineClickedEventArgs> LineClicked;

        public ConnectionManager(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            var hub = new HubConnection("http://localhost:5317");
            
            _chat = hub.CreateProxy("GameHub");
            _chat.On("addMessage", ProcessMessage);
            _chat.On<int, int, int>("lineClicked", HandleLineClicked);

            hub.Start().Wait();
        }

        public GameModel JoinGame()
        {
            var task = _chat.Invoke<GameModel>("JoinGame");
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
            _chat.Invoke("Send", message);
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
            var task = _chat.Invoke("ClickLine", row, column);
            task.Wait();
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