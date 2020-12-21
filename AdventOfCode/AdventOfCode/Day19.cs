using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class Day19
	{
		public static void Execute()
		{
			var input = File
				.ReadAllText("inputs/Day 19/input.txt")
				.Split("\n\n")
				.Select(x => x.Split('\n'))
				.ToArray();

			var rules = ParseRules(input[0]);
			var messages = input[1];
			SolvePart1(rules, messages);

			input = File
				.ReadAllText("inputs/Day 19/input.2.txt")
				.Split("\n\n")
				.Select(x => x.Split('\n'))
				.ToArray();
			rules = ParseRules(input[0]);
			messages = input[1];
			SolvePart2(rules, messages);
		}

		private static void SolvePart1(Dictionary<int, Rule> rules, string[] messages)
		{
			int matches = 0;

			for (int i = 0; i < messages.Length; i++)
			{
				if (IsMatch(messages[i]))
					matches++;
			}

			Console.WriteLine("Part 1 ------");
			Console.WriteLine("How many messages completely match rule 0?");
			Console.WriteLine(matches);

			bool IsMatch(string message)
			{
				Rule rule = rules[0];
				var perms = rule.Permutations;
				return perms.Contains(message);
			}
		}

		private static void SolvePart2(Dictionary<int, Rule> rules, string[] messages)
        {
			// 0: 8 11
			// 8: 42 | 42 8
			// 11: 42 31 | 42 11 31
			Rule rule42 = rules[42];
			Rule rule31 = rules[31];

			HashSet<string> rule42Map = rule42.Permutations;
			HashSet<string> rule31Map = rule31.Permutations;

			int matches = 0;

			Console.WriteLine($"messages = {messages.Length}, <=24 {messages.Count(x => x.Length <= 24)}, >24 {messages.Count(x => x.Length > 24)}");

			foreach (string message in messages)
			{
				// if we start at the end and go down until we don't match 31
				if (!rule42Map.Contains(message.Substring(0, 8)) ||
					!rule42Map.Contains(message.Substring(8, 8)))
					continue;
				if (!rule31Map.Contains(message.Substring(message.Length - 8, 8)))
					continue;
				int minRequired42 = 0;
				int currentIndex = message.Length-8;

				while (currentIndex > (16 + minRequired42*8))
                {
					if (!rule31Map.Contains(message.Substring(currentIndex-8, 8)))
						break;

					minRequired42++;
					currentIndex -= 8;
				}

				bool match = true;
				while (currentIndex > 16)
                {
					if (!rule42Map.Contains(message.Substring(currentIndex - 8, 8)))
					{
						match = false;
						break;
					}

					currentIndex -= 8;
				}
				if (!match || minRequired42 > 0) continue;

				matches++;
            }

			Console.WriteLine("Part 2 ------");
			Console.WriteLine("After updating rules 8 and 11, how many messages completely match rule 0?");
			Console.WriteLine(matches);
		}

		private static Dictionary<int, Rule> ParseRules(string[] input)
        {
			var dict = new Dictionary<int, Rule>();

            for (int i = 0; i < input.Length; i++)
            {
				string[] components = input[i].Split(':');
				int ruleNumber = int.Parse(components[0]);
				
				if (!dict.ContainsKey(ruleNumber))
					dict[ruleNumber] = new Rule { Number = ruleNumber };

				Rule rule = dict[ruleNumber];
				string[][] ruleComponents = components[1].Trim().Split('|').Select(s => s.Trim().Split(' ')).ToArray();
				for (int j = 0; j < ruleComponents.Length; j++)
                {
					var subRules = new List<Rule>();
					rule.SubRules.Add(subRules);
					for (int k = 0; k < ruleComponents[j].Length; k++)
					{
						if (int.TryParse(ruleComponents[j][k], out int subRuleNumber))
                        {
							if (!dict.ContainsKey(subRuleNumber))
								dict[subRuleNumber] = new Rule { Number = subRuleNumber };

							subRules.Add(dict[subRuleNumber]);
						}
						else
                        {
							rule.Value = ruleComponents[j][k].Trim('"')[0];
						}
					}
				}
            }

			return dict;
        }

		public class Rule
        {
			private HashSet<string> _permutations = null;

			public int Number { get; set; }

			public char? Value { get; set; }

			public List<List<Rule>> SubRules { get; set; } = new List<List<Rule>>();

			public HashSet<string> Permutations => _permutations ?? (_permutations = GeneratePermutations());

			public HashSet<string> GeneratePermutations()
			{
				// this should build up all the valid messages based on the rule!
				HashSet<string> permutations = new HashSet<string>();

				if (Value.HasValue)
				{
					permutations.Add($"{Value}");
					return permutations;
				}

				foreach (var ruleSet in SubRules)
				{
					List<StringBuilder> messages = new List<StringBuilder>();

					foreach (var subRule in ruleSet)
					{
						List<StringBuilder> working = new List<StringBuilder>();

						foreach (var permutation in subRule.Permutations)
						{
							if (!messages.Any())
								working.Add(new StringBuilder(permutation));
							else
                            {
								foreach (var message in messages)
								{
									var sb = new StringBuilder(permutation);
									sb.Insert(0, message.ToString());
									working.Add(sb);
								}
							}
						}

						messages = working;
					}

					foreach (var message in messages)
						permutations.Add(message.ToString());
				}

				return permutations;
			}
		}
	}
}