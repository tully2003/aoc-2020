using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdventOfCode.Tests
{
	public class Day14Tests
	{
		public class DecoderChipV1
		{
			public class TheApplyMaskMethod
			{
				[Theory]
				[InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 11, 73)]
				[InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 101, 101)]
				[InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 0, 64)]
				[InlineData("1XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", 0, 34359738368)]
				public void TestSeat(string mask, ulong value, ulong expected)
				{
					Assert.Equal(expected, Day14.DecoderChipV1.ApplyMask(mask, value));
				}
			}
		}
	}
}
