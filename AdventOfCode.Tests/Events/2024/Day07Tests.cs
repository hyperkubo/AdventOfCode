using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day07Tests : DayTest<Day07>
    {
        public Day07Tests()
        {
            InputLines = [
                "190: 10 19",
                "3267: 81 40 27",
                "83: 17 5",
                "156: 15 6",
                "7290: 6 8 6 15",
                "161011: 16 10 13",
                "192: 17 8 14",
                "21037: 9 7 18 13",
                "292: 11 6 16 20"
            ];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1();

            Assert.That(result, Is.EqualTo(3749));
        }

        [Test]
        [Ignore("Part one, then, part two.")]
        public override void Part2()
        {
        }
    }
}
