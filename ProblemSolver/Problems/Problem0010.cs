using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerDomain;
using EulerMath;

namespace ProblemSolver.Problems
{
    /// <summary>
    ///  The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
    ///  Find the sum of all the primes below two million.
    /// </summary>
    public class Problem0010 : IProblem
    {
        public int ProblemId => 10;

        public Task<string> Run(Test test)
        {
            var config = test.GetParameters<Problem0010Config>();

            long sum = 0;

            List<long> primes = new List<long>();

            for (long number = 2; number < config.Roof; number++)
            {
                if (number.IsPrime())
                    sum += number;
            }

            return Task.FromResult(sum.ToString());
        }
    }

    public class Problem0010Config : IProblemParameters
    {
        public long Roof { get; set; }
    }
}
