namespace AdventOfCode2015
{
    public class Day03
    {
        [Theory]
        [InlineData("2565")]
        public void Part1(string expected)
        {
            var lines = Helpers.FetchContent(3, true);
            var result = ExecuteLogicPart1(lines);

            Assert.Equal(expected, result.ToString());
        }

        private object ExecuteLogicPart1(string[] lines)
        {
            return CalculatePositions(lines[0].ToArray()).GroupBy(x => x).Count();
        }

        [Theory]
        [InlineData("2639")]
        public void Part2(string expected)
        {
            var lines = Helpers.FetchContent(3, false);
            var result = ExecuteLogicPart2(lines);

            Assert.Equal(expected, result.ToString());
        }

        private object ExecuteLogicPart2(string[] lines)
        {
            var oddSubset = lines[0].ToArray().Select((s, i) => new { s, i }).Where(w => w.i % 2 != 0).Select(s => s.s).ToArray();
            var evenSubset = lines[0].ToArray().Select((s, i) => new { s, i }).Where(w => w.i % 2 == 0).Select(s => s.s).ToArray();

            var joinResult = CalculatePositions(oddSubset).Concat(CalculatePositions(evenSubset));

            return joinResult.GroupBy(x => x).Count();
        }

        private List<string> CalculatePositions(char[] array)
        {
            int x, y;
            x = y = 0;
            var positions = new List<string>() { "0,0" };

            foreach (var item in array)
            {
                switch (item)
                {
                    case '^':
                        y++;
                        break;
                    case 'v':
                        y--;
                        break;
                    case '<':
                        x--;
                        break;
                    case '>':
                        x++;
                        break;
                }
                positions.Add($"{x},{y}");
            }

            return positions;
        }
    }
}

