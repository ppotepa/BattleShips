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
            .SetGridSize(-1)
            .Build();
           

        while (board.InProgress)
        {
            board.Tick();
        }
    }
}