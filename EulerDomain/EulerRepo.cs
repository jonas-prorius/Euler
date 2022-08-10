using System.Linq;
using EulerDb;
using EulerDomain.Repos;

namespace EulerDomain
{
    public class EulerRepo
    {
        public Problems Problems { get; }
        public Tests Tests { get; }

        private readonly EulerDbContext _dbContext;

        public EulerRepo(EulerDbContextFactory dbFactory)
        {
            _dbContext = dbFactory.CreateDbContext();
            Problems = new Problems(_dbContext);
            Tests = new Tests(_dbContext);
        }
    }
}
