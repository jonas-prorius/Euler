using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;

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

        public Test Get(int id)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                return db.Tests.First(t => t.Id == id);
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
