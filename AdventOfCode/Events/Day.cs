using AdventOfCode.Utils;

namespace AdventOfCode.Events
{
    public abstract class Day(IFileManager? fileManager) : IDay
    {
        protected readonly IFileManager _fileManager = fileManager ?? new FileManager();

        public abstract int Part1(string inputFilePath);
        public abstract int Part2(string inputFilePath);
    }
}
