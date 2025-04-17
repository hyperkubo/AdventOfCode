using AdventOfCode.Utils;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 8: Resonant Collinearity ---
    public class Day08(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private readonly Dictionary<char, List<Position>> _antennasCoordinates = [];
        private readonly Dictionary<int, List<(int, int)>> _indexCombinations = new()
        {
            { 1, new List<(int, int)>() },
            { 2, [(0,1)] }
        };
        private readonly char _freePosition = '.';

        public override long Part1()
        {
            List<Position> antinodesPosition = [];
            IndexCombinations(1);
            SaveAntennasCoordinates();

            foreach(var antennaType in _antennasCoordinates)
            {
                var antennasCombinations = IndexCombinations(antennaType.Value.Count);

                foreach(var antennaPair in antennasCombinations)
                {
                    Position antenna1 = antennaType.Value.ElementAt(antennaPair.Item1);
                    Position antenna2 = antennaType.Value.ElementAt(antennaPair.Item2);

                    var newAntinodePosition = Position.AntinodeToAntenna1(antenna1, antenna2);
                    antinodesPosition.Add(newAntinodePosition);
                    newAntinodePosition = Position.AntinodeToAntenna2(antenna1, antenna2);
                    antinodesPosition.Add(newAntinodePosition);
                }
            }
            return antinodesPosition
                .Distinct(new PositionEqualityComparer())
                .Count(p => !PositionOutOfBounds(p));
        }

        public override long Part2()
        {
            List<Position> antinodesPosition = [];
            IndexCombinations(1);
            SaveAntennasCoordinates();

            foreach(var antennaType in _antennasCoordinates)
            {
                var antennasCombinations = IndexCombinations(antennaType.Value.Count);

                foreach(var antennaPair in antennasCombinations)
                {
                    Position antenna1 = antennaType.Value.ElementAt(antennaPair.Item1);
                    Position antenna2 = antennaType.Value.ElementAt(antennaPair.Item2);
                    antinodesPosition.AddRange(AntinodesInLine(antenna1, antenna2));
                }
            }

            return antinodesPosition.Distinct(new PositionEqualityComparer()).Count();
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

        private void SaveAntennasCoordinates()
        {
            for(int row = 0; row < InputLines.Count; row++)
            {
                for(int col = 0; col < InputLines[0].Length; col++)
                {
                    var currElement = InputLines[row][col];
                    if (currElement != _freePosition)
                    {
                        if (_antennasCoordinates.TryGetValue(currElement, out List<Position>? value))
                        {
                            value.Add(new Position(row, col));
                        }
                        else
                        {
                            _antennasCoordinates.Add(currElement, [new Position(row, col)]);
                        }
                    }
                }
            }
        }

        private List<Position> AntinodesInLine(Position antenna1, Position antenna2)
        {
            Position ant1 = new(antenna1.Row, antenna1.Col);
            Position ant2 = new(antenna2.Row, antenna2.Col);
            List<Position> antinodes = [];
            Position temp;
            do
            {
                antinodes.Add(ant1);
                temp = Position.AntinodeToAntenna1(ant1, ant2);
                ant2 = new Position(ant1.Row, ant1.Col);
                ant1 = new Position(temp.Row, temp.Col);
            } while (!PositionOutOfBounds(temp));

            ant1 = new(antenna1.Row, antenna1.Col);
            ant2 = new(antenna2.Row, antenna2.Col);
            do
            {
                antinodes.Add(ant2);
                temp = Position.AntinodeToAntenna1(ant2, ant1);
                ant1 = new Position(ant2.Row, ant2.Col);
                ant2 = new Position(temp.Row, temp.Col);
            } while (!PositionOutOfBounds(temp));

            return antinodes;
        }

        private bool PositionOutOfBounds(Position position)
        {
            return position.Row < 0 || position.Row >= InputLines.Count
                || position.Col < 0 || position.Col >= InputLines[0].Length;
        }

        class Position(int row, int col)
        {
            public int Row { get; } = row;
            public int Col { get; } = col;

            public static Position AntinodeToAntenna1(Position antenna1, Position antenna2)
            {
                var row = antenna1.Row - (antenna2.Row - antenna1.Row);
                var col = antenna1.Col - (antenna2.Col - antenna1.Col);

                return new Position(row, col);
            }
            public static Position AntinodeToAntenna2(Position antenna1, Position antenna2)
            {
                var row = antenna2.Row + (antenna2.Row - antenna1.Row);
                var col = antenna2.Col + (antenna2.Col - antenna1.Col);

                return new Position(row, col);
            }
        }
        class PositionEqualityComparer : IEqualityComparer<Position>
        {
            public bool Equals(Position? x, Position? y)
            {
                if (x is null || y is null) return false;

                return x.Row == y.Row && x.Col == y.Col;
            }

            public int GetHashCode([DisallowNull] Position obj)
            {
                return obj.Row * obj.Col;
            }
        }
        #endregion
    }
}
