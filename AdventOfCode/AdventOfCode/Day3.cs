using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public class Day3
	{
		public static void Execute()
		{
			var map = File
				.ReadAllLines("inputs/Day 3/input.txt")
				.Select(x => x.ToCharArray())
				.ToArray();

			Console.WriteLine("Executing Day 3.");

			Part1(map);
			Part2(map);

			Console.WriteLine("Finished Day 3.");
		}

		private static void Part1(char[][] map)
		{
			Console.WriteLine($"{TreeCounter(map, 3, 1)} trees encountered");
		}

		private static void Part2(char[][] map)
		{
			long treesMultiple =
				TreeCounter(map, 1, 1) *
				TreeCounter(map, 3, 1) *
				TreeCounter(map, 5, 1) *
				TreeCounter(map, 7, 1) *
				TreeCounter(map, 1, 2);

			Console.WriteLine($"{treesMultiple}");
		}

		private static long TreeCounter(char[][] map, int right, int down)
		{
			int row = down, col = right;
			int trees = 0;

			while (row < map.Length)
			{
				if (map[row][col % map[row].Length] == '#')
					trees++;

				row += down;
				col += right;
			}

			Console.WriteLine($"Right {right}, down {down} - {trees} trees encountered");
			return trees;
		}
	}
}
