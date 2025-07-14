using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 15: Warehouse Woes ---
    public class Day15(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private const char Robot = '@';
        private const char Wall = '#';
        private const char Box = 'O';
        private const char EmptySpace = '.';
        private const string BoxWide = "[]";
        private List<StringBuilder> _warehouseMap = [];
        private List<RobotDirection> _robotMovements = [];
        (int Row, int Col) _robotPosition;
        private int MaxRow => InputLines.Count - 3;
        private int MaxCol => InputLines.First().Length - 1;
        private int MaxColWide => (InputLines.First().Length * 2) - 1;

        public override long Part1()
        {
            _warehouseMap = [.. InputLines.GetRange(0, InputLines.Count - 2).Select(line => new StringBuilder(line))];
            int rowRobot = _warehouseMap.ToList().FindIndex(line => line.ToString().Contains(Robot));
            int colRobot = _warehouseMap.ElementAt(rowRobot).ToString().ToList().FindIndex(c => c ==Robot);
            _robotPosition = (rowRobot, colRobot);
            _robotMovements = [.. InputLines.Last().Select(c => c switch
            {
                '^' => RobotDirection.Up,
                '>' => RobotDirection.Right,
                'v' => RobotDirection.Down,
                '<' => RobotDirection.Left,
                _ => throw new ArgumentException("What kind of direction is that...?")
            })];

            _robotMovements.ForEach(direction => PushBoxes(direction));

            List<(int Row, int Col)> boxesCoords = [];
            for(int row = 1; row < MaxRow; row++)
            {
                for(int col = 1; col < MaxCol; col++)
                {
                    if (_warehouseMap[row][col] == Box)
                    {
                        boxesCoords.Add((row, col));
                    }
                }
            }

            return boxesCoords.Sum(c => (c.Row * 100) + c.Col);
        }

        public override long Part2()
        {
            _warehouseMap = [.. InputLines.GetRange(0, InputLines.Count - 2).Select(line => new StringBuilder(line))];
            _warehouseMap.ToList().ForEach(line =>
            {
                line.Replace(Wall.ToString(), new string(Wall, 2));
                line.Replace(EmptySpace.ToString(), new string(EmptySpace, 2));
                line.Replace(Box.ToString(), BoxWide);
                line.Replace(Robot.ToString(), $"{Robot}{EmptySpace}");
            });
            int rowRobot = _warehouseMap.ToList().FindIndex(line => line.ToString().Contains(Robot));
            int colRobot = _warehouseMap.ElementAt(rowRobot).ToString().ToList().FindIndex(c => c == Robot);
            _robotPosition = (rowRobot, colRobot);
            _robotMovements = [.. InputLines.Last().Select(c => c switch
            {
                '^' => RobotDirection.Up,
                '>' => RobotDirection.Right,
                'v' => RobotDirection.Down,
                '<' => RobotDirection.Left,
                _ => throw new ArgumentException("What kind of direction is that...?")
            })];

            _robotMovements.ForEach(direction => PushWideBoxes(direction));

            List<(int Row, int Col)> boxesCoords = [];
            for(int row = 1; row < MaxRow; row++)
            {
                for(int col = 2; col < MaxColWide; col++)
                {
                    if (_warehouseMap[row][col] == BoxWide[0])
                    {
                        boxesCoords.Add((row, col));
                    }
                }
            }

            return boxesCoords.Sum(c => (c.Row * 100) + c.Col);
        }

        #region Private methods
        private void PushBoxes(RobotDirection direction)
        {
            StringBuilder lineConfiguration;
            switch (direction)
            {
                case RobotDirection.Up:
                    lineConfiguration = new StringBuilder(string.Join("",
                        _warehouseMap
                        .Select((l, i) => new { Line = l, Index = i })
                        .Where(e => e.Index < _robotPosition.Row)
                        .Select(l => l.Line[_robotPosition.Col])
                     ));
                    if(lineConfiguration.ToString().Last() == EmptySpace)
                    {
                        _warehouseMap[_robotPosition.Row - 1][_robotPosition.Col] = Robot;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _robotPosition.Row--;
                    }
                    else if(lineConfiguration.ToString().Contains(EmptySpace)
                        && lineConfiguration.ToString().LastIndexOf(EmptySpace) > lineConfiguration.ToString().LastIndexOf(Wall))
                    {
                        _warehouseMap[lineConfiguration.ToString().LastIndexOf(EmptySpace)][_robotPosition.Col] = Box;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _warehouseMap[_robotPosition.Row - 1][_robotPosition.Col] = Robot;
                        _robotPosition.Row--;
                    }
                    break;
                case RobotDirection.Right:
                    lineConfiguration = new StringBuilder(_warehouseMap[_robotPosition.Row].ToString()[(_robotPosition.Col + 1)..]);
                    if(lineConfiguration.ToString().First() == EmptySpace)
                    {
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col + 1] = Robot;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _robotPosition.Col++;
                    }
                    else if(lineConfiguration.ToString().Contains(EmptySpace)
                        && lineConfiguration.ToString().IndexOf(EmptySpace) < lineConfiguration.ToString().IndexOf(Wall))
                    {
                        _warehouseMap[_robotPosition.Row][lineConfiguration.ToString().IndexOf(EmptySpace) + _robotPosition.Col + 1] = Box;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col + 1] = Robot;
                        _robotPosition.Col++;
                    }
                    break;
                case RobotDirection.Down:
                    lineConfiguration = new StringBuilder(string.Join("",
                        _warehouseMap
                        .Select((l, i) => new { Line = l, Index = i })
                        .Where(e => e.Index > _robotPosition.Row)
                        .Select(l => l.Line[_robotPosition.Col])
                     ));
                    if(lineConfiguration.ToString().First() == EmptySpace)
                    {
                        _warehouseMap[_robotPosition.Row + 1][_robotPosition.Col] = Robot;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _robotPosition.Row++;
                    }
                    else if(lineConfiguration.ToString().Contains(EmptySpace)
                        && lineConfiguration.ToString().IndexOf(EmptySpace) < lineConfiguration.ToString().IndexOf(Wall))
                    {
                        _warehouseMap[lineConfiguration.ToString().IndexOf(EmptySpace) + _robotPosition.Row + 1][_robotPosition.Col] = Box;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _warehouseMap[_robotPosition.Row + 1][_robotPosition.Col] = Robot;
                        _robotPosition.Row++;
                    }
                    break;
                case RobotDirection.Left:
                    lineConfiguration = new StringBuilder(_warehouseMap[_robotPosition.Row].ToString()[..(_robotPosition.Col)]);
                    if(lineConfiguration.ToString().Last() == EmptySpace)
                    {
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col - 1] = Robot;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _robotPosition.Col--;
                    }
                    else if(lineConfiguration.ToString().Contains(EmptySpace)
                        && lineConfiguration.ToString().LastIndexOf(EmptySpace) > lineConfiguration.ToString().LastIndexOf(Wall))
                    {
                        _warehouseMap[_robotPosition.Row][lineConfiguration.ToString().LastIndexOf(EmptySpace)] = Box;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col - 1] = Robot;
                        _robotPosition.Col--;
                    }
                    break;
            }
        }

        private void PushWideBoxes(RobotDirection direction)
        {
            StringBuilder lineConfiguration;
            switch (direction)
            {
                case RobotDirection.Up:
                case RobotDirection.Down:
                    int nextRow = direction == RobotDirection.Up ? _robotPosition.Row - 1 : _robotPosition.Row + 1;
                    if(nextRow > 0 && nextRow < MaxRow && CanPushVertical(nextRow, _robotPosition.Col, direction))
                    {
                        PushBoxesVertical(nextRow, _robotPosition.Col, direction);

                        _warehouseMap[nextRow][_robotPosition.Col] = Robot;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        if(direction == RobotDirection.Up)
                        {
                            _robotPosition.Row--;
                        }
                        else
                        {
                            _robotPosition.Row++;
                        }
                    }
                        break;
                case RobotDirection.Right:
                    lineConfiguration = new StringBuilder(_warehouseMap[_robotPosition.Row].ToString()[(_robotPosition.Col + 1)..]);
                    if(lineConfiguration.ToString().First() == EmptySpace)
                    {
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col + 1] = Robot;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _robotPosition.Col++;
                    }
                    else if(lineConfiguration.ToString().Contains(EmptySpace)
                        && lineConfiguration.ToString().IndexOf(EmptySpace) < lineConfiguration.ToString().IndexOf(Wall))
                    {
                        _warehouseMap[_robotPosition.Row].Remove(lineConfiguration.ToString().IndexOf(EmptySpace) + _robotPosition.Col + 1, 1);
                        _warehouseMap[_robotPosition.Row].Insert(_robotPosition.Col, EmptySpace);
                        _robotPosition.Col++;
                    }
                    break;
                case RobotDirection.Left:
                    lineConfiguration = new StringBuilder(_warehouseMap[_robotPosition.Row].ToString()[..(_robotPosition.Col)]);
                    if(lineConfiguration.ToString().Last() == EmptySpace)
                    {
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col - 1] = Robot;
                        _warehouseMap[_robotPosition.Row][_robotPosition.Col] = EmptySpace;
                        _robotPosition.Col--;
                    }
                    else if(lineConfiguration.ToString().Contains(EmptySpace)
                        && lineConfiguration.ToString().LastIndexOf(EmptySpace) > lineConfiguration.ToString().LastIndexOf(Wall))
                    {
                        _warehouseMap[_robotPosition.Row].Remove(lineConfiguration.ToString().LastIndexOf(EmptySpace), 1);
                        _warehouseMap[_robotPosition.Row].Insert(_robotPosition.Col, EmptySpace);
                        _robotPosition.Col--;
                    }
                    break;
            }
        }

        private bool CanPushVertical(int row, int col, RobotDirection robotDirection)
        {
            if(row <= 0 || row >= MaxRow)
            {
                return false;
            }
            int nextRow = robotDirection switch
            {
                RobotDirection.Up => row - 1,
                RobotDirection.Down => row + 1,
                _ => throw new ArgumentException("This method verifies only Up or Down")
            };

            char contentNextCell = _warehouseMap[row][col];

            if (contentNextCell == Wall)
            {
                return false;
            }
            else if (contentNextCell == EmptySpace)
            {
                return true;
            }

            int colStartBox = (contentNextCell == BoxWide[0]) ? col : col - 1;
            return CanPushVertical(nextRow, colStartBox, robotDirection)
                && CanPushVertical(nextRow, colStartBox + 1, robotDirection);
        }

        private void PushBoxesVertical(int row, int col, RobotDirection robotDirection)
        {
            char contentCell = _warehouseMap[row][col];
            if(_warehouseMap[row][col] is Wall or EmptySpace)
            {
                return;
            }

            int nextRow = robotDirection == RobotDirection.Up ? row - 1 : row + 1;
            int colStartBox = (contentCell == BoxWide[0]) ? col : col - 1;
            PushBoxesVertical(nextRow, colStartBox, robotDirection);
            PushBoxesVertical(nextRow, colStartBox + 1, robotDirection);

            _warehouseMap[nextRow].Remove(colStartBox, 2).Insert(colStartBox, BoxWide);
            _warehouseMap[row].Remove(colStartBox, 2).Insert(colStartBox, $"{EmptySpace}{EmptySpace}");
        }

        private enum RobotDirection
        {
            Up,
            Right,
            Down,
            Left
        }
        #endregion
    }
}
