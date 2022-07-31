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
            var repo = new EulerRepo(new EulerDbContextFactory(connectionString));
            var tests = repo.Tests.GetAll(true);
            var problems = repo.Problems.GetAll();

            foreach (var problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (var test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (var test in tests)
                await repo.Tests.SetAnswerAsync(test.Id, await test.Run(repo));
        }

        [TestMethod]
        public async Task CalculateUnsolved()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(connectionString));
            var tests = repo.Tests.GetAll(true, false);
            var problems = repo.Problems.GetAll(false);

            foreach (var problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (var test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await test.Run(repo));
        }

        [TestMethod]
        public async Task TestSolved()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(connectionString));
            var tests = repo.Tests.GetAll(true, true);
            var problems = repo.Problems.GetAll(true);

            foreach (var problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (var test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await test.Run(repo));
        }
    }
}
