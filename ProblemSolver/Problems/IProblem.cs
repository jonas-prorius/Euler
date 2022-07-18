using System.Threading.Tasks;
using EulerDomain;

namespace ProblemSolver.Problems
{
    public interface IProblem<T, R> where T : IProblemConfig
    {
        public abstract int ProblemId { get; }

        public abstract Task<R> Run(T config, EulerRepo repo);
    }

    public interface IProblemConfig
    {
    }
}
