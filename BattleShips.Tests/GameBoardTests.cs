using BattleShips.Game;
using BattleShips.Ships;
using NUnit.Framework;
using System.Linq;
using BattleShips.Options;

namespace BattleShips.Tests
{
    [TestFixture]
    [Category(nameof(GameBoard))]
    public class GameBoardTests
    {
        [Test]
        public void Initializing_GameBoard_With_Two_Ships_Returns_Count_Of_Two_Ships()
        {
            GameBoard gameBoard = new()
            {
                Options = new BoardOptions
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
            bool containsBattleShip = gameBoard.Ships.Any(ship => ship.GetType() == typeof(BattleShip));
            bool containsDestroyer = gameBoard.Ships.Any(ship => ship.GetType() == typeof(Destroyer));
            Assert.That(gameBoard.Ships.Count is 2 && containsBattleShip && containsDestroyer);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Ships_Should_Be_Correct_Length()
        {
            GameBoard gameBoard = new()
            {
                Options = new BoardOptions
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

            bool containsCorrectBattleShip = gameBoard.Ships.First(ship => ship.GetType() == typeof(BattleShip)).AllTiles.Count == 5;
            bool containsCorrectDestroyer = gameBoard.Ships.First(ship => ship.GetType() == typeof(Destroyer)).AllTiles.Count == 4;
            Assert.That(gameBoard.Ships.Count is 2 && containsCorrectDestroyer && containsCorrectBattleShip, Is.True);
        }
    }
}