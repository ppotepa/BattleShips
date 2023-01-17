using System;
using BattleShips.Builders;
using BattleShips.Game;

namespace BattleShips.Demo;

public static class Program
{
    public static void Main()
    {
        GameBoard board = new GameBoardBuilder()
            .AddPlayer("Pawel")
            .SetGridSize(10)
            .SetMaxDestroyers(4)
            .SetMaxBattleShips(2)
            .SetInput(Console.In)
            .SetOutput(Console.Out)
            .Build();
           

        while (board.InProgress)
        {
            board.Tick();
        }
    }
}