using BattleShips.Abstractions;
using BattleShips.Options;

namespace BattleShips.Game
{
    public sealed class GameBoard
    {
        private readonly List<Ship> Ships = new();

        public bool InProgress { get; internal set; }
        internal BoardOptions Options { get; set; }
        private Tile[,] Tiles { get; set; }
        public void Tick()
        {
            throw new NotImplementedException();
        }

        internal GameBoard Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
