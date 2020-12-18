using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	public class Day17
	{
		public static void Execute()
		{
			var input = File.ReadAllLines("inputs/Day 17/input.txt").Select(s => s.ToCharArray()).ToArray();

			HashSet<Cube> cubes = new HashSet<Cube>();
			for (int y = 0; y < input.Length; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					cubes.Add(new Cube(x, y, value: input[y][x]));
				}
			}

			SolvePart1(CloneConwayCube(cubes));
			SolvePart2(CloneConwayCube(cubes));
		}

		public static void SolvePart1(HashSet<Cube> conwayCube)
        {
			Console.WriteLine("\nPart 1 ----------");
			Console.WriteLine("Before any cycles:");
			//OutputHyperCube(cubes);

			int cycles = 6;
			int currentCycle = 0;
			while (currentCycle < cycles)
			{
				// first let's build all neighbours for our current position
				foreach (var cube in conwayCube.ToList())
					AddNeighbours(cube, conwayCube);

				// now take a clone of the base set of cubes
				var cubes = CloneConwayCube(conwayCube);
				foreach (var cube in cubes)
				{
					int activeNeighbourCount = GetActiveNeighbourCount(cube, conwayCube);

					if (cube.Active && !(activeNeighbourCount == 2 || activeNeighbourCount == 3))
						cube.Value = '.';
					else if (!cube.Active && activeNeighbourCount == 3)
						cube.Value = '#';
				}

				currentCycle++;
				conwayCube = new HashSet<Cube>(cubes.Where(x => x.Active));
				Console.WriteLine($"After {currentCycle} cycle: {conwayCube.Count(x => x.Active)}");
				//OutputHyperCube(cubes);
			}

			void AddNeighbours(Cube cube, HashSet<Cube> cubes)
			{
				// how to loop all positions
				for (int x = cube.X - 1; x <= cube.X + 1; x++)
				{
					for (int y = cube.Y - 1; y <= cube.Y + 1; y++)
					{
						for (int z = cube.Z - 1; z <= cube.Z + 1; z++)
						{
							var neighbour = new Cube(x, y, z);
							if (neighbour.Equals(cube))
								continue;

							if (!cubes.Contains(neighbour))
								cubes.Add(neighbour);
						}
					}
				}
			}

			int GetActiveNeighbourCount(Cube cube, HashSet<Cube> cubes)
			{
				int count = 0;
				int checks = 0;

				// how to loop all positions
				for (int x = cube.X - 1; x <= cube.X + 1; x++)
				{
					for (int y = cube.Y - 1; y <= cube.Y + 1; y++)
					{
						for (int z = cube.Z - 1; z <= cube.Z + 1; z++)
						{
							checks++;
							var neighbour = new Cube(x, y, z);
							if (neighbour.Equals(cube) || !cubes.TryGetValue(neighbour, out Cube ccc))
								continue;

							if (ccc.Active)
								count++;
						}
					}
				}

				return count;
			}
		}

		public static void SolvePart2(HashSet<Cube> conwayCube)
		{
			Console.WriteLine("\nPart 2 ----------");
			Console.WriteLine("Before any cycles:");
			//OutputHyperCube(cubes);

			int cycles = 6;
			int currentCycle = 0;
			while (currentCycle < cycles)
			{
				// first let's build all neighbours for our current position
				foreach (var cube in conwayCube.ToList())
					AddNeighbours(cube, conwayCube);

				// now take a clone of the base set of cubes
				var cubes = CloneConwayCube(conwayCube);
				foreach (var cube in cubes)
				{
					int activeNeighbourCount = GetActiveNeighbourCount(cube, conwayCube);

					if (cube.Active && !(activeNeighbourCount == 2 || activeNeighbourCount == 3))
						cube.Value = '.';
					else if (!cube.Active && activeNeighbourCount == 3)
						cube.Value = '#';
				}

				currentCycle++;
				conwayCube = new HashSet<Cube>(cubes.Where(x => x.Active));
				Console.WriteLine($"After {currentCycle} cycle: {conwayCube.Count(x => x.Active)}");
				//OutputHyperCube(cubes);
			}

			void AddNeighbours(Cube cube, HashSet<Cube> cubes)
			{
				// how to loop all positions
				for (int x = cube.X - 1; x <= cube.X + 1; x++)
				{
					for (int y = cube.Y - 1; y <= cube.Y + 1; y++)
					{
						for (int z = cube.Z - 1; z <= cube.Z + 1; z++)
						{
							for (int w = cube.W - 1; w <= cube.W + 1; w++)
							{
								var newNeighbour = new Cube(x, y, z, w);
								if (newNeighbour.Equals(cube))
									continue;

								if (!cubes.Contains(newNeighbour))
									cubes.Add(newNeighbour);
							}
						}
					}
				}
			}

			int GetActiveNeighbourCount(Cube cube, HashSet<Cube> cubes)
			{
				int count = 0;

				// how to loop all positions
				for (int x = cube.X - 1; x <= cube.X + 1; x++)
				{
					for (int y = cube.Y - 1; y <= cube.Y + 1; y++)
					{
						for (int z = cube.Z - 1; z <= cube.Z + 1; z++)
						{
							for (int w = cube.W - 1; w <= cube.W + 1; w++)
							{
								var newNeighbour = new Cube(x, y, z, w);
								if (newNeighbour.Equals(cube) || !cubes.TryGetValue(newNeighbour, out Cube neighbour))
									continue;

								if (neighbour.Active)
									count++;
							}
						}
					}
				}

				return count;
			}
		}

		private static void OutputCube(HashSet<Cube> cubes)
        {
			var cx = cubes.OrderBy(x => x.Z).ThenBy(x => x.Y).ThenBy(x => x.X).ToList();

			int z = -1000;
			int y = -1000;
			foreach (var cube in cx)
            {
				if (cube.Z != z)
				{
					Console.Write($"\n\nz={cube.Z}");
					z = cube.Z;
				}

				if (cube.Y != y)
				{
					Console.WriteLine();
					y = cube.Y;
				}

				Console.Write(cube.Value);
			}

			Console.WriteLine();
			Console.WriteLine();
		}

		private static HashSet<Cube> CloneConwayCube(HashSet<Cube> cubes) => new HashSet<Cube>(cubes.Select(c => c.Clone()));
		
		[DebuggerDisplay("{DebuggerDisplay,nq}")]
		public class Cube
		{
            public Cube(int x, int y, int z = 0, int w = 0, char value = '.')
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
				Value = value;
            }

			public int X { get; }
			public int Y { get; }
			public int Z { get; }
			public int W { get; }
			public char Value { get; set; }
			public bool Active => Value == '#';

			public Cube Clone() => new Cube(X, Y, Z, W, Value);

			public override int GetHashCode()
			{
				return (X, Y, Z, W).GetHashCode();
			}

			public override bool Equals(object obj)
			{
				var cube = obj as Cube;
				if (cube == null) return false;

				return (X, Y, Z, W) == (cube.X, cube.Y, cube.Z, cube.W);
			}

			private string DebuggerDisplay => $"X = {X}, Y = {Y}, Z = {Z}, W = {W} => {Value}";
		}
	}
}