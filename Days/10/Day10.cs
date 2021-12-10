using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._10
{
    public class Day10 : Day
    {
        public override string Source { get; set; }
        private List<string> NonCorruptedLines { get; set; }

        public Day10(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\10\\{Source}.data"));
            var dict = InputHelpers.MapToStringDictionary(text);

            Console.WriteLine("########## Day 10 2021 ##########");
            Console.WriteLine($"Part one solution: {SolveOne(dict)}");
            Console.WriteLine($"Part two solution: {SolveTwo()}");
            Console.WriteLine("################################");
        }

        private string SolveOne(Dictionary<int, string> dict)
        {
            NonCorruptedLines = new List<string>();
            var res = 0;

            foreach (var value in dict.Values)
            {
                var v = 0;
                var stack = new Stack<char>();
                foreach(var s in value.ToCharArray())
                {
                    char peek = stack.Any() ? stack.Peek() : '\0';
                    switch (s)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            stack.Push(s);
                            break;
                        case ')':
                            v += Peekaboo(stack, 3, '(');
                            break;
                        case ']':
                            v += Peekaboo(stack, 57, '[');
                            break;
                        case '}':
                            v += Peekaboo(stack, 1197, '{');
                            break;
                        case '>':
                            v += Peekaboo(stack, 25137, '<');
                            break;
                    }
                    if (v > 0)
                    {
                        res += v;
                        break;
                    }
                }

                if (v == 0)
                {
                    NonCorruptedLines.Add(value);
                }
            }

            return res.ToString();
        }

        private string SolveTwo()
        {
            var numbers = new List<double>();
            foreach (var value in NonCorruptedLines)
            {
                var stack = new Stack<char>();
                foreach (var s in value.ToCharArray())
                {
                    switch (s)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            stack.Push(s);
                            break;
                        default: 
                            stack.Pop();
                            break;
                    }
                }

                double score = 0;
                do
                {
                    var p = stack.Pop();
                    switch (p)
                    {
                        case '(':
                            score = score * 5 + 1;
                            break;
                        case '[':
                            score = score * 5 + 2;
                            break;
                        case '{':
                            score = score * 5 + 3;
                            break;
                        case '<':
                            score = score * 5 + 4;
                            break;
                    }
                }
                while (stack.Count > 0);
                numbers.Add(score);
            }

            return numbers.OrderBy(x => x).ElementAt((numbers.Count() / 2)).ToString();
        }

        private int Peekaboo(Stack<char> stack, int n, char check)
        {
            var peek = stack.Peek();
            if (peek != check)
            {
                return n;
            }
            else
            {
                stack.Pop();
                return 0;
            }
        }
    }
}
