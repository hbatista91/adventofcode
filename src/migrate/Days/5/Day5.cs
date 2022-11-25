using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._5
{
    public class Day5 : Day
    {
        public override string Source { get; set; }

        public Day5(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\5\\{Source}.data"));

            Console.WriteLine("########## Day 4 2021 ##########");
            Console.WriteLine($"Part one solution: {Solve(text, true)}");
            Console.WriteLine($"Part two solution: {Solve(text, false)}");
            Console.WriteLine("################################");
        }

        private string Solve(string[] text, bool firstPart)
        {
            var allPositions = new List<string>();
            foreach (var line in text)
            {
                var positions = line.Split("->");
                var pos1 = positions[0].Split(',').Select(x => int.Parse(x.Trim())).ToArray();
                var pos2 = positions[1].Split(',').Select(x => int.Parse(x.Trim())).ToArray();

                if (pos1[0] == pos2[0] && pos1[1] == pos2[1])
                {
                    allPositions.Add(pos1[0] + "," + pos2[0]);
                    continue;
                };

                // Ignore diagonal lines (only on first part of the problem)
                if (pos1[0] != pos2[0] && pos1[1] != pos2[1])
                {
                    if (firstPart) continue;
                    else if (pos1[0] == pos1[1] && pos2[0] == pos2[1])
                    {
                        var max = Math.Max(pos1[0], pos2[0]);
                        var min = Math.Min(pos1[0], pos2[0]);
                        var range = Enumerable.Range(min, (max-min)+1);
                        foreach (var n in range)
                        {
                            allPositions.Add(n + "," + n);
                        }
                        continue;
                    }
                    else
                    {
                        allPositions.Add(pos1[0] + "," + pos1[1]);
                        var c = Math.Abs(pos2[0] - pos1[0]);

                        for (int i = 0; i < c; i++)
                        {
                            pos1[0] = (pos1[0] < pos2[0]) ? pos1[0] + 1 : pos1[0] - 1;
                            pos1[1] = (pos1[1] < pos2[1]) ? pos1[1] + 1 : pos1[1] - 1;
                            allPositions.Add(pos1[0] + "," + pos1[1]);
                        }
                        continue;
                    }
                }

                if (pos1[0] == pos2[0])
                {
                    var range = GeneratePositions(pos1, pos2, 1);

                    foreach (var n in range)
                    {
                        allPositions.Add(pos1[0] + "," + n);
                    }
                    continue;
                }

                if (pos1[1] == pos2[1])
                {
                    var range = GeneratePositions(pos1, pos2, 0);

                    foreach (var n in range)
                    {
                        allPositions.Add(n + "," + pos1[1]);
                    }
                    continue;
                }
            }

            return allPositions
                .GroupBy(x => x)
                .Select(c => new { Key = c.Key, total = c.Count() })
                .Where(x => x.total >= 2)
                .Count()
                .ToString();
        }

        private List<int> GeneratePositions(int[] pos1, int[] pos2, int index)
        {
            if (pos1[index] > pos2[index])
            {
                return Enumerable.Range(pos2[index], (pos1[index] - pos2[index]) + 1).ToList();
            }
            else
            {
                return Enumerable.Range(pos1[index], (pos2[index] - pos1[index]) + 1).ToList();
            }
        }
    }
}
