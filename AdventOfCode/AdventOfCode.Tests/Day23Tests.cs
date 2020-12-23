using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day23Tests
    {
        public class TheSolvePart1Method
        {
            [Theory]
            [InlineData("389125467", 1, "54673289")]
            [InlineData("389125467", 2, "32546789")]
            [InlineData("389125467", 3, "34672589")]
            [InlineData("389125467", 4, "32584679")]
            [InlineData("389125467", 5, "36792584")]
            [InlineData("389125467", 6, "93672584")]
            [InlineData("389125467", 7, "92583674")]
            [InlineData("389125467", 8, "58392674")]
            [InlineData("389125467", 9, "83926574")]
            [InlineData("389125467", 10, "92658374")]
            [InlineData("389125467", 100, "67384529")]
            [InlineData("215694783", 100, "46978532")]
            public void Test(string input, int moves, string expected)
            {
                Assert.Equal(expected, Day23.SolvePart1(input, moves));
            }
        }

        public class TheSolvePart2Method
        {
            [Theory]
            [InlineData("389125467", 149245887792)]
            [InlineData("215694783", 163035127721)]
            public void Test(string input, long expected)
            {
                Assert.Equal(expected, Day23.SolvePart2(input));
            }
        }
    }
}
