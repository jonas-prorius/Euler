using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerHelper;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// The sum of the squares of the first ten natural numbers is,
    /// 1² + 2² + ... + 10² = 385
    /// 
    /// The square of the sum of the first ten natural numbers is,
    /// (1 + 2 + ... + 10)² = 55² = 3025
    /// 
    /// Hence the difference between the sum of the squares of the first ten natural numbers 
    /// and the square of the sum is 3025 − 385 = 2640.
    /// 
    /// Find the difference between the sum of the squares of the first one hundred natural numbers 
    /// and the square of the sum.
    /// </summary>
    public class Problem0006 : IProblem
    {
        /// <summary>
        /// This problem can run without external dependencies.
        /// </summary>
        public bool IsSelfContained => true;

        /// <summary>
        /// Solves the sum square difference problem.
        /// </summary>
        /// <param name="test">Test configuration containing the upper limit parameter</param>
        /// <returns>The difference between square of sum and sum of squares</returns>
        public Task<string> Run(Test test)
        {
            Problem0006Config config = test.GetParameters<Problem0006Config>();
            
            // Generate the sequence of natural numbers from 1 to the upper limit
            var numbers = Miscellaneous.CreateLongList(1, config.UpperLimit);
            
            // Calculate the sum of squares: 1² + 2² + ... + n²
            long sumOfSquares = numbers.Sum(n => n * n);
            
            // Calculate the sum of numbers: 1 + 2 + ... + n
            long sum = numbers.Sum();
            
            // Calculate the square of the sum: (1 + 2 + ... + n)²
            long squareOfSum = sum * sum;
            
            // Calculate the difference
            long difference = squareOfSum - sumOfSquares;
            
            return Task.FromResult(difference.ToString());
        }
    }

    /// <summary>
    /// Configuration parameters for Problem 6.
    /// </summary>
    public class Problem0006Config : IProblemParameters
    {
        /// <summary>
        /// The upper limit for the sequence of natural numbers.
        /// For the original problem, this should be 100.
        /// </summary>
        public long UpperLimit { get; set; }
    }
}