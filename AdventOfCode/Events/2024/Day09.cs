using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 9: Disk Fragmenter ---
    public class Day09(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private string _diskMap = "";
        private readonly List<long> _diskBlocks = [];
        private int _maxFileId = 0;
        private List<(int StartsAt, int Size)> _freeSizeMap = [];
        public override T Part1<T>()
        {
            _diskMap = InputLines[0];
            SetDiskBlocks();

            while (DiscBlocksHasLooseFreeSpace())
            {
                MoveBlockToFreeSpace();
            }

            return (T)(object)_diskBlocks
                .Where(b => b != -1)
                .Select((block, index) => block * index)
                .Sum();
        }
        public override T Part2<T>()
        {
            _diskMap = InputLines[0];
            SetDiskBlocks();

            for(int i = _maxFileId; i > 1; i--)
            {
                MoveFileToFreeSpace(i);
            }

            return (T)(object)_diskBlocks
                .Select((block, index) => new { block, index })
                .Where(b => b.block >= 0)
                .Select(b => b.block * b.index)
                .Sum();
        }

        #region Private methods
        private void SetDiskBlocks()
        {
            var diskMapArr = _diskMap.Select(c => Convert.ToUInt16(c.ToString())).ToArray();

            for(int i = 0; i < diskMapArr.Length; i++)
            {
                long block;
                if (i % 2 == 0)
                {
                    block = (long)i / 2;
                    _maxFileId = i / 2;
                }
                else
                {
                    block = -1;
                    if (diskMapArr[i] != 0)
                    {
                        _freeSizeMap.Add((_diskBlocks.Count, diskMapArr[i]));
                    }
                }
                _diskBlocks.AddRange(Enumerable.Range(1, diskMapArr[i]).Select(e => block));
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

        private void MoveFileToFreeSpace(int fileId)
        {
            var fileSize = FileSizeById(fileId);
            var freeSpaceIdx = _freeSizeMap.FindIndex(m => m.Size >= fileSize.Size);
            if (freeSpaceIdx >= 0 && fileSize.StartsAt > _freeSizeMap[freeSpaceIdx].StartsAt)
            {
                for (int i = 0; i < fileSize.Size; i++)
                {
                    _diskBlocks[_freeSizeMap[freeSpaceIdx].StartsAt + i] = fileId;
                }
                var freeSize = _freeSizeMap.ElementAt(freeSpaceIdx);
                freeSize.Size -= fileSize.Size;
                freeSize.StartsAt += fileSize.Size;
                if(freeSize.Size == 0)
                {
                    _freeSizeMap.RemoveAt(freeSpaceIdx);
                }
                else
                {
                    _freeSizeMap[freeSpaceIdx] = freeSize;
                }

                for (int i = fileSize.StartsAt; i <= fileSize.EndsAt; i++)
                {
                    _diskBlocks[i] = -1;
                }
                var idx = _freeSizeMap.FindIndex(f => f.StartsAt > fileSize.StartsAt);
                if(idx == -1)
                {
                    _freeSizeMap.Add((fileSize.StartsAt, fileSize.Size));
                }
                else
                {
                    _freeSizeMap.Insert(idx, (fileSize.StartsAt, fileSize.Size));
                }
                RecalculateFreeSpaceMap();
            }
        }

        private (int StartsAt, int EndsAt, int Size) FileSizeById(int id)
        {
            return (_diskBlocks.IndexOf(id), _diskBlocks.LastIndexOf(id), _diskBlocks.Count(b => b == id));
        }

        private void RecalculateFreeSpaceMap()
        {
            for(int i = 0; i < _freeSizeMap.Count - 1; i++)
            {
                var currentFreeSize = _freeSizeMap[i];
                var nextFreeSize = _freeSizeMap[i + 1];
                if (currentFreeSize .StartsAt + currentFreeSize .Size == nextFreeSize.StartsAt)
                {
                    currentFreeSize.Size += nextFreeSize.Size;
                    _freeSizeMap[i] = currentFreeSize;
                    _freeSizeMap.RemoveAt(i + 1);
                    i--;
                }
            }
        }
        #endregion
    }
}
