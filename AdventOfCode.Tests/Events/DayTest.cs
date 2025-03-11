using AdventOfCode.Events;
using AdventOfCode.Utils;
using Moq;

namespace AdventOfCode.Tests.Events
{
    abstract class DayTest<T> where T : Day
    {
        protected Mock<IFileManager> FileManager { get; private set; }
        protected T Day { get; private set; }
        protected List<string> InputLines { get; set; } = [];

        [SetUp]
        public void SetUp()
        {
            FileManager = new Mock<IFileManager>();
            Day = (T)(Activator.CreateInstance(typeof(T), It.IsAny<string>(), FileManager.Object) ?? new object());
            FileManager.Setup(f => f.GetLines(It.IsAny<string>())).Returns(() => InputLines);
        }

        public abstract void Part1();
        public abstract void Part2();
    }
}
