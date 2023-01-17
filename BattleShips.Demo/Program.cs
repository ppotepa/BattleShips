using BattleShips.Builders;
using BattleShips.Game;
using System;

namespace BattleShips.Demo;

public static class Program
{
    public static void Main()
    {
        GameBoard board = new GameBoardBuilder()
            .AddPlayer("Pawel")
            .SetGridSize(10)
            .SetMaxDestroyers(3)
            .SetMaxBattleShips(3)
            .Build();

        while (board.InProgress)
        {
            board.Tick();
        }
    }
}