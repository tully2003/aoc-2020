using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class Day1
	{
		public static void Execute()
		{
			var arr = File
				.ReadAllLines("inputs/Day 1/input.txt")
				.Select(x => Convert.ToInt32(x))
				.ToArray();

			Console.WriteLine("Executing Day 1.");

			Part1(arr);
			Part2(arr);

			Console.WriteLine("Finished Day 1.");
		}

		private static void Part1(int[] arr)
		{
			const int REQUIRED_SUM = 2020;
			HashSet<int> hash = new HashSet<int>();

			for (int i = 0; i < arr.Length; i++)
			{
				if (hash.Contains(REQUIRED_SUM - arr[i]))
				{
					int x = REQUIRED_SUM - arr[i], y = arr[i];
					Console.WriteLine($"{x}+{y}={REQUIRED_SUM}");
					Console.WriteLine($"{x}*{y}={x * y}");
					return;
				}
				hash.Add(arr[i]);
			}
		}

		private static void Part2(int[] arr)
		{
			const int REQUIRED_SUM = 2020;
			Array.Sort(arr);

			// needs converting to 3sum
			for (int i = 0; i < arr.Length - 2; i++)
			{
				int l = i + 1, r = arr.Length - 1;

				while (l < r)
				{
					if (arr[i] + arr[l] + arr[r] == REQUIRED_SUM)
					{
						Console.WriteLine($"{arr[i]}+{arr[l]}+{arr[r]}=2020!");
						Console.WriteLine($"{arr[i]}*{arr[l]}*{arr[r]}={arr[i] * arr[l] * arr[r]}!");
						return;
					}
					else if (arr[i] + arr[l] + arr[r] < REQUIRED_SUM)
					{
						l++;
					}
					else
					{
						r--;
					}
				}
			}
		}
	}
}
