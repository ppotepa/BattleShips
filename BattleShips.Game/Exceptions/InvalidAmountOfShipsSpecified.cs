namespace BattleShips.Exceptions
{
    [Serializable]
    public class InvalidAmountOfShipsSpecifiedException : Exception
    {
        public InvalidAmountOfShipsSpecifiedException() { }

        public InvalidAmountOfShipsSpecifiedException(string message)
            : base(message) { }

        public InvalidAmountOfShipsSpecifiedException(string message, Exception inner)
            : base(message, inner) { }
    }
}
