using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    ///  The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
    ///  Find the sum of all the primes below two million.
    /// </summary>
    public class Problem0010 : IProblem
    {
        public int ProblemId => 10;

        public async Task<string> Run(Problem0010Config config, EulerRepo repo)
        {
            return (await repo.Numbers.GetPrimesUntilAsync(config.Roof)).Sum().ToString();
        }

        public async Task<string> Run(Test test, EulerRepo repo)
            => await Run(test.GetParameters<Problem0010Config>(), repo);
    }

    public class Problem0010Config : IProblemParameters
    {
        public long Roof { get; set; }
    }
}
