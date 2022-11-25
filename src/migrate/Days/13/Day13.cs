using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._13
{
    public class Day13 : Day
    {
        public override string Source { get; set; }
        private List<string> Points { get; set; }
        private Queue<string> Folds { get; set; }

        public Day13(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\13\\{Source}.data"));

            Points = new List<string>();
            Folds = new Queue<string>();

            foreach (var line in text)
            {
                if (line == "")
                {
                    continue;
                }
                else if (line.StartsWith("fold"))
                {
                    Folds.Enqueue(line.Split(' ')[2]);
                }
                else
                {
                    Points.Add(line);
                }
            }

            Console.WriteLine("########## Day 11 2021 ##########");
            Console.WriteLine($"Part one solution: {SolveOne(Folds.Take(1))}");
            Console.WriteLine($"Part two solution: {SolveTwo(Folds)}");
            Console.WriteLine("################################");
        }

        private string SolveOne(IEnumerable<string> folds)
        {
            // bounds of map
            var col = Points.Select(x => int.Parse(x.Split(',')[0])).ToList().Max() + 1;
            var row = Points.Select(x => int.Parse(x.Split(',')[1])).ToList().Max() + 1;

            foreach(var fold in folds)
            {
                var foldOptions = fold.Split('=');

                if (foldOptions[0] == "y")
                {
                    var index = int.Parse(foldOptions[1]);
                    var rows = ((row - 1) / 2);
                    for (int y = 1; y <= rows; y++)
                    {
                        for (int x = 0; x < col; x++)
                        {
                            var foldSide = Points.FirstOrDefault(o => o == $"{x},{index + y}");
                            var foldedSide = Points.FirstOrDefault(o => o == $"{x},{index - y}");
                            if (foldSide != null && foldedSide == null)
                            {
                                Points.Add($"{x},{index - y}");
                            }
                        }
                    }
                    row = index;
                }
                else
                {
                    var index = int.Parse(foldOptions[1]);
                    var cols = ((col - 1) / 2);
                    for(int y = 1; y <= cols; y++)
                    {
                        var itemsToRemove = new List<string>();
                        for (int x = 0; x < row; x++)
                        {
                            var foldSide = Points.FirstOrDefault(o => o == $"{index + y},{x}");
                            var foldedSide = Points.FirstOrDefault(o => o == $"{index - y}.,{x}");
                            if (foldSide != null && foldedSide == null)
                            {
                                Points.Add($"{index - y},{x}");
                            }
                            itemsToRemove.Add($"{index + y},{x}");
                        }
                        Points.RemoveAll(i => itemsToRemove.Any(itr => itr == i));
                    }
                    col = index;
                }
            }

            var res = 0;
            for(int i = 0; i <= col; i++)
            {
                for(int j = 0; j <= row; j++)
                {
                    if (Points.Any(x => x == $"{i},{j}")) 
                    {
                        res++;
                    }
                }
            }

            return res.ToString();
        }

        // copied everything but takes all folds
        private string SolveTwo(IEnumerable<string> folds)
        {
            // bounds of map
            var col = Points.Select(x => int.Parse(x.Split(',')[0])).ToList().Max() + 1;
            var row = Points.Select(x => int.Parse(x.Split(',')[1])).ToList().Max() + 1;

            foreach (var fold in folds)
            {
                var foldOptions = fold.Split('=');

                if (foldOptions[0] == "y")
                {
                    var index = int.Parse(foldOptions[1]);
                    var rows = ((row - 1) / 2);
                    for (int y = 1; y <= rows; y++)
                    {
                        var itemsToRemove = new List<string>();
                        for (int x = 0; x < col; x++)
                        {
                            var foldSide = Points.FirstOrDefault(o => o == $"{x},{index + y}");
                            var foldedSide = Points.FirstOrDefault(o => o == $"{x},{index - y}");
                            if (foldSide != null && foldedSide == null)
                            {
                                Points.Add($"{x},{index - y}");
                            }
                            itemsToRemove.Add($"{x},{index + y}");
                        }
                        Points.RemoveAll(i => itemsToRemove.Any(itr => itr == i));
                    }
                    row = index;
                }
                else
                {
                    var index = int.Parse(foldOptions[1]);
                    var cols = ((col - 1) / 2);
                    for (int y = 1; y <= cols; y++)
                    {
                        var itemsToRemove = new List<string>();
                        for (int x = 0; x < row; x++)
                        {
                            var foldSide = Points.FirstOrDefault(o => o == $"{index + y},{x}");
                            var foldedSide = Points.FirstOrDefault(o => o == $"{index - y}.,{x}");
                            if (foldSide != null && foldedSide == null)
                            {
                                Points.Add($"{index - y},{x}");
                            }
                            itemsToRemove.Add($"{index + y},{x}");
                        }
                        Points.RemoveAll(i => itemsToRemove.Any(itr => itr == i));
                    }
                    col = index;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                var s = "";
                for (int j = 0; j < 40; j++)
                {
                    s += (Points.Any(x => x == $"{j},{i}")) ? "O" : " ";
                }
                Console.WriteLine(s);
            }

            // print is sent to console
            return "";
        }
    }
}
