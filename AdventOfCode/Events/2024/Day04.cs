using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 4: Ceres Search ---
    public partial class Day04(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        static Day04()
        {
            var theWordArr = _magicWord.ToCharArray();
            Array.Reverse(theWordArr);
            _reversedMagicWord = new string(theWordArr);
        }

        private static readonly string _magicWord = "XMAS";
        private static readonly string _reversedMagicWord;

        public override long Part1()
        {
            int xmasCounter = 0;

            xmasCounter += MagicWordOcurrences(InputLines);
            xmasCounter += MagicWordOcurrences(VerticallyRotate(InputLines));
            xmasCounter += MagicWordOcurrences(ClockwiseRotate(InputLines));
            xmasCounter += MagicWordOcurrences(CounterClockwiseRotate(InputLines));

            return xmasCounter;
        }

        public override long Part2()
        {
            int xmasCounter = 0;

            for(var row = 1; row < InputLines.Count - 1; row++)
            {
                for(var col = 1; col < InputLines.First().Length - 1; col++)
                {
                    if (InputLines[row][col] == 'A')
                    {
                        var ul = InputLines[row - 1][col - 1];
                        var ur = InputLines[row - 1][col + 1];
                        var dl = InputLines[row + 1][col - 1];
                        var dr = InputLines[row + 1][col + 1];
                        if (ul is 'M' or 'S' && ul + dr == 160 && ur is 'M' or 'S' && ur + dl == 160)
                        {
                            xmasCounter++;
                        }
                    }
                }
            }
            return xmasCounter;
        }

        #region Private helpers
        private static int MagicWordOcurrences(List<string> myCrossword)
        {
            int magicWordCounter = 0;

            magicWordCounter += myCrossword.Sum(line => line.Split(_magicWord).Length - 1);
            magicWordCounter += myCrossword.Sum(line => line.Split(_reversedMagicWord).Length - 1);

            return magicWordCounter;
        }

        private static List<string> VerticallyRotate(List<string> myCrossword)
        {
            var rotatedCrossword = new List<string>();
            StringBuilder myNewLine;

            for (int col = 0; col < myCrossword.First().Length; col++)
            {
                myNewLine = new();
                for (int row = 0; row < myCrossword.Count; row++)
                {
                    myNewLine.Append(myCrossword[row][col]);
                }
                rotatedCrossword.Add(myNewLine.ToString());
            }
            return rotatedCrossword;
        }

        private static List<string> ClockwiseRotate(List<string> myCrossword)
        {
            var diagonallyClockwiseRotatedLinesArr = new List<string>();
            var totalCols = myCrossword.First().Length;
            var totalRows = myCrossword.Count;
            StringBuilder myNewLine;
            for (int i = totalCols - _magicWord.Length; i >= 0; i--)
            {
                int col = i;
                int row = 0;
                myNewLine = new();
                while (col < totalCols)
                {
                    myNewLine.Append(myCrossword[row][col]);
                    row++;
                    col++;
                }
                diagonallyClockwiseRotatedLinesArr.Add(myNewLine.ToString());
            }
            for (int i = 1; i <= totalRows - _magicWord.Length; i++)
            {
                int row = i;
                int col = 0;
                myNewLine = new StringBuilder();
                while (row < totalRows)
                {
                    myNewLine.Append(myCrossword[row][col]);
                    row++;
                    col++;
                }
                diagonallyClockwiseRotatedLinesArr.Add(myNewLine.ToString());
            }
            return diagonallyClockwiseRotatedLinesArr;
        }

        private static List<string> CounterClockwiseRotate(List<string> myCrossword)
        {
            var diagonallyCounterClockwiseRotatedLinesArr = new List<string>();
            var totalCols = myCrossword.First().Length;
            var totalRows = myCrossword.Count;
            StringBuilder myNewLine;
            for(int i = _magicWord.Length - 1; i < totalRows; i++)
            {
                int row = i;
                int col = 0;
                myNewLine = new ();
                while(row >= 0)
                {
                    myNewLine.Append(myCrossword[row][col]);
                    row--;
                    col++;
                }
                diagonallyCounterClockwiseRotatedLinesArr.Add(myNewLine.ToString());
            }
            for(int i = 1; i <= totalCols - _magicWord.Length; i++)
            {
                int col = i;
                int row = totalRows - 1;
                myNewLine = new ();
                while(col < totalCols)
                {
                    myNewLine.Append(myCrossword[row][col]);
                    row--;
                    col++;
                }
                diagonallyCounterClockwiseRotatedLinesArr.Add(myNewLine.ToString());
            }
            return diagonallyCounterClockwiseRotatedLinesArr;
        }
        #endregion
    }
}
