using BattleShips.Game;
using BattleShips.Ships;

namespace BattleShips.Abstractions;

internal abstract class Ship
{
    public List<Tile> AllTiles { get; set; } = new();
    public bool IsSunk => AllTiles.All(tile => tile.Hit);
    public Player Owner { get; set; }
    public Tile ShipTile { get; set; }

    public static Ship Create(Type shipType, Tile target, Player player)
    {
        if (shipType == typeof(BattleShip)) return new BattleShip { Owner = player, ShipTile = target };
        if (shipType == typeof(Destroyer)) return new Destroyer { Owner = player, ShipTile = target };

        return null;
    }

    public override string ToString() => this.GetType().Name[..1];
    public string FullName => this.GetType().Name;
}