using System;
using SignalLines.Common;
using SignalR.Hubs;

namespace SignalRSandbox.Hubs
{
    [HubName("GameHub")]
    public class GameHub : Hub
    {
        private GameModel _gameModel;
        public GameHub()
        {
            //_gameModel = new GameModel();
        }

        public void Send(string message)
        {
            // Call the addMessage method on all clients
            Clients.addMessage(message);
        }

        public Tuple<int, int> GameSize()
        {
            return new Tuple<int, int>(8, 9);
        }

        public void ClickLine(int row, int column)
        {
            // Click the line

            // Tell everyone that the line was clicked
        }
    }
}