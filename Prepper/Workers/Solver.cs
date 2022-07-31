using System.Threading;
using System.Threading.Tasks;
using EulerDomain;
using Microsoft.Extensions.Hosting;
using ProblemSolver;
using ProblemSolver.Problems;

namespace Prepper.Workers
{
    internal class Solver : BackgroundService
    {
        private readonly EulerRepo _repo;

        public Solver(EulerRepo repo)
        {
            _repo = repo;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var unsolved = _repo.Tests.GetAll(true, false);

            ProblemHelper.GetProblemInstances();

            foreach (var test in unsolved)
            {
                var answer = await new Problem0001().Run(test.GetParameters<Problem0001Config>(), _repo);
                await _repo.Tests.SetAnswerAsync(test.ProblemId, answer);
            }
        }
    }
}
