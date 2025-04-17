using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day09Tests : DayTest<Day09>
    {
        public Day09Tests()
        {
            InputLines = ["2333133121414131402"];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1();

            Assert.That(result, Is.EqualTo(1928));
        }

        public override void Part2()
        {
            throw new NotImplementedException();
        }
    }
}
