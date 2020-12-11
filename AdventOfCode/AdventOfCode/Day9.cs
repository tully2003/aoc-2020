using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class Day9
	{
		public static void Execute()
		{
			var arr = File
				.ReadAllLines("inputs/Day 9/input.txt")
				.Select(x => Convert.ToInt64(x))
				.ToArray();

			Console.WriteLine("Executing Day 9.");

			Part1(arr);
			//Part2(arr);

			Console.WriteLine("Finished Day 9.");
		}

		private static void Part1(long[] arr)
		{
			int preambleLength = 25;
			for (int i = preambleLength; i < arr.Length; i++)
			{
				long[] preamble = new long[preambleLength];
				Array.Copy(arr, i - preambleLength, preamble, 0, preambleLength);
				long required = arr[i];
				if (!TwoSum(preamble, required))
				{
					Console.WriteLine($"{required}");
					Part2(arr, required);
					break;
				}
			}
		}

		private static bool TwoSum(long[] arr, long required)
		{
			HashSet<long> hash = new HashSet<long>();
			for (int i = 0; i < arr.Length; i++)
			{
				if (hash.Contains(required - arr[i]))
					return true;

				hash.Add(arr[i]);
			}
			return false;
		}

		//https://www.geeksforgeeks.org/number-subarrays-sum-exactly-equal-k/

		private static void Part2(long[] arr, long target)
		{
			const int MIN_SIZE = 2;
			long currentSum = 0;
			int start = 0;

			for (int end = 0; end < arr.Length; end++)
			{
				currentSum += arr[end];

				while (currentSum > target && (end-start) >= MIN_SIZE)
				{
					currentSum -= arr[start];
					start++;
				}

				if (currentSum == target)
				{
					long min = long.MaxValue, max = long.MinValue;

					for (int i = start; i <= end; i++)
					{
						min = Math.Min(arr[i], min);
						max = Math.Max(arr[i], max);
					}

					Console.WriteLine($"{target} found. {string.Join(',', arr[start .. (end+1)])}");
					Console.WriteLine($"{min}+{max}={min + max}");
					return;
				}
			}
		}
	}
}
