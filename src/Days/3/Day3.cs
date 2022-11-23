using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._3
{
    public class Day3 : Day
    {
        public override string Source { get; set; }

        public Day3(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\3\\{Source}.data"));

            Console.WriteLine("########## Day 3 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(text)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(text)}");
            Console.WriteLine("################################");
        }

        private string SolvePartOne(string[] text)
        {
            var size = text[0].Length;
            var count = new int[size];

            for(int i = 0; i < text.Length; i++)
            {
                var values = text[i].ToArray().Select(x => int.Parse(x + "")).ToArray();
                for (int j = 0; j < size; j++)
                {
                    count[j] += values[j];
                }
            }

            var gammaRate = "";
            var epsilonRate = "";
            foreach (var value in count)
            {
                if (value > text.Length/2)
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
                else
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
            }

            return (Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2)).ToString();
        }

        private string SolvePartTwo(string[] text)
        {
            var oxygenGeneratorRating = RemovalLogic(text, true);
            var co2ScrubberRating = RemovalLogic(text, false);

            return (Convert.ToInt32(oxygenGeneratorRating, 2) * Convert.ToInt32(co2ScrubberRating, 2)).ToString();
        }

        private string RemovalLogic(string[] text, bool flag)
        {
            var count = 0;
            var currentIndex = 0;

            var list = text.Select(x => x.Select(c => c - '0').ToArray()).ToArray();
            do
            {
                count = 0;

                for (int i = 0; i < list.Count(); i++)
                {
                    count += list[i][currentIndex];
                }

                if (count >= (float)list.Count() / 2)
                {
                    list = list.Where(x => x[currentIndex] == (flag ? 1 : 0)).ToArray();
                }
                else
                {
                    list = list.Where(x => x[currentIndex] == (flag ? 0 : 1)).ToArray();
                }

                currentIndex++;
            }
            while (list.Count() > 1);

            return string.Join("", list[0]);
        }
    }
}
