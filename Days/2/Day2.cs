using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._2
{
    public class Day2 : Day
    {
        public override string Source { get; set; }

        public Day2(string source)
        {
            Source = source;
        }

        public enum Direction { Forward, Down, Up }
        public readonly record struct Action(Direction direction, int value);

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Days\\2\\input.data"));

            Console.WriteLine("########## Day 2 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(text)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(text)}");
            Console.WriteLine("################################");
        }

        private string SolvePartOne(string[] text)
        {
            var arrayOfActions = Array.ConvertAll(text, i => new Action((Direction)Enum.Parse(typeof(Direction),i.Split(' ')[0], true), int.Parse(i.Split(' ')[1])));
            var horizontalPosition = 0;
            var depthPosition = 0;

            foreach (var action in arrayOfActions)
            {
                switch (action.direction)
                {
                    case Direction.Forward:
                        horizontalPosition += action.value;
                        break;
                    case Direction.Up:
                        depthPosition -= action.value;
                        break;
                    case Direction.Down:
                        depthPosition += action.value;
                        break;
                }
            }

            return (horizontalPosition * depthPosition).ToString();
        }

        private string SolvePartTwo(string[] text)
        {
            var arrayOfActions = Array.ConvertAll(text, i => new Action((Direction)Enum.Parse(typeof(Direction), i.Split(' ')[0], true), int.Parse(i.Split(' ')[1])));
            var horizontalPosition = 0;
            var depthPosition = 0;
            var aimPosition = 0;

            foreach (var action in arrayOfActions)
            {
                switch (action.direction)
                {
                    case Direction.Forward:
                        horizontalPosition += action.value;
                        depthPosition += aimPosition * action.value;
                        break;
                    case Direction.Up:
                        aimPosition -= action.value;
                        break;
                    case Direction.Down:
                        aimPosition += action.value;
                        break;
                }
            }

            return (horizontalPosition * depthPosition).ToString();
        }
    }
}
