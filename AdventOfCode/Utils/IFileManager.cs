namespace AdventOfCode.Utils
{
    public interface IFileManager
    {
        abstract List<string> GetLines(string path);
    }
}