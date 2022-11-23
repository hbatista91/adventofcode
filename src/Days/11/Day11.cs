using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._11
{
    public class Day11 : Day
    {
        public override string Source { get; set; }
        private List<string> NonCorruptedLines { get; set; }

        public Day11(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\11\\{Source}.data"));
            var arr = InputHelpers.MapToTwoDimensionalIntArray(text);

            Console.WriteLine("########## Day 11 2021 ##########");
            Console.WriteLine($"Part one solution: {SolveOne(arr)}");
            Console.WriteLine($"Part two solution: {SolveTwo(arr)}");
            Console.WriteLine("################################");
        }

        private string SolveOne(int[,] arr)
        {
            var res = 0;
            var points = new Dictionary<string, int>();

            // Let's first create a dict with all positions
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    points.Add($"{i},{j}", arr[i, j]);
                }
            }

            for(int k = 0; k < 100; k++)
            {
                // Add +1 to all values
                points = points.ToDictionary(x => x.Key, x => x.Value + 1);
                var flashedPoints = new List<string>();

                while(points.Any(x => x.Value > 9))
                {
                    foreach (var point in points.Where(x => x.Value > 9))
                    {
                        flashedPoints.Add(point.Key);
                        var neighbors = GetNeighbors(point, arr.GetLength(0));

                        points[point.Key] = 0;
                        points = points
                            .ToDictionary(x => x.Key, x => neighbors.Any(y => x.Key == y) && !flashedPoints.Any(y => x.Key == y) ? x.Value + 1 : x.Value);
                    }
                }

                res += flashedPoints.Count;
            }

            return res.ToString();
        }

        // Pretty much the same thing replacing a for..loop with a while to get the index of when all flashs happen
        private string SolveTwo(int[,] arr)
        {
            var res = 0;
            var points = new Dictionary<string, int>();

            // Let's first create a dict with all positions
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    points.Add($"{i},{j}", arr[i, j]);
                }
            }

            int k = 0;
            do
            {
                // Add +1 to all values
                points = points.ToDictionary(x => x.Key, x => x.Value + 1);
                var flashedPoints = new List<string>();

                while (points.Any(x => x.Value > 9))
                {
                    foreach (var point in points.Where(x => x.Value > 9))
                    {
                        flashedPoints.Add(point.Key);
                        var neighbors = GetNeighbors(point, arr.GetLength(0));

                        points[point.Key] = 0;
                        points = points
                            .ToDictionary(x => x.Key, x => neighbors.Any(y => x.Key == y) && !flashedPoints.Any(y => x.Key == y) ? x.Value + 1 : x.Value);
                    }
                }

                k++;
                if(flashedPoints.Count() == points.Count())
                {
                    return k.ToString();
                }
            }
            while (true);
        }

        private List<string> GetNeighbors(KeyValuePair<string, int> point, int gridSize)
        {
            var px = int.Parse(point.Key.Split(',')[0]);
            var py = int.Parse(point.Key.Split(',')[1]);

            return (from x in Enumerable.Range(px - 1, 3)
                    from y in Enumerable.Range(py - 1, 3)
                    where (x >= 0) && (x < gridSize) && (y >= 0) && (y < gridSize)
                    select $"{x},{y}").Where(x => x != point.Key).ToList();
        }
    }
}
