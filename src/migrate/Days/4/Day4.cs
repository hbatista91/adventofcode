using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days._4
{
    public class Day4 : Day
    {
        /*
        Quick note for context. The problem has the bingo cards to be always 5x5 and for that reason I'm simplifying the solution to avoid extra work
        */

        public override string Source { get; set; }
        public IEnumerable<int> Actions { get; private set; }
        public Dictionary<string, List<string>> Boards { get; private set; }

        public Day4(string source)
        {
            Source = source;
        }

        public override void Run()
        {
            // Fetch input data
            string[] text = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Days\\4\\{Source}.data"));

            SetupData(text);

            Console.WriteLine("########## Day 4 2021 ##########");
            SetupData(text);
            Console.WriteLine($"Part one solution: {SolvePartOne()}");
            SetupData(text);
            Console.WriteLine($"Part two solution: {SolvePartTwo()}");
            Console.WriteLine("################################");
        }

        private void SetupData(string[] text)
        {
            Actions = text[0].Split(',').Select(x => int.Parse(x));
            Boards = new Dictionary<string, List<string>>();

            var boardValues = new List<string>();
            foreach (var line in text.Skip(1))
            {
                if (line == string.Empty)
                {
                    if (boardValues.Count > 0)
                    {
                        Boards.Add(string.Join(',', boardValues), null);
                        boardValues = new List<string>();
                    }
                    continue;
                }
                boardValues.AddRange(line.Split(' ').Where(x => x != string.Empty));
            }
            Boards.Add(string.Join(',', boardValues), null);

            // Let's do all permutations of possible rows for the bingo validations
            foreach(var board in Boards)
            {
                var permutations = new List<string>();
                var boardSplit = board.Key.Split(',');

                for (int i = 0; i < 5; i++)
                {
                    permutations.Add(string.Join(',', boardSplit.Skip(i * 5).Take(5)));
                }

                for (int i = 0; i < 5; i++)
                {
                    var permutation = "";
                    for (int j = 0; j < 25; j+=5)
                    {
                        permutation += boardSplit[j + i] + ",";
                    }

                    permutations.Add(permutation.TrimEnd(','));
                }
                
                Boards[board.Key] = permutations;
            }
        }

        private string SolvePartOne()
        {
            foreach (var item in Actions)
            {
                var itemsToDelete = new List<string>();

                foreach (var board in Boards.ToList())
                {
                    itemsToDelete.Add(board.Key);

                    var newKey = string.Join(',',board.Key.Split(',').Select(x => x == item + "" ? "-1" : x));
                    var newPermutations = new List<string>();
                    foreach(var permutation in board.Value)
                    {
                        newPermutations.Add(string.Join(',',permutation.Split(',').Select(x => x == item + "" ? "-1" : x)));
                    }

                    if (!Boards.ContainsKey(newKey))
                    {
                        Boards.Add(newKey, newPermutations);
                    }
                    else itemsToDelete.Remove(board.Key);
                }

                foreach (var itemToDelete in itemsToDelete)
                {
                    Boards.Remove(itemToDelete);
                }

                foreach (var board in Boards.ToList())
                {
                    if (board.Value.Any(x => x == "-1,-1,-1,-1,-1"))
                    {
                        // Winner board
                        return (item * board.Key.Split(',').Select(x => int.Parse(x)).Where(x => x != -1).Sum()).ToString();
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// This part two is basically the reverse and just remove that particular board from the list until only one is left
        /// </summary>
        /// <returns></returns>
        private string SolvePartTwo()
        {
            foreach (var item in Actions)
            {
                var itemsToDelete = new List<string>();

                foreach (var board in Boards.ToList())
                {
                    itemsToDelete.Add(board.Key);

                    var newKey = string.Join(',', board.Key.Split(',').Select(x => x == item + "" ? "-1" : x));
                    var newPermutations = new List<string>();
                    foreach (var permutation in board.Value)
                    {
                        newPermutations.Add(string.Join(',', permutation.Split(',').Select(x => x == item + "" ? "-1" : x)));
                    }

                    if (!Boards.ContainsKey(newKey))
                    {
                        Boards.Add(newKey, newPermutations);
                    }
                    else itemsToDelete.Remove(board.Key);
                }

                foreach (var itemToDelete in itemsToDelete)
                {
                    Boards.Remove(itemToDelete);
                }

                itemsToDelete = new List<string>();

                foreach (var board in Boards.ToList())
                {
                    if (board.Value.Any(x => x == "-1,-1,-1,-1,-1"))
                    {
                        itemsToDelete.Add(board.Key);
                    }
                }

                foreach (var itemToDelete in itemsToDelete)
                {
                    Boards.Remove(itemToDelete);
                    if (Boards.Count == 0)
                    {
                        return (item * itemToDelete.Split(',').Select(x => int.Parse(x)).Where(x => x != -1).Sum()).ToString();
                    }
                }
            }

            return "";
        }
    }
}
