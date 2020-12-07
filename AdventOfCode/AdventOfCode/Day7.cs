using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day7
	{
		public static void Execute()
		{
			List<Dictionary<string, string>> forms = new List<Dictionary<string, string>>();

			var input = File
				.ReadAllLines("inputs/Day 7/input.txt");

			//Regex r = new Regex("(.*) bags contain (([\d]+) (([\w]+) bag[s]?))+\.$")
		}

		private class Node
        {
			public Node(string colour) => Colour = colour;
            
			public string Colour { get; }

			public IList<Node> Parents { get; } = new List<Node>();

			public IList<Node> Children { get; } = new List<Node>();
		}
	}
}
