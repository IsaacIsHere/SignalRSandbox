using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SignalLines.Common;
using SignalLines.Common.GamePieces;
using SignalR.Hubs;

namespace SignalRSandbox.Hubs
{
    [HubName("GameHub")]
    public class GameHub : Hub, IConnected
    {
        private readonly GameWorld _world;

        public GameHub()
        {
            _world = GameWorld.Instance;
        }

        public void Send(string message)
        {
            // Call the addMessage method on all clients
            Clients.addMessage(message);
        }

        public GameModel JoinGame()
        {
            var model = _world.GameModel;
            return model;
        }

        public void ClickLine(int row, int column)
        {
            const int playerId = 1;

            // Click the line
            var item = _world.GameModel.GetElementAt(row, column) as Line;

            if (item != null && item.Occupy(playerId))
            {
                _world.GameModel.LinesOccupied.Add(new Tuple<int, int>(row, column));
                // Tell everyone that the line was clicked
                Clients.lineClicked(row, column, playerId);
            }
        }

        public Task Connect()
        {
            _world.Join(Context.ConnectionId);
            return null;
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            _world.Join(Context.ConnectionId);
            return null;
        }
    }
}