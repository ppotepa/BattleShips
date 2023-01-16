using BattleShips.Game;

namespace BattleShips.Abstractions;

internal abstract class Ship
{
    public bool IsSunk { get; }
    public abstract int Length { get; }
    public Player Owner { get; set; }
    public Tile ShipTile { get; set; }
    public List<Tile> AllTiles { get; set; } = new();
}