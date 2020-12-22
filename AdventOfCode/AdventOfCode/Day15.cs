using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day15
    {
        public static void Execute()
        {
            Solve(new int[] { 16, 12, 1, 0, 15, 7, 11 }, 2020);
            Solve(new int[] { 16, 12, 1, 0, 15, 7, 11 }, 30000000);

            Console.ReadLine();
        }

        public static int Solve(int[] input, int turns)
        {
            int turn = 1;
            var dict = new Dictionary<int, (int, int, bool)>();
            int lastNumberSpoken = -1;

            while (turn <= turns)
            {
                if (turn <= input.Length)
                {
                    if (!dict.ContainsKey(input[turn - 1]))
                        dict.Add(input[turn - 1], (turn, -1, true));
                    else
                        dict[input[turn - 1]] = (turn, dict[input[turn - 1]].Item1, false);

                    lastNumberSpoken = input[turn - 1];
                }
                else
                {
                    if (!dict.ContainsKey(lastNumberSpoken))
                    {
                        dict.Add(lastNumberSpoken, (turn - 1, -1, true));
                        lastNumberSpoken = 0;
                    }
                    else
                    {
                        (int prevTurn, int prev, bool firstOccurence) = dict[lastNumberSpoken];
                        lastNumberSpoken = firstOccurence ? 0 : prevTurn - prev;

                        if (!dict.ContainsKey(lastNumberSpoken))
                            dict.Add(lastNumberSpoken, (turn, -1, true));
                        else
                        {
                            (prevTurn, _, _) = dict[lastNumberSpoken];
                            dict[lastNumberSpoken] = (turn, prevTurn, false);
                        }
                    }
                }

                turn++;
            }

            Console.WriteLine("Part -------------");
            Console.WriteLine($"Given your starting numbers [{string.Join(',', input)}], what will the {turns}th number spoken?");
            Console.WriteLine(lastNumberSpoken);
            return lastNumberSpoken;
        }
    }
}
