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

			var lines = File
				.ReadAllLines("inputs/Day 4/input.txt")
				.Select(x =>
				{

				});
			
		}

		private static void Part1(IEnumerable<Dictionary<string, string>> passports)
		{
		}

		private static void Part2(IEnumerable<Dictionary<string, string>> passports)
		{
		}

		private static int CalculateSeatId(string seat)
		{
			int rowMin = 0, rowMax = 127;

			for(int i = 0; i< s)
		}

	}
}
