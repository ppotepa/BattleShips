using BattleShips.Abstractions;

namespace BattleShips.Game
{
    internal sealed class Tile
    {
        public Tile Down { get; set; }
        public bool Hit { get; set; }
        public bool IsOccupied => Ship is not null;
        public Tile Left { get; set; }
        public string Position { get; set; }
        public Tile Right { get; set; }
        public Ship Ship { get; set; }
        public Tile Up { get; set; }

        public void MarkAsAHit() => this.Hit = true;

        public override string ToString()
        {
            if (Hit) return "[X]";
            return IsOccupied ? $"[{Ship}]" : "[ ]";
        }
    }
}
