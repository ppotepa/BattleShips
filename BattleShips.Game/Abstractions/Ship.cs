using BattleShips.Game;
using BattleShips.Ships;

namespace BattleShips.Abstractions;

internal abstract class Ship
{
    public const int Length = 0;
    public bool IsSunk { get; }
    public Player Owner { get; set; }
    public Tile ShipTile { get; set; }
    public List<Tile> AllTiles { get; set; } = new();
    public override string ToString()
    {
        return this.GetType().Name.Substring(0, 1);
    }

    public static Ship Create(Type shipType, Tile target, Player player)
    {
        if (shipType == typeof(BattleShip)) return new BattleShip { Owner = player, ShipTile = target };
        if (shipType == typeof(Destroyer)) return new Destroyer { Owner = player, ShipTile = target };
        return null;
    }
}