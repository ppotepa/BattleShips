using BattleShips.Builders;
using BattleShips.Game;
using System;

namespace BattleShips.Demo;

public static class Program
{
    public static void Main()
    {
        GameBoard gameBoard = new GameBoardBuilder()
            .AddPlayer("GuestLine")
            .SetOutput(Console.Out)
            .SetInput(Console.In)
            .SetGridSize(10)
            .SetMaxDestroyers(2)
            // EnableDebugMode allows you to see where the ships are placed from the start
            //.EnableDebugMode() 
            .SetMaxBattleShips(1)
            .Build();

        while (gameBoard.InProgress)
        {
            gameBoard.Draw();
            gameBoard.Tick();
        }
    }
}