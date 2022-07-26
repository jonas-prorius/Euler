using System.Linq;
using System.Threading.Tasks;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    ///  The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
    ///  Find the sum of all the primes below two million.
    /// </summary>
    public class Problem0010 : IProblem<Problem0010Config>
    {
        public int ProblemId => 10;

        public async Task<string> Run(Problem0010Config config, EulerRepo repo)
        {
            return (await repo.Numbers.GetPrimesUntilAsync(config.Roof)).Sum().ToString();
        }
    }

    public class Problem0010Config : IProblemParameters
    {
        public long Roof { get; set; }
    }
}
