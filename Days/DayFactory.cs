using AdventOfCode2021.Days._1;
using AdventOfCode2021.Days._2;
using AdventOfCode2021.Days._3;
using AdventOfCode2021.Days._4;
using AdventOfCode2021.Days._5;
using AdventOfCode2021.Days._6;
using AdventOfCode2021.Days._7;
using AdventOfCode2021.Days._8;
using AdventOfCode2021.Days._9;
using AdventOfCode2021.Days._10;
using AdventOfCode2021.Days._11;

namespace AdventOfCode2021.Days
{
    public class DayFactory
    {
        internal void Run(string day, string source)
        {
            try
            {
                int dayParsed = int.Parse(day);
                
                if (dayParsed <= 0 || dayParsed > 25)
                {
                    throw new ArgumentException("Invalid day");
                }

                if (source != "input" && source != "sample")
                {
                    throw new ArgumentException("Invalid source");
                }

                Day? d = null;
                switch (dayParsed)
                {
                    case 1:
                        d = new Day1(source);
                        break;
                    case 2:
                        d = new Day2(source);
                        break;
                    case 3:
                        d = new Day3(source);
                        break;
                    case 4:
                        d = new Day4(source);
                        break;
                    case 5:
                        d = new Day5(source);
                        break;
                    case 6:
                        d = new Day6(source);
                        break;
                    case 7:
                        d = new Day7(source);
                        break;
                    case 8:
                        d = new Day8(source);
                        break;
                    case 9:
                        d = new Day9(source);
                        break;
                    case 10:
                        d = new Day10(source);
                        break;
                    case 11:
                        d = new Day11(source);
                        break;
                }

                if (d != null)
                {
                    d.Run();
                }
                else
                {
                    Console.WriteLine("Day not yet implemented");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
