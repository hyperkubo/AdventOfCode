using AdventOfCode.Events.Year2024;
using Moq;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day01Tests : DayTest<Day01>
    {
        public Day01Tests()
        {
            InputLines = [
                "3   4",
                "4   3",
                "2   5",
                "1   3",
                "3   9",
                "3   3"
            ];
        }

        [Test]
        public override void Part1()
        {
            FileManager.Setup(f => f.GetLines(It.IsAny<string>())).Returns(() => InputLines);

            var output = Day.Part1(It.IsAny<string>());

            Assert.That(output, Is.EqualTo(11));
        }

        [Test]
        public override void Part2()
        {
            FileManager.Setup(f => f.GetLines(It.IsAny<string>())).Returns(() => InputLines);

            var output = Day.Part2(It.IsAny<string>());

            Assert.That(output, Is.EqualTo(31));
        }
    }
}
