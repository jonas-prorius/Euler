using System.Threading.Tasks;
using EulerDb.Entities;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
    /// What is the 10 001st prime number?
    /// </summary>
    public class Problem0007 : IProblem
    {
        public int ProblemId => 7;

        public static async Task<string> Run(Problem0007Config config, EulerRepo repo)
        {
            return (await repo.Numbers.GetPrimeAsync(config.PrimeNo)).ToString();
        }

        public async Task<string> Run(Test test, EulerRepo repo)
            => await Run(test.GetParameters<Problem0007Config>(), repo);
    }

    public class Problem0007Config : IProblemParameters
    {
        public long PrimeNo { get; set; }
    }
}
