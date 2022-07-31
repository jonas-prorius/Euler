using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerDomain;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// The prime factors of 13195 are 5, 7, 13 and 29.
    /// What is the largest prime factor of the number 600851475143 ?
    /// </summary>
    public class Problem0003 : IProblem
    {
        public int ProblemId => 3;

        public static async Task<string> Run(Problem0003Config config, EulerRepo repo)
        {
            return (await repo.Numbers.GetFactorsAsync(config.Number)).Max().ToString();
        }

        public async Task<string> Run(Test test, EulerRepo repo)
            => await Run(test.GetParameters<Problem0003Config>(), repo);
    }

    public class Problem0003Config : IProblemParameters
    {
        public long Number { get; set; }
    }
}
