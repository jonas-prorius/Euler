using System.Threading.Tasks;
using EulerDb;
using EulerDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSolver.Problems;

namespace ProblemsTests
{
    [TestClass]
    public class InitTests
    {
        [TestMethod]
        public async Task Problem0001Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(1))
                Assert.AreEqual(test.Answer, await new Problem0001().Run(test.GetParameters<Problem0001Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0002Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(2))
                Assert.AreEqual(test.Answer, await new Problem0002().Run(test.GetParameters<Problem0002Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0003Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(3))
                Assert.AreEqual(test.Answer, await new Problem0003().Run(test.GetParameters<Problem0003Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0004Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(4))
                Assert.AreEqual(test.Answer, await new Problem0004().Run(test.GetParameters<Problem0004Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0005Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(5))
                Assert.AreEqual(test.Answer, await new Problem0005().Run(test.GetParameters<Problem0005Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0006Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(6))
                Assert.AreEqual(test.Answer, await new Problem0006().Run(test.GetParameters<Problem0006Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0007Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(7))
                Assert.AreEqual(test.Answer, await new Problem0007().Run(test.GetParameters<Problem0007Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0008Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(8))
                Assert.AreEqual(test.Answer, await new Problem0008().Run(test.GetParameters<Problem0008Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0010Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(10))
                Assert.AreEqual(test.Answer, await new Problem0010().Run(test.GetParameters<Problem0010Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0011Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(11))
                Assert.AreEqual(test.Answer, await new Problem0011().Run(test.GetParameters<Problem0011Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0014nit()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));

            foreach (var test in repo.Tests.GetByProblemId(14))
                Assert.AreEqual(test.Answer, await new Problem0014().Run(test.GetParameters<Problem0014Config>(), repo));
        }
    }
}
