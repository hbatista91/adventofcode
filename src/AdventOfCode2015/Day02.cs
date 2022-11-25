namespace AdventOfCode2015
{
    public class Day02
    {
        [Theory]
        [InlineData("2x3x4", 58)]
        [InlineData("1x1x10", 43)]
        public void ValidatePart1(string input, int expected)
        {
            var result = ExecuteLogicPart1(new string[] { input });

            Assert.Equal(expected.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("1586300")]
        public void Part1(string expected)
        {
            var lines = Helpers.FetchContent(2, true);
            var result = ExecuteLogicPart1(lines);

            Assert.Equal(expected, result.ToString());
        }

        private object ExecuteLogicPart1(string[] lines)
        {
            var result = 0;

            foreach(var line in lines)
            {
                var lineSplit = line.Split('x');
                result += CalculateSurfaceNeeded(int.Parse(lineSplit[0]), int.Parse(lineSplit[1]), int.Parse(lineSplit[2]));
            }

            return result;
        }

        [Theory]
        [InlineData("2x3x4", 34)]
        [InlineData("1x1x10", 14)]
        public void ValidatePart2(string input, int expected)
        {
            var result = ExecuteLogicPart2(new string[] { input });

            Assert.Equal(expected.ToString(), result.ToString());
        }

        [Theory]
        [InlineData("3737498")]
        public void Part2(string expected)
        {
            var lines = Helpers.FetchContent(2, false);
            var result = ExecuteLogicPart2(lines);

            Assert.Equal(expected, result.ToString());
        }

        private object ExecuteLogicPart2(string[] lines)
        {
            var result = 0;

            foreach (var line in lines)
            {
                var lineSplit = line.Split('x');
                result += CalculateRibbonNeeded(int.Parse(lineSplit[0]), int.Parse(lineSplit[1]), int.Parse(lineSplit[2]));
            }

            return result;
        }

        private int CalculateRibbonNeeded(int l, int w, int h)
        {
            var smallestValue = new int[] 
            { 
                2*l + 2*w,
                2*w + 2*h,
                2*l + 2*h
            }.Min();
            return (l * w * h) + smallestValue;
        }

        private int CalculateSurfaceNeeded(int l, int w, int h)
        {
            var smallestArea = new int[] { l*w, w*h, l*h }.Min();
            return 2 * l * w + 2 * w * h + 2 * h * l + smallestArea;
        }
    }
}

