using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ProblemSolver.Problems;

namespace Prepper.Workers
{
    internal class TestCreator : BackgroundService
    {
        private readonly EulerDbContextFactory _dbFactory;

        public TestCreator(EulerDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<Test> tests = new List<Test>
            {
                new Test(1, false, JsonConvert.SerializeObject(new Problem0001Config { Roof = 10 }), "23"),
                new Test(1, true, JsonConvert.SerializeObject(new Problem0001Config { Roof = 1000 })),
                new Test(2, false, JsonConvert.SerializeObject(new Problem0002Config { MaxValue = 89 }), "44"),
                new Test(2, true, JsonConvert.SerializeObject(new Problem0002Config { MaxValue = 4000000 })),
                new Test(3, false, JsonConvert.SerializeObject(new Problem0003Config { Number = 13195 }), "29"),
                new Test(3, true, JsonConvert.SerializeObject(new Problem0003Config { Number = 600851475143  })),
                new Test(4, false, JsonConvert.SerializeObject(new Problem0004Config { Digits = 2 }), "9009"),
                new Test(4, true, JsonConvert.SerializeObject(new Problem0004Config { Digits = 3 })),
                new Test(5, false, JsonConvert.SerializeObject(new Problem0005Config { Numbers = 10 }), "2520"),
                new Test(5, true, JsonConvert.SerializeObject(new Problem0005Config { Numbers = 20 })),
                new Test(6, false, JsonConvert.SerializeObject(new Problem0006Config { NumbersToRun = 10 }), "2640"),
                new Test(6, true, JsonConvert.SerializeObject(new Problem0006Config { NumbersToRun = 100 })),
                new Test(7, false, JsonConvert.SerializeObject(new Problem0007Config { PrimeNo = 6 }), "13"),
                new Test(7, true, JsonConvert.SerializeObject(new Problem0007Config { PrimeNo = 10001 })),
                new Test(8, false, JsonConvert.SerializeObject(new Problem0008Config { SampleSize = 4 }), "5832"),
                new Test(8, true, JsonConvert.SerializeObject(new Problem0008Config { SampleSize = 13 })),
                new Test(10, false, JsonConvert.SerializeObject(new Problem0010Config { Roof = 10 }), "17"),
                new Test(10, true, JsonConvert.SerializeObject(new Problem0010Config { Roof = 2000000 })),
                new Test(11, false, JsonConvert.SerializeObject(new Problem0011Config { SegmentLength = 1 }), "99"),
                new Test(11, true, JsonConvert.SerializeObject(new Problem0011Config { SegmentLength = 4 })),
                new Test(14, false, JsonConvert.SerializeObject(new Problem0014Config { MinStart = 13, MaxStart = 13 }), "13"),
                new Test(14, true, JsonConvert.SerializeObject(new Problem0014Config { MinStart = 2, MaxStart = 1000000 })),
                new Test(47, false, JsonConvert.SerializeObject(new Problem0047Config { ConsecutivesAndDistinct = 2 }), "14"),
                new Test(47, false, JsonConvert.SerializeObject(new Problem0047Config { ConsecutivesAndDistinct = 3 }), "644"),
                new Test(47, true, JsonConvert.SerializeObject(new Problem0047Config { ConsecutivesAndDistinct = 4 })),
            };

            using (var db = _dbFactory.CreateDbContext())
            {
                foreach (var test in tests)
                {
                    var problem = db.Problems.Include(p => p.Tests).FirstOrDefault(p => p.Id == test.ProblemId);

                    if (problem == null)
                    {
                        problem = new Problem(test.ProblemId);
                        db.Problems.Add(problem);
                    }

                    if (!problem.Tests.Select(t => t.ToString()).Contains(test.ToString()))
                        problem.Tests.Add(test);

                    await db.SaveChangesAsync(stoppingToken);
                }
            }
        }
    }
}
