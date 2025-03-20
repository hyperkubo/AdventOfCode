using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 6: Guard Gallivant ---
    public class Day06 : Day
    {
        private enum LookingAt
        {
            Up,
            Right,
            Down,
            Left
        };
        private readonly char _lookingRight = '>';
        private readonly char _lookingLeftt = '<';
        private readonly char _lookingUp = '^';
        private readonly char _lookingDown = 'v';
        private readonly char _obstacle = '#';
        private readonly char _freePosition = '.';
        private readonly char[] _guardPositions;

        public Day06(string inputFilePath, IFileManager? fileManager = null) : base (inputFilePath, fileManager)
        {
            _guardPositions =
            [
                _lookingRight,
                _lookingLeftt,
                _lookingUp,
                _lookingDown,
            ];
        }

        public override long Part1()
        {
            var (VisitedCoords, Loop) = VisitedCoordinates(InputLines);
            return VisitedCoords.Select(c => (c.Row, c.Col)).Distinct().Count();
        }

        public override long Part2()
#warning The method works but it is extremely slow.
        {
            var (VisitedCoords, Loop) = VisitedCoordinates(InputLines);
            var visitedCoords = VisitedCoords.Select(c => (c.Row, c.Col)).Distinct();

            List<string> newObstacleInLab;
            StringBuilder sb;

            int loopLabMaps = 0;
            foreach (var (row, col) in visitedCoords)
            {
                if (InputLines[row][col] == _freePosition)
                {
                    newObstacleInLab = InputLines;
                    sb = new StringBuilder(newObstacleInLab[row]);
                    sb[col] = _obstacle;
                    newObstacleInLab[row] = sb.ToString();
                    if (VisitedCoordinates(newObstacleInLab).Loop) loopLabMaps++;

                    sb[col] = _freePosition;
                    newObstacleInLab[row] = sb.ToString();
                }
            }

            return loopLabMaps;
        }

        #region Private helpers
        private (List<(int Row, int Col, LookingAt LookingAt)> VisitedCoords, bool Loop) VisitedCoordinates(List<string> labMap)
        {
            (List<(int Row, int Col, LookingAt LookingAt)> VisitedCoords, bool Loop) result = new();
            var guardRow = InputLines.First(l => l.IndexOfAny(_guardPositions) != -1);
            var guardCoords = (Row: InputLines.IndexOf(guardRow), Col: guardRow.IndexOfAny(_guardPositions), LookingAt: LookingAt.Up);
            var guardIsLookingAt = InputLines[guardCoords.Row][guardCoords.Col] switch
            {
                '^' => LookingAt.Up,
                '>' => LookingAt.Right,
                '<' => LookingAt.Left,
                'v' => LookingAt.Down,
                _ => throw new Exception("Something very very strange has just happened...")
            };
            guardCoords.LookingAt = guardIsLookingAt;
            List<(int, int, LookingAt)> visitedCoords = [(guardCoords)];
            var guardIsVisible = true;
            do{
                switch (guardIsLookingAt)
                {
                    case LookingAt.Up:
                        if (guardCoords.Row == 0)
                        {
                            guardIsVisible = false;
                            break;
                        }
                        if(labMap[guardCoords.Row - 1][guardCoords.Col] == _obstacle)
                        {
                            guardIsLookingAt = LookingAt.Right;
                        }
                        else
                        {
                            guardCoords.Row -= 1;
                        }
                        break;

                    case LookingAt.Right:
                        if (guardCoords.Col == labMap[0].Length - 1)
                        {
                            guardIsVisible = false;
                            break;
                        }
                        if(labMap[guardCoords.Row][guardCoords.Col + 1] == _obstacle)
                        {
                            guardIsLookingAt = LookingAt.Down;
                        }
                        else
                        {
                            guardCoords.Col += 1;
                        }
                        break;

                    case LookingAt.Left:
                        if (guardCoords.Col == 0)
                        {
                            guardIsVisible = false;
                            break;
                        }
                        if(labMap[guardCoords.Row][guardCoords.Col - 1] == _obstacle)
                        {
                            guardIsLookingAt = LookingAt.Up;
                        }
                        else
                        {
                            guardCoords.Col -= 1;
                        }
                        break;

                    case LookingAt.Down:
                        if (guardCoords.Row == labMap.Count - 1)
                        {
                            guardIsVisible = false;
                            break;
                        }
                        if(labMap[guardCoords.Row + 1][guardCoords.Col] == _obstacle)
                        {
                            guardIsLookingAt = LookingAt.Left;
                        }
                        else
                        {
                            guardCoords.Row += 1;
                        }
                        break;
                }
                guardCoords.LookingAt = guardIsLookingAt;
                if(guardIsVisible && visitedCoords.Exists(c => c.Equals(guardCoords)))
                {
                    result.Loop = true;
                    break;
                }
                visitedCoords.Add(guardCoords);
            }
            while (guardIsVisible);

            result.VisitedCoords = visitedCoords;
            return result;
        }
        #endregion
    }
}
