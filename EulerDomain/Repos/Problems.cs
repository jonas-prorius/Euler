using System.Collections.Generic;
using System.Linq;
using EulerDb;
using EulerDb.Entities;

namespace EulerDomain.Repos
{
    public class Problems
    {
        #region Fields

        private readonly EulerDbContextFactory _dbFactory;

        #endregion Fields

        #region Constructors

        public Problems(EulerDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion Constructors

        #region Public Methods

        public List<Problem> GetAll(bool? isSolved = null)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                IEnumerable<Problem> problems = db.Problems;

                if (isSolved.HasValue)
                    problems = problems.Where(p => p.IsSolved == isSolved.Value);

                return problems.ToList();
            }
        }

        #endregion Public Methods
    }
}
