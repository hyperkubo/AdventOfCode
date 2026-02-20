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
        public override T Part1<T>()
        {
            return (T)(object)UpdatesInOrder(_updates, _rules)
                .Sum(u =>
                {
                    var pagesInUpdate = u.Split(',');
                    var middleIdx = (int)Math.Ceiling(pagesInUpdate.Length / 2.0) - 1;
                    return Convert.ToInt32(pagesInUpdate[middleIdx]);
                }
            );
        }

        public override T Part2<T>()
        {
            var updatesInOrder = UpdatesInOrder(_updates, _rules);
            var unorderedUpdates = _updates.ToList();
            unorderedUpdates.RemoveAll(u => updatesInOrder.Contains(u));

            List<int> reorderedUpdatesMidPage = [];
            foreach(var update in unorderedUpdates)
            {
                string[] updatePages;
                bool updateRemainsSame;
                updatePages = update.Split(',');
                do{
                    updateRemainsSame = true;
                    for (int i = 0; i < updatePages.Length - 1; i++)
                    {
                        var seqPages = $"{updatePages[i]}|{updatePages[i + 1]}";
                        var updateExists = _rules.Any(rule => rule == seqPages);
                            updateRemainsSame &= updateExists;
                        if (!updateExists)
                        {
                            (updatePages[i + 1], updatePages[i]) = (updatePages[i], updatePages[i + 1]);
                        }
                    }

                } while (!updateRemainsSame);
                var middleIdx = (int)Math.Ceiling(updatePages.Length / 2.0) - 1;
                reorderedUpdatesMidPage.Add(Convert.ToInt32(updatePages[middleIdx]));
            }
            return (T)(object)reorderedUpdatesMidPage.Sum();
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
