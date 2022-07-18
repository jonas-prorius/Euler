using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;
using EulerDomain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProblemSolver.Problems;

namespace Prepper.Workers
{
    internal class TestCreator : BackgroundService
    {
        private readonly EulerDbContextFactory _dbFactory;
        private readonly ILogger<TestCreator> _logger;

        public TestCreator(EulerDbContextFactory dbFactory, ILogger<TestCreator> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<Test> tests = new List<Test>
            {
                new Test(1, 1, false, JsonConvert.SerializeObject(new Problem0001Config { MaxNumber = 9 }), "23"),
                new Test(2, 1, false, JsonConvert.SerializeObject(new Problem0002Config { MaxValue = 89 }), "44"),
                new Test(3, 1, false, JsonConvert.SerializeObject(new Problem0003Config { Number = 13195 }), "29"),
                new Test(4, 1, false, JsonConvert.SerializeObject(new Problem0004Config { Digits = 2 }), "9009"),
                new Test(5, 1, false, JsonConvert.SerializeObject(new Problem0005Config { Numbers = 10 }), "2520"),
                new Test(6, 1, false, JsonConvert.SerializeObject(new Problem0006Config { NumbersToRun = 10 }), "2640"),
                new Test(7, 1, false, JsonConvert.SerializeObject(new Problem0007Config { PrimeNo = 6 }), "13"),
                new Test(8, 1, false, JsonConvert.SerializeObject(new Problem0008Config { SampleSize = 4 }), "5832"),
                new Test(10, 1, false, JsonConvert.SerializeObject(new Problem0010Config { Roof = 10 }), "17"),
                new Test(11, 1, false, JsonConvert.SerializeObject(new Problem0011Config { SegmentLength = 1 }), "99"),
                new Test(14, 1, false, JsonConvert.SerializeObject(new Problem0014Config { MinStart = 13, MaxStart = 13 }), "13"),
            };

            using (var db = _dbFactory.CreateDbContext())
            {
                await db.Tests.AddRangeAsync(tests, stoppingToken);
                await db.SaveChangesAsync(stoppingToken);
            }
        }
    }
}
