using BattleShips.Abstractions;

namespace BattleShips.Game
{
    internal sealed class Tile
    {
        public Ship Ship { get; set; }
        public Tile Up { get; set; }
        public Tile Down { get; set; }
        public Tile Left { get; set; }
        public Tile Right { get; set; }
        public bool IsOccupied => Ship is not null;
    }
}
