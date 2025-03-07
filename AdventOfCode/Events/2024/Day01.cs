using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 1: Historian Hysteria ---
    public class Day01(IFileManager? fileManager = null) : Day(fileManager)
    {
        public override int Part1(string inputFilePath)
        {
            int result = 0;

            var inputLines = FileManager.GetLines(inputFilePath);

            var left = inputLines.Select(l => Convert.ToInt32(l.Split(' ').First())).OrderBy(a => a);
            var right = inputLines.Select(l => Convert.ToInt32(l.Split(' ').Last())).OrderBy(a => a);

            foreach (var (l, r) in left.Zip(right, (l, r) => (l, r)))
            {
                result += Math.Abs(l - r);
            }

            return result;
        }

        public override int Part2(string inputFilePath)
        {
            throw new NotImplementedException();
            int result = 0;

            var inputLines = FileManager.GetLines(inputFilePath);

            var left = inputLines.Select(l => Convert.ToInt32(l.Split(' ').First()));
            var right = inputLines.Select(l => Convert.ToInt32(l.Split(' ').Last()));

            foreach (var l in left)
            {
                result += l * right.Count(r => r == l);
            }

            return result;
        }
    }
}
