using AdventOfCode2021.Days;

Console.WriteLine("AdventOfCode 2021");
var factory = new DayFactory();

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
Console.Write("Pick a day:");
string day = Console.ReadLine();

Console.Write("Pick sample or input data:");
string source = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

factory.Run(day, source);