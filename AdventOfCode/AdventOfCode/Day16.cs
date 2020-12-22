using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	public class Day16
	{
		public static void Execute()
		{
			var input = File
				.ReadAllText("inputs/Day 16/input.txt")
				.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

			// process rules
			var rules = ParseRules(input[0]);
			var myTicket = ParseMyTicket(input[1]);
			var nearbyTickets = ParseNearbyTickets(input[2]);

			SolvePart1(rules, myTicket, nearbyTickets);
			SolvePart2(rules, myTicket, nearbyTickets);

			Console.ReadLine();
		}

        private static void SolvePart1(IReadOnlyDictionary<string, (Range Low, Range High)> rules, int[] myTicket, int[][] nearbyTickets)
        {
			List<int> errors = new List<int>();

			for (int i = 0; i < nearbyTickets.Length; i++)
            {
				for (int j = 0; j < nearbyTickets[i].Length; j++)
				{
					int ticket = nearbyTickets[i][j];
					bool valid = false;

					foreach (var rule in rules)
                    {
						if (rule.Value.Low.In(ticket) || rule.Value.High.In(ticket))
                        {
							valid = true;
							break;
						}
                    }

					if (!valid) errors.Add(ticket);
				}
			}

			Console.WriteLine("Part 1: ------------");
			Console.WriteLine("Consider the validity of the nearby tickets you scanned. What is your ticket scanning error rate?");
			Console.WriteLine($"{string.Join(" + ", errors)} = {errors.Sum()}");
		}

		private static void SolvePart2(IReadOnlyDictionary<string, (Range Low, Range High)> rules, int[] myTicket, int[][] nearbyTickets)
		{
			List<List<int>> validTickets = GetValidTickets();
			var rulePositions = rules.ToDictionary(x => x.Key, x => Enumerable.Range(0, validTickets[0].Count).ToList());

			// so if you start with each rule able to use every position
			// then go through every position and if the rule doesn't match
			// eliminate that position?

			for (int i = 0; i < validTickets.Count; i++)
			{
				for (int j = 0; j < validTickets[i].Count; j++)
				{
					int ticket = validTickets[i][j];

					foreach (var (rule, (low, high)) in rules)
					{
						if (!low.In(ticket) && !high.In(ticket))
						{
							// we can remove that position
							rulePositions[rule] = rulePositions[rule].Except(new[] { j }).ToList();
						}
					}
				}
			}

			// at this point we have the basis of the list it just needs fixing up
			Dictionary<string, int> finalPositions = new Dictionary<string, int>();
			while (rulePositions.Values.Any(x => x.Count() > 1) || rulePositions.Count != finalPositions.Count)
			{
				var rulez = rulePositions.Keys.ToList();
				foreach (var rule in rulez)
				{
					var positions = rulePositions[rule];
					if (positions.Count() == 1)
						finalPositions[rule] = positions.Single();
					else
						rulePositions[rule] = positions.Except(finalPositions.Values).ToList();
				}
			}

			long sum = 1;
			foreach (var (rule, position) in finalPositions.Where(x => x.Key.StartsWith("departure")))
				sum *= myTicket[position];

			Console.WriteLine("Part 2: ------------");
			Console.WriteLine("Once you work out which field is which, look for the six fields on your ticket that start with the word departure.");
			Console.WriteLine("What do you get if you multiply those six values together?");
			Console.WriteLine(sum);

			List<List<int>> GetValidTickets()
            {
				List<List<int>> validTickets = new List<List<int>>();

				for (int i = 0; i < nearbyTickets.Length; i++)
				{
					bool ticketValid = true;
					for (int j = 0; j < nearbyTickets[i].Length; j++)
					{
						bool valid = false;
						int ticket = nearbyTickets[i][j];

						foreach (var (rule, (low, high)) in rules)
						{
							if (low.In(ticket) || high.In(ticket))
							{
								valid = true;
								break;
							}
						}

						if (!valid)
						{
							ticketValid = false;
							break;
						}
					}

					if (ticketValid) validTickets.Add(nearbyTickets[i].ToList());
				}

				return validTickets;
			}
		}

		private static int[] ParseMyTicket(string input)
        {
			var components = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
			return components[1].Split(',').Select(x => int.Parse(x)).ToArray();
		}

		private static int[][] ParseNearbyTickets(string input)
		{
			var components = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
			return components[1..].Select(s => s.Split(',').Select(x => int.Parse(x)).ToArray()).ToArray();
		}

		private static IReadOnlyDictionary<string, (Range, Range)> ParseRules(string input)
        {
			return input.Split("\r\n").Select(ruleText =>
			{
				string[] components = ruleText.Split(':');
				string rule = components[0];

				string[] ranges = components[1].Trim().Split(" or ");
				Range low = new Range(int.Parse(ranges[0].Split('-')[0]), int.Parse(ranges[0].Split('-')[1]));
				Range high = new Range(int.Parse(ranges[1].Split('-')[0]), int.Parse(ranges[1].Split('-')[1]));

				return new { Key = rule, Value = (low, high) };

			}).ToDictionary(x => x.Key, x => x.Value);
		}

		private class Range
        {
            public Range(int low, int high)
            {
                Low = low;
                High = high;
            }

            public int Low { get; }

            public int High { get; }

			public bool In(int value) => value >= Low && value <= High;
        }
	}
}