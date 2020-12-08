using System;
using System.IO;

namespace AdventOfCode
{
	public class Day8
	{
		public static void Execute()
		{
			var input = File
				.ReadAllLines("inputs/Day 8/input.txt");

			Program.Run(input);

			Console.WriteLine(new string('-', 10));

			for (int i = 0; i < input.Length; i++)
			{
				string operation = input[i].Substring(0, 3);
				string[] instructions = new string[input.Length];

				if (operation == "jmp" || operation == "nop")
				{
					Array.Copy(input, instructions, input.Length);

					if (operation == "jmp")
						instructions[i] = instructions[i].Replace(operation, "nop");
					else if (operation == "nop")
						instructions[i] = instructions[i].Replace(operation, "jmp");

					if (Program.Run(instructions))
					{
						Console.WriteLine("Uncorrupted");
						break;
					}
				}
			}

			Console.ReadLine();
		}

		private class Program
		{
			public static bool Run(string[] instructions)
			{
				int accumulator = 0, current = 0;
				bool[] visited = new bool[instructions.Length];

				// boot program
				while (!visited[current])
				{
					visited[current] = true;
					string instruction = instructions[current];
					string operation = instruction.Substring(0, 3);
					int argument = int.Parse(instruction.Substring(4));

					switch (operation)
					{
						case "acc":
							accumulator += argument;
							current++;
							break;
						case "jmp":
							current += argument;
							break;
						case "nop":
							current++;
							break;
						default:
							throw new InvalidOperationException($"Unknown argument: {operation}");
					}

					// should terminate by attempting to execute an instruction immediately after the last instruction in the file
					if (current >= instructions.Length)
						break;
				}

				Console.WriteLine($"Value in the accumulator => {accumulator}");

				return current == instructions.Length;
			}
		}
	}
}