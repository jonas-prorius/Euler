using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerHelper;

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
        public bool IsSelfContained => false;

        public Task<string> Run(Test test)
        {
            Problem0006Config config = test.GetParameters<Problem0006Config>();

            List<long>? numbers = Miscellaneous.CreateLongList(1, config.NumbersToRun);
            long sumSquare = (long)Math.Pow(numbers.Sum(), 2);
            long squareSum = numbers.Sum(n => (long)Math.Pow(n, 2));

            return Task.FromResult(Math.Abs(sumSquare - squareSum).ToString());
        }
    }

    public class Problem0006Config : IProblemParameters
    {
        public int NumbersToRun { get; set; }
    }
}
