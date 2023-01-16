﻿using BattleShips.Abstractions;
using BattleShips.Enumerations;
using BattleShips.Options;
using BattleShips.Ships;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace BattleShips.Game
{
    public sealed class GameBoard
    {
        internal readonly List<Ship> Ships = new();

        private const byte BattleshipLength = 5;
        private const byte DestroyerLength = 4;
        private const string YAxisLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public bool InProgress { get; internal set; }
        internal BoardOptions Options { get; set; }
        internal Tile[,] Tiles { get; set; }
        public void Tick()
        {
            Options.Output.Close();

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
                        Options.Output.Write(Tiles[y, x].Ship != null ? $"[{Tiles[y, x].Ship}]" : "[ ]");
                    }
                }

                Options.Output.WriteLine();
            }

            Options.Reader.ReadLine();
        }

        internal GameBoard Initialize()
        {
            Tiles = new Tile[Options.GridSize, Options.GridSize];

            for (int x = 0; x < Options.GridSize; x++)
            {
                for (int y = 0; y < Options.GridSize; y++)
                {
                    Tiles[x, y] = new Tile();
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
                Enumerable.Range(0, Options.MaxNumberOfBattleShips).ToList().ForEach((battleShip) =>
                {
                    Random random = new Random();
                    ShipDirection shipDirection = (ShipDirection)random.Next(1, 3);
                    bool isBuilt = false;

                    while (isBuilt is false)
                    {
                        Vector2 startingPos = new Vector2
                        {
                            X = random.Next(0, Options.GridSize),
                            Y = random.Next(0, Options.GridSize)
                        };

                        Tile target = Tiles[(int)startingPos.X, (int)startingPos.Y];
                        Tile[] line = default;

                        if (shipDirection == ShipDirection.Horizontal && startingPos.X < BattleshipLength)
                        {
                            line = GenerateLineTiles(target, shipDirection, BattleshipLength);
                        }

                        if (shipDirection == ShipDirection.Vertical && startingPos.Y < BattleshipLength)
                        {
                            line = GenerateLineTiles(target, shipDirection, BattleshipLength);
                        }

                        if (line != null && line.All(tile => tile.IsOccupied is false))
                        {
                            Ship newShip = new BattleShip { ShipTile = target, Owner = player };
                            player.Ships.Add(newShip);
                            Ships.Add(newShip);

                            Array.ForEach(line, tile =>
                            {
                                tile.Ship = newShip;
                                newShip.AllTiles.Add(tile);
                            });

                            isBuilt = true;
                        }
                    }
                });

                Enumerable.Range(0, Options.MaxNumberOfDestroyers).ToList().ForEach((destroyer) =>
                {
                    Random random = new Random();
                    ShipDirection shipDirection = (ShipDirection)random.Next(1, 3);
                    bool isBuilt = false;

                    while (isBuilt is false)
                    {
                        Vector2 startingPos = new Vector2
                        {
                            X = random.Next(0, Options.GridSize),
                            Y = random.Next(0, Options.GridSize)
                        };

                        Tile target = Tiles[(int)startingPos.X, (int)startingPos.Y];
                        Tile[] line = default;

                        if (shipDirection == ShipDirection.Horizontal && startingPos.X < DestroyerLength)
                        {
                            line = GenerateLineTiles(target, shipDirection, DestroyerLength);
                        }

                        if (shipDirection == ShipDirection.Vertical && startingPos.Y < DestroyerLength)
                        {
                            line = GenerateLineTiles(target, shipDirection, DestroyerLength);
                        }

                        if (line != null && line.All(tile => tile.IsOccupied is false))
                        {
                            Destroyer newShip = new Destroyer() { ShipTile = target, Owner = player };
                            player.Ships.Add(newShip);
                            Ships.Add(newShip);

                            Array.ForEach(line, tile =>
                            {
                                tile.Ship = newShip;
                                newShip.AllTiles.Add(tile);
                            });

                            isBuilt = true;
                        }
                    }
                });
            }

            return this;
        }

        private Tile[] GenerateLineTiles(Tile target, ShipDirection shipDirection, int shipLength)
        {
            IEnumerable<Tile> result = new[] { target };

            for ((int length, Tile target) data = (1, target); data.length++ < shipLength;)
            {
                if (shipDirection == ShipDirection.Horizontal)
                {
                    data.target = data.target.Right;
                    result = result.Append(data.target);
                }
                else
                {
                    data.target = data.target.Down;
                    result = result.Append(data.target);
                }
            }

            return result.ToArray();
        }
    }
}
