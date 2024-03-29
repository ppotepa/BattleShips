﻿using BattleShips.Game;

namespace BattleShips.Options
{
    internal sealed class BoardOptions
    {
        public TextWriter Output = Console.Out;

        internal bool DebugModeEnabled { get; set; }
        public int GridSize { get; internal set; }
        public TextReader Input { get; set; }
        public int MaxNumberOfBattleShips { get; internal set; }
        public int MaxNumberOfDestroyers { get; internal set; }
        public List<Player> Players { get; init; } = new();

        public TextReader Reader { get; set; }

        public bool ShipsAreNonNegativeNumber => new[] {
                    MaxNumberOfBattleShips < 0,
                    MaxNumberOfDestroyers < 0
        }
        .Any(condition => condition);

        public int TotalAmountOfShips
                    => MaxNumberOfBattleShips + MaxNumberOfDestroyers;
    }
}
