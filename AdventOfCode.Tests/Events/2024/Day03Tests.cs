using AdventOfCode.Events.Year2024;
using Moq;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day03Tests : DayTest<Day03>
    {
        public Day03Tests()
        {
            InputLines = ["xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1(It.IsAny<string>());

            Assert.That(result, Is.EqualTo(161));
        }

        [Test]
        [Ignore("Maybe next commit...")]
        public override void Part2()
        {
        }
    }
}
