namespace AdventOfCode2015
{
    public class Day01
    {
        [Theory]
        [InlineData("(())",0)]
        [InlineData("()()", 0)]
        [InlineData("(((", 3)]
        [InlineData("(()(()(", 3)]
        [InlineData("))(((((", 3)]
        [InlineData("())", -1)]
        [InlineData("))(", -1)]
        [InlineData(")))", -3)]
        [InlineData(")())())", -3)]
        public void ValidatePart1(string input, int expected)
        {
            var result = ExecuteLogicPart1(new string[] {input});

            Assert.Equal(expected.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("138")]
        public void Part1(string expected)
        {
            var lines = Helpers.FetchContent(1, true);
            var result = ExecuteLogicPart1(lines);

            Assert.Equal(expected, result);
        }

        private object ExecuteLogicPart1(string[] lines)
        {
            var floor = 0;
            foreach (var item in lines[0].ToArray())
            {
                switch (item)
                {
                    case '(':
                        floor++;
                        break;
                    case ')':
                        floor--;
                        break;
                }
            }

            return floor.ToString();
        }

        [Theory]
        [InlineData(")", 1)]
        [InlineData("()())", 5)]
        public void ValidatePart2(string input, int expected)
        {
            var result = ExecuteLogicPart2(new string[] { input });

            Assert.Equal(expected.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("1771")]
        public void Part2(string expected)
        {
            var lines = Helpers.FetchContent(1, false);
            var result = ExecuteLogicPart2(lines);

            Assert.Equal(expected.ToString(), result.ToString());
        }

        private object ExecuteLogicPart2(string[] lines)
        {
            var floor = 0;
            var index = 1;
            foreach (var item in lines[0].ToArray())
            {
                switch (item)
                {
                    case '(':
                        floor++;
                        break;
                    case ')':
                        floor--;
                        break;
                }
                if (floor == -1)
                {
                    return index;
                }
                index++;
            }

            return floor.ToString();
        }
    }
}
