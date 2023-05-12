using BattleShips.Exceptions;
using BattleShips.Options;

namespace BattleShips.Validation;

internal class GameBoardValidator
{
    public GameBoardValidator(BoardOptions options)
    {
        Options = options;
    }

    public BoardOptions Options { get; set; }

    public void Validate()
    {
        List<Exception> exceptions = new();

        if (Options.ShipsAreNonNegativeNumber)
        {
            exceptions.Add(new InvalidAmountOfShipsSpecifiedException(message: "Amount of any ships cannot be a negative number."));
        }

        if (Options.GridSize < 0)
        {
            exceptions.Add(new InvalidGridSizeException(message: $"Grid size cannot be lower or equal to zero."));
        }

        if (Options.GridSize < 5 && Options.MaxNumberOfBattleShips > 0)
        {
            exceptions.Add(new InvalidGridSizeException(
                message: $"Grid size cannot be lower than {Options.GridSize} because '{nameof(Options.MaxNumberOfBattleShips)}' " +
                $"was specified which minimal length is {5}.")
            );
        }

        if (Options.GridSize < 4 && Options.MaxNumberOfDestroyers > 0)
        {
            exceptions.Add(new InvalidGridSizeException(
                message: $"Grid size cannot be lower than {Options.GridSize} because '{nameof(Options.MaxNumberOfDestroyers)}' " +
                         $"was specified which minimal length is {4}.")
            );
        }

        if (Options.TotalAmountOfShips <= 0)
        {
            exceptions.Add(new InvalidAmountOfShipsSpecifiedException(
                message: "Total amount of ships must be greater than 0."));
        }

        switch (Options.Players.Count)
        {
            case <= 0:
                exceptions.Add(new InvalidAmountOfPlayersSpecifiedException(
                    message: "Total amount of players must be greater than 0.")
                );
                break;
            case > 1:
                exceptions.Add(new NotImplementedException(
                    message: "MultiPlayer is currently not implemented")
                );
                break;
        }

        if (new object[] { Options.Input, Options.Output }.Any(option => option is null))
        {
            exceptions.Add(new NotImplementedException(
                message: "Input and Output have to be specified.")
            );
        }

        if (exceptions.Any())
        {
            throw new AggregateException(message: "Errors occurred when building a Validation a GameBoard.", exceptions);
        }
    }

}