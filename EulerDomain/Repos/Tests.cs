using EulerDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EulerDb;

using DbE = EulerDb.Entities;
using EulerDomain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using System;
using System.Collections.Generic;
using System.Linq;
using EulerDb;

using EulerMath;

using Microsoft.EntityFrameworkCore;
using DbE = EulerDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using DbE = EulerDb.Entities;
using EulerDomain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace EulerDomain.Repos
{
    public class Tests
    {
        private readonly EulerDbContextFactory _dbFactory;

        public Tests(EulerDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Test Get(int id)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                return new Test(db.Tests.First(t => t.Id == id), _dbFactory);
            }
        }

        public List<Test> GetByProblem(Problem problem, bool? isProblem = null, bool? isSolved = null)
        {
            return GetByProblemId(problem.Id, isProblem, isSolved);
        }

        public List<Test> GetByProblemId(long problemId, bool? isProblem = null, bool? isSolved = null)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                IQueryable<Test>? tests = db.Tests
                    .Where(t => t.ProblemId == problemId)
                    .Select(t => new Test(t, _dbFactory));

                if (isProblem.HasValue)
                    tests = tests.Where(t => t.IsProblem == isProblem.Value);

                if (isSolved.HasValue)
                    tests = tests.Where(t => t.Problem.IsSolved == isSolved.Value);

                return tests.ToList();
            }
        }

        public void Add(Test test)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                db.Add(test);
                db.SaveChanges();
            }
        }

        public async Task AddAsync(Test test)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                await db.AddAsync(test);
                await db.SaveChangesAsync();
            }
        }
    }
}
