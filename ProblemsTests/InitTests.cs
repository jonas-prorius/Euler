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
    public class InitTests
    {
        private static readonly string connectionString = @"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;";

        [TestMethod]
        public async Task TestProblems()
        {
            EulerRepo? repo = new(new EulerDbContextFactory(connectionString));
            List<Test>? tests = repo.Tests.GetAll(false);
            List<Problem>? problems = repo.Problems.GetAll();

            foreach (Test? test in tests)
                Assert.IsTrue(problems.Any(p => p.Id == test.ProblemId));

            foreach (Test? test in tests)
                Assert.AreEqual(test.Answer, await test.Run());
        }
    }
}
