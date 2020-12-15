using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
	public class Day15Tests
	{
		public class TheSolvePart1Method
		{
			[Theory]
			[InlineData("0,3,6", 10, 0)]
			[InlineData("0,3,6", 30000000, 175594)]
			[InlineData("1,3,2", 2020, 1)]
			[InlineData("1,3,2", 30000000, 2578)]
			[InlineData("2,1,3", 2020, 10)]
			[InlineData("2,1,3", 30000000, 3544142)]
			[InlineData("1,2,3", 2020, 27)]
			[InlineData("1,2,3", 30000000, 261214)]
			[InlineData("2,3,1", 2020, 78)]
			[InlineData("2,3,1", 30000000, 6895259)]
			[InlineData("3,2,1", 2020, 438)]
			[InlineData("3,2,1", 30000000, 18)]
			[InlineData("3,1,2", 2020, 1836)]
			[InlineData("3,1,2", 30000000, 362)]
			[InlineData("16,12,1,0,15,7,11", 2020, -1)]
			[InlineData("16,12,1,0,15,7,11", 30000000, 175594)]
			public void TestSeat(string input, int turns, long expectedId)
			{
				Assert.Equal(expectedId, Day15.Solve(input.Split(',').Select(x => int.Parse(x)).ToArray(), turns));
			}
		}
	}
}
