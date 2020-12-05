using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public class Day5
	{
		public static void Execute()
		{
			List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();
			var passport = new Dictionary<string, string>();

			// part 1
			var seats = File
				.ReadAllLines("inputs/Day 5/input.txt")
				.Select(x => CalculateSeatId(x))
				.ToArray();

			Console.WriteLine($"highest seat ID = {seats.Max()}");

			
			Console.WriteLine($"my seat ID = {FindMySeat(seats)}");
		}

		public static int CalculateSeatId(string seat)
		{
			int rowMin = 0, rowMax = 127;
			int colMin = 0, colMax = 7;
			int row = (rowMin + rowMax) / 2, col = (colMin + colMax) / 2;

			for (int i = 0; i < 7 && rowMin <= rowMax; i++)
			{
				if (seat[i] == 'F')
					rowMax = row;
				else if (seat[i] == 'B')
					rowMin = row + 1;

				row = (rowMin + rowMax) / 2;
			}

			for (int i = 7; i < 10 && colMin <= colMax; i++)
			{
				if (seat[i] == 'L')
					colMax = col;
				else if (seat[i] == 'R')
					colMin = col + 1;

				col = (colMin + colMax) / 2;
			}

			return (row * 8) + col;
		}
	
		public static int FindMySeat(int[] seats)
        {
			// sort the seats step through until jump in numbers is > 1
			Array.Sort(seats);

			for (int i = 0; i < seats.Length; i++)
            {
				if (seats[i + 1] > seats[i] + 1)
					return seats[i] + 1;
			}

			return -1;
		}
	}
}
