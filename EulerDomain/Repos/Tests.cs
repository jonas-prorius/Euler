using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace EulerDomain.Repos
{
    public class Tests
    {
        #region Fields

        private readonly EulerDbContextFactory _dbFactory;

        #endregion Fields

        #region Constructors

        public Tests(EulerDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion Constructors

        #region Public Methods

        public async Task<Test> GetAsync(int id)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                return await db.Tests.FirstAsync(t => t.Id == id);
            }
        }

        public List<Test> GetAll(bool? isProblem = null, bool? isSolved = null)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                IEnumerable<Test> tests = db.Tests;

                if (isProblem.HasValue)
                    tests = tests.Where(t => t.IsProblem == isProblem.Value);

                if (isSolved.HasValue)
                    tests = tests.Where(t => !string.IsNullOrEmpty(t.Answer) == isSolved.Value);

                return tests.ToList();
            }
        }

        public List<Test> GetByProblemId(long problemId, bool? isProblem = null, bool? isSolved = null)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                IQueryable<Test>? tests = db.Tests
                    .Where(t => t.ProblemId == problemId);

                if (isProblem.HasValue)
                    tests = tests.Where(t => t.IsProblem == isProblem.Value);

                if (isSolved.HasValue)
                    tests = tests.Where(t => t.Problem.IsSolved == isSolved.Value);

                return tests.ToList();
            }
        }

        public async Task SetAnswerAsync(int problemId, string answer)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                var test = await db.Tests.FirstAsync(t => t.ProblemId == problemId);
                test.Answer = answer;
                await db.SaveChangesAsync();
            }
        }

        public void Add(Test test)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                db.Tests.Add(test);
                db.SaveChanges();
            }
        }

        public async Task AddAsync(Test test)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                await db.Tests.AddAsync(test);
                await db.SaveChangesAsync();
            }
        }

        #endregion Public Methods

        //public List<Test> GetByProblem(Problem problem, bool? isProblem = null, bool? isSolved = null)
        //{
        //    return GetByProblemId(problem.Id, isProblem, isSolved);
        //}
    }
}
