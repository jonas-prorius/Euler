using System.Linq;
using EulerDb;
using EulerDomain.Repos;

namespace EulerDomain
{
    public class EulerRepo
    {
        public Numbers Numbers { get; }
        public Problems Problems { get; }
        public Tests Tests { get; }

        private readonly EulerDbContext _dbContext;

        public EulerRepo(EulerDbContextFactory dbFactory)
        {
            _dbContext = dbFactory.CreateDbContext();
            Numbers = new Numbers(_dbContext);
            Problems = new Problems(_dbContext);
            Tests = new Tests(_dbContext);

            if (!_dbContext.Numbers.Any())
                Numbers.AddNumber(0);
        }
    }
}
