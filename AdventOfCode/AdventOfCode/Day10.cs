using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day10
	{
		public static void Execute()
		{
			var arr = File
				.ReadAllLines("inputs/Day 10/input.txt")
				.Select(x => Convert.ToInt32(x))
				.ToArray();
			Array.Sort(arr);

			Console.WriteLine("Executing Day 10.");

			Part1(arr);
			Part2(arr);

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
			long[] adapters = new long[arr.Length + 1];
			Array.Copy(arr, 0, adapters, 1, arr.Length);

			long[] dp = new long[arr.Length+1];
			dp[0] = 1; // this is acting as 0!

            for (int i = 1; i < adapters.Length; i++)
            {
                for (int j = 1; j <= 3 && i - j >= 0; j++)
                {
                    if (adapters[i] - adapters[i - j] <= 3)
                        dp[i] += dp[i-j];
                }
            }

			Console.WriteLine($"What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device?");
			Console.WriteLine($"{dp[arr.Length]}");
		}
	}
}
