using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// The sum of the squares of the first ten natural numbers is,
    /// $$1^2 + 2^2 + ... + 10^2 = 385$$
    /// The square of the sum of the first ten natural numbers is,
    /// $$(1 + 2 + ... + 10)^2 = 55^2 = 3025$$
    /// Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is $3025 - 385 = 2640$.
    /// Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
    /// </summary>
    public class Problem0006 : IProblem
    {
        public int ProblemId => 6;

        public Task<string> Run(Problem0006Config config, EulerRepo repo)
        {
            var numbers = CreateLongList(1, config.NumbersToRun);
            long sumSquare = (long)Math.Pow(numbers.Sum(), 2);
            long squareSum = numbers.Sum(n => (long)Math.Pow(n, 2));

            return Task.FromResult(Math.Abs(sumSquare - squareSum).ToString());
        }

        public async Task<string> Run(Test test, EulerRepo repo)
            => await Run(test.GetParameters<Problem0006Config>(), repo);

        private static List<long> CreateLongList(long from, long to, long interval = 1)
        {
            List<long> result = new();
            for (long i = from; i <= to; i += interval)
                result.Add(i);

            return result;
        }
    }

    public class Problem0006Config : IProblemParameters
    {
        public int NumbersToRun { get; set; }
    }
}
