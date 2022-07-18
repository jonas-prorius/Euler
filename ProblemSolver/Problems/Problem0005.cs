﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDomain;
using EulerMath;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
    /// What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
    /// </summary>
    public class Problem0005 : IProblem<Problem0005Config, long?>
    {
        public int ProblemId => 5;

        public Task<long?> Run(Problem0005Config config, EulerRepo repo)
        {
            var factors = CreateLongList(2, config.Numbers).OrderByDescending(f => f).ToList();
            long start = factors[0] * factors[1];
            for (long? i = start; i < factors.Product(); i += factors.Last())
            {
                if (factors.All(f => i % f == 0))
                    return Task.FromResult(i);
            }
            return Task.FromResult(default(long?));
        }

        private static List<long> CreateLongList(long from, long to, long interval = 1)
        {
            List<long> result = new();
            for (long i = from; i <= to; i += interval)
                result.Add(i);
            return result;
        }
    }

    public class Problem0005Config : IProblemParameters
    {
        public long Numbers { get; set; }
    }
}
