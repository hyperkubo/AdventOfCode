using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 11: Plutonian Pebbles ---
    public class Day11 : Day
    {
        private readonly Dictionary<string, long> _stonesCount = [];

        public Day11(string inputFilePath, IFileManager? fileManager = null) : base(inputFilePath, fileManager)
        {
            _stonesCount = InputLines.First().Split(' ').Select(s => new KeyValuePair<string, long>(s, 1)).ToDictionary();
        }

        public override long Part1()
        {
            Blink(25);

            return _stonesCount.Sum(s => s.Value);
        }

        public override long Part2()
        {
            Blink(75);

            return _stonesCount.Sum(s => s.Value);
        }

        #region Private methods
        private void Blink(int times = 1)
        {
            if (times < 0) throw new ArgumentException("How do you blink in a negative way...?");
            for(int i = times; i > 0; i--)
            {
                var stonesInBlink = _stonesCount.Where(s => s.Value > 0).Select(s => s.Key).ToList();
                Dictionary<string, long> newStonesCount = [];
                foreach (var stone in stonesInBlink)
                {
                    if (stone == "0")
                    {
                        if (newStonesCount.ContainsKey("1")){
                            newStonesCount["1"] += _stonesCount["0"];
                        }
                        else
                        {
                            newStonesCount.Add("1", _stonesCount["0"]);
                        }
                    }
                    else if (stone.Length % 2 == 0)
                    {
                        var halfStoneLength = stone.Length / 2;
                        var stone1 = Convert.ToInt32(stone[..halfStoneLength]).ToString();
                        if (newStonesCount.ContainsKey(stone1)){
                            newStonesCount[stone1] += _stonesCount[stone];
                        }
                        else
                        {
                            newStonesCount.Add(stone1, _stonesCount[stone]);
                        }
                        var stone2 = Convert.ToInt32(stone.Substring(halfStoneLength, halfStoneLength)).ToString();
                        if (newStonesCount.ContainsKey(stone2)){
                            newStonesCount[stone2] += _stonesCount[stone];
                        }
                        else
                        {
                            newStonesCount.Add(stone2, _stonesCount[stone]);
                        }
                    }
                    else
                    {
                        var newStone = (Convert.ToInt64(stone) * 2024).ToString();
                        if (newStonesCount.ContainsKey(newStone)){
                            newStonesCount[newStone] += _stonesCount[stone];
                        }
                        else
                        {
                            newStonesCount.Add(newStone, _stonesCount[stone]);
                        }
                    }
                    _stonesCount[stone] = 0;
                }
                foreach (var stoneCount in newStonesCount)
                {
                    if (_stonesCount.ContainsKey(stoneCount.Key))
                    {
                        _stonesCount[stoneCount.Key] = stoneCount.Value;
                    }
                    else
                    {
                        _stonesCount.Add(stoneCount.Key, stoneCount.Value);
                    }
                }
            }
        }
        #endregion
    }
}
