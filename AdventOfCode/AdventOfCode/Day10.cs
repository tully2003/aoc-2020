using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class Day10
	{
		public static void Execute()
		{
			var arr = File
				.ReadAllLines("inputs/Day 10/input.test.txt")
				.Select(x => Convert.ToInt32(x))
				.ToArray();
			Array.Sort(arr);

			Console.WriteLine("Executing Day 10.");

			Part1(arr);
			Part2(arr);
			//var permutations = permute(arr);
			//Console.WriteLine($"What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device? {permutations.Count}");

			Console.WriteLine("Finished Day 10.");
		}

		public static void Part1(int[] arr)
		{
			int onejoltCount = 0, threejoltCount = 1;
			Diff(0, arr[0]);
			
			for (int i = 0; i < arr.Length-1; i++)
			{
				Diff(arr[i], arr[i + 1]);
			}

			Console.WriteLine("What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?");
			Console.WriteLine($"1jolt = {onejoltCount}, 3jolt = {threejoltCount} => {onejoltCount}*{threejoltCount} = {onejoltCount * threejoltCount}");

			void Diff(int l, int r)
			{
				int diff = r - l;

				switch (diff)
				{
					case 1:
						onejoltCount++;
						break;
					case 3:
						threejoltCount++;
						break;
				};
			}
		}

		public static void Part2(int[] arr)
		{
			// curr + 1 == 1/2/3 -> 1
			// curr + 2 == 2/3 -> 2
			// curr + 3 == 3 -> 3

			// curr + 

			//arr = new int[] { 1, 2, 4 };
			//arr = new int[] { 1, 2, 4, 5, 6, 7 };
			//int[] dp = new int[arr.Length];
			//dp[0] = 1;
			//dp[1] = 2;
			//dp[2] = 2;

			//for (int i = 3; i < arr.Length; i++)
			//{
			//	for (int j = 1; j < 3; j++)
			//	{
			//		if (arr[i] - arr[i - j] <= 3)
			//			dp[i] +=1;
			//	}
			//	// add the previous two to yourself!
			//	dp[i] = dp[i] + dp[i - 1]; // + dp[i - 2];
			//}

			//Console.WriteLine($"What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device? {dp[arr.Length-1]}");

			Console.WriteLine($"What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device? {ClimbStairs(arr)}");

			// works but too inefficient
			long total = 0; // = dp[arr.Length];
			Backtrack(arr, 0, 0, ref total);
			Console.WriteLine($"What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device? {total}");
		}

		public static void Backtrack(int[] arr, int index, int prev, ref long total)
		{
			if (index == arr.Length)
			{
				//Console.WriteLine(string.Join(',', temp));
				total += 1;
			}
			else if (arr[index] - prev > 3)
				return;
			else
			{
				for (int i = index; i < arr.Length; i++)
				{
					if (arr[i] - prev <= 3)
					{
						Backtrack(arr, i + 1, arr[i], ref total);
					}
					else
					{
						break;
					}
				}
			}
		}

		public static int ClimbStairs(int[] arr)
		{
			return ClimbStairs(0, arr);
		}

		private static int ClimbStairs(int i, int[] arr)
		{
			if (i == arr.Length)
				return 1;
			if (i > arr.Length)
				return 0;
			return ClimbStairs(i + 1, arr) + ClimbStairs(i + 2, arr);
		}
	}
}
