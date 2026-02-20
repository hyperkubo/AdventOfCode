using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day08Tests : DayTest<Day08>
    {
        public Day08Tests()
        {
            InputLines = [
                "............",
                "........0...",
                ".....0......",
                ".......0....",
                "....0.......",
                "......A.....",
                "............",
                "............",
                "........A...",
                ".........A..",
                "............",
                "............"
            ];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1<int>();

            Assert.That(result, Is.EqualTo(14));
        }

        [Test]
        public override void Part2()
        {
            var result = Day.Part2<int>();

            Assert.That(result, Is.EqualTo(34));
        }
    }
}
