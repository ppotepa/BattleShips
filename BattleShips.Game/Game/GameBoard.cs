using BattleShips.Abstractions;
using BattleShips.Enumerations;
using BattleShips.Options;
using BattleShips.Primitives;
using BattleShips.Ships;

namespace BattleShips.Game
{
    public sealed class GameBoard
    {
        internal readonly List<Ship> Ships = new();
        private const string YAxisLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public bool InProgress { get; internal set; }
        internal BoardOptions Options { get; set; }
        internal Tile[,] Tiles { get; set; }
        public void Tick()
        {
            for (int x = -1; x < Options.GridSize; x++)
            {
                for (int y = -1; y < Options.GridSize; y++)
                {
                    if (x == -1)
                    {
                        Console.Write(y == -1 ? "   " : $"[{y + 1}]");
                    }
                    else if (y == -1)
                    {
                        Console.Write($"[{YAxisLetters[x]}]");
                    }
                    else
                    {
                        Console.Write(Tiles[y, x].Ship != null ? $"[{Tiles[y, x].Ship}]" : "[ ]");
                    }
                }

                Console.WriteLine();
            }

           Console.ReadLine();
        }

        internal GameBoard Initialize()
        {
            Tiles = new Tile[Options.GridSize, Options.GridSize];

            for (int x = 0; x < Options.GridSize; x++)
            {
                for (int y = 0; y < Options.GridSize; y++)
                {
                    Tiles[x, y] = new Tile { Position = $"{YAxisLetters[x]}{y + 1}" };
                }
            }

            for (int x = 0; x < Options.GridSize; x++)
            {
                for (int y = 0; y < Options.GridSize; y++)
                {
                    if (y > 0) Tiles[x, y].Up = Tiles[x, y - 1];
                    if (y < Options.GridSize - 1) Tiles[x, y].Down = Tiles[x, y + 1];

                    if (x > 0) Tiles[x, y].Left = Tiles[x - 1, y];
                    if (x < Options.GridSize - 1) Tiles[x, y].Right = Tiles[x + 1, y];
                }
            }

            foreach (Player player in Options.Players)
            {
                Random random = new();

                IEnumerable<Type> battleShips = Enumerable.Repeat(typeof(BattleShip), Options.MaxNumberOfBattleShips);
                IEnumerable<Type> destroyers = Enumerable.Repeat(typeof(Destroyer), Options.MaxNumberOfDestroyers);

                List<Type> allShips = battleShips.Concat(destroyers).ToList();

                allShips.ForEach(shipType =>
                {
                    ShipDirection shipDirection = (ShipDirection)random.Next(1, 3);
                    bool shipBuilt = false;

                    while (shipBuilt is false)
                    {
                        int targetLength = shipType == typeof(BattleShip) ? BattleShip.Length : Destroyer.Length;

                        Vector2D startingPos = new Vector2D
                        {
                            X = random.Next(0, Options.GridSize),
                            Y = random.Next(0, Options.GridSize)
                        };

                        Tile target = Tiles[startingPos.X, startingPos.Y];
                        Tile[] line = default;

                        bool canBuildHorizontal = shipDirection == ShipDirection.Horizontal && startingPos.X + targetLength <= Options.GridSize;
                        bool canBuildVertical = shipDirection == ShipDirection.Vertical && startingPos.Y + targetLength <= Options.GridSize;

                        if (canBuildHorizontal || canBuildVertical)
                        {
                            line = GenerateLineTiles(target, shipDirection, targetLength);
                        }

                        if (line != null && line.All(tile => tile.IsOccupied is false))
                        {
                            Ship newShip = Ship.Create(shipType, target, player);
                            player.Ships.Add(newShip);

                            Array.ForEach(line, tile =>
                            {
                                tile.Ship = newShip;
                                newShip.AllTiles.Add(tile);
                            });

                            this.Ships.Add(newShip);
                            shipBuilt = true;
                        }
                    }
                });
            }

            return this;
        }
        private static Tile[] GenerateLineTiles(Tile target, ShipDirection shipDirection, int shipLength)
        {
            IEnumerable<Tile> result = new[] { target };

            for ((int length, Tile target) data = (1, target); data.length < shipLength; data.length++)
            {
                data.target = shipDirection == ShipDirection.Horizontal ? data.target.Right : data.target.Down;
                result = result.Append(data.target);
            }

            result = result.ToArray();
            return result.Any(tile => tile is null) ? Array.Empty<Tile>() : (Tile[])result;
        }
    }
}
