using System;
using Xunit;

namespace AdventOfCode.Tests
{
	public class Day5Tests
	{
		public class TheCalculateSeatIdMethod
		{
			[Theory]
			[InlineData("FBFBBFFRLR", 357)]
			[InlineData("BFFFBBFRRR", 567)]
			[InlineData("FFFBBBFRRR", 119)]
			[InlineData("BBFFBBFRLL", 820)]
			[InlineData("FFFFFFFLLL", 0)]
			[InlineData("FFFFFFFRRR", 7)]
			[InlineData("BBBBBBBLLL", 1016)]
			[InlineData("BBBBBBBRRR", 1023)]
			public void TestSeat(string seat, int expectedId)
			{
				Assert.Equal(expectedId, Day5.CalculateSeatId(seat));
			}
		}
	}
}
