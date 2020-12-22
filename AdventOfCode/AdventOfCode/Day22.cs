using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day22
    {
        public static void Execute()
        {
            var input = File
                .ReadAllText("inputs/Day 22/input.txt")
                .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split('\n', StringSplitOptions.RemoveEmptyEntries)[1..].Select(card => int.Parse(card)).ToArray()).ToArray();


            SolvePart1(input[0], input[1]);
            SolvePart2(input[0], input[1]);
        }

        private static void SolvePart1(int[] player1, int[] player2)
        {
            var winner = PlayCombat(player1, player2);

            Console.WriteLine("Part 1: ---------");
            Console.WriteLine("Play the small crab in a game of Combat using the two decks you just dealt.");
            Console.WriteLine("What is the winning player's score?");
            Console.WriteLine(CalculateWinningScore(winner));
        }

        private static void SolvePart2(int[] player1, int[] player2)
        {
            PlayRecursiveCombat(player1, player2, 1, out int[] winner);

            Console.WriteLine("Part 2: ---------");
            Console.WriteLine("Defend your honor as Raft Captain by playing the small crab in a game of Recursive Combat using the same two decks as before.");
            Console.WriteLine("What is the winning player's score?");
            Console.WriteLine(CalculateWinningScore(winner));
        }

        private static int[] PlayCombat(int[] player1, int[] player2)
        {
            int round = 0;
            while (player1.Any() && player2.Any())
            {
                round++;

                if (player1[0] > player2[0])
                    HandleWin(ref player1, ref player2);
                else
                    HandleWin(ref player2, ref player1);
            }

            return player1.Any() ? player1 : player2;
        }

        private static bool PlayRecursiveCombat(int[] player1, int[] player2, int game, out int[] winner)
        {
            HashSet<string> player1Hands = new HashSet<string>();
            HashSet<string> player2Hands = new HashSet<string>();

            int round = 0;
            while (player1.Any() && player2.Any())
            {
                round++;

                // if this hand has been played before then player 1 wins the round!
                string p1Hand = string.Join(',', player1);
                string p2Hand = string.Join(',', player2);

                if (player1Hands.Contains(p1Hand) || player2Hands.Contains(p2Hand))
                {
                    HandleWin(ref player1, ref player2);
                    winner = player1;
                    return true; // we return true if p1 wins
                }
                if (!player1Hands.Contains(p1Hand)) player1Hands.Add(p1Hand);
                if (!player2Hands.Contains(p2Hand)) player2Hands.Add(p2Hand);

                // If both players have at least as many cards remaining in their deck as the value of the card they just drew, 
                // the winner of the round is determined by playing a new game of Recursive Combat
                int p1Card = player1[0];
                int p2Card = player2[0];
                if (player1.Length - 1 >= p1Card && player2.Length - 1 >= p2Card)
                {
                    // play recursive combat!
                    var (p1, p2) = MakeCopies(p1Card, p2Card);
                    if (PlayRecursiveCombat(p1, p2, game++, out _))
                        HandleWin(ref player1, ref player2);
                    else
                        HandleWin(ref player2, ref player1);
                }
                else if (player1[0] > player2[0])
                    HandleWin(ref player1, ref player2);
                else
                    HandleWin(ref player2, ref player1);
            }

            winner = player1.Any() ? player1 : player2;
            return player1.Any();


            (int[], int[]) MakeCopies(int p1Card, int p2Card)
            {
                int[] p1 = new int[p1Card];
                int[] p2 = new int[p2Card];

                Array.Copy(player1, 1, p1, 0, p1Card);
                Array.Copy(player2, 1, p2, 0, p2Card);

                return (p1, p2);
            }
        }

        private static int CalculateWinningScore(int[] player)
        {
            int sum = 0;
            int multiplier = player.Length;
            for (int i = 0; i < player.Length; i++)
            {
                sum += player[i] * multiplier;
                multiplier--;
            }
            return sum;
        }

        private static void HandleWin(ref int[] winner, ref int[] loser)
        {
            int[] w = new int[winner.Length + 1];
            int[] l = new int[loser.Length - 1];

            Array.Copy(winner, 1, w, 0, winner.Length - 1);
            Array.Copy(loser, 1, l, 0, loser.Length - 1);
            w[w.Length - 2] = winner[0];
            w[w.Length - 1] = loser[0];
            winner = w;
            loser = l;
        }
    }
}
