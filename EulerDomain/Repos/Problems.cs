using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace EulerDomain.Repos
{
    public class Problems
    {
        #region Fields

        private readonly EulerDbContext _dbContext;

        #endregion Fields

        #region Constructors

        public Problems(EulerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Constructors

        #region Public Methods

        public List<Problem> GetAll(bool? isSolved = null)
        {
            IEnumerable<Problem> problems = _dbContext.Problems;

            if (isSolved.HasValue)
                problems = problems.Where(p => p.IsSolved == isSolved.Value);

            return problems.ToList();
        }

        public async Task SetIsSolvedAsync(int problemId, bool isSolved)
        {
            Problem? problem = await _dbContext.Problems
                .FirstAsync(p => p.Id == problemId);

            problem.IsSolved = isSolved;

            await _dbContext.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}
