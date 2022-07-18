using System.Threading.Tasks;

namespace EulerDomain
{
    public interface IProblem<T> where T : IProblemParameters
    {
        public abstract int ProblemId { get; }

        public abstract Task<string> Run(T parameters, EulerRepo repo);
    }

    public interface IProblemParameters
    {
    }
}
