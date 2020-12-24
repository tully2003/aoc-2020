using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day24
    {
        private enum Colour
        {
            White,
            Black
        }

        public static void Execute()
        {
            var input = File.ReadAllLines("inputs/Day 24/input.txt");

            var hexGrid = SolvePart1(input);
            SolvePart2(hexGrid);
        }

        private static void SolvePart2(HashSet<Tile> hexGrid)
        {
            int day = 1;
            while(day <= 100)
            {
                // first let's build all neighbours for our current position
                foreach (var tile in hexGrid.ToList())
                    AddNeighbours(tile, hexGrid);

                List<Tile> tilesToFlip = new List<Tile>();

                var tiles = hexGrid.ToList();
                foreach (var tile in tiles)
                {
                    int adjacentBlackCount = 0;
                    foreach (var neighbour in GetNeighbours(tile))
                    {
                        if (hexGrid.TryGetValue(neighbour, out Tile n) && n.Colour == Colour.Black)
                            adjacentBlackCount += 1;
                    }

                    if (tile.Colour == Colour.Black && (adjacentBlackCount == 0 || adjacentBlackCount > 2))
                        tilesToFlip.Add(tile);
                    else if (tile.Colour == Colour.White && adjacentBlackCount == 2)
                        tilesToFlip.Add(tile);
                }

                foreach (var tile in tilesToFlip)
                    tile.Colour = tile.Colour == Colour.Black ? Colour.White : Colour.Black;

                if (day <= 10 || day % 10 == 0)
                    Console.WriteLine($"Day {day}: {hexGrid.Count(x => x.Colour == Colour.Black)}");
                day++;
            }

            Console.WriteLine("Part 2: ----------");
            Console.WriteLine("Go through the renovation crew's list and determine which tiles they need to flip.");
            Console.WriteLine("After all of the instructions have been followed, how many tiles are left with the black side up?");
            Console.WriteLine(hexGrid.Count(x => x.Colour == Colour.Black));
        }

        private static HashSet<Tile> SolvePart1(string[] input)
        {
            Dictionary<string, (int, int, int)> transitions = new Dictionary<string, (int, int, int)>
            {
                ["ne"] = (1, 0, -1),
                ["e"] = (1, -1, 0),
                ["se"] = (0, -1, 1),
                ["sw"] = (-1, 0, 1),
                ["w"] = (-1, 1, 0),
                ["nw"] = (0, 1, -1),
            };
            HashSet<Tile> hexGrid = new HashSet<Tile>();
            Tile reference = new Tile(0, 0, 0);
            hexGrid.Add(reference);
            
            foreach (var line in input)
            {
                var current = reference;

                for (int i = 0; i < line.Length; i++)
                {
                    string direction = line[i].ToString();
                    if (!transitions.ContainsKey(line[i].ToString()))
                        direction += line[++i];

                    (int x, int y, int z) transition = transitions[direction];
                    var tile = new Tile(current.X + transition.x, current.Y + transition.y, current.Z + transition.z);

                    if (!hexGrid.TryGetValue(tile, out current))
                    {
                        hexGrid.Add(tile);
                        current = tile;
                    }
                }

                current.Colour = current.Colour == Colour.White ? Colour.Black : Colour.White;
            }

            Console.WriteLine("Part 1: ----------");
            Console.WriteLine("Go through the renovation crew's list and determine which tiles they need to flip.");
            Console.WriteLine("After all of the instructions have been followed, how many tiles are left with the black side up?");
            Console.WriteLine(hexGrid.Count(x => x.Colour == Colour.Black));
            Console.WriteLine();

            return hexGrid;
        }

        private static IEnumerable<Tile> GetNeighbours(Tile tile)
        {
            Dictionary<string, (int x, int y, int z)> transitions = new Dictionary<string, (int, int, int)>
            {
                ["ne"] = (1, 0, -1),
                ["e"] = (1, -1, 0),
                ["se"] = (0, -1, 1),
                ["sw"] = (-1, 0, 1),
                ["w"] = (-1, 1, 0),
                ["nw"] = (0, 1, -1),
            };

            foreach (var transition in transitions.Values)
                yield return new Tile(tile.X + transition.x, tile.Y + transition.y, tile.Z + transition.z);
        }

        private static void AddNeighbours(Tile tile, HashSet<Tile> hexGrid)
        {
            Dictionary<string, (int x, int y, int z)> transitions = new Dictionary<string, (int, int, int)>
            {
                ["ne"] = (1, 0, -1),
                ["e"] = (1, -1, 0),
                ["se"] = (0, -1, 1),
                ["sw"] = (-1, 0, 1),
                ["w"] = (-1, 1, 0),
                ["nw"] = (0, 1, -1),
            };

            foreach (var transition in transitions.Values)
            {
                var newNeighbour = new Tile(tile.X + transition.x, tile.Y + transition.y, tile.Z + transition.z);

                if (!hexGrid.Contains(newNeighbour))
                    hexGrid.Add(newNeighbour);
            }
        }

        [DebuggerDisplay("{DebuggerDisplay,nq}")]
        private class Tile
        {
            public Tile(int x, int y, int z, Colour colour = Colour.White)
            {
                X = x;
                Y = y;
                Z = z;
                Colour = colour;
            }

            public int X { get; }
            public int Y { get; }
            public int Z { get; }
            public Colour Colour { get; set; }

            public override int GetHashCode()
            {
                return (X, Y, Z).GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var tile = obj as Tile;
                if (tile == null) return false;

                return (X, Y, Z) == (tile.X, tile.Y, tile.Z);
            }

            private string DebuggerDisplay => $"X = {X}, Y = {Y}, Z = {Z}";
        }
    }
}
