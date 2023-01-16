namespace BattleShips.Game;

internal class Player
{
    public Player(string name)
    {
        this.Name = name;
    }

    public string Name { get; init; }
}