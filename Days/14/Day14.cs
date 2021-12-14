using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._14
{
    public class Day14 : Day
    {
        public override string Source { get; set; }
        public Dictionary<string,string> Combinations { get; set; }
        public Dictionary<string, List<string>> CombinationsPartTwo { get; set; }

        public Day14(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\14\\{Source}.data"));

            Combinations = new Dictionary<string, string>();
            var originalString = text[0];
            foreach(var line in text.Skip(2))
            {
                var lineSplit = line.Split("->");
                Combinations.Add(lineSplit[0].Trim(), lineSplit[1].Trim());
            }

            // This one generates the combinations ahead of time
            CombinationsPartTwo = new Dictionary<string, List<string>>();
            foreach (var line in text.Skip(2))
            {
                var lineSplit = line.Split("->");
                var s1 = lineSplit[0].Trim()[0] + lineSplit[1].Trim();
                var s2 = lineSplit[1].Trim() + lineSplit[0].Trim()[1];

                CombinationsPartTwo.Add(lineSplit[0].Trim(),new List<string> { s1,s2 });
            }

            Console.WriteLine("########## Day 11 2021 ##########");
            Console.WriteLine($"Part one solution: {SolveOne(originalString, 10)}");
            Console.WriteLine($"Part one solution: {SolveTwo(originalString, 40)}");
            Console.WriteLine("################################");
        }

        private string SolveOne(string originalString, int iterations)
        {
            var os = originalString;
            for (int j = 0; j < iterations; j++)
            {
                var sb = new StringBuilder();
                for (var i = 0; i < os.Length - 1; i++)
                {
                    var subString = os.Substring(0 + i, 2);
                    var combo = Combinations.FirstOrDefault(x => x.Key == subString);
                    if (combo.Key != null)
                    {
                        sb.Append(subString[0] + combo.Value);
                        if (i == (os.Length - 2))
                        {
                            sb.Append(subString[1]);
                        }
                    }
                    else
                    {

                    }
                }
                os = sb.ToString();
            }

            var dict = new Dictionary<char, double>();
            foreach(var c in os.ToCharArray())
            {
                if (dict.ContainsKey(c))
                {
                    dict[c] += 1;
                }
                else dict.Add(c, 1);
            }

            var leastCommon = dict.OrderBy(x => x.Value).First().Value;
            var mostCommon = dict.OrderByDescending(x => x.Value).First().Value;

            return (mostCommon - leastCommon).ToString();
        }

        private string SolveTwo(string originalString, int iterations)
        {
            var pairings = new Dictionary<string, double>();
            for (var i = 0; i < originalString.Length - 1; i++)
            {
                var subString = originalString.Substring(0 + i, 2);
                if (pairings.ContainsKey(subString))
                {
                    pairings[subString] += 1;
                }
                else
                {
                    pairings.Add(subString, 1);
                }
            }

            for (int j = 0; j < iterations; j++)
            {
                var newPairings = new Dictionary<string, double>();
                foreach (var pairing in pairings.ToList())
                {
                    var p1 = CombinationsPartTwo.First(x => x.Key == pairing.Key).Value[0];
                    var p2 = CombinationsPartTwo.First(x => x.Key == pairing.Key).Value[1];

                    if (newPairings.ContainsKey(p1))
                    {
                        newPairings[p1] += pairing.Value;
                    }
                    else
                    {
                        newPairings.Add(p1, pairing.Value);
                    }

                    if (newPairings.ContainsKey(p2))
                    {
                        newPairings[p2] += pairing.Value;
                    }
                    else
                    {
                        newPairings.Add(p2, pairing.Value);
                    }
                }
                pairings = newPairings;
            }

            var dict = new Dictionary<char, double>();
            dict.Add(char.Parse(originalString.Substring(originalString.Length - 1, 1)), 1);
            foreach (var pairing in pairings)
            {
                if (dict.ContainsKey(pairing.Key[0]))
                {
                    dict[pairing.Key[0]] += pairing.Value;
                }
                else
                {
                    dict.Add((char)pairing.Key[0], pairing.Value);
                }
            }

            var leastCommon = dict.OrderBy(x => x.Value).First().Value;
            var mostCommon = dict.OrderByDescending(x => x.Value).First().Value;

            return (mostCommon - leastCommon).ToString();
        }
    }
}
