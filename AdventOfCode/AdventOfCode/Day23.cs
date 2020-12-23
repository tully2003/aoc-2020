using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public class Day23
    {
        public static void Execute()
        {
            SolvePart1("215694783", 100);
            SolvePart2("215694783");
        }

        public static string SolvePart1(string input, int moves)
        {
            (Cup current, Dictionary<int, Cup> map) = BuildCupList(input, input.Length);
            Play(current, map, moves);

            Cup cup1 = map[1];
            current = cup1.Next;
            StringBuilder sb = new StringBuilder();
            while (current != cup1)
            {
                sb.Append(current.Label);
                current = current.Next;
            }


            Console.WriteLine("Part 1: ---------");
            Console.WriteLine("Using your labeling, simulate 100 moves. What are the labels on the cups after cup 1?");
            Console.WriteLine("What is the winning player's score?");

            return sb.ToString();
        }

        public static long SolvePart2(string input)
        {
            (Cup current, Dictionary<int, Cup> map) = BuildCupList(input, 1_000_000);
            Play(current, map, 10_000_000);
            Cup cup1 = map[1];

            Console.WriteLine("Part 2: ---------");
            Console.WriteLine("Determine which two cups will end up immediately clockwise of cup 1.");
            Console.WriteLine("What do you get if you multiply their labels together?");
            Console.WriteLine($"{cup1.Next.Label} * {cup1.Next.Next.Label} = {cup1.Next.Label * (long)cup1.Next.Next.Label}");

            return cup1.Next.Label * (long)cup1.Next.Next.Label;
        }

        private static void Play(Cup cups, Dictionary<int, Cup> map, int moves)
        {
            Cup current = cups;
            int currentMove = 1;
            while (currentMove <= moves)
            {
                // pickup cup
                Cup pickup = current.Next;
                Cup a = current.Next, b = a.Next, c = b.Next;
                pickup.Prev = null;
                current.Next = pickup.Next.Next.Next;
                pickup.Next.Next.Next = null;

                // find destination
                int destinationLabel = current.Label == 1 ? map.Count : current.Label - 1;
                int min = 1, max = map.Count;
                Cup destination = map[destinationLabel];
                while (pickup == destination || pickup.Next == destination || pickup.Next.Next == destination)
                {
                    destinationLabel--;

                    if (destinationLabel < min)
                        destinationLabel = max;

                    destination = map[destinationLabel];
                }

                // now need to place pickup after desitination
                pickup.Prev = destination;
                pickup.Next.Next.Next = destination.Next;
                destination.Next = pickup;
                current = current.Next;
                currentMove++;
            }
        }

        private static (Cup current, Dictionary<int, Cup>) BuildCupList(string input, int length)
        {
            // lets convert the input to a linked list :)
            var map = new Dictionary<int, Cup>();
            Cup current = null;
            Cup previous = null;

            for (int i = 0; i < length; i++)
            {
                Cup cup = new Cup
                {
                    Label = i < input.Length ? int.Parse(input[i].ToString()) : i + 1
                };
                map.Add(cup.Label, cup);

                if (current == null)
                {
                    previous = current = cup;
                }
                else
                {
                    cup.Prev = previous;
                    previous.Next = cup;
                    previous = cup;
                }
            }
            current.Prev = previous;
            previous.Next = current;
            return (current, map);
        }

        private class Cup
        {
            public int Label { get; set; }

            public Cup Next { get; set; }

            public Cup Prev { get; set; }
        }
    }
}
