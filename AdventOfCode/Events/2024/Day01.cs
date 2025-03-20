using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 1: Historian Hysteria ---
    public class Day01(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        public override long Part1()
        {
            int result = 0;

            var left = InputLines.Select(l => Convert.ToInt32(l.Split(' ').First())).OrderBy(a => a);
            var right = InputLines.Select(l => Convert.ToInt32(l.Split(' ').Last())).OrderBy(a => a);

            foreach (var (l, r) in left.Zip(right, (l, r) => (l, r)))
            {
                result += Math.Abs(l - r);
            }

            return result;
        }

        public override long Part2()
        {
            int result = 0;

            var left = InputLines.Select(l => Convert.ToInt32(l.Split(' ').First()));
            var right = InputLines.Select(l => Convert.ToInt32(l.Split(' ').Last()));

            foreach (var l in left)
            {
                result += l * right.Count(r => r == l);
            }

            return result;
        }
    }
}
