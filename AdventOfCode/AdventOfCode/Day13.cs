using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	public class Day13
	{
		public static void Execute()
		{
			var input = File
				.ReadAllLines("inputs/Day 13/input.txt");

			SolvePart1(input);
			SolvePart2(input[1]);

			Console.ReadLine();
		}

		public static void SolvePart1(string[] input)
		{
			int earliestDepartureTimestamp = int.Parse(input[0]);
			int[] busIds = input[1].Split(',').Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToArray();

			int minWaitingTime = int.MaxValue;
			int earliestBus = -1;
			for (int i = 0; i < busIds.Length; i++)
			{
				int bus = busIds[i];
				int lastDeparture = ((earliestDepartureTimestamp / bus) * bus);
				int nextDeparture = lastDeparture + bus;
				int waitingTime = nextDeparture - earliestDepartureTimestamp;

				if (waitingTime < minWaitingTime)
				{
					minWaitingTime = waitingTime;
					earliestBus = bus;
				}
			}

			Console.WriteLine("Part 1 -------------");
			Console.WriteLine("What is the ID of the earliest bus you can take to the airport multiplied by the number of minutes you'll need to wait for that bus?");
			Console.WriteLine($"{earliestBus} * {minWaitingTime} = {earliestBus * minWaitingTime}");
		}

		public static long SolvePart2(string input)
		{
			long[] busIds = input.Split(',').Select(x => (x == "x") ? 1 : long.Parse(x)).ToArray();

			long @base = 0;
			while (true)
			{
				long current = @base;
				long lcm = 1;

				for (int i = 0; i <= busIds.Length; i++)
				{
					if (i == busIds.Length)
					{
						Console.WriteLine("Part 2 -------------");
						Console.WriteLine("What is the earliest timestamp such that all of the listed bus IDs depart at offsets matching their positions in the list?");
						Console.WriteLine(@base);
						return @base;
					}

					long bus = busIds[i];
					if (current % bus == 0)
					{
						lcm = LCM(busIds[0..(i + 1)]);
					}
					else
						break;

					current++;
				}

				@base += lcm;
			}
		}

		private static long LCM(params long[] arr)
		{
			if (arr.Length == 1)
				return arr[0];

			return lcm(arr[0], LCM(arr[1..arr.Length]));

			long gcd(long a, long b)
			{
				if (a == 0)
					return b;
				return gcd(b % a, a);
			}

			// method to return 
			// LCM of two numbers
			long lcm(long a, long b)
			{
				return (a / gcd(a, b)) * b;
			}
		}
	}
}