using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._1
{
    public class Day1 : Day
    {
        public override string Source { get; set; }

        public Day1(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\1\\{Source}.data"));

            Console.WriteLine("########## Day 1 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(text)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(text)}");
            Console.WriteLine("################################");
        }

        private string SolvePartOne(string[] text)
        {
            var arrayOfInts = Array.ConvertAll(text, i => int.Parse(i));

            var solution = 0;
            for (int i = 0; i < arrayOfInts.Length - 1; i++)
            {
                if (arrayOfInts[i + 1] > arrayOfInts[i])
                {
                    solution++;
                }
            }

            return solution.ToString();
        }

        private string SolvePartTwo(string[] text)
        {
            var arrayOfInts = Array.ConvertAll(text, i => int.Parse(i));

            var slidingWindowSums = new List<int>();
            for (int i = 0; i < arrayOfInts.Length; i++)
            {
                if (i == arrayOfInts.Length - 2) break;
                slidingWindowSums.Add(arrayOfInts[i] + arrayOfInts[i + 1] + arrayOfInts[i + 2]);
            }

            return SolvePartOne(Array.ConvertAll(slidingWindowSums.ToArray(), i => i.ToString()));
        }
    }
}
