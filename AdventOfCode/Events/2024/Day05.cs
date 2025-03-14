using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 5: Print Queue ---
    public class Day05 : Day
    {
        private readonly IEnumerable<string> _rules;
        private readonly IEnumerable<string> _updates;
        public Day05(string inputFilePath, IFileManager? fileManager = null) : base (inputFilePath, fileManager)
        {
            _rules = InputLines.Where(line => line.Contains('|'));
            _updates = InputLines.Where(line => line.Contains(','));
        }
        public override int Part1()
        {
            return UpdatesInOrder(_updates, _rules)
                .Sum(u =>
                {
                    var pagesInUpdate = u.Split(',');
                    var middleIdx = (int)Math.Ceiling(pagesInUpdate.Length / 2.0) - 1;
                    return Convert.ToInt32(pagesInUpdate[middleIdx]);
                }
            );
        }

        public override int Part2()
        {
            throw new NotImplementedException();
        }

        #region Private helpers
        private static List<string> UpdatesInOrder(IEnumerable<string> updates, IEnumerable<string> rules)
        {
            List<string> result = [];
            foreach (var update in updates)
            {
                bool validUpdate = false;
                var updatePages = update.Split(',');
                for (int i = 0; i < updatePages.Length - 1; i++)
                {
                    validUpdate = rules.Any(rule => rule == $"{updatePages[i]}|{updatePages[i + 1]}");
                    if (!validUpdate) break;
                }
                if (validUpdate)
                {
                    result.Add(update);
                }
            }

            return result;
        }
        #endregion
    }
}
