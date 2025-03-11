using AdventOfCode.Events.Year2024;
using Moq;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day04Tests : DayTest<Day04>
    {
        public Day04Tests()
        {
            InputLines = [
                "MMMSXXMASM",
                "MSAMXMSMSA",
                "AMXSXMAAMM",
                "MSAMASMSMX",
                "XMASAMXAMM",
                "XXAMMXXAMA",
                "SMSMSASXSS",
                "SAXAMASAAA",
                "MAMMMXMMMM",
                "MXMXAXMASX"
            ];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1(It.IsAny<string>());

            Assert.That(result, Is.EqualTo(18));
        }

        [Test]
        [Ignore("Part1 first =)")]
        public override void Part2()
        {
        }
    }
}
