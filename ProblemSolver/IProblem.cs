using System.Threading.Tasks;
using EulerDb.Entities;
using EulerDomain;

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
