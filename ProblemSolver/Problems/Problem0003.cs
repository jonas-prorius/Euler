using System.Linq;
using System.Threading.Tasks;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// The prime factors of 13195 are 5, 7, 13 and 29.
    /// What is the largest prime factor of the number 600851475143 ?
    /// </summary>
    public class Problem0003 : IProblem<Problem0003Config>
    {
        public int ProblemId => 3;

        public async Task<string> Run(Problem0003Config config, EulerRepo repo)
        {
            await repo.Numbers.EnsurePrimesCalculatedUntilAsync(config.Number);
            var n = repo.Numbers.Get(config.Number);
            var nf = n.Factors.Max(f => f.FactorNumberId);
            return nf.ToString();
        }
    }

    public class Problem0003Config : IProblemParameters
    {
        public long Number { get; set; }
    }
}
