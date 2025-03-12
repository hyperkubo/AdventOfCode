using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 5: Print Queue ---
    public class Day05(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        public override int Part1()
        {
            var rules = InputLines.Where(line => line.Contains('|'));
            var updates = InputLines.Where(line => line.Contains(','));

            List<string> updatesInOrder = [];
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
                    updatesInOrder.Add(update);
                }
            }

            return updatesInOrder.Sum(u =>
            {
                var pagesInUpdate = u.Split(',');
                var middleIdx = (int)Math.Ceiling(pagesInUpdate.Length / 2.0) - 1;
                return Convert.ToInt32(pagesInUpdate[middleIdx]);
            });
        }

        public override int Part2()
        {
            throw new NotImplementedException();
        }
    }
}
