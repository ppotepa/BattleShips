using BattleShips.Builders;
using BattleShips.Exceptions;
using BattleShips.Game;
using BattleShips.Ships;
using NUnit.Framework;

namespace BattleShips.Tests
{
    [TestFixture]
    [Category(nameof(GameBoard))]
    public class GameBoardTests
    {
        [Test]
        public void Initializing_GameBoard_With_Two_Ships_Returns_Count_Of_Two_Ships()
        {
            GameBoard gameBoard = new GameBoard
            {
                Options = new()
                {
                    GridSize = 10,
                    MaxNumberOfBattleShips = 1,
                    MaxNumberOfDestroyers = 1,
                    Players =
                    {
                        new Player("Player 1")
                    }
                }
            };

            gameBoard.Initialize();
            Assert.True(
                gameBoard.Ships.Count is 2 &&
                gameBoard.Ships.Any(ship => ship.GetType() == typeof(BattleShip)) &&
                gameBoard.Ships.Any(ship => ship.GetType() == typeof(Destroyer))
            );
        }

        [SetUp]
        public void Setup()
        {
        }
    }
}