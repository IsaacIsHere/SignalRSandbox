using System;
using SignalLines.Common;
using SignalLines.Common.GamePieces;
using SignalR.Hubs;

namespace SignalRSandbox.Hubs
{
    [HubName("GameHub")]
    public class GameHub : Hub
    {
        private readonly GameModel _gameModel;
        private const int Height = 3;
        private const int Width = 3;

        public GameHub()
        {
            _gameModel = new GameModel(Height, Width);
        }

        public void Send(string message)
        {
            // Call the addMessage method on all clients
            Clients.addMessage(message);
        }

        public GameModel JoinGame()
        {
            
            return _gameModel;
        }

        public void ClickLine(int row, int column)
        {
            const int playerId = 1;

            // Click the line
            var item = _gameModel.GetElementAt(row, column) as Line;

            if (item != null && item.Occupy(playerId))
            {
                // Tell everyone that the line was clicked
                Clients.lineClicked(row, column, playerId);
            }
        }
    }
}