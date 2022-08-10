using System.Threading.Tasks;
using EulerDb.Entities;

namespace ProblemSolver
{
    public interface IProblem
    {
        public abstract int ProblemId { get; }

        public abstract Task<string> Run(Test test);
    }

    public interface IProblemParameters
    {
    }
}
