using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSolver;

namespace ProblemsTests
{
    [TestClass]
    public class InitTests
    {
        private static readonly string connectionString = @"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;";

        [TestMethod]
        public async Task TestProblems()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(connectionString));
            var tests = repo.Tests.GetAll(false);
            var problems = repo.Problems.GetAll();

            foreach (var problem in problems)
                Assert.IsTrue(tests.Any(t => t.ProblemId == problem.Id));

            foreach (var test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await test.Run());
        }
    }
}
