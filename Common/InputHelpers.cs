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
    }
}
