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

        private readonly EulerDbContext _dbContext;

        #endregion Fields

        #region Constructors

        public Tests(EulerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Constructors

        #region Public Methods

        public async Task<Test> GetAsync(int id)
        {
            return await _dbContext.Tests.FirstAsync(t => t.Id == id);
        }

        public List<Test> GetAll(bool? isProblem = null, bool? isSolved = null)
        {
            IEnumerable<Test> tests = _dbContext.Tests;

            if (isProblem.HasValue)
                tests = tests.Where(t => t.IsProblem == isProblem.Value);

            if (isSolved.HasValue)
                tests = tests.Where(t => !string.IsNullOrEmpty(t.Answer) == isSolved.Value);

            return tests.ToList();
        }

        public List<Test> GetByProblemId(long problemId, bool? isProblem = null, bool? isSolved = null)
        {
            IQueryable<Test>? tests = _dbContext.Tests
                .Where(t => t.ProblemId == problemId);

            if (isProblem.HasValue)
                tests = tests.Where(t => t.IsProblem == isProblem.Value);

            if (isSolved.HasValue)
                tests = tests.Where(t => t.Problem.IsSolved == isSolved.Value);

            return tests.ToList();
        }

        public async Task SetAnswerAsync(int testId, string answer)
        {
            Test? test = await _dbContext.Tests.FirstAsync(t => t.Id == testId && t.IsProblem);
            test.Answer = answer;
            await _dbContext.SaveChangesAsync();
        }

        public void Add(Test test)
        {
            _dbContext.Tests.Add(test);
            _dbContext.SaveChanges();
        }

        public async Task AddAsync(Test test)
        {
            await _dbContext.Tests.AddAsync(test);
            await _dbContext.SaveChangesAsync();
        }

        #endregion Public Methods

        //public List<Test> GetByProblem(Problem problem, bool? isProblem = null, bool? isSolved = null)
        //{
        //    return GetByProblemId(problem.Id, isProblem, isSolved);
        //}
    }
}
