using System.Linq;
using System.Threading.Tasks;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// The prime factors of 13195 are 5, 7, 13 and 29.
    /// What is the largest prime factor of the number 600851475143 ?
    /// </summary>
    public class Problem0003 : IProblem<Problem0003Config, long>
    {
        public int ProblemId => 3;

        public async Task<long> Run(Problem0003Config config, EulerRepo repo)
        {
            await repo.Numbers.EnsurePrimesCalculatedUntilAsync(config.Number);
            var n = repo.Numbers.Get(config.Number);
            var nf = n.Factors.Max(f => f.Id);
            return nf;
        }
    }

    public class Problem0003Config : IProblemConfig
    {
        public long Number { get; set; }
    }
}
