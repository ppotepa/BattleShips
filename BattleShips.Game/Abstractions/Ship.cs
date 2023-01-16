using BattleShips.Game;

namespace BattleShips.Abstractions;

internal abstract class Ship
{
    public abstract int Length { get; }
    public bool IsSunk { get; }
    public List<Tile> ShipTile { get; set; }
    public Player Owner { get; set; }
}