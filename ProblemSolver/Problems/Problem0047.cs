using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EulerDb.Entities;
using EulerDomain;

using EulerMath;

namespace ProblemSolver.Problems
{
    /// <summary>
    ///  <p>The first two consecutive numbers to have two distinct prime factors are:</p>
    ///  <p class="margin_left">14 = 2 × 7<br />15 = 3 × 5</p>
    ///  <p>The first three consecutive numbers to have three distinct prime factors are:</p>
    ///  <p class="margin_left">644 = 2² × 7 × 23<br />645 = 3 × 5 × 43<br />646 = 2 × 17 × 19.</p>
    ///  <p>Find the first four consecutive integers to have four distinct prime factors each.What is the first of these numbers?</p>
    /// </summary>
    public class Problem0047 : IProblem
    {
        public int ProblemId => 14;

        public Task<string> Run(Test test)
        {
            var config = test.GetParameters<Problem0047Config>();

            long current = 1;

            Queue<long> numbers = new Queue<long>();

            while (numbers.Count != config.ConsecutivesAndDistinct)
            {
                if (current.IsPrime())
                {
                    numbers.Clear();
                    current++;
                    continue;
                }

                var factors = (current.Factors()).Distinct();
                if (factors.Count() != config.ConsecutivesAndDistinct)
                {
                    numbers.Clear();
                    current++;
                    continue;
                }

                numbers.Enqueue(current++);
            }

            return Task.FromResult(numbers.Dequeue().ToString());
        }
    }

    public class Problem0047Config : IProblemParameters
    {
        public int ConsecutivesAndDistinct { get; set; }
    }
}
