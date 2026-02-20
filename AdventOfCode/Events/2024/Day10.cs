using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 10: Hoof It ---
    public class Day10(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private readonly char _trailStart = '0';
        private readonly char _trailEnd = '9';
        private readonly List<HikingTrail> _hikingHeads = [];
        private int MaxRow => InputLines.Count - 1;
        private int MaxCol => InputLines.First().Length - 1;
        public override T Part1<T>()
        {
            FindTrailheads();

            foreach(var hikingHead in _hikingHeads)
            {
                GetNextHikingTrailSteps(hikingHead);
            }

            return (T)(object)_hikingHeads.Sum(h => h.Score);
        }

        public override T Part2<T>()
        {
            FindTrailheads();

            foreach(var hikingHead in _hikingHeads)
            {
                GetNextHikingTrailSteps(hikingHead);
            }

            return (T)(object)_hikingHeads.Sum(h => h.Rating);
        }

        #region Private methods
        private void FindTrailheads()
        {
            for(int row = 0; row <= MaxRow; row++)
            {
                for(int col = 0; col <= MaxCol; col++)
                {
                    if (InputLines[row][col] == _trailStart)
                    {
                        _hikingHeads.Add(new(_trailStart, row, col));
                    }
                }
            }
        }

        private void GetNextHikingTrailSteps(HikingTrail hikingTrail, HikingTrail? start = null)
        {
            start ??= hikingTrail;
            if (hikingTrail.Height == _trailEnd)
            {
                if(!start.DifferentHikingEnds.Exists(h => h.Row == hikingTrail.Row && h.Col == hikingTrail.Col))
                {
                    start.DifferentHikingEnds.Add(hikingTrail);
                }
                start.Rating++;
                return;
            }
            var nextCol = hikingTrail.Col + 1;
            var prevCol = hikingTrail.Col - 1;
            var nextRow = hikingTrail.Row + 1;
            var prevRow = hikingTrail.Row - 1;

            if(prevCol >= 0 && InputLines[hikingTrail.Row][prevCol] == hikingTrail.Height + 1)
            {
                hikingTrail.NextPositions.Add(new((char)(hikingTrail.Height + 1), hikingTrail.Row, prevCol));
            }
            if(nextCol <= MaxCol && InputLines[hikingTrail.Row][nextCol] == hikingTrail.Height + 1)
            {
                hikingTrail.NextPositions.Add(new((char)(hikingTrail.Height + 1), hikingTrail.Row, nextCol));
            }
            if(prevRow >= 0 && InputLines[prevRow][hikingTrail.Col] == hikingTrail.Height + 1)
            {
                hikingTrail.NextPositions.Add(new((char)(hikingTrail.Height + 1), prevRow, hikingTrail.Col));
            }
            if(nextRow <= MaxRow && InputLines[nextRow][hikingTrail.Col] == hikingTrail.Height + 1)
            {
                hikingTrail.NextPositions.Add(new((char)(hikingTrail.Height + 1), nextRow, hikingTrail.Col));
            }
            foreach(var nextStep in hikingTrail.NextPositions)
            {
                GetNextHikingTrailSteps(nextStep, start);
            }
        }

        class HikingTrail(char height, int row, int col)
        {
            public char Height { get; set; } = height;
            public int Row { get; set; } = row;
            public int Col { get; set; } = col;
            public int Score => DifferentHikingEnds.Count;
            public int Rating { get; set; }
            public List<HikingTrail> DifferentHikingEnds { get; set; } = [];
            public List<HikingTrail> NextPositions { get; set; } = [];
        }
        #endregion
    }
}
