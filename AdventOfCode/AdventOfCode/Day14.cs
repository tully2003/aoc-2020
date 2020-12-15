using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public class Day14
	{
		private static readonly Regex s_maskRegex = new Regex("^mask = (?<mask>[01X]{36})$", RegexOptions.Compiled);
		private static readonly Regex s_memoryRegex = new Regex(@"^mem\[(?<address>\d+)\] = (?<value>\d+)$", RegexOptions.Compiled);

		public static void Execute()
		{
			var input = File
				.ReadAllLines("inputs/Day 14/input.test.part2.txt");

			SolvePart1(input);
			SolvePart2(input);

			Console.ReadLine();
		}

		public static void SolvePart1(string[] input)
		{
			Dictionary<int, ulong> memory = new Dictionary<int, ulong>();
			string mask = new string('X', 36);
			
			foreach (string line in input)
			{
				var maskMatch = s_maskRegex.Match(line);
				if (maskMatch.Success)
				{
					mask = maskMatch.Groups["mask"].Value;
					continue;
				}

				var memMatch = s_memoryRegex.Match(line);
				if (memMatch.Success)
				{
					int address = int.Parse(memMatch.Groups["address"].Value);
					ulong value = ulong.Parse(memMatch.Groups["value"].Value);
					memory[address] = DecoderChipV1.ApplyMask(mask, value);
				}
			}

			Console.WriteLine("Part 1 -------------");
			Console.WriteLine("What is the sum of all values left in memory after it completes?");
			Console.WriteLine($"{memory.Values.Sum(x => (decimal)x)}");
		}

		public static void SolvePart2(string[] input)
		{
			Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();
			string mask = new string('X', 36);

			foreach (string line in input)
			{
				var maskMatch = s_maskRegex.Match(line);
				if (maskMatch.Success)
				{
					mask = maskMatch.Groups["mask"].Value;
					continue;
				}

				var memMatch = s_memoryRegex.Match(line);
				if (memMatch.Success)
				{
					ulong address = ulong.Parse(memMatch.Groups["address"].Value);
					ulong value = ulong.Parse(memMatch.Groups["value"].Value);

					var addresses = GetAddressesToWriteTo(mask, address);
					foreach (var addr in addresses)
						memory[addr] = value;
				}
			}

			Console.WriteLine("Part 2 -------------");
			Console.WriteLine("What is the sum of all values left in memory after it completes?");
			Console.WriteLine($"{memory.Values.Sum(x => (decimal)x)}");
		}

		public static IEnumerable<ulong> GetAddressesToWriteTo(string mask, ulong address)
		{
			// first we have to generate all addresses
			// you're actually generating masks?
			Stack<string> s = new Stack<string>();
			s.Push(DecoderChipV2.ApplyMask(mask, address));

			List<string> addresses = new List<string>();
			while (s.Count > 0)
			{
				string current = s.Pop();
				int index = current.IndexOf('X');
				if (index > -1)
				{
					char[] chars = current.ToCharArray();
					chars[index] = '0';
					s.Push(new string(chars));
					chars[index] = '1';
					s.Push(new string(chars));
				}
				else
				{
					masks.Add(current);
				}
			}

			List<ulong> addresses = new List<ulong>();
			foreach (string msk in masks)
			{
				addresses.Add();
			}
			return addresses;
		}

		public class DecoderChipV1
		{
			public static ulong ApplyMask(string mask, ulong value)
			{
				for (int i = 0; i < mask.Length; i++)
				{
					if (mask[i] != 'X')
						value = ChangeBit(value, mask.Length - 1 - i, mask[i] == '1');
				}

				return value;
			}

			private static ulong ChangeBit(ulong number, int bitLocation, bool bit)
			{
				// If indexed bit and parameter bit are different
				if ((number & (ulong)(1UL << bitLocation)) != 0 ^ bit)
				{
					number = number ^ (ulong)(1UL << bitLocation);
				}
				return number;
			}
		}

		public class DecoderChipV2
		{
			public static string ApplyMask(string mask, ulong value)
			{
				char[] chars = mask.ToCharArray();
				for (int i = 0; i < mask.Length; i++)
				{
					if (mask[i] == '1')
						value = ChangeBit(value, mask.Length - 1 - i, true);
				}

				return value;
			}
		}
	}
}