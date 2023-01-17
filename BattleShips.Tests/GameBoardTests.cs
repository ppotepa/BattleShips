using BattleShips.Game;
using BattleShips.Ships;
using NUnit.Framework;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using BattleShips.Options;

namespace BattleShips.Tests
{
    [TestFixture]
    [Category(nameof(GameBoard))]
    public class GameBoardTests
    {
        [Test]
        public void Adding_Destroyer_And_BattleShip_Should_Result_In_5_Tiles_Taken_In_Total()
        {
            const int gridSize = 10;
            const int destroyersNumber = 2;
            const int battleShipsNumber = 1;

            GameBoard gameBoard = new()
            {
                Options = new BoardOptions
                {
                    GridSize = gridSize,
                    MaxNumberOfDestroyers = destroyersNumber,
                    MaxNumberOfBattleShips = battleShipsNumber,
                    Players =
                    {
                        new Player("Player 1")
                    }
                }
            };

            gameBoard.Initialize();
            int totalCount = 0;

            for (int x = 0; x < gridSize; x++)
                for (int y = 0; y < gridSize; y++)
                    if (gameBoard.Tiles[x, y].IsOccupied) totalCount++;

            int shouldEqual = (BattleShip.Length * battleShipsNumber) + (Destroyer.Length * destroyersNumber);
            Assert.That(shouldEqual, Is.EqualTo(totalCount));
        }

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
            Assert.That(gameBoard.Ships.Count is 2 && containsBattleShip && containsDestroyer, Is.True);
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

            bool containsCorrectBattleShip = gameBoard.Ships.First(ship => ship.GetType() == typeof(BattleShip)).AllTiles.Count == BattleShip.Length;
            bool containsCorrectDestroyer = gameBoard.Ships.First(ship => ship.GetType() == typeof(Destroyer)).AllTiles.Count == Destroyer.Length;
            Assert.That(gameBoard.Ships.Count is 2 && containsCorrectDestroyer && containsCorrectBattleShip, Is.True);
        }
    }
}