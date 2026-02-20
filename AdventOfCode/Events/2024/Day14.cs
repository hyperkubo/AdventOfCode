using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 14: Restroom Redoubt ---
    public partial class Day14(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private readonly List<Robot> _robots = [];
        private int _middleCol;
        private int _middleRow;
        private int _horizontalTiles;
        private int _verticalTiles;

        public long Part1(int horizontalTiles, int verticalTiles)
        {
            _horizontalTiles = horizontalTiles;
            _verticalTiles = verticalTiles;
            _middleCol = (int)Math.Ceiling((_horizontalTiles - 1) / 2M);
            _middleRow = (int)Math.Ceiling((_verticalTiles - 1) / 2M);

            Robot newRobot;
            foreach(var line in InputLines)
            {
                newRobot = new ();
                MatchCollection matches = DigitRegex().Matches(line);
                var posX = Convert.ToInt32(matches[0].Value);
                var posY = Convert.ToInt32(matches[1].Value);
                var velX = Convert.ToInt32(matches[2].Value);
                var velY = Convert.ToInt32(matches[3].Value);

                if (velX < 0) velX = _horizontalTiles + velX;
                if (velY < 0) velY = _verticalTiles + velY;

                newRobot.Position = (posX, posY);
                newRobot.Velocity = (velX, velY);

                _robots.Add(newRobot);
            }
            HundredSecondsAfter();

            int[] robotsInQuadrants =
            [
                _robots.Count(r => r.Position.X < _middleCol && r.Position.Y < _middleRow),
                _robots.Count(r => r.Position.X < _middleCol && r.Position.Y > _middleRow),
                _robots.Count(r => r.Position.X > _middleCol && r.Position.Y < _middleRow),
                _robots.Count(r => r.Position.X > _middleCol && r.Position.Y > _middleRow),
            ];
            return robotsInQuadrants.Aggregate((a, b) => a * b);
        }

        public override T Part1<T>()
        {
            throw new NotImplementedException();
#warning Use the other Part1 =)
        }

        public override T Part2<T>()
        {
            throw new NotImplementedException();
        }

        #region Private methods
        void HundredSecondsAfter()
        {
            foreach(var robot in _robots)
            {
                var newXpos = ((robot.Velocity.X * 100) + robot.Position.X) % _horizontalTiles;
                var newYpos = ((robot.Velocity.Y * 100) + robot.Position.Y) % _verticalTiles;

                robot.Position = (newXpos, newYpos);
            }
        }

        class Robot
        {
            public (int X, int Y) Position { get; set; }
            public (int X, int Y) Velocity { get; set; }
        }

        [GeneratedRegex(@"-?\d+")]
        private static partial Regex DigitRegex();
        #endregion
    }
}
