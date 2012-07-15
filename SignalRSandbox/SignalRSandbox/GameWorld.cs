using System;
using System.Collections.Generic;
using System.Linq;
using SignalLines.Common;

namespace SignalRSandbox
{
    public class GameWorld
    {
        private const int Height = 3;
        private const int Width = 3;

        private int _nextId = 1;
        private GameModel _gameModel;
        private GameState _state = new GameState { Size = new Tuple<int, int>(Height, Width) };

        private static GameWorld _instance;

        public GameModel GameModel
        {
            get { return _gameModel; }
        }

        public GameState State { get { return _state; } }

        public static GameWorld Instance
        {
            get { return _instance ?? (_instance = new GameWorld()); }
        }

        private GameWorld()
        {
            _gameModel = new GameModel(Height, Width);
        }

        public int Join(string connectionId)
        {
            if (State.Players.Count(p => p.ConnectionId == connectionId) == 0)
                State.Players.Add(new Player
                {
                    ConnectionId = connectionId,
                    PlayerId = _nextId
                });
            var retVal = _nextId;
            _nextId++;
            return retVal;
        }

        public void StartNewGame()
        {
            _gameModel = new GameModel(Height, Width);
            var players = _state.Players;
            _state = new GameState { Size = new Tuple<int, int>(Height, Width), Players = players};
        }
    }
}