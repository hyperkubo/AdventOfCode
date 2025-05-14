using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day14Tests : DayTest<Day14>
    {
        public Day14Tests()
        {
            InputLines = [
                "p=0,4 v=3,-3",
                "p=6,3 v=-1,-3",
                "p=10,3 v=-1,2",
                "p=2,0 v=2,-1",
                "p=0,0 v=1,3",
                "p=3,0 v=-2,-2",
                "p=7,6 v=-1,-3",
                "p=3,0 v=-1,-2",
                "p=9,3 v=2,3",
                "p=7,3 v=-1,2",
                "p=2,4 v=2,-3",
                "p=9,5 v=-3,-3"
            ];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1(horizontalTiles: 7, verticalTiles: 11);

            Assert.That(result, Is.EqualTo(12));
        }

        public override void Part2()
        {
            throw new NotImplementedException();
        }
    }
}
