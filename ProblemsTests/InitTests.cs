using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;
using EulerDomain;
using EulerDomain.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSolver.Problems;

namespace ProblemsTests
{
    [TestClass]
    public class InitTests
    {
        private static readonly string connectionString = @"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;";

        [TestMethod]
        public async Task Problem0001Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 1);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0001().Run(test.GetParameters<Problem0001Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0002Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 2);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0002().Run(test.GetParameters<Problem0002Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0003Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 3);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0003().Run(test.GetParameters<Problem0003Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0004Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 4);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0004().Run(test.GetParameters<Problem0004Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0005Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 5);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0005().Run(test.GetParameters<Problem0005Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0006Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 6);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0006().Run(test.GetParameters<Problem0006Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0007Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 7);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0007().Run(test.GetParameters<Problem0007Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0008Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 8);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0008().Run(test.GetParameters<Problem0008Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0010Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 10);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0010().Run(test.GetParameters<Problem0010Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0011Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 11);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0011().Run(test.GetParameters<Problem0011Config>(), repo));
        }

        [TestMethod]
        public async Task Problem0014Init()
        {
            (var dbFactory, var repo, var tests) = GetDependencies(connectionString, 14);
            Assert.IsTrue(tests.Any());

            foreach (var test in tests)
                Assert.AreEqual(test.Answer, await new Problem0014().Run(test.GetParameters<Problem0014Config>(), repo));
        }

        private (EulerDbContextFactory dBfactory, EulerRepo repo) GetDependencies(string connectionString)
        {
            var dbFactory = new EulerDbContextFactory(connectionString);
            var repo = new EulerRepo(dbFactory);
            return (dbFactory, repo);
        }

        private (EulerDbContextFactory dBfactory, EulerRepo repo, IEnumerable<Test> tests) GetDependencies(string connectionString, int problemId)
        {
            var dbFactory = new EulerDbContextFactory(connectionString);
            var repo = new EulerRepo(dbFactory);
            var tests = repo.Tests.GetByProblemId(problemId);
            return (dbFactory, repo, tests);
        }
    }
}
