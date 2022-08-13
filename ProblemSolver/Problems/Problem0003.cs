using System.Threading.Tasks;
using EulerDb.Entities;
using EulerMath;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// The prime factors of 13195 are 5, 7, 13 and 29.
    /// What is the largest prime factor of the number 600851475143 ?
    /// </summary>
    public class Problem0003 : IProblem
    {
        public bool IsSelfContained => false;

        public Task<string> Run(Test test)
        {
            Problem0003Config config = test.GetParameters<Problem0003Config>();
            return Task.FromResult(config.Number.GetLargestFactor().ToString());
        }
    }

    public class Problem0003Config : IProblemParameters
    {
        public long Number { get; set; }
    }
}
