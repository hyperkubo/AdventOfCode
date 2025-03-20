using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 7: Bridge Repair ---
    public class Day07 : Day
    {
        private int _operatorsToTake;
        private readonly IEnumerable<(long TestValue, IEnumerable<long> Operands)> _data;
        private readonly Dictionary <int, List<List<Operators>>> _operatorsCombinationsDirectory = [];
        public Day07(string inputFilePath, IFileManager? fileManager = null) : base(inputFilePath, fileManager)
        {
            _data = InputLines.Select(l =>
            {
                var testAndOperands = l.Split(':');
                var testValue = Convert.ToInt64(testAndOperands[0]);
                var operands = testAndOperands[1].Split(' ').Where(c => !string.IsNullOrEmpty(c)).Select(o => Convert.ToInt64(o));
                return (TestValue: Convert.ToInt64(testAndOperands[0]), Operands: operands);
            });
        }
        public override long Part1()
        {
            return SumOfTrueEquations(2);
        }

        public override long Part2()
        {
            return SumOfTrueEquations(3);
        }

        #region Private helpers
        private static long Calc(List<long> operands, List<Operators> operators)
        {
            while(operands.Count > 1)
            {
                var @operator = operators.First();
                long operationResult = @operator switch
                {
                    Operators.Add => operands.ElementAt(0) + operands.ElementAt(1),
                    Operators.Multiply => operands.ElementAt(0) * operands.ElementAt(1),
                    Operators.Concat => Convert.ToInt64($"{operands.ElementAt(0)}{operands.ElementAt(1)}"),
                    _ => 0
                };
                operands.RemoveAt(0);
                operands.RemoveAt(0);
                operators.RemoveAt(0);
                operands.Insert(0, operationResult);
            }
            return operands.Single();
        }

        private List<List<Operators>> OperatorsCombinations(int operandsQty)
        {
            if (_operatorsCombinationsDirectory.TryGetValue(operandsQty, out List<List<Operators>>? value))
            {
                return value;
            }
            else
            {
                int operatorsQty = operandsQty - 1;
                int totalPermutations = (int)Math.Pow(_operatorsToTake, operatorsQty) - 1;

                List<List<int>> operatorsList = [];
                for(int i = 0; i <= totalPermutations; i++)
                {
                    operatorsList.Add(ConvertToBase(i, _operatorsToTake, operatorsQty));
                }

                var operators = operatorsList.Select(l => l.Select(v => (Operators)v).ToList()).ToList();
                _operatorsCombinationsDirectory.Add(operandsQty, operators);
                return operators;
            }
        }

        private static List<int> ConvertToBase(int myNum, int @base, int positions)
        {
           List<int> baseNums = [];
            while(myNum > 0)
            {
                baseNums.Add(myNum % @base);
                myNum /= @base;
            }

            baseNums.AddRange(Enumerable.Repeat(0, positions - baseNums.Count));

            baseNums.Reverse();
            return baseNums;
        }

        private long SumOfTrueEquations(int operatorsToTake)
        {
            _operatorsToTake = operatorsToTake;
            long sumOfTrueEquations = 0;
            foreach (var (TestValue, Operands) in _data)
            {
                var operatorsList = OperatorsCombinations(Operands.Count());

                foreach(var operators in operatorsList)
                {
                    var result = Calc([.. Operands], [.. operators]);
                    if (result == TestValue)
                    {
                        sumOfTrueEquations += TestValue;
                        break;
                    }
                }
            }
            return sumOfTrueEquations;
        }

        private enum Operators
        {
            Add,
            Multiply,
            Concat
        }
        #endregion
    }
}
