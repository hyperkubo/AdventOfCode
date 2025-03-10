using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 2: Red-Nosed Reports ---
    public class Day02(IFileManager? fileManager = null) : Day(fileManager)
    {
        public override int Part1(string inputFilePath)
        {
            var inputLines = FileManager.GetLines(inputFilePath);
            var reports = inputLines.Select(l => l.Split(' ').Select(n => Convert.ToInt32(n)));

            return reports.Count(report => IsSafe([..report]));
        }

        public override int Part2(string inputFilePath)
        {
            throw new NotImplementedException();
        }

        #region Private helper methods
        private static bool IsSafe(int[] report)
        {
            int[] diffs = new int[report.Length - 1];
            for (int i = 0; i < report.Length - 1; i++)
            {
                diffs[i] = report[i] - report[i + 1];
            }
            if (diffs.Any(d => d >= 0) && diffs.Any(d => d <= 0)){
                return false;
            }

            return diffs.All(d => Math.Abs(d) is 1 or 2 or 3);
        }
        #endregion
    }
}
