using AdventOfCode.Utils;

namespace AdventOfCode.Events.Year2024
{
    //--- Day 7: Bridge Repair ---
    public class Day07(string inputFilePath, IFileManager? fileManager = null) : Day(inputFilePath, fileManager)
    {
        public override long Part1()
        {
            var data = InputLines.Select(l =>
            {
                var testAndOperands = l.Split(':');
                var testValue = Convert.ToInt64(testAndOperands[0]);
                var operands = testAndOperands[1].Split(' ').Where(c => !string.IsNullOrEmpty(c)).Select(o => Convert.ToInt64(o));
                return (TestValue: Convert.ToInt64(testAndOperands[0]), Operands: operands);
            });

            long sumOfTrueEquations = 0;
            foreach (var (TestValue, Operands) in data)
            {
                var operationsQty = Operands.Count() - 1;
                var operatorsList = Operators(operationsQty);

                foreach(var operators in operatorsList)
                {
                    var result = Calc([.. Operands], operators);
                    if (result == TestValue)
                    {
                        sumOfTrueEquations += TestValue;
                        break;
                    }
                }
            }
            return sumOfTrueEquations;
        }

        public override long Part2()
        {
            throw new NotImplementedException();
        }

        #region Private helpers
        private static long Calc(List<long> operands, List<char> operators)
        {
            if (operands.Count == 1) return operands.Single();

            var @operator = operators.First();
            long operationResult = @operator switch
            {
                '+' => operands.ElementAt(0) + operands.ElementAt(1),
                '*' => operands.ElementAt(0) * operands.ElementAt(1),
                _ => 0
            };
            operands.RemoveAt(0);
            operands.RemoveAt(0);
            operators.RemoveAt(0);
            operands.Insert(0, operationResult);

            return Calc(operands, operators);
        }

        private static List<List<char>> Operators(int totalOperations)
        {
            return [.. Enumerable.Range(0, (int)Math.Pow(2, totalOperations))
                .Select(n => n.ToString("b").PadLeft(totalOperations, '0'))
                .Select(b => b.Select(c => c switch
                {
                    '0' => '+',
                    '1' => '*',
                    _ => throw new Exception()
                }).ToList())
            ];

            throw new NotImplementedException();
        }
        #endregion
    }
}
