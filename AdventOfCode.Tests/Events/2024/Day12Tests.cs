using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day12Tests : DayTest<Day12>
    {
        public Day12Tests()
        {
            InputLines = [
                "RRRRIICCFF",
                "RRRRIICCCF",
                "VVRRRCCFFF",
                "VVRCCCJFFF",
                "VVVVCJJCFE",
                "VVIVCCJJEE",
                "VVIIICJJEE",
                "MIIIIIJJEE",
                "MIIISIJEEE",
                "MMMISSJEEE"
            ];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1();

            Assert.That(result, Is.EqualTo(1930));
        }

        public override void Part2()
        {
            throw new NotImplementedException();
        }
    }
}
