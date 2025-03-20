using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 3: Mull It Over ---
    public partial class Day03(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        public override long Part1()
        {
            var input = string.Join("", InputLines);

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

        public override long Part2()
        {
            var splitByDos = string.Join("", InputLines).Split("do()");
            string mulsInDos = string.Empty;
            foreach(string dos in splitByDos)
            {
                mulsInDos += dos.Split("don't()")[0];
            }

            int mulResult = 0;

            foreach(Match match in MulFuncRegex().Matches(mulsInDos))
            {
                var vals = MulValuesRegex().Match(match.Value).Value;
                var op1 = Convert.ToInt32(vals.Split(',')[0]);
                var op2 = Convert.ToInt32(vals.Split(',')[1]);
                mulResult += op1 * op2;
            }

            return mulResult;
        }

        #region Private helpers
        [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
        private static partial Regex MulFuncRegex();
        [GeneratedRegex(@"\d{1,3},\d{1,3}")]
        private static partial Regex MulValuesRegex();
        #endregion
    }
}
