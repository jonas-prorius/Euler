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

        private readonly EulerDbContextFactory _dbFactory;

        public EulerRepo(EulerDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
            Numbers = new Numbers(_dbFactory);
            Problems = new Problems(_dbFactory);
            Tests = new Tests(_dbFactory);

            using (var db = _dbFactory.CreateDbContext())
            {
                if (!db.Numbers.Any())
                    Numbers.Add(0);
            }
        }
    }
}
