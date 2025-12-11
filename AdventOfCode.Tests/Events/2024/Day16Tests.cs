using AdventOfCode.Events.Year2024;

namespace AdventOfCode.Tests.Events.Year2024
{
    class Day16Tests : DayTest<Day16>
    {
        private readonly string[] _inputLinesPartA = [
            "###############",
            "#.......#....E#",
            "#.#.###.#.###.#",
            "#.....#.#...#.#",
            "#.###.#####.#.#",
            "#.#.#.......#.#",
            "#.#.#####.###.#",
            "#...........#.#",
            "###.#.#####.#.#",
            "#...#.....#.#.#",
            "#.#.#.###.#.#.#",
            "#.....#...#.#.#",
            "#.###.#.#.#.#.#",
            "#S..#.....#...#",
            "###############"
        ];
        private readonly string[] _inputLinesPartB = [
            "#################",
            "#...#...#...#..E#",
            "#.#.#.#.#.#.#.#.#",
            "#.#.#.#...#...#.#",
            "#.#.#.#.###.#.#.#",
            "#...#.#.#.....#.#",
            "#.#.#.#.#.#####.#",
            "#.#...#.#.#.....#",
            "#.#.#####.#.###.#",
            "#.#.#.......#...#",
            "#.#.###.#####.###",
            "#.#.#...#.....#.#",
            "#.#.#.#####.###.#",
            "#.#.#.........#.#",
            "#.#.#.#########.#",
            "#S#.............#",
            "#################"
        ];

        [Test]
        public override void Part1()
        {
            InputLines = [.. _inputLinesPartA];

            var result = Day.Part1();

            Assert.That(result, Is.EqualTo(7036));
        }

        [Test]
        public void Part1_b()
        {
            InputLines = [.. _inputLinesPartB];

            var result = Day.Part1();

            Assert.That(result, Is.EqualTo(11048));
        }

        [Test]
        public override void Part2()
        {
            InputLines = [.. _inputLinesPartA];

            var result = Day.Part2();

            Assert.That(result, Is.EqualTo(45));
        }

        [Test]
        public void Part2_b()
        {
            InputLines = [.. _inputLinesPartB];

            var result = Day.Part2();

            Assert.That(result, Is.EqualTo(64));
        }
    }
}
