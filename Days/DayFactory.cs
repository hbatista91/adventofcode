using AdventOfCode2021.Days._1;
using AdventOfCode2021.Days._2;
using AdventOfCode2021.Days._3;

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
