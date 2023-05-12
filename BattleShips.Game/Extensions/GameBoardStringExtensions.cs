using BattleShips.Game;

namespace BattleShips.Extensions
{
    internal static class GameBoardStringExtensions
    {
        public static (int yAxis, int xAxis) ToGameBoardCoordinates(this string input)
        {
            return (char.ToUpper(input[0]) - 64, short.Parse(input.Substring(1, input.Length - 1)));
        }
    }
}
