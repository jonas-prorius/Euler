using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;
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
            List<Test>? tests = repo.Tests.GetAll(true);
            List<Problem>? problems = repo.Problems.GetAll();

            foreach (Problem? problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (Test? test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (Test? test in tests)
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
            List<Test>? tests = repo.Tests.GetAll(true, false);
            List<Problem>? problems = repo.Problems.GetAll(false);

            foreach (Problem? problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (Test? test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (Test? test in tests)
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
            List<Test>? tests = repo.Tests.GetAll(true, true);
            List<Problem>? problems = repo.Problems.GetAll(true);

            foreach (Problem? problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (Test? test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (Test? test in tests)
            {
                string? runAnswer = await test.Run();
                Assert.AreEqual(test.Answer, runAnswer);
            }
        }
    }
}
