using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSolver;

namespace ProblemsTests
{
    [TestClass]
    public class ProblemTests
    {
        private static readonly string connectionString = @"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;";

        [TestMethod]
        public async Task CalculateAll()
        {
            EulerRepo? repo = new(new EulerDbContextFactory(connectionString));
            System.Collections.Generic.List<EulerDb.Entities.Test>? tests = repo.Tests.GetAll(true);
            System.Collections.Generic.List<EulerDb.Entities.Problem>? problems = repo.Problems.GetAll();

            foreach (EulerDb.Entities.Problem? problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (EulerDb.Entities.Test? test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (EulerDb.Entities.Test? test in tests)
            {
                string? runAnswer = await test.Run();
                await repo.Tests.SetAnswerAsync(test.Id, runAnswer);
                await repo.Problems.SetIsSolvedAsync(test.ProblemId, true);
            }
        }

        [TestMethod]
        public async Task CalculateUnsolved()
        {
            EulerRepo? repo = new(new EulerDbContextFactory(connectionString));
            System.Collections.Generic.List<EulerDb.Entities.Test>? tests = repo.Tests.GetAll(true, false);
            System.Collections.Generic.List<EulerDb.Entities.Problem>? problems = repo.Problems.GetAll(false);

            foreach (EulerDb.Entities.Problem? problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (EulerDb.Entities.Test? test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (EulerDb.Entities.Test? test in tests)
            {
                string? runAnswer = await test.Run();
                await repo.Tests.SetAnswerAsync(test.Id, runAnswer);
                await repo.Problems.SetIsSolvedAsync(test.ProblemId, true);
            }
        }

        [TestMethod]
        public async Task TestSolved()
        {
            EulerRepo? repo = new(new EulerDbContextFactory(connectionString));
            System.Collections.Generic.List<EulerDb.Entities.Test>? tests = repo.Tests.GetAll(true, true);
            System.Collections.Generic.List<EulerDb.Entities.Problem>? problems = repo.Problems.GetAll(true);

            foreach (EulerDb.Entities.Problem? problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (EulerDb.Entities.Test? test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (EulerDb.Entities.Test? test in tests)
            {
                string? runAnswer = await test.Run();
                Assert.AreEqual(test.Answer, runAnswer);
            }
        }
    }
}
