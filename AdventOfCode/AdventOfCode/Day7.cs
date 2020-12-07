using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public class Day7
	{
		public static void Execute()
		{
			var graph = CreateBagGraph();

			var shinyGoldBag = graph["shiny gold"];

			Console.WriteLine($"{GetParentCount(shinyGoldBag)} bag colors contain at least one shiny gold bag");
			Console.WriteLine($"{GetChildBagCount(shinyGoldBag)} individual bags are required inside the single shiny gold bag");
		}

		private static IReadOnlyDictionary<string, Node> CreateBagGraph()
		{
			var input = File
				.ReadAllLines("inputs/Day 7/input.txt");

			Regex parentRegex = new Regex(@"(?<colour>.*) bags contain(?<children> [\d]+ [\w\s]+ bag[s]?[,\.])+$", RegexOptions.Compiled);
			Regex childRegex = new Regex(@"(?<number>[\d]+) (?<colour>[\w\s]+) bag[s]?[,\.]$", RegexOptions.Compiled);

			Dictionary<string, Node> dict = new Dictionary<string, Node>();

			foreach (string line in input)
			{
				var parentMatch = parentRegex.Match(line);
				if (parentMatch.Success)
				{
					var colour = parentMatch.Groups["colour"].Value;
					if (!dict.ContainsKey(colour))
						dict[colour] = new Node(colour);

					Node parent = dict[colour];

					foreach (Capture capture in parentMatch.Groups["children"].Captures)
					{
						var childMatch = childRegex.Match(capture.Value);

						if (childMatch.Success)
						{
							var childColour = childMatch.Groups["colour"].Value;
							var childCount = int.Parse(childMatch.Groups["number"].Value);

							if (!dict.ContainsKey(childColour))
								dict[childColour] = new Node(childColour);

							Node child = dict[childColour];

							parent.Children.Add((child, childCount));
							child.Parents.Add(parent);
						}
					}
				}
			}

			return dict;
		}

		private static int GetParentCount(Node root)
		{
			HashSet<string> visited = new HashSet<string>();

			var queue = new Queue<Node>();
			queue.Enqueue(root);

			while (queue.Count != 0)
			{
				Node node = queue.Dequeue();

				foreach (var parent in node.Parents)
				{
					if (!visited.Contains(parent.Colour))
					{
						visited.Add(parent.Colour);
						queue.Enqueue(parent);
					}
				}
			}

			return visited.Count;
		}

		private static int GetChildBagCount(Node root)
		{
			int count = 0;

			foreach (var child in root.Children)
				count += child.Count + (child.Count * GetChildBagCount(child.Node));

			return count;
		}

		private class Node
        {
			public Node(string colour) => Colour = colour;
            
			public string Colour { get; }

			public IList<Node> Parents { get; } = new List<Node>();

			public IList<(Node Node, int Count)> Children { get; } = new List<(Node, int)>();
		}
	}
}
