namespace AdventOfCode.Utils
{
    public class FileManager : IFileManager
    {
        public List<string> GetLines(string path)
        {
            List<string> result = [];
            StreamReader sr = new(path);
            string? line = sr.ReadLine();
            while (line != null)
            {
                result.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();

            return result;
        }
    }
}
