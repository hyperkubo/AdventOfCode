using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 3: Mull It Over ---
    public partial class Day03(IFileManager? fileManager = null) : Day(fileManager)
    {
        public override int Part1(string inputFilePath)
        {
            var input = string.Join("", FileManager.GetLines(inputFilePath));

            int mulResult = 0;
            foreach(Match match in MulFuncRegex().Matches(input))
            {
                var vals = MulValuesRegex().Match(match.Value).Value;
                var op1 = Convert.ToInt32(vals.Split(',')[0]);
                var op2 = Convert.ToInt32(vals.Split(',')[1]);
                mulResult += op1 * op2;
            }

            return mulResult;
        }

        public override int Part2(string inputFilePath)
        {
            throw new NotImplementedException();
        }

        #region Private helpers
        [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
        private static partial Regex MulFuncRegex();
        [GeneratedRegex(@"\d{1,3},\d{1,3}")]
        private static partial Regex MulValuesRegex();
        #endregion
    }
}
