using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
	public class Day18Tests
	{
		public class TheEvaluateExpressionMethod
		{
			[Theory]
			[InlineData("1 + 2", 3)]
			[InlineData("2 + 2", 4)]
			[InlineData("4 * 4", 16)]
			[InlineData("9 * 9", 81)]
			[InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
			[InlineData("1 + 2 * 3 + 4 * 5 + 6", 71)]
			[InlineData("2 * 3 + (4 * 5)", 26)]
			[InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
			[InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
			[InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
			public void Test(string expression, long expected)
			{
				Assert.Equal(expected, Day18.EvaluateExpression(expression));
			}
		}

		public class TheRewriteExpressionMethod
		{
			[Theory]
			[InlineData("1 + 2", "(1 + 2)")]
			[InlineData("2 + 2", "(2 + 2)")]
			[InlineData("4 * 4", "4 * 4")]
			[InlineData("1 + 2 * 4", "(1 + 2) * 4")]
			[InlineData("1 * 2 + 4", "1 * (2 + 4)")]
			[InlineData("1 + 2 * 3 + 4", "(1 + 2) * (3 + 4)")]
			[InlineData("1 + 2 * 3 + 4 * 5 + 6", "(1 + 2) * (3 + 4) * (5 + 6)")]
			[InlineData("1 + (2 * 4)", "(1 + (2 * 4))")]
			[InlineData("2 * 3 + (4 * 5)", "2 * (3 + (4 * 5))")]
			[InlineData("(1 + (2 * 4))", "((1 + (2 * 4)))")]
			[InlineData("1 + 2 + 3", "((1 + 2) + 3)")]
			[InlineData("1 + (2 + 3)", "(1 + ((2 + 3)))")]
			[InlineData("1 + (2 + 3 + 4)", "(1 + (((2 + 3) + 4)))")]
			[InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", "(5 + (8 * ((3 + 9) + 3) * 4 * 3))")]
			public void Test(string expression, string expected)
			{
				Assert.Equal(expected, Day18.RewriteExpression(expression));
			}
		}

		public class TheEvaluateExpressionAdvancedMethod
		{
			[Theory]
			[InlineData("1 + 2", 3)]
			[InlineData("2 + 2", 4)]
			[InlineData("4 * 4", 16)]
			[InlineData("9 * 9", 81)]
			[InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
			[InlineData("1 + 2 * 3 + 4 * 5 + 6", 231)]
			[InlineData("2 * 3 + (4 * 5)", 46)]
			[InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
			[InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
			[InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
			public void Test(string expression, long expected)
			{
				Assert.Equal(expected, Day18.EvaluateExpressionAdvanced(expression));
			}
		}
	}
}
