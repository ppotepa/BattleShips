namespace BattleShips.Exceptions
{
    [Serializable]
    public class InvalidAmountOfPlayersSpecifiedException : Exception
    {
        public InvalidAmountOfPlayersSpecifiedException() { }

        public InvalidAmountOfPlayersSpecifiedException(string message)
            : base(message) { }

        public InvalidAmountOfPlayersSpecifiedException(string message, Exception inner)
            : base(message, inner) { }
    }
}
