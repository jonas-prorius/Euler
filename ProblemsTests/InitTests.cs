using System.Threading.Tasks;
using EulerDb;
using EulerDomain;
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
            Assert.AreEqual(23, await new Problem0001().Run(new Problem0001Config { MaxNumber = 9 }, repo));
        }

        [TestMethod]
        public async Task Problem0002Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(44, await new Problem0002().Run(new Problem0002Config { MaxValue = 89 }, repo));
        }

        [TestMethod]
        public async Task Problem0003Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(29, await new Problem0003().Run(new Problem0003Config { Number = 13195 }, repo));
        }

        [TestMethod]
        public async Task Problem0004Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(9009, await new Problem0004().Run(new Problem0004Config { Digits = 2 }, repo));
        }

        [TestMethod]
        public async Task Problem0005Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(2520, await new Problem0005().Run(new Problem0005Config { Numbers = 10 }, repo));
        }

        [TestMethod]
        public async Task Problem0006Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(2640, await new Problem0006().Run(new Problem0006Config { NumbersToRun = 10 }, repo));
        }

        [TestMethod]
        public async Task Problem0007Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(13, await new Problem0007().Run(new Problem0007Config { PrimeNo = 6 }, repo));
        }

        [TestMethod]
        public async Task Problem0008Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(5832, await new Problem0008().Run(new Problem0008Config { SampleSize = 4 }, repo));
        }

        [TestMethod]
        public async Task Problem0010Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(17, await new Problem0010().Run(new Problem0010Config { Roof = 10 }, repo));
        }

        [TestMethod]
        public async Task Problem0011Init()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(99, await new Problem0011().Run(new Problem0011Config { SegmentLength = 1 }, repo));
        }

        [TestMethod]
        public async Task Problem0014nit()
        {
            var repo = new EulerRepo(new EulerDbContextFactory(@"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;"));
            Assert.AreEqual(13, await new Problem0014().Run(new Problem0014Config { MinStart = 13, MaxStart = 13 }, repo));
        }
    }
}
