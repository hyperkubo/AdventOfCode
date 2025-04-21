using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 9: Disk Fragmenter ---
    public class Day09(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private string _diskMap = "";
        private readonly List<long> _diskBlocks = [];
        public override long Part1()
        {
            _diskMap = InputLines[0];
            SetDiskBlocks();

            while (DiscBlocksHasLooseFreeSpace())
            {
                MoveBlockToFreeSpace();
            }

            return _diskBlocks
                .Where(b => b != -1)
                .Select((block, index) => block * index)
                .Sum();
        }
        public override long Part2()
        {
            throw new NotImplementedException();
        }

        #region Private methods
        private void SetDiskBlocks()
        {
            var diskMapArr = _diskMap.Select(c => Convert.ToUInt16(c.ToString())).ToArray();

            for(int i = 0; i < diskMapArr.Length; i++)
            {
                _diskBlocks.AddRange(Enumerable.Range(1, diskMapArr[i]).Select(e => i % 2 == 0 ? (long)(i / 2) : -1));
            }
        }

        private bool DiscBlocksHasLooseFreeSpace()
        {
            return _diskBlocks.IndexOf(-1) < _diskBlocks.FindLastIndex(b => b != -1);
        }

        private void MoveBlockToFreeSpace()
        {
            var firstFreeSpaceIdx = _diskBlocks.IndexOf(-1);
            var lastFileBlockIdx = _diskBlocks.FindLastIndex(b => b != -1);

            _diskBlocks[firstFreeSpaceIdx] = _diskBlocks[lastFileBlockIdx];
            _diskBlocks[lastFileBlockIdx] = -1;
        }
        #endregion
    }
}
