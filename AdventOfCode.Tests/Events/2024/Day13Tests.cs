using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day13Tests : DayTest<Day13>
    {
        public Day13Tests()
        {
            InputLines = [
                "Button A: X+94, Y+34",
                "Button B: X+22, Y+67",
                "Prize: X=8400, Y=5400",
                "",
                "Button A: X+26, Y+66",
                "Button B: X+67, Y+21",
                "Prize: X=12748, Y=12176",
                "",
                "Button A: X+17, Y+86",
                "Button B: X+84, Y+37",
                "Prize: X=7870, Y=6450",
                "",
                "Button A: X+69, Y+23",
                "Button B: X+27, Y+71",
                "Prize: X=18641, Y=10279"
            ];
        }

        [Test]
        public override void Part1()
        {
            var result = Day.Part1();

            Assert.That(result, Is.EqualTo(480));
        }

        public override void Part2()
        {
            throw new NotImplementedException();
        }
    }
}
