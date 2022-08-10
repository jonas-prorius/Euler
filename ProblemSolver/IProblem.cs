using System.Threading.Tasks;
using EulerDb.Entities;

namespace ProblemSolver
{
    public interface IProblem
    {
        public abstract Task<string> Run(Test test);
    }

    public interface IProblemParameters
    {
    }
}
