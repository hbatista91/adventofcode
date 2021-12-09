using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Common
{
    public class InputHelpers
    {
        public static Dictionary<int, int> MapDictionary(string[] text)
        {
            var items = text[0].Split(',').Select(x => int.Parse(x)).ToList();
            return items.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        }

        public static Dictionary<int, string> MapToStringDictionary(string[] text)
        {
            return text
                .Select((x, i) => new { Item = x, Index = i })
                .ToDictionary(x => x.Index, x => x.Item);
        }

        public static int[,] MapToTwoDimensionalIntArray(string[] text)
        {
            var arr = new int[text.Count(), text[0].Length];

            for(var i = 0; i < text.Count(); i++)
            {
                var s = text[i].ToCharArray().Select(x => int.Parse(x+"")).ToArray();
                for(var j = 0; j < text[i].Length; j++)
                {
                    arr[i, j] = s[j];
                }
            }

            return arr;
        }
    }
}
