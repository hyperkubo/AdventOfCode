using AdventOfCode.Utils;

namespace AdventOfCode.Events
{
    public abstract class Day(string inputFilePath, IFileManager? fileManager) : IDay
    {
        private readonly string _inputFilePath = inputFilePath;
        private readonly IFileManager _fileManager = fileManager ?? new FileManager();
        private List<string>? _inputLines;
        protected List<string> InputLines { get {
                _inputLines ??= _fileManager.GetLines(_inputFilePath);
                return _inputLines;
            } }

        public abstract long Part1();
        public abstract long Part2();
    }
}
