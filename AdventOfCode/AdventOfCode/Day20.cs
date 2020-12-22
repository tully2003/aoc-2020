using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class Day20
	{
		public static void Execute()
		{
			var input = File
				.ReadAllText("inputs/Day 20/input.test.txt")
				.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(s =>
				{
					var elements = s.Split('\n');

					return new Tile
					{
						Number = int.Parse(elements[0].Split(' ')[1].Trim(':')),
						Image = elements[1..].Select(x => x.ToCharArray()).ToArray()
					};
				})
				.ToArray();

			SolvePart1(input);
		}

		private static void SolvePart1(IEnumerable<Tile> tiles)
        {
			OutputTiles(tiles);

			Tile[][] grid = null;
			foreach (var arrangement in GenerateGridArrangements(tiles))
            {
				if (IsGridAligned(arrangement))
				{
					grid = arrangement;
					break;
				}
            }

			Console.WriteLine("Part 1 ------------");
			Console.WriteLine("What do you get if you multiply together the IDs of the four corner tiles?");
			Console.WriteLine($"{grid[0][0].Number} * {grid[0][grid.Length - 1].Number} * {grid[grid.Length - 1][0].Number} * {grid[grid.Length-1][grid.Length - 1].Number}");
			Console.WriteLine($"{grid[0][0].Number * grid[0][grid.Length - 1].Number * grid[grid.Length - 1][0].Number * (long)grid[grid.Length - 1][grid.Length - 1].Number}");
		}

        private static bool IsGridAligned(Tile[][] grid)
        {
            for (int row = 0; row < grid.Length; row++)
            {
				for (int col = 0; col < grid[row].Length; col++)
				{
					if (row > 0 && row < grid.Length)
						if (!CheckHorizontalEdge(grid[row - 1][col], grid[row][col]))
							return false;

					if (col > 0 && col < grid.Length)
						if (!CheckVerticalEdge(grid[row][col-1], grid[row][col]))
							return false;
				}
			}

			return true;

			bool CheckHorizontalEdge(Tile top, Tile bottom)
            {
				int columns = top.Image[0].Length;
				for (int col = 0; col < columns; col++)
				{
					if (top.Image[top.Image.Length - 1][col] != bottom.Image[0][col])
						return false;
				}

				return true;
			}

			bool CheckVerticalEdge(Tile left, Tile right)
			{
				int rows = left.Image.Length;
				for (int row = 0; row < rows; row++)
				{
					if (left.Image[row][left.Image.Length - 1] != right.Image[row][0])
						return false;
				}

				return true;
			}
		}

		private static IEnumerable<Tile[][]> GenerateGridArrangements(IEnumerable<Tile> tiles)
        {
			// we need to generate every single permutation of the grid... this won't be easy!


			yield return ArrangeGrid(tiles);
		}

        private static Tile[][] ArrangeGrid(IEnumerable<Tile> tiles)
        {
			int gridSize = (int)Math.Sqrt(tiles.Count());
			int row = 0, col = 0;
			Tile[][] grid = new Tile[gridSize][];

			foreach (var tile in tiles)
            {
				if (grid[row] is null)
					grid[row] = new Tile[gridSize];

				grid[row][col] = tile;
				col++;

				if (col >= gridSize)
				{
					row++;
					col = 0;
				}
            }

			return grid;
		}

		private static void OutputTiles(IEnumerable<Tile> tiles)
        {
			foreach(var tile in tiles)
            {
				Console.WriteLine($"Tile {tile.Number}:");
				
				foreach (var line in tile.Image)
                {
					Console.WriteLine(string.Join('\0', line));
                }
				Console.WriteLine();
			}
        }

		private class Tile
        {
            public int Number { get; set; }

            public char[][] Image { get; set; }

			public IEnumerable<Tile> GenerateArrangements()
            {
				yield return this;
				Tile current = this;
				for (int i = 0; i < 3; i++)
                {
					current = current.Rotate();
					yield return current;
                }

				current = this.Flip();
				yield return current;
				for (int i = 0; i < 3; i++)
				{
					current = current.Rotate();
					yield return current;
				}
			}

			private Tile Rotate()
            {
				var tile = Clone();

				char[][] image = new char[tile.Image.Length][];

				for (int i = 0; i < tile.Image.Length; ++i)
				{
					for (int j = 0; j < tile.Image.Length; ++j)
					{
						if (image[i] == null) image[i] = new char[tile.Image.Length];
						image[i][j] = tile.Image[tile.Image.Length - j - 1][i];
					}
				}
				
				tile.Image = image;
				return tile;
			}

			private Tile Flip()
			{
				var tile = Clone();

				int top = 0, bottom = tile.Image.Length;
				while (top < bottom)
                {
					for(int col = 0; col < tile.Image.Length; col++)
                    {
						char temp = tile.Image[top][col];
						tile.Image[top][col] = tile.Image[bottom][col];
						tile.Image[bottom][col] = temp;
					}

					top++;
					bottom--;
                }

				return tile;
			}

			private Tile Clone()
            {
				return new Tile
				{
					Number = Number,
					Image = Image.Select(c => c.ToArray()).ToArray()
				};
            }
		}
	}
}