using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._6
{
    public class Day6 : Day
    {
        public override string Source { get; set; }

        public Day6(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\6\\{Source}.data"));

            Console.WriteLine("########## Day 4 2021 ##########");
            Console.WriteLine($"Part one solution: {SolveOne(text, 80)}");
            Console.WriteLine($"Part two solution: {SolveTwo(text, 256)}");
            Console.WriteLine("################################");
        }

        // Clean way, not that efficient for solving the second part
        private string SolveOne(string[] text, int iterations)
        {
            var items = text[0].Split(',').Select(x => int.Parse(x)).ToList();
            for(int i = 0; i < iterations; i++)
            {
                items.AddRange(Enumerable.Range(0, items.Where(x => x == 0).Count()).Select(x => x = 9));
                items = items.Select(x => x == 0 ? 7 : x).Select(x => x - 1).ToList();
            }
            return items.Count().ToString();
        }

        private string SolveTwo(string[] text, int iterations)
        {
            // Instead of couting number of fishs, we can just keep track of how many there are per age group
            var fishs = text[0].Split(',')
                .Select(x => int.Parse(x))
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => (long)g.Count());

            // Fill in empty keys
            InitDictionary(fishs);
            var newFishs = new Dictionary<int, long>();

            for (int i = 0; i < iterations; i++)
            {
                newFishs = new Dictionary<int, long>();
                InitDictionary(newFishs);

                foreach (var entry in fishs)
                {
                    switch (entry.Key)
                    {
                        case 0:
                            newFishs[6] += entry.Value;
                            newFishs[8] += entry.Value;
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            newFishs[entry.Key - 1] += entry.Value;
                            break;
                    }
                }

                fishs = newFishs;
            }

            return fishs.Sum(x => x.Value).ToString();
        }

        private void InitDictionary(Dictionary<int, long> fishs)
        {
            for (int i = 0; i <= 8; i++)
            {
                if (!fishs.ContainsKey(i))
                {
                    fishs.Add(i, 0);
                }
            }
        }
    }
}
