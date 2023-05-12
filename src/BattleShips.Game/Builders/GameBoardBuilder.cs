using BattleShips.Game;
using BattleShips.Options;
using BattleShips.Validation;

namespace BattleShips.Builders
{
    public class GameBoardBuilder
    {
        private readonly BoardOptions _options = new();

        public GameBoardBuilder AddPlayer(string name)
        {
            _options.Players.Add(new Player(name));
            return this;
        }

        public GameBoard Build()
        {
            GameBoard gameBoard = new();

            try
            {
                new GameBoardValidator(_options).Validate();
            }
            catch (AggregateException ex)
            {
                if (_options.Output != null)
                {
                    _options.Output?.WriteLine(ex.Message);
                }

                throw;
            }

            gameBoard.Options = _options;

            return gameBoard.Initialize();
        }

        public GameBoardBuilder SetGridSize(int gridSize)
        {
            _options.GridSize = gridSize;
            return this;
        }

        public GameBoardBuilder SetMaxBattleShips(int maxBattleShips)
        {
            _options.MaxNumberOfBattleShips = maxBattleShips;
            return this;
        }

        public GameBoardBuilder SetMaxDestroyers(int maxDestroyers)
        {
            _options.MaxNumberOfDestroyers = maxDestroyers;
            return this;
        }

        public GameBoardBuilder SetOutput(TextWriter output)
        {
            _options.Output = output;
            return this;
        }

        public GameBoardBuilder SetInput(TextReader input)
        {
            _options.Input = input;
            return this;
        }

        public GameBoardBuilder EnableDebugMode()
        {
            this._options.DebugModeEnabled = true;
            return this;
        }
    }
}
