using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using BattleShips.Abstractions;
using BattleShips.Enumerations;
using BattleShips.Extensions;
using BattleShips.Options;
using BattleShips.Primitives;
using BattleShips.Ships;

namespace BattleShips.Game
{
    public sealed class GameBoard
    {
        internal readonly List<Ship> Ships = new();

        private const string YAxisLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly Regex _pattern = new("([A-Za-z][1-9]|[1-9])");
        public bool InProgress => Ships.Any(ship => ship.IsSunk is false);
        internal BoardOptions Options { get; set; }
        internal Tile[,] Tiles { get; set; }

        public void Tick()
        {
            string input;
            Tile targetTile = default;

            do
            {
                input = Options.Input.ReadLine();
            }
            while (input != null && !_pattern.IsMatch(input));

            Vector2D coords = input.ToGameBoardCoordinates();

            try
            {
                targetTile = Tiles[coords.X - 1, coords.Y - 1];
            }
            catch (IndexOutOfRangeException)
            {
                Options.Output.WriteLine($"{input?.ToUpper()} is Out of Bounds. Try again.");
                throw;
            }

            if (targetTile is { IsOccupied: true })
            {
                targetTile.MarkAsAHit();
                Options.Output.WriteLine($"{input?.ToUpper()} was a BullsEye ! Keep shooting.");

                if (targetTile.Ship.AllTiles.All(tile => tile.Hit))
                {
                    Options.Output.WriteLine($"Congratulations, you've sunk a {targetTile.Ship.FullName}. Keep stacking them up!");

                    if (Ships.All(ship => ship.IsSunk))
                    {
                        Options.Output.WriteLine("Game is over. All Enemy Ships have been sunk. You win.");
                    }
                }
            }
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

                        Vector2D startingPos = new()
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

                        if (line == null || !line.All(tile => tile.IsOccupied is false)) continue;
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

        public void Draw()
        {
            for (int x = -1; x < Options.GridSize; x++)
            {
                for (int y = -1; y < Options.GridSize; y++)
                {
                    if (x == -1)
                    {
                        Options.Output.Write(y == -1 ? "   " : $"[{y + 1}]");
                    }
                    else if (y == -1)
                    {
                        Options.Output.Write($"[{YAxisLetters[x]}]");
                    }
                    else
                    {
                        if (Tiles[y, x].Hit)
                        {
                            Options.Output.Write("[X]");
                        }
                        else
                        {
                            if (Options.DebugModeEnabled)
                            {
                                Options.Output.Write(Tiles[y, x].Ship != null ? $"[{Tiles[y, x].Ship}]" : "[ ]");
                            }
                            else
                            {
                                Options.Output.Write($"[ ]");
                            }
                            
                        }
                    }
                }

                Options.Output.WriteLine();
            }
        }
    }
}
