using System;
using System.Collections.Generic;
using SignalLines.Common;

namespace SignalRSandbox
{
    public class GameWorld
    {
        private const int Height = 10;
        private const int Width = 10;

        private int _nextId = 1;
        private readonly GameModel _gameModel;
        private readonly List<Player> _players;
        private readonly GameState _state = new GameState { Size = new Tuple<int, int>(Height, Width) };

        private static GameWorld _instance;

        public GameModel GameModel
        {
            get { return _gameModel; }
        }

        public GameState State { get { return _state; } }

        public List<Player> Players
        {
            get { return _players; }
        }

        public static GameWorld Instance
        {
            get { return _instance ?? (_instance = new GameWorld()); }
        }

        private GameWorld()
        {
            _gameModel = new GameModel(Height, Width);
            _players = new List<Player>();
        }

        public void Join(string connectionId)
        {
            Players.Add(new Player
            {
                ConnectionId = connectionId,
                PlayerId = _nextId
            });
            _nextId++;
        }
    }
}