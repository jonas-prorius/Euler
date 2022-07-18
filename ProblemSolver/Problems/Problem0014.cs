using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    ///  <p>The following iterative sequence is defined for the set of positive integers:</p>
    ///  <p class="margin_left"><var>n</var> → <var>n</var>/2 (<var>n</var> is even)<br /><var>n</var> → 3<var>n</var> + 1 (<var>n</var> is odd)</p>
    ///  <p>Using the rule above and starting with 13, we generate the following sequence:</p>
    ///  <div class="center">13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1</div>
    ///  <p>It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.</p>
    ///  <p>Which starting number, under one million, produces the longest chain?</p>
    ///  <p class="note"><b>NOTE:</b> Once the chain starts the terms are allowed to go above one million.</p>
    /// </summary>
    public class Problem0014 : IProblem<Problem0014Config, long>
    {
        public int ProblemId => 14;

        public async Task<long> Run(Problem0014Config config, EulerRepo repo)
        {
            Dictionary<long, List<long>> results = new();
            for (long i = config.MinStart; i <= config.MaxStart; i++)
            {
                long count = 1;
                long current = i;
                while (current != 1)
                {
                    current = NextCollatz(current);
                    count++;
                }
                if (!results.ContainsKey(count))
                    results[count] = new List<long>() { i };
                else
                    results[count].Add(i);
            }
            return results[results.Keys.Max()].First();
        }

        private static long NextCollatz(long current)
        {
            if (current % 2 == 0)
                return current / 2;
            else return 3 * current + 1;
        }
    }

    public class Problem0014Config : IProblemParameters
    {
        public long MinStart { get; set; }
        public long MaxStart { get; set; }
    }
}
