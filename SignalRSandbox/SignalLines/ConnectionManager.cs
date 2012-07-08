﻿using System;
using SignalR.Client.Hubs;

namespace SignalLines
{
    public class ConnectionManager
    {
        private readonly IDispatcher _dispatcher;
        private readonly IHubProxy _chat;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public ConnectionManager(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            var hub = new HubConnection("http://localhost:5317");
            
            _chat = hub.CreateProxy("GameHub");
            _chat.On("addMessage", ProcessMessage);

            hub.Start().Wait();
        }

        public Tuple<int, int> GetSize()
        {
            var task = _chat.Invoke<Tuple<int, int>>("GameSize");
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
}