using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day11Tests : DayTest<Day11>
    {
        public Day11Tests()
        {
            InputLines = [
                "125 17"
            ];
        }

        public override void Part1()
        {
            var result = Day.Part1();

            Assert.That(result, Is.EqualTo(55312));
        }

        public override void Part2()
        {
            throw new NotImplementedException();
        }
    }
}
