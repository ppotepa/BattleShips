using BattleShips.Abstractions;

namespace BattleShips.Game;

internal class Player
{
    public Player(string name)
    {
        this.Name = name;
    }

    public string Name { get; init; }
    public List<Ship> Ships { get; set; } = new();
}