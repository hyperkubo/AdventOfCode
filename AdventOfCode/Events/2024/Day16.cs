using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 16: Reindeer Maze ---
    public class Day16(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private const char Start = 'S';
        private const char End = 'E';
        private const char Wall = '#';

        private int MaxRow => InputLines.Count - 2;
        private int MaxCol => InputLines.First().Length - 1;

        public override long Part1()
        {
            int startRow = InputLines.FindIndex(line => line.Contains(Start));
            int startCol = InputLines[startRow].IndexOf(Start);

            int endRow = InputLines.FindIndex(line => line.Contains(End));
            int endCol = InputLines[endRow].IndexOf(End);

            var pathList = new PriorityQueue<(int Row, int Col, ReindeerDirection Direction), int>();
            var visited = new Dictionary<(int Row, int Col), int>();

            pathList.Enqueue((startRow, startCol, ReindeerDirection.Right), 0);
            visited[(startRow, startCol)] = 0;

            while (pathList.TryDequeue(out var current, out var cost))
            {
                var (row, col, direction) = current;

                if (cost > visited[(row, col)])
                {
                    continue;
                }

                if (row == endRow && col == endCol)
                {
                    return cost;
                }

                int[] rowsMovs = { -1, 1, 0, 0 };
                int[] colsMovs = { 0, 0, -1, 1 };
                for (int i = 0; i < 4; i++)
                {
                    int newRow = row + rowsMovs[i];
                    int newCol = col + colsMovs[i];
                    (int, int) movs = (rowsMovs[i], colsMovs[i]);

                    ReindeerDirection newDirection = movs switch
                    {
                        (-1, 0) => ReindeerDirection.Up,
                        (1, 0) => ReindeerDirection.Down,
                        (0, -1) => ReindeerDirection.Left,
                        (0, 1) => ReindeerDirection.Right,
                        _ => throw new System.Exception()
                    };

                    int newCost = cost + (newDirection != direction ? 1001 : 1);
                    if (((int)newDirection + (int)direction) is 101 or 1010)
                    {
                        newCost += 1000;
                    }

                    if (newRow >= 1 && newRow <= MaxRow && newCol >= 1 && newCol <= MaxCol &&
                        InputLines[newRow][newCol] != Wall)
                    {
                        if (!visited.TryGetValue((newRow, newCol), out int existingCost) || newCost < existingCost)
                        {
                            visited[(newRow, newCol)] = newCost;
                            pathList.Enqueue((newRow, newCol, newDirection), newCost);
                        }
                    }
                }
            }

            return -1;
        }

        public override long Part2()
        {
            throw new NotImplementedException();
        }

        #region Private methods
        enum ReindeerDirection
        {
            Up = 1,
            Right = 10,
            Down = 100,
            Left = 1000
        }
        #endregion
    }
}
