﻿using System.Linq;
using System.Threading.Tasks;
using EulerDomain;
using EulerDomain.Repos;

namespace ProblemSolver.Problems
{
    /// <summary>
    ///  If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
    ///  Find the sum of all the multiples of 3 or 5 below 1000.
    /// </summary>
    public class Problem0001 : IProblem<Problem0001Config>
    {
        public int ProblemId => 1;

        public Task<string> Run(Problem0001Config config, EulerRepo repo)
        {
            return Task.FromResult(Enumerable.Range(0, config.MaxNumber + 1)
                .Where(n => n % 3 == 0 || n % 5 == 0)
                .Sum()
                .ToString());
        }
    }

    public class Problem0001Config : IProblemParameters
    {
        public int MaxNumber { get; set; }
    }
}
