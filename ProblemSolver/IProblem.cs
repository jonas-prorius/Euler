using System.Threading.Tasks;
using EulerDb.Entities;

namespace ProblemSolver
{
    /// <summary>
    /// Interface that all Project Euler problem implementations must implement.
    /// </summary>
    public interface IProblem
    {
        /// <summary>
        /// Indicates whether this problem can be executed without external dependencies.
        /// Set to true if the problem can run standalone, false if it requires database connections or other external resources.
        /// </summary>
        public abstract bool IsSelfContained { get; }

        /// <summary>
        /// Executes the problem logic and returns the result.
        /// </summary>
        /// <param name="test">Test configuration containing parameters and expected results</param>
        /// <returns>The solution result as a string</returns>
        public abstract Task<string> Run(Test test);
    }

    /// <summary>
    /// Marker interface for problem configuration parameter classes.
    /// Implement this interface for classes that hold problem-specific configuration.
    /// </summary>
    public interface IProblemParameters
    {
    }
}
