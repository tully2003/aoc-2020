using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class Day21
	{
		public static void Execute()
		{
			var input = File
				.ReadAllLines("inputs/Day 21/input.txt")
				.Select(x =>
				{
					int parenIndex = x.IndexOf('(');
					List<string> ingredients = x.Substring(0, parenIndex).Trim().Split(' ').ToList();
					List<string> allergens = parenIndex == -1 ? new List<string>() : 
						x.Substring(parenIndex)
							.Replace("contains", string.Empty)
							.Trim('(', ')')
							.Replace(",", string.Empty)
							.Trim()
							.Split(' ')
							.ToList();

					return (ingredients, allergens);
				})
				.ToArray();

			Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
			Dictionary<string, int> ing = new Dictionary<string, int>();

			foreach (var (ingredients, allergens) in input)
            {
				foreach (var allergen in allergens)
                {
					if (!dict.ContainsKey(allergen))
						dict[allergen] = ingredients;

					dict[allergen] = dict[allergen].Intersect(ingredients).ToList();
                }

				foreach (var ingredient in ingredients)
				{
					if (!ing.ContainsKey(ingredient))
						ing[ingredient] = 0;

					ing[ingredient] += 1;
				}
			}

			// ingredients without allergens
			foreach (string ingredient in dict.Values.SelectMany(s => s))
				if (ing.ContainsKey(ingredient)) ing.Remove(ingredient);

			Console.WriteLine("Part 1: ---------");
			Console.WriteLine("How many times do any of those ingredients appear?");
			Console.WriteLine(ing.Sum(x => x.Value));

			Dictionary<string, string> dangerList = new Dictionary<string, string>();
			while (dict.Values.Any(x => x.Count() > 1) || dict.Count != dangerList.Count)
			{
				var allergens = dict.Keys.ToList();
				foreach (var allergen in allergens)
				{
					var ingredients = dict[allergen];
					if (ingredients.Count() == 1)
						dangerList[allergen] = ingredients.Single();
					else
						dict[allergen] = ingredients.Except(dangerList.Values).ToList();
				}
			}

			Console.WriteLine("Part 2: ---------");
			Console.WriteLine("What is your canonical dangerous ingredient list?");
			Console.WriteLine(string.Join(',', dangerList.OrderBy(x => x.Key).Select(x => x.Value)));
		}
	}
}