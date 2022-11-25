namespace AdventOfCode2015
{
    public class Helpers
    {
        public static string[] FetchContent(int day, bool sampleFlag)
        {
            var dayPath = day <= 9 ? "0" + day : day.ToString();
            var samplePath = sampleFlag == true ? "-sample" : "";
            return File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "data", $"day{dayPath}{samplePath}.data"));
        }
    }
}
