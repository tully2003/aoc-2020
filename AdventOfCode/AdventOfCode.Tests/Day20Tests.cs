using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdventOfCode.Tests
{
	public class Day20Tests
	{
		public class TheSolvePart2Method
		{
			[Theory]
			[InlineData("7,13", 77)]
			[InlineData("17,x,13,19", 3417)]
			[InlineData("67,7,59,61", 754018)]
			[InlineData("67,x,7,59,61", 779210)]
			[InlineData("67,7,x,59,61", 1261476)]
			[InlineData("7,13,x,x,59,x,31,19", 1068781)]
			[InlineData("1789,37,47,1889", 1202161486)]
			public void TestSeat(string input, long expectedId)
			{
				Assert.Equal(expectedId, Day13.SolvePart2(input));
			}
		}
	}
}
