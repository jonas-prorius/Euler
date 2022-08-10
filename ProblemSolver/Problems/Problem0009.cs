using System;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerMath;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
    /// a2 + b2 = c2
    /// For example, 32 + 42 = 9 + 16 = 25 = 52.
    /// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
    /// Find the product abc.
    /// The four adjacent digits in the 1000-digit number that have the greatest product are 9 × 9 × 8 × 9 = 5832.
    /// </summary>
    public class Problem0009 : IProblem
    {
        public async Task<string> Run(Test test)
        {
            Problem0009Config config = test.GetParameters<Problem0009Config>();

            long a = 0;
            long b;
            double c = 0;

            while (c != config.Target)
            {
                a++;

                for (b = a + 1; Math.Pow(a, 2) + Math.Pow(b, 2) < Math.Sqrt((long)Math.Pow(a, 2) + (long)Math.Pow(b, 2)); b++)
                    c = Math.Sqrt((long)Math.Pow(a, 2) + (long)Math.Pow(b, 2));
            }

            return new long[] { 1, 2, 4 }.Product().ToString();
        }
    }

    public class Problem0009Config : IProblemParameters
    {
        public int Target { get; set; }
    }
}
