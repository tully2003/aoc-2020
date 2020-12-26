using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day25
    {
        public static void Execute()
        {
            var input = File.ReadAllLines("inputs/Day 25/input.txt");
            long cardPublicKey = int.Parse(input[0]);
            long doorPublicKey = int.Parse(input[1]);

            SolvePart1(cardPublicKey, doorPublicKey);
        }

        private static long SolvePart1(long cardPublicKey, long doorPublicKey)
        {
            const int subjectNumber = 7;
            long cardLoopSize = -1;
            long doorLoopSize = -1;
            int loop = 1;
            long value = 1;
            while (cardLoopSize == -1 || doorLoopSize == -1)
            {
                value = (value * subjectNumber) % 20201227;

                if (value == cardPublicKey)
                    cardLoopSize = loop;
                if (value == doorPublicKey)
                    doorLoopSize = loop;

                loop++;
            }

            long encryptionKey = CalculateKey(cardPublicKey, doorLoopSize);

            Console.WriteLine("Part 1: ----------");
            Console.WriteLine("What encryption key is the handshake trying to establish?");
            Console.WriteLine(encryptionKey);

            return encryptionKey;
        }

        public static long CalculateLoopSize(long publicKey, long subjectNumber)
        {
            int loop = 1;
            long value = 1;
            while (true)
            {
                value = (value * subjectNumber) % 20201227;

                if (value == publicKey)
                    return loop;

                loop++;
            }

            return -1;
        }

        public static long CalculateKey(long subjectNumber, long loopSize)
        {
            long value = 1;
            for (int loop = 1; loop <= loopSize; loop++)
            { 
                value = (value * subjectNumber) % 20201227;
            }

            return value;
        }
    }
}
