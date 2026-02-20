using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 13: Claw Contraption ---
    public partial class Day13(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        private readonly List<ClawMachine> _machines = [];
        public override T Part1<T>()
        {
            CreateClawMachines();
            _machines.ForEach(m => m.GaussJordan());
            return (T)(object)_machines.Where(m => m.HasSolution).Sum(m => m.ButtonATotalCost + m.ButtonBTotalCost);
        }

        public override T Part2<T>()
        {
            CreateClawMachines(10000000000000);
            _machines.ForEach(m => m.GaussJordan());
            return (T)(object)_machines.Where(m => m.HasSolution).Sum(m => m.ButtonATotalCost + m.ButtonBTotalCost);
        }

        #region Private methods
        private void CreateClawMachines(long missingPriceSteps = 0)
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
                newMachine.Prize = (x + missingPriceSteps, y + missingPriceSteps);

                _machines.Add(newMachine);
            }
        }

        class ClawMachine()
        {
            public (long X, long Y) ButtonA { get; set; }
            public (long X, long Y) ButtonB { get; set; }
            public (long X, long Y) Prize { get; set; }
            private static readonly int _buttonACost = 3;
            private static readonly int _buttonBCost = 1;
            private readonly Fraction[,] _matrix = new Fraction[2, 3];
            public bool HasSolution => _matrix[0, 2].Numerator % _matrix[0, 2].Denominator == 0 && _matrix[1,2].Numerator % _matrix[1,2].Denominator == 0;
            public long ButtonAHits => HasSolution ? _matrix[0, 2].Numerator / _matrix[0, 2].Denominator : -1;
            public long ButtonBHits => HasSolution ? _matrix[1, 2].Numerator / _matrix[1, 2].Denominator : -1;
            public long ButtonATotalCost => HasSolution ? ButtonAHits * _buttonACost : 0;
            public long ButtonBTotalCost => HasSolution ? ButtonBHits * _buttonBCost : 0;

            public void GaussJordan()
            {
                _matrix[0, 0] = new Fraction(1);
                _matrix[0, 1] = new Fraction(ButtonB.X) / new Fraction(ButtonA.X);
                _matrix[0, 2] = new Fraction(Prize.X) / new Fraction(ButtonA.X);

                _matrix[1, 0] = new Fraction(0);
                _matrix[1, 1] = (new Fraction(-ButtonA.Y) * _matrix[0, 1]) + new Fraction(ButtonB.Y);
                _matrix[1, 2] = (new Fraction(-ButtonA.Y) * _matrix[0, 2]) + new Fraction(Prize.Y);

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
                if(numerator % denominator == 0)
                {
                    Numerator = numerator / denominator;
                    Denominator = 1;
                }
                else
                {
                    Numerator = numerator;
                    Denominator = denominator;
                }
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