using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 12: Garden Groups ---
    public class Day12(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private readonly List<PlantGroup> _plantGroups = [];
        private int MaxRow => InputLines.Count - 1;
        private int MaxCol => InputLines.First().Length - 1;
        public override long Part1()
        {
            SeparateGroups();

            return _plantGroups.Sum(g => g.Price);
        }

        public override long Part2()
        {
            throw new NotImplementedException();
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
            public List<Coordinates> PlantsPositions { get; } = [new(row, col)];
            List<Coordinates> Borders { get; } = [
                new(row - 1, col),
                new(row + 1, col),
                new(row, col - 1),
                new(row, col + 1)
            ];

            public int Perimeter => Borders.Count;
            public int Area => PlantsPositions.Count;
            public int Price => Perimeter * Area;

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
                    Borders.Add(new(row, prevCol));
                }
                if(!PlantsPositions.Exists(pp => pp.Row == row && pp.Col == nextCol))
                {
                    Borders.Add(new(row, nextCol));
                }
                if(!PlantsPositions.Exists(pp => pp.Row == prevRow && pp.Col == col))
                {
                    Borders.Add(new(prevRow, col));
                }
                if(!PlantsPositions.Exists(pp => pp.Row == nextRow && pp.Col == col))
                {
                    Borders.Add(new(nextRow, col));
                }
            }

            public void AddGroup(PlantGroup group)
            {
                group.PlantsPositions.ForEach(p =>
                {
                    AddPlant(p.Row, p.Col);
                });
            }

            public class Coordinates(int row, int col)
            {
                public int Row { get; set; } = row;
                public int Col { get; set; } = col;
            }
        }
        #endregion
    }
}