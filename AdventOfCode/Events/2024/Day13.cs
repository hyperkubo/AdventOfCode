using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 13: Claw Contraption ---
    public partial class Day13(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private readonly int _buttonACost = 3;
        private readonly int _buttonBCost = 1;
        private readonly List<ClawMachine> _machines = [];
        public override long Part1()
        {
            CreateClawMachines();
            _machines.ForEach(m => m.GaussJordan());
            return _machines.Where(m => m.HasSolution).Sum(m => (m.ButtonAHits * _buttonACost) + (m.ButtonBHits * _buttonBCost));
        }

        public override long Part2()
        {
            throw new NotImplementedException();
        }

        #region Private methods
        private void CreateClawMachines()
        {
            Regex regexDigits = DigitsRegex();
            ClawMachine newMachine;
            MatchCollection matches;
            for(int i = 0; i < InputLines.Count; i+=4)
            {
                var inputInstructions = string.Join(' ', InputLines[i..(i+3)]);
                newMachine = new ClawMachine();
                matches = regexDigits.Matches(inputInstructions);
                int x = Convert.ToInt32(matches.ElementAt(0).Value);
                int y = Convert.ToInt32(matches.ElementAt(1).Value);
                newMachine.ButtonA = (x, y);

                x = Convert.ToInt32(matches.ElementAt(2).Value);
                y = Convert.ToInt32(matches.ElementAt(3).Value);
                newMachine.ButtonB = (x, y);

                x = Convert.ToInt32(matches.ElementAt(4).Value);
                y = Convert.ToInt32(matches.ElementAt(5).Value);
                newMachine.Price = (x, y);

                _machines.Add(newMachine);
                newMachine.GaussJordan();
            }
        }

        class ClawMachine()
        {
            public (int X, int Y) ButtonA { get; set; }
            public (int X, int Y) ButtonB { get; set; }
            public (int X, int Y) Price { get; set; }
            private readonly Fraction[,] _matrix = new Fraction[2, 3];
            public bool HasSolution => _matrix[0, 2].Numerator % _matrix[0, 2].Denominator == 0 && _matrix[1,2].Numerator % _matrix[1,2].Denominator == 0;
            public long ButtonAHits => HasSolution ? _matrix[0, 2].Numerator / _matrix[0, 2].Denominator : -1;
            public long ButtonBHits => HasSolution ? _matrix[1, 2].Numerator / _matrix[1, 2].Denominator : -1;

            public void GaussJordan()
            {
                _matrix[0, 0] = new Fraction(1);
                _matrix[0, 1] = new Fraction(ButtonB.X) / new Fraction(ButtonA.X);
                _matrix[0, 2] = new Fraction(Price.X) / new Fraction(ButtonA.X);

                _matrix[1, 0] = new Fraction(0);
                _matrix[1, 1] = (new Fraction(-ButtonA.Y) * _matrix[0, 1]) + new Fraction(ButtonB.Y);
                _matrix[1, 2] = (new Fraction(-ButtonA.Y) * _matrix[0, 2]) + new Fraction(Price.Y);

                _matrix[1, 2] = _matrix[1, 2] / _matrix[1, 1];
                _matrix[1, 1] = new Fraction(1);

                _matrix[0, 2] = (_matrix[1, 2] * (-1) * _matrix[0, 1]) + _matrix[0, 2];
                _matrix[0, 1] = new Fraction(0);
            }
        }

        class Fraction
        {
            public Fraction(long myInt)
            {
                Numerator = myInt;
                Denominator = 1;
            }
            public Fraction(long numerator, long denominator)
            {
                Numerator = numerator;
                Denominator = denominator;
            }
            public long Numerator { get; private set; }
            public long Denominator { get; private set; }

            public static Fraction operator +(Fraction fraction1, Fraction fraction2)
            {
                long numerator, denominator;
                if(fraction1.Denominator == fraction2.Denominator)
                {
                    return new Fraction(fraction1.Numerator + fraction2.Numerator, fraction1.Denominator);
                }

                denominator = fraction1.Denominator * fraction2.Denominator;
                numerator = (fraction1.Numerator * fraction2.Denominator) + (fraction2.Numerator * fraction1.Denominator);

                return new Fraction(numerator, denominator);
            }
            public static Fraction operator *(Fraction fraction1, Fraction fraction2)
            {
                long numerator = fraction1.Numerator * fraction2.Numerator;
                long denominator = fraction1.Denominator * fraction2.Denominator;
                if(denominator < 0)
                {
                    numerator *= -1;
                    denominator *= -1;
                }
                return new Fraction(numerator, denominator);
            }
            public static Fraction operator *(Fraction fraction, long myInt)
            {
                return fraction * new Fraction(myInt);
            }
            public static Fraction operator /(Fraction fraction1, Fraction fraction2)
            {
                return fraction1 * new Fraction(fraction2.Denominator, fraction2.Numerator);
            }
        }

        [GeneratedRegex(@"\d+")]
        private static partial Regex DigitsRegex();
        #endregion
    }
}