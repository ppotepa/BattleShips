using BattleShips.Primitives;

namespace BattleShips.Extensions
{
    internal static class GameBoardStringExtensions
    {
        public static Vector2D ToGameBoardCoordinates(this string input)
        {
            return new Vector2D
            {
                X = char.ToUpper(input[0]) - 64,
                Y = short.Parse(input.Substring(1, input.Length - 1))
            };
        }
    }
}
