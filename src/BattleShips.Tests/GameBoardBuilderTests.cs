using BattleShips.Builders;
using BattleShips.Exceptions;
using BattleShips.Game;
using NUnit.Framework;

namespace BattleShips.Tests
{
    [TestFixture]
    [Category(nameof(GameBoardBuilder))]
    public class GameBoardBuilderTests
    {
        [Test]
        public void Adding_More_Than_One_Players_Throws_Not_Implemented_Exception()
        {
            GameBoardBuilder builder = new GameBoardBuilder()
                .AddPlayer("Player 1")
                .AddPlayer("Player 2");

            Assert.That(() => builder.Build(), Throws.TypeOf<AggregateException>()
                .With
                .Property(nameof(AggregateException.InnerExceptions))
                .ItemAt(1)
                .TypeOf<NotImplementedException>()
            );
        }

        [Test]
        public void Builder_Does_Not_Throw_Any_Exceptions_If_Options_Are_Correct()
        {
            GameBoardBuilder? builder = new GameBoardBuilder()
                .AddPlayer("Player 1")
                .SetGridSize(10)
                .SetInput(Console.In)
                .SetOutput(Console.Out)
                .SetMaxBattleShips(2)
                .SetMaxDestroyers(1);
               

            Assert.That(() => builder.Build(), Is.TypeOf<GameBoard>());
        }

        [Test]
        public void Grid_Size_Should_Be_Bigger_Than_BattleShip_Length()
        {
            GameBoardBuilder builder = new GameBoardBuilder()
                .AddPlayer("Player 1")
                .SetGridSize(3)
                .SetMaxBattleShips(1);

            Assert.That(() => builder.Build(), Throws.TypeOf<AggregateException>()
                .With
                .Property(nameof(AggregateException.InnerExceptions))
                .One
                .Items
                .TypeOf<InvalidGridSizeException>()
            );
        }

        [Test]
        public void Grid_Size_Should_Be_Bigger_Than_Destroyer_Length()
        {
            GameBoardBuilder? builder = new GameBoardBuilder()
                .AddPlayer("Player 1")
                .SetGridSize(3)
                .SetMaxBattleShips(1);

            Assert.That(() => builder.Build(), Throws.TypeOf<AggregateException>()
                .With
                .Property(nameof(AggregateException.InnerExceptions))
                .One
                .Items
                .TypeOf<InvalidGridSizeException>()
            );
        }

        [Test]
        public void Player_Should_Be_Unable_To_Start_The_Game_If_Not_A_Single_Player_Was_Specified()
        {
            GameBoardBuilder builder = new();

            Assert.That(() => builder.Build(), Throws.TypeOf<AggregateException>()
                .With
                .Property(nameof(AggregateException.InnerExceptions))
                .One
                .Items
                .TypeOf<InvalidAmountOfPlayersSpecifiedException>()
            );
        }

        [Test]
        public void Player_Should_Specify_Ship_Amount()
        {
            GameBoardBuilder? builder = new GameBoardBuilder()
                .AddPlayer("Player 1");

            Assert.That(() => builder.Build(), Throws.TypeOf<AggregateException>()
                .With
                .Property(nameof(AggregateException.InnerExceptions))
                .One
                .Items
                .TypeOf<InvalidAmountOfShipsSpecifiedException>()

            );
        }

        [SetUp]
        public void Setup()
        {
            //intentionally left-empty
        }

        [Test]
        public void User_Should_Be_Unable_To_Create_Board_Of_Negative_Size()
        {
            GameBoardBuilder? builder = new GameBoardBuilder()
                .SetGridSize(-1);

            Assert.That(() => builder.Build(), 
                Throws.TypeOf<AggregateException>().With.InnerException.TypeOf<InvalidGridSizeException>()
            );
        }
    }
}