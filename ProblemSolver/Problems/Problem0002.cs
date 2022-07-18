﻿using System.Threading.Tasks;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// Each new term in the Fibonacci sequence is generated by adding the previous two terms. By starting with 1 and 2, the first 10 terms will be:
    /// 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
    /// By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.
    /// </summary>
    public class Problem0002 : IProblem<Problem0002Config, long>
    {
        public int ProblemId => 2;

        public Task<long> Run(Problem0002Config config, EulerRepo repo)
        {
            long e1 = 1;
            long e2 = 2;
            long e3;
            long sum = 2;
            while (e1 + e2 <= config.MaxValue)
            {
                e3 = e1 + e2;
                e1 = e2;
                e2 = e3;
                if (e3 % 2 == 0)
                    sum += e3;
            }

            return Task.FromResult(sum);
        }
    }

    public class Problem0002Config : IProblemConfig
    {
        public long MaxValue { get; set; }
    }
}
