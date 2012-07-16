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
        private GameWorld _world;

        public GameHub()
        {
            _world = GameWorld.Instance;
        }

        public void Send(string message)
        {
            var player = _world.State.Players.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
            if (player != null)
            {
                // Call the addMessage method on all clients
                Clients.addMessage(player.Name + ": " + message);
            }
            else
            {
                Clients.addMessage("Anon: ");
            }
        }

        public GameState JoinGame(string playerName)
        {
            var state = _world.State;
            var player = _world.Join(playerName, Context.ConnectionId);
            if (player != null)
            {
                Clients.newPlayerJoined(player);
                return state;
            }
            return null;
        }

        public void Leave()
        {
            var player = _world.State.Players.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
            if (player != null)
            {
                _world.State.Players.Remove(player);
                Clients.playerLeft(player);
            }
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
            return null;
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            return null;
        }

        public void ResetGame()
        {
            _world.StartNewGame();

            Clients.resetGame(_world.State);
        }
    }
}