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
        private List<StringBuilder> _warehouseMap = [];
        private List<RobotDirection> _robotMovements = [];
        (int Row, int Col) _robotPosition;
        private int MaxRow => InputLines.Count - 3;
        private int MaxCol => InputLines.First().Length - 1;

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
            throw new NotImplementedException();
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
