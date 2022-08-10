using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerDomain;
using EulerMath;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// The prime factors of 13195 are 5, 7, 13 and 29.
    /// What is the largest prime factor of the number 600851475143 ?
    /// </summary>
    public class Problem0003 : IProblem
    {
        public int ProblemId => 3;

        public static Task<string> Run(Problem0003Config config)
        {
            return Task.FromResult(config.Number.GetLargestFactor().ToString());
        }

        public async Task<string> Run(Test test, EulerRepo _)
            => await Run(test.GetParameters<Problem0003Config>());
    }

    public class Problem0003Config : IProblemParameters
    {
        public long Number { get; set; }
    }
}
