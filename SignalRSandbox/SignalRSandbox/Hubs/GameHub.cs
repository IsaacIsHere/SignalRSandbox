using System;
using System.Collections.Generic;
using System.Linq;
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
            var player = _world.State.Players.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
            if (player != null)
            {
                var playerId = player.PlayerId;
                // Call the addMessage method on all clients
                Clients.addMessage("Player" + playerId + ": " + message);
            }
        }

        public GameState JoinGame()
        {
            var state = _world.State;
            return state;
        }

        public void ClickLine(int row, int column)
        {
            var playerId = _world.State.Players.First(p => p.ConnectionId == Context.ConnectionId).PlayerId;

            var item = _world.GameModel.GetElementAt(row, column) as Line;

            if (item != null && item.Occupy(playerId))
            {
                _world.State.OccupiedLines.Add(item);
                Clients.lineClicked(row, column, playerId);
            }
        }

        public Task Connect()
        {
            return AddPlayerToGame();
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            return AddPlayerToGame();
        }

        private Task AddPlayerToGame()
        {
            var playerId = _world.Join(Context.ConnectionId);
            Clients.newPlayerJoined(playerId);
            return null;
        }
    }
}