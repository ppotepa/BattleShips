using System;
using BattleShips.Builders;
using BattleShips.Game;

namespace BattleShips.Demo;

public static class Program
{
    public static void Main()
    {
        GameBoard board = new GameBoardBuilder()
            .AddPlayer("GuestLine")
            .SetOutput(Console.Out)
            .SetInput(Console.In)
            .SetGridSize(10)
            .SetMaxDestroyers(2)
            .SetMaxBattleShips(1)
            .Build();

        while (board.InProgress)
        {
            board.Tick();
        }
    }
}