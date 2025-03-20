using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 2: Red-Nosed Reports ---
    public class Day02(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        public override long Part1()
        {
            var reports = InputLines.Select(l => l.Split(' ').Select(n => Convert.ToInt32(n)));

            return reports.Count(report => IsSafe([..report]));
        }

        public override long Part2()
        {
            var reports = InputLines.Select(l => l.Split(' ').Select(n => Convert.ToInt32(n)));

            var unsafeReports = reports.Where(report => !IsSafe([..report]));
            var totalSafeReports = reports.Count() - unsafeReports.Count();

            int newSafeReportsCount = 0;
            foreach (var unsafeReport in unsafeReports)
            {
                for(int i = 0; i < unsafeReport.Count(); i++)
                {
                    var report = unsafeReport.ToList();
                    report.RemoveAt(i);
                    if (IsSafe([.. report]))
                    {
                        newSafeReportsCount++;
                        break;
                    }
                }
            }

            return totalSafeReports + newSafeReportsCount;
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
