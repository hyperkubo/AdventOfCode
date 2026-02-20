using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 12: Garden Groups ---
    public class Day12(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private readonly List<PlantGroup> _plantGroups = [];
        private int MaxRow => InputLines.Count - 1;
        private int MaxCol => InputLines.First().Length - 1;
        public override T Part1<T>()
        {
            SeparateGroups();

            return (T)(object)_plantGroups.Sum(g => g.Price);
        }

        public override T Part2<T>()
        {
            SeparateGroups();

            _plantGroups.ForEach(g => g.CalculateSides());

            return (T)(object)_plantGroups.Sum(g => g.DiscountPrice);
        }

        #region Private methods
        private void SeparateGroups()
        {
            int prevCol, prevRow, nextRow, nextCol;
            for (int row = 0; row <= MaxRow; row++)
            {
                for (int col = 0; col <= MaxCol; col++)
                {
                    prevCol = col - 1;
                    prevRow = row - 1;
                    nextRow = row + 1;
                    nextCol = col + 1;
                    var currPlantType = InputLines[row][col];

                    var groupsSamePlant = _plantGroups.Where(g => g.Type == currPlantType);
                    var leftGroup = groupsSamePlant.FirstOrDefault(g => g.PlantsPositions.Exists(pp => pp.Row == row && pp.Col == prevCol));
                    var upperGroup = groupsSamePlant.FirstOrDefault(g => g.PlantsPositions.Exists(pp => pp.Row == prevRow && pp.Col == col));
                    if(leftGroup is not null && upperGroup is not null && !leftGroup.Equals(upperGroup))
                    {
                        upperGroup.AddPlant(row, col);
                        _plantGroups.Remove(leftGroup);
                        upperGroup.AddGroup(leftGroup);
                    }
                    else if(leftGroup is not null)
                    {
                        leftGroup.AddPlant(row, col);
                    }
                    else if(upperGroup is not null)
                    {
                        upperGroup.AddPlant(row, col);
                    }
                    else
                    {
                        _plantGroups.Add(new(currPlantType, row, col));
                    }
                }
            }
        }

        class PlantGroup(char type, int row, int col)
        {
            public char Type { get; } = type;
            public int CompleteSides { get; private set; }
            public List<Coordinates> PlantsPositions { get; } = [new(row, col)];
            List<Border> Borders { get; } = [
                new(row - 1, col, BorderPosition.Upper),
                new(row + 1, col, BorderPosition.Lower),
                new(row, col - 1, BorderPosition.Left),
                new(row, col + 1, BorderPosition.Right)
            ];

            public int Perimeter => Borders.Count;
            public int Area => PlantsPositions.Count;
            public int Price => Perimeter * Area;
            public int DiscountPrice => CompleteSides * Area;

            public void AddPlant(int row, int col)
            {
                Borders.RemoveAll(b => b.Row == row && b.Col == col);

                PlantsPositions.Add(new(row, col));

                int prevCol = col - 1;
                int prevRow = row - 1;
                int nextCol = col + 1;
                int nextRow = row + 1;
                if(!PlantsPositions.Exists(pp => pp.Row == row && pp.Col == prevCol))
                {
                    Borders.Add(new(row, prevCol, BorderPosition.Left));
                }
                if(!PlantsPositions.Exists(pp => pp.Row == row && pp.Col == nextCol))
                {
                    Borders.Add(new(row, nextCol, BorderPosition.Right));
                }
                if(!PlantsPositions.Exists(pp => pp.Row == prevRow && pp.Col == col))
                {
                    Borders.Add(new(prevRow, col, BorderPosition.Upper));
                }
                if(!PlantsPositions.Exists(pp => pp.Row == nextRow && pp.Col == col))
                {
                    Borders.Add(new(nextRow, col, BorderPosition.Lower));
                }
            }

            public void AddGroup(PlantGroup group)
            {
                group.PlantsPositions.ForEach(p =>
                {
                    AddPlant(p.Row, p.Col);
                });
            }

            public void CalculateSides()
            {
                var upperBorders = Borders.Where(b => b.Type == BorderPosition.Upper).GroupBy(b => b.Row).Select(g => new {Row = g.Key, Cols = g.Select(v => v.Col).ToList()});
                var lowerBorders = Borders.Where(b => b.Type == BorderPosition.Lower).GroupBy(b => b.Row).Select(g => new {Row = g.Key, Cols = g.Select(v => v.Col).ToList()});
                var rightBorders = Borders.Where(b => b.Type == BorderPosition.Right).GroupBy(b => b.Col).Select(g => new {Col = g.Key, Rows = g.Select(v => v.Row).ToList()});
                var leftBorders = Borders.Where(b => b.Type == BorderPosition.Left).GroupBy(b => b.Col).Select(g => new {Col = g.Key, Rows = g.Select(v => v.Row).ToList()});

                upperBorders.ToList().ForEach(b => CompleteSides += BordersInRow([.. b.Cols]));
                rightBorders.ToList().ForEach(b => CompleteSides += BordersInRow([.. b.Rows]));
                lowerBorders.ToList().ForEach(b => CompleteSides += BordersInRow([.. b.Cols]));
                leftBorders.ToList().ForEach(b => CompleteSides += BordersInRow([.. b.Rows]));
            }

            private static int BordersInRow(int[] positions)
            {
                positions = [.. positions.Order()];
                int borders = 1;

                for(int i = 1; i < positions.Length; i++)
                {
                    if (positions[i] - positions[i - 1] > 1) borders++;
                }

                return borders;
            }

            public class Coordinates(int row, int col)
            {
                public int Row { get; set; } = row;
                public int Col { get; set; } = col;
            }

            enum BorderPosition
            {
                Upper,
                Right,
                Lower,
                Left
            }

            class Border(int row, int col, BorderPosition position) : Coordinates(row, col)
            {
                public BorderPosition Type { get; } = position;
            }
        }
        #endregion
    }
}