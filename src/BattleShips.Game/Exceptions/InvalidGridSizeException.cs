namespace BattleShips.Exceptions
{
    [Serializable]
    public class InvalidGridSizeException : Exception
    {
        public InvalidGridSizeException() { }

        public InvalidGridSizeException(string message)
            : base(message) { }

        public InvalidGridSizeException(string message, Exception inner)
            : base(message, inner) { }
    }
}
