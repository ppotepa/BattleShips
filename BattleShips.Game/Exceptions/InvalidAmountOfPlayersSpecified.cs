namespace BattleShips.Exceptions
{
    [Serializable]
    public class InvalidAmountOfPlayersSpecified : Exception
    {
        public InvalidAmountOfPlayersSpecified() { }

        public InvalidAmountOfPlayersSpecified(string message)
            : base(message) { }

        public InvalidAmountOfPlayersSpecified(string message, Exception inner)
            : base(message, inner) { }
    }
}
