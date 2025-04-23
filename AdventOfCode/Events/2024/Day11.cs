using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 11: Plutonian Pebbles ---
    public class Day11(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private List<string> _stones = [];
        public override long Part1()
        {
            _stones = [.. InputLines.First().Split(' ')];
            Blink(25);

            return _stones.Count();
        }

        public override long Part2()
        {
            throw new NotImplementedException();
        }

        #region Private methods
        private void Blink(int times = 0)
        {
            List<string> newStonesLine = [];
            foreach(var stone in _stones)
            {
                if(stone == "0")
                {
                    newStonesLine.Add("1");
                }
                else if(stone.Length % 2 == 0)
                {
                    var halfStoneLength = stone.Length / 2;
                    var stone1 = stone[..(halfStoneLength)];
                    var stone2 = stone.Substring(halfStoneLength, halfStoneLength);
                    newStonesLine.AddRange([stone1, Convert.ToInt64(stone2).ToString()]);
                }
                else
                {
                    newStonesLine.Add((Convert.ToInt64(stone) * 2024).ToString());
                }
            }
            _stones = newStonesLine;

            if(times > 1)
            {
                Blink(times - 1);
            }
        }
        #endregion
    }
}
