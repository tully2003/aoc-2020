using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day25Tests
    {
        public class TheCalculateLoopSizeMethod
        {
            [Theory]
            [InlineData(5764801, 7, 8)]
            [InlineData(17807724, 7, 11)]
            public void Test(long publicKey, int subjectNumber, long expected)
            {
                Assert.Equal(expected, Day25.CalculateLoopSize(publicKey, subjectNumber));
            }
        }

        public class TheCalculateKeyMethod
        {
            [Theory]
            [InlineData(5764801, 11, 14897079)]
            [InlineData(17807724, 8, 14897079)]
            public void Test(long subjectNumber, long loopSize, long expected)
            {
                Assert.Equal(expected, Day25.CalculateKey(subjectNumber, loopSize));
            }
        }
    }
}
