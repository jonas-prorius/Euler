using System.Threading.Tasks;
using EulerDb.Entities;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
    /// a2 + b2 = c2
    /// For example, 32 + 42 = 9 + 16 = 25 = 52.
    /// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
    /// Find the product abc.
    /// </summary>
    public class Problem0009 : IProblem
    {
        public bool IsSelfContained => true;

        public Task<string> Run(Test test)
        {
            Problem0009Config config = test.GetParameters<Problem0009Config>();

            long a;
            long b = 0;
            long c = 0;

            for (a = 1; a < config.Target; a++)
                for (b = a + 1; b < config.Target; b++)
                    for (c = b + 1; c < config.Target; c++)
                        if (a * a + b * b == c * c && a + b + c == config.Target)
                            return Task.FromResult((a * b * c).ToString());

            return Task.FromResult((a * b * c).ToString());
        }
    }

    public class Problem0009Config : IProblemParameters
    {
        public int Target { get; set; }
    }
}
