using System;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    public class Day18
    {
        public static void Execute()
        {
            var input = File.ReadAllLines("inputs/Day 18/input.txt");
            SolvePart1(input);
            SolvePart2(input);
        }

        public static void SolvePart1(string[] input)
        {
            long sum = 0;

            for (int i = 0; i < input.Length; i++)
                sum += EvaluateExpression(input[i]);

            Console.WriteLine("Part 1 -----------");
            Console.WriteLine("Evaluate the expression on each line of the homework; what is the sum of the resulting values?");
            Console.WriteLine($"{sum}");
        }

        public static void SolvePart2(string[] input)
        {
            long sum = 0;

            for (int i = 0; i < input.Length; i++)
                sum += EvaluateExpressionAdvanced(input[i]);

            Console.WriteLine("Part 2 -----------");
            Console.WriteLine("What do you get if you add up the results of evaluating the homework problems using these new rules?");
            Console.WriteLine($"{sum}");
        }

        public static long EvaluateExpression(string expression)
        {
            long current = 0;
            int index = 0;
            Func<long, long, long> add = (left, right) => left + right;
            Func<long, long, long> multiply = (left, right) => left * right;
            Func<long, long, long> evaluate = add;

            while (index < expression.Length)
            {
                if (expression[index] == '(')
                {
                    int start = index + 1;
                    int count = 1;
                    // we now have to find the matching end paren
                    while (count > 0)
                    {
                        index += 1;
                        if (expression[index] == '(')
                            count++;
                        else if (expression[index] == ')')
                            count--;
                    }

                    current = evaluate(current, EvaluateExpression(expression.Substring(start, index - start)));
                }
                else if (char.IsDigit(expression[index]))
                    current = evaluate(current, int.Parse(expression[index].ToString()));
                else if (IsOperator(expression[index]))
                    evaluate = expression[index] == '+' ? add : multiply;

                index++;
            }

            return current;

            bool IsOperator(char c) => c == '+' || c == '*';
        }

        public static long EvaluateExpressionAdvanced(string expression) => EvaluateExpression(RewriteExpression(expression));

        public static string RewriteExpression(string expression)
        {
            StringBuilder expressionBuilder = new StringBuilder(expression);

            int index = 0;
            while (index < expressionBuilder.Length)
            {
                if (expressionBuilder[index] == '+')
                {
                    int lhs = index - 1;
                    while (lhs >= 0)
                    {
                        if (char.IsDigit(expressionBuilder[lhs]))
                        {
                            expressionBuilder.Insert(lhs, '(');
                            index++;
                            break;
                        }
                        else if (expressionBuilder[lhs] == ')')
                        {
                            int count = 1;
                            while (count > 0)
                            {
                                lhs--;
                                if (expressionBuilder[lhs] == ')')
                                    count++;
                                else if (expressionBuilder[lhs] == '(')
                                    count--;
                            }

                            expressionBuilder.Insert(lhs, '(');
                            index++;
                            break;
                        }
                        lhs--;
                    }

                    int rhs = index + 1; // ? is this right?
                    while (rhs < expressionBuilder.Length)
                    {
                        if (char.IsDigit(expressionBuilder[rhs]))
                        {
                            expressionBuilder.Insert(rhs + 1, ')');
                            break;
                        }
                        else if (expressionBuilder[rhs] == '(')
                        {
                            int count = 1;
                            while (count > 0)
                            {
                                rhs++;
                                if (expressionBuilder[rhs] == '(')
                                    count++;
                                else if (expressionBuilder[rhs] == ')')
                                    count--;
                            }

                            expressionBuilder.Insert(rhs + 1, ')');
                            break;
                        }

                        rhs++;
                    }
                }
                index++;
            }

            return expressionBuilder.ToString();
        }
    }
}
