namespace AdventOfCode2015
{
    public class Day18
    {
        [Theory]
        [InlineData("")]
        public void Part1(string expected)
        {
            var lines = Helpers.FetchContent(18, true);
            var result = ExecuteLogicPart1(lines);

            Assert.Equal(expected, result);
        }

        private object ExecuteLogicPart1(string[] lines)
        {
            throw new NotImplementedException();
        }

        [Theory]
        [InlineData("")]
        public void Part2(string expected)
        {
            var lines = Helpers.FetchContent(18, false);
            var result = ExecuteLogicPart2(lines);

            Assert.Equal(expected, result);
        }

        private object ExecuteLogicPart2(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}

