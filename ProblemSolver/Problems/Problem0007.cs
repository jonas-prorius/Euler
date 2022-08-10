using System.Threading.Tasks;
using EulerDb.Entities;
using EulerMath;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
    /// What is the 10 001st prime number?
    /// </summary>
    public class Problem0007 : IProblem
    {
        public Task<string> Run(Test test)
        {
            Problem0007Config config = test.GetParameters<Problem0007Config>();

            long primeNo = 0;
            long current = 0;

            while (primeNo < config.PrimeNo)
            {
                current = current.NextPrime();
                primeNo++;
            }

            return Task.FromResult(current.ToString());
        }
    }

    public class Problem0007Config : IProblemParameters
    {
        public long PrimeNo { get; set; }
    }
}
