using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day03Tests : DayTest<Day03>
    {
        [Test]
        public override void Part1()
        {
            InputLines = ["xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"];

            var result = Day.Part1();

            Assert.That(result, Is.EqualTo(161));
        }

        [Test]
        public override void Part2()
        {
            InputLines = ["xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"];

            var result = Day.Part2();

            Assert.That(result, Is.EqualTo(48));
        }
    }
}
