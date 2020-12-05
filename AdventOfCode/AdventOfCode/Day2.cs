using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public class Day2
	{
		public static void Execute()
		{
			var arr = File
				.ReadAllLines("inputs/Day 2/input.txt")
				.Select(x =>
				{
					Regex regex = new Regex(@"^(?<low>\d+)-(?<high>\d+) (?<char>[a-z]): (?<password>.*)$");
					var match = regex.Match(x);

					PasswordPolicy policy = new PasswordPolicy
					{
						C = match.Groups["char"].Value[0],
						Low = Convert.ToInt32(match.Groups["low"].Value),
						High = Convert.ToInt32(match.Groups["high"].Value),
					};

					string password = match.Groups["password"].Value;

					return (policy, password);
				})
				.ToArray();

			Console.WriteLine("Executing Day 2.");

			Part1(arr);
			Part2(arr);

			Console.WriteLine("Finished Day 2.");
		}

		private static void Part1(IEnumerable<(PasswordPolicy policy, string pasword)> arr)
		{
			int valid = 0;
			
			foreach (var (policy, password) in arr)
			{
				int count = 0;
				for (int i = 0; i < password.Length; i++)
				{
					if (password[i] == policy.C)
						count++;
				}

				if (count >= policy.Low && count <= policy.High)
					valid++;
			}

			Console.WriteLine($"{valid} valid passwords");
		}

		private static void Part2(IEnumerable<(PasswordPolicy policy, string pasword)> arr)
		{
			int valid = 0;

			foreach (var (policy, password) in arr)
			{
				if (password[policy.Low-1] == policy.C && password[policy.High - 1] != policy.C)
					valid++;
				else if (password[policy.Low - 1] != policy.C && password[policy.High - 1] == policy.C)
					valid++;
			}

			Console.WriteLine($"{valid} valid passwords");
		}



		private class PasswordPolicy
		{
			public char C { get; set; }
			public int Low { get; set; }
			public int High { get; set; }
		}
	}
}
