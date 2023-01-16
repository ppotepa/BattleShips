using BattleShips.Game;

namespace BattleShips.Options
{
    internal sealed class BoardOptions
    {
        public int GridSize { get; internal set; }
        public int MaxNumberOfBattleShips { get; internal set; }
        public int MaxNumberOfDestroyers { get; internal set; }

        public int TotalAmountOfShips 
            => MaxNumberOfBattleShips + MaxNumberOfDestroyers;

        public bool ShipsAreNonNegativeNumber
        {
            get
            {
                return new[]
                {
                    MaxNumberOfBattleShips < 0,
                    MaxNumberOfDestroyers < 0
                }
                .Any(condition => condition);
            }
        }

        public List<Player> Players { get; init; } = new List<Player>();
        public TextWriter Output { get; set; }
        public TextReader Reader { get; set; }
    }
}
