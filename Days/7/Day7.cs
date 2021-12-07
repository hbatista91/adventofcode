using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._7
{
    public class Day7 : Day
    {
        public override string Source { get; set; }

        public Day7(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\7\\{Source}.data"));
            var dict = InputHelpers.MapDictionary(text);

            Console.WriteLine("########## Day 4 2021 ##########");
            Console.WriteLine($"Part one solution: {SolveOne(dict)}");
            Console.WriteLine($"Part two solution: {SolveTwo(dict)}");
            Console.WriteLine("################################");
        }

        private object SolveOne(Dictionary<int, int> dict)
        {
            var cost = int.MaxValue;
            for (int i = 0; i < dict.Keys.Max(); i++)
            {
                var newCost = dict.Where(x => x.Key != i).Select(x => x.Value * Math.Abs(x.Key - i)).Sum();
                if (newCost < cost)
                {
                    cost = newCost;
                }
            }

            return cost.ToString();
        }

        private object SolveTwo(Dictionary<int, int> dict)
        {
            var cost = int.MaxValue;
            for(int i = 5; i < dict.Keys.Max(); i++)
            {
                var newCost = dict.Where(x => x.Key != i).Select(x => x.Value * Enumerable.Range(1, Math.Abs(x.Key - i)).Sum()).Sum();
                if (newCost < cost)
                {
                    cost = newCost;
                }
            }

            return cost.ToString();
        }
    }
}
