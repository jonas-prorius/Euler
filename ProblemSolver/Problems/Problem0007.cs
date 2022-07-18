using System.Threading.Tasks;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
    /// What is the 10 001st prime number?
    /// </summary>
    public class Problem0007 : IProblem<Problem0007Config>
    {
        public int ProblemId => 7;

        public async Task<string> Run(Problem0007Config config, EulerRepo repo)
        {
            return (await repo.Numbers.GetPrimeAsync(config.PrimeNo)).Id.ToString();
        }
    }

    public class Problem0007Config : IProblemParameters
    {
        public long PrimeNo { get; set; }
    }
}
