using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._9
{
    public class Day9 : Day
    {
        public override string Source { get; set; }

        public Day9(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\9\\{Source}.data"));
            var map = InputHelpers.MapToTwoDimensionalIntArray(text);

            Console.WriteLine("########## Day 4 2021 ##########");
            Console.WriteLine($"Part one solution: {SolveOne(map)}");
            Console.WriteLine($"Part two solution: {SolveTwo(map)}");
            Console.WriteLine("################################");
        }

        private object SolveOne(int[,] map)
        {
            var maxRow = map.GetLength(0);
            var maxCol = map.GetLength(1);
            var res = 0;

            for (int x = 0; x < maxRow; x++)
            {
                for (int y = 0; y < maxCol; y++)
                {
                    var left = y - 1 < 0 ? -1 : map[x, y - 1];
                    var right = y + 1 > (maxCol - 1) ? -1 : map[x, y + 1];
                    var top = x - 1 < 0 ? -1 : map[x - 1, y];
                    var bottom = x + 1 > (maxRow - 1) ? -1 : map[x + 1, y];

                    if (right <= map[x, y] && right != -1)
                    {
                        continue;
                    }

                    if (left <= map[x, y] && left != -1)
                    {
                        continue;
                    }

                    if (bottom <= map[x, y] && bottom != -1)
                    {
                        continue;
                    }

                    if (top <= map[x, y] && top != -1)
                    {
                        continue;
                    }

                    res += map[x, y] + 1;
                }
            }

            return res.ToString();
        }

        private object SolveTwo(int[,] map)
        {
            var maxRow = map.GetLength(0);
            var maxCol = map.GetLength(1);
            var dict = new Dictionary<string, int>();

            for (int x = 0; x < maxRow; x++)
            {
                for (int y = 0; y < maxCol; y++)
                {
                    if (map[x,y] == 9)
                    {
                        continue;
                    }

                    var origin = TraverseBasin(x, y, map, maxRow, maxCol);
                    if (dict.ContainsKey(origin)) {
                        dict[origin] = dict[origin] + 1;
                    }
                    else
                    {
                        dict.Add(origin, 1);
                    }
                }
            }

            return dict.OrderByDescending(x => x.Value).Take(3).Select(x => x.Value).Aggregate((x, y) => x * y).ToString();
        }

        private string TraverseBasin(int x, int y, int[,] map, int maxRow, int maxCol)
        {
            var left = y - 1 < 0 ? -1 : map[x, y - 1];
            var right = y + 1 > (maxCol - 1) ? -1 : map[x, y + 1];
            var top = x - 1 < 0 ? -1 : map[x - 1, y];
            var bottom = x + 1 > (maxRow - 1) ? -1 : map[x + 1, y];

            var lookup = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(map[x, y], $"{x},{y}"),
                new KeyValuePair<int, string>(left, $"{x},{y - 1}"),
                new KeyValuePair<int, string>(right, $"{x},{y+1}"),
                new KeyValuePair<int, string>(top, $"{x-1},{y}"),
                new KeyValuePair<int, string>(bottom, $"{x+1},{y}")
            }.Where(x => x.Key != -1).OrderBy(x => x.Key).First();

            var pos = lookup.Value.Split(',').Select(x => int.Parse(x)).ToArray();
            if (lookup.Key == map[x, y])
            {
                return $"{x},{y}";
            }
            else
            {
                return TraverseBasin(pos[0], pos[1], map, maxRow, maxCol);
            }
        }
    }
}
