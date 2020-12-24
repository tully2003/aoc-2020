using System;
using System.Collections.Generic;
using System.Diagnostics;
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
				.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(s =>
				{
					var elements = s.Split("\r\n");

					return new Tile
					{
						Number = int.Parse(elements[0].Split(' ')[1].Trim(':')),
						Image = elements[1..].Select(x => x.ToCharArray()).ToArray()
					};
				})
				.ToArray();

			var grid = SolvePart1(input);
            SolvePart2(grid);
        }

		private static Tile[][] SolvePart1(IEnumerable<Tile> tiles)
        {
			OutputTiles(tiles);

            Tile[][] grid = CreateEmptyGrid(tiles);
            if (!GenerateValidGrid(grid, 0, 0, tiles.ToList()))
            {
                Console.WriteLine("Unable to find valid arrangement......");
                return null;
            }

            OutputGrid(grid);
            Console.WriteLine("Part 1 ------------");
			Console.WriteLine("What do you get if you multiply together the IDs of the four corner tiles?");
			Console.WriteLine($"{grid[0][0].Number} * {grid[0][grid.Length - 1].Number} * {grid[grid.Length - 1][0].Number} * {grid[grid.Length-1][grid.Length - 1].Number}");
			Console.WriteLine($"{grid[0][0].Number * grid[0][grid.Length - 1].Number * grid[grid.Length - 1][0].Number * grid[grid.Length - 1][grid.Length - 1].Number}");
            return grid;
		}

        private static void SolvePart2(Tile[][] grid)
        {
            Console.WriteLine("Part 2 ------------");

            // convert grid to image - we have to remove the borders!
            char[][] image = new char[(grid[0][0].Image.Length-2)*grid.Length][];

            int rows = 0;
            foreach (var gridRow in grid)
            {
                int cols = 0;
                foreach (var tile in gridRow)
                {
                    for (int row = 0; row < tile.Image.Length - 2; row++) // 10
                    {
                        if (image[row + rows] is null)
                            image[row + rows] = new char[(tile.Image[0].Length-2) * gridRow.Length];

                        for (int col = 0; col < tile.Image[row].Length-2; col++)
                        {
                            image[row + rows][col+cols] = tile.Image[row+1][col+1];
                        }
                    }
                    cols += tile.Image[0].Length-2;
                }
                rows += gridRow[0].Image.Length-2;
            }

            foreach (var img in GenerateArrangements(image))
            {
                // draw image
                for (int row = 0; row < img.Length; row++)
                {
                    Console.Write(string.Join('\0', img[row]) + " ");
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }

        private static bool GenerateValidGrid(Tile[][] grid, int row, int col, IList<Tile> tiles)
        {
            /* base case: If all tiles are placed then return true */
            if (!grid.Any(x => x.Any(tile => tile == null)))
                return true;

            for (int i = 0; i < tiles.Count; i++)
            {
                foreach (var tile in tiles[i].GenerateArrangements())
                {
                    grid[row][col] = tile;

                    if (IsValidPlacement(grid, row, col))
                    {
                        var copy = tiles.ToList();
                        copy.RemoveAt(i);

                        if (GenerateValidGrid(grid, col + 1 >= grid[row].Length ? row + 1 : row, col + 1 >= grid[row].Length ? 0 : col + 1, copy))
                            return true;
                        else
                            grid[row][col] = null;
                    }
                    else
                    {
                        grid[row][col] = null;
                    }
                }
            }

            return false;
        }

        private static bool IsValidPlacement(Tile[][] grid, int row, int col)
        {
            Tile contender = grid[row][col];

            // check left edge
            if (col - 1 >= 0 && !CheckVerticalEdge(grid[row][col - 1], contender))
                return false;

            // check right edge
            if (col + 1 < grid[row].Length && !CheckVerticalEdge(contender, grid[row][col + 1]))
                return false;

            // check top edge
            if (row - 1 >= 0 && !CheckHorizontalEdge(grid[row - 1][col], contender))
                return false;

            // check bottom edge
            if (row + 1 < grid.Length && !CheckHorizontalEdge(contender, grid[row+1][col]))
                return true;

            return true;

            bool CheckHorizontalEdge(Tile top, Tile bottom)
            {
                // if an edge hasn't been set then return true
                if (top is null || bottom is null)
                    return true;

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
                // if an edge hasn't been set then return true
                if (left is null || right is null)
                    return true;

                int rows = left.Image.Length;
                for (int row = 0; row < rows; row++)
                {
                    if (left.Image[row][left.Image.Length - 1] != right.Image[row][0])
                        return false;
                }

                return true;
            }
        }

        private static Tile[][] CreateEmptyGrid(IEnumerable<Tile> tiles)
        {
            int gridSize = (int)Math.Sqrt(tiles.Count());
            return Enumerable.Range(0, gridSize).Select(i => new Tile[gridSize]).ToArray();
        }

        private static void OutputTiles(IEnumerable<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                Console.WriteLine($"Tile {tile.Number}:");

                foreach (var line in tile.Image)
                {
                    Console.WriteLine(string.Join('\0', line));
                }
                Console.WriteLine();
            }
        }

        private static void OutputGrid(Tile[][] grid)
        {
            for (int row = 0; row < grid.Length; row++)
            {
                int rows = -1;
                while (rows < grid[row][0].Image.Length)
                {
                    for (int col = 0; col < grid.Length; col++)
                    {
                        var tile = grid[row][col];

                        if (rows == -1)
                            Console.Write($"Tile {tile.Number}: ");
                        else
                            Console.Write(string.Join('\0', tile.Image[rows]) + " ");
                    }
                    rows++;
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }

        [DebuggerDisplay("{DebuggerDisplay,nq}")]
        private class Tile
        {
            public long Number { get; set; }

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

				int top = 0, bottom = tile.Image.Length-1;
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

            private string DebuggerDisplay => $"{Number}";
        }

        private static IEnumerable<char[][]> GenerateArrangements(char[][] image)
        {
            yield return image;
            char[][] current = image;
            for (int i = 0; i < 3; i++)
            {
                current = Rotate(current);
                yield return current;
            }

            current = Flip(current);
            yield return current;
            for (int i = 0; i < 3; i++)
            {
                current = Rotate(current);
                yield return current;
            }
        }

        private static char[][] Rotate(char[][] image)
        {
            char[][] rotated = new char[image.Length][];

            for (int i = 0; i < image.Length; ++i)
            {
                for (int j = 0; j < image.Length; ++j)
                {
                    if (rotated[i] == null) rotated[i] = new char[image[j].Length];
                    rotated[i][j] = image[image.Length - j - 1][i];
                }
            }

            return rotated;
        }

        private static char[][] Flip(char[][] image)
        {
            char[][] flipped = new char[image.Length][];

            int top = 0, bottom = image.Length - 1;
            while (top < bottom)
            {
                for (int col = 0; col < image.Length; col++)
                {
                    if (flipped[top] == null) flipped[top] = new char[image[top].Length];
                    if (flipped[bottom] == null) flipped[bottom] = new char[image[bottom].Length];

                    flipped[top][col] = image[bottom][col];
                    flipped[bottom][col] = image[top][col];
                }

                top++;
                bottom--;
            }

            return flipped;
        }
    }
}
