﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerHelper;
using EulerMath;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
    /// What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
    /// </summary>
    public class Problem0005 : IProblem
    {
        public bool IsSelfContained => false;

        public Task<string> Run(Test test)
        {
            Problem0005Config config = test.GetParameters<Problem0005Config>();

            List<long> factors = Miscellaneous.CreateLongList(2, config.Numbers).OrderByDescending(f => f).ToList();
            long start = factors[0] * factors[1];
            for (long i = start; i < factors.Product(); i += factors.Last())
                if (factors.All(f => i % f == 0))
                    return Task.FromResult(i.ToString());

            return Task.FromResult(factors.Product().ToString());
        }
    }

    public class Problem0005Config : IProblemParameters
    {
        public long Numbers { get; set; }
    }
}
