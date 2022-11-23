using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._8
{
    public class Day8 : Day
    {
        public override string Source { get; set; }

        public Day8(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\8\\{Source}.data"));
            var dict = InputHelpers.MapToStringDictionary(text);

            Console.WriteLine("########## Day 4 2021 ##########");
            Console.WriteLine($"Part one solution: {SolveOne(dict)}");
            Console.WriteLine($"Part two solution: {SolveTwo(dict)}");
            Console.WriteLine("################################");
        }

        private object SolveOne(Dictionary<int, string> dict)
        {
            int[] uniqueValues = { 2,4,3,7 };
            var res = 0;
            foreach (var value in dict.Values)
            {
                res += value.Split('|')[1].Trim().Split(" ").Where(x => uniqueValues.Contains(x.Trim().Length)).Count();
            }
            return res.ToString();
        }

        private object SolveTwo(Dictionary<int, string> dict)
        {
            var res = 0;

            foreach (var value in dict.Values)
            {
                var map = new Dictionary<int, string>();

                var inputValues = value.Split('|')[0].Trim().Split(" ").Where(x => x != string.Empty);
                var outputValues = value.Split('|')[1].Trim().Split(" ").Where(x => x != string.Empty);

                // Push known values
                map.Add(1, inputValues.Where(x => x.Length == 2).First());
                map.Add(4, inputValues.Where(x => x.Length == 4).First());
                map.Add(7, inputValues.Where(x => x.Length == 3).First());
                map.Add(8, inputValues.Where(x => x.Length == 7).First());

                // 2,5,3
                foreach (var k in inputValues.Where(x => x.Length == 5))
                {
                    var fourWithoutOne = string.Join("",map[4].ToCharArray().Where(x => !map[1].ToCharArray().Contains(x)));
                    if (Contains(k, map[1]))
                    {
                        map.Add(3, k);
                    } else if (Contains(k, fourWithoutOne))
                    {
                        map.Add(5, k);
                    }
                    else
                    {
                        map.Add(2, k);
                    }
                }

                // 6,9,0
                foreach (var k in inputValues.Where(x => x.Length == 6))
                {
                    if (Contains(k, map[4]))
                    {
                        map.Add(9, k);
                    } else if(Contains(k, map[1]))
                    {
                        map.Add(0, k);
                    }
                    else
                    {
                        map.Add(6, k);
                    }
                }

                var s = "";
                foreach (var outputValue in outputValues)
                {
                    switch(outputValue.Length)
                    {
                        case 2:
                            s += "1";
                            break;
                        case 4:
                            s += "4";
                            break;
                        case 3:
                            s += "7";
                            break;
                        case 7:
                            s += "8";
                            break;
                        case 5:
                            foreach (var k in map.Where(x => x.Value.Length == 5))
                            {
                                if (Contains(outputValue, k.Value))
                                {
                                    s += k.Key;
                                }
                            }
                            break;
                        case 6:
                            foreach(var k in map.Where(x => x.Value.Length == 6))
                            {
                                if (Contains(outputValue, k.Value))
                                {
                                    s += k.Key;
                                }
                            }
                            break;
                    }
                }
                res += int.Parse(s);
            }

            return res.ToString();
        }

        public bool Contains(string value, string match)
        {
            foreach (var m in match)
            {
                bool contains = false;
                foreach (var s in value)
                {
                    if (m == s)
                    {
                        contains = true;
                    }
                }

                if (!contains) return false;
            }

            return true;
        }
    }
}
