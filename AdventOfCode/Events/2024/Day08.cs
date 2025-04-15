using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 8: Resonant Collinearity ---
    public class Day08(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private readonly Dictionary<char, List<(int Row, int Col)>> _antennasCoordinates = [];
        private readonly Dictionary<int, List<(int, int)>> _indexCombinations = new()
        {
            { 1, new List<(int, int)>() },
            { 2, [(0,1)] }
        };
        private readonly char _freePosition = '.';
        public override long Part1()
        {
            List<(int Row, int Col)> antinodesCoords = [];
            IndexCombinations(1);
            for(int row = 0; row < InputLines.Count; row++)
            {
                for(int col = 0; col < InputLines[0].Length; col++)
                {
                    var currElement = InputLines[row][col];
                    if (currElement != _freePosition)
                    {
                        if (_antennasCoordinates.TryGetValue(currElement, out List<(int Row, int Col)>? value))
                        {
                            value.Add((row, col));
                        }
                        else
                        {
                            _antennasCoordinates.Add(currElement, [(row, col)]);
                        }
                    }
                }
            }

            foreach(var antennaType in _antennasCoordinates)
            {
                var antennasCombinations = IndexCombinations(antennaType.Value.Count);

                foreach(var antennaPair in antennasCombinations)
                {
                    var (ant1row, ant1col) = antennaType.Value.ElementAt(antennaPair.Item1);
                    var (ant2row, ant2col) = antennaType.Value.ElementAt(antennaPair.Item2);

                    var antinode1Row = ant1row - (ant2row - ant1row);
                    var antinode1Col = ant1col - (ant2col - ant1col);
                    antinodesCoords.Add((antinode1Row, antinode1Col));

                    var antinode2Row = ant2row + (ant2row - ant1row);
                    var antinode2Col = ant2col + (ant2col - ant1col);
                    antinodesCoords.Add((antinode2Row, antinode2Col));
                }
            }
            return antinodesCoords
                .Distinct()
                .Count(c => c.Row >= 0 && c.Row < InputLines.Count
                         && c.Col >= 0 && c.Col < InputLines[0].Length);
        }

        public override long Part2()
        {
            throw new NotImplementedException();
        }

        #region Private helpers
        private List<(int, int)> IndexCombinations(int antennasCount)
        {
            List<(int, int)> result = [];
            if (_indexCombinations.ContainsKey(antennasCount))
            {
                for(int i = antennasCount; i > 1; i--)
                {
                    result.AddRange(_indexCombinations[i]);
                }
            }
            else
            {
                int maxValueSaved = _indexCombinations.Max(c => c.Key);

                for (int i = 1; i <= antennasCount; i++)
                {
                    if (i <= maxValueSaved)
                    {
                        result.AddRange(_indexCombinations[i]);
                    }
                    else
                    {
                        List<(int, int)> temp = [];
                        for (int j = 0; j < i - 1; j++)
                        {
                            temp.Add((i - 1, j));
                        }
                        _indexCombinations.Add(i, temp);
                        result.AddRange(temp);
                    }
                }
            }

            return result;
        }
        #endregion
    }
}
