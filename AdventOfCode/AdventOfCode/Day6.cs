using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day6
	{
		public static void Execute()
		{
			List<Dictionary<string, string>> forms = new List<Dictionary<string, string>>();

			// part 1
			var groups = File
				.ReadAllText("inputs/Day 6/input.txt")
				.Split("\n\n")
				.Select(x => ParseGroup(x));
				
			Console.WriteLine($"Part 1 -> Sum of group counts = {groups.Sum(x => x.AnsweredYes)}");
			Console.WriteLine($"Part 2 -> Sum of group counts = {groups.Sum(x => x.EveryoneAnsweredYes)}");
		}

		public static Group ParseGroup(string raw)
		{
			var group = new Group();
			
			var members = raw.Split('\n');
			group.Count = members.Length;

			foreach (string member in members)
				for (int i = 0; i < member.Length; i++)
					group.AddAnswer(member[i]);

			return group;
		}

		public class Group
        {
			private readonly Dictionary<char, int> answers = new Dictionary<char, int>();

			public int Count { get; set; }

			public IReadOnlyDictionary<char, int> Answers => answers;

			public int AnsweredYes => Answers.Keys.Count();
			
			public int EveryoneAnsweredYes => Answers.Keys.Count(x => Answers[x] == Count);

			public void AddAnswer(char c)
            {
				if (!answers.ContainsKey(c))
					answers[c] = 0;

				answers[c] += 1;
			}
		}
	}
}
