using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day06Tests : DayTest<Day06>
    {
        public Day06Tests()
        {
            InputLines = [
                "....#.....",
                ".........#",
                "..........",
                "..#.......",
                ".......#..",
                "..........",
                ".#..^.....",
                "........#.",
                "#.........",
                "......#..."
            ];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1<int>();

            Assert.That(result, Is.EqualTo(41));
        }

        [Test]
        public override void Part2()
        {
            var result = Day.Part2<int>();

            Assert.That(result, Is.EqualTo(6));
        }
    }
}
