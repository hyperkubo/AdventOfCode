using AdventOfCode.Events.Year2024;
using Moq;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day02Tests : DayTest<Day02>
    {
        public Day02Tests()
        {
            InputLines =
            [
                "7 6 4 2 1",
                "1 2 7 8 9",
                "9 7 6 2 1",
                "1 3 2 4 5",
                "8 6 4 4 1",
                "1 3 6 7 9"
            ];
        }

        [Test]
        public override void Part1()
        {
            FileManager.Setup(f => f.GetLines(It.IsAny<string>())).Returns(() => InputLines);

            var output = Day.Part1(It.IsAny<string>());

            Assert.That(output, Is.EqualTo(2));
        }

        [Test]
        [Ignore("Almost there...")]
        public override void Part2()
        {
        }
    }
}
