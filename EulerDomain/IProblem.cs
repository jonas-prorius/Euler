using System.Threading.Tasks;
using EulerDomain;
using System.Linq;

namespace EulerDomain
{
    public interface IProblem<T, R> where T : IProblemParameters
    {
        public abstract int ProblemId { get; }

        public abstract Task<R> Run(T parameters, EulerRepo repo);

        public void AddConfig(T config, EulerRepo repo)
        {
            repo.Problems.Add(ProblemId, this);
        }
    }

    public interface IProblemParameters
    {
    }
}
