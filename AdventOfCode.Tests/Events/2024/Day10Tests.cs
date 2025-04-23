using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day10Tests : DayTest<Day10>
    {
        public Day10Tests()
        {
            InputLines = [
               "89010123",
               "78121874",
               "87430965",
               "96549874",
               "45678903",
               "32019012",
               "01329801",
               "10456732"
            ];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1();

            Assert.That(result, Is.EqualTo(36));
        }

        [Test]
        public override void Part2()
        {
            var result = Day.Part2();

            Assert.That(result, Is.EqualTo(81));
        }
    }
}
