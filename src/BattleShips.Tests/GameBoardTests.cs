using BattleShips.Builders;
using BattleShips.Game;
using BattleShips.Options;
using BattleShips.Ships;
using NUnit.Framework;

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
            //intentionally left empty
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

        [Test]
        public void Sinking_All_Ships_Results_In_Game_Over()
        {
            GameBoard gameBoard = new()
            {
                Options = new BoardOptions
                {
                    GridSize = 10,
                    MaxNumberOfBattleShips = 1,
                    Players =
                    {
                        new Player("Player 1")
                    }
                }
            };
            
            gameBoard.Initialize();
            gameBoard.Ships.ForEach(ship => ship.AllTiles.ForEach(tile => tile.MarkAsAHit()));


            Assert.That(gameBoard.InProgress, Is.False);
        }

        [Test]
        public void User_Cannot_Place_A_Shot_Out_Of_Bounds()
        {
            string playerInput = "A20";
            StringReader playerInputReader = new(playerInput);

            GameBoardBuilder? builder = new GameBoardBuilder()
                .AddPlayer("Player 1")
                .SetGridSize(10)
                .SetInput(playerInputReader)
                .SetOutput(Console.Out)
                .SetMaxBattleShips(2)
                .SetMaxDestroyers(1);

            GameBoard? board = builder.Build();

            Assert.That(() => board.Tick(),
                Throws.TypeOf<IndexOutOfRangeException>()
            );
        }

    }
}