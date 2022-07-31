using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    ///  If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
    ///  Find the sum of all the multiples of 3 or 5 below 1000.
    /// </summary>
    public class Problem0001 : IProblem
    {
        public int ProblemId => 1;

        public static Task<string> Run(Problem0001Config config)
        {
            return Task.FromResult(Enumerable.Range(0, config.Roof)
                .Where(n => n % 3 == 0 || n % 5 == 0)
                .Sum()
                .ToString());
        }

        public async Task<string> Run(Test test, EulerRepo _)
            => await Run(test.GetParameters<Problem0001Config>());
    }

    public class Problem0001Config : IProblemParameters
    {
        public int Roof { get; set; }
    }
}
