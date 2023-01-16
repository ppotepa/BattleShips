using BattleShips.Game;

namespace BattleShips.Abstractions;

internal abstract class Ship
{
    public bool IsSunk { get; }
    public abstract int Length { get; }
    public Player Owner { get; set; }
    public List<Tile> ShipTile { get; set; }
}