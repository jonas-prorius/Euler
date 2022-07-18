using EulerDb;
using EulerDomain.Repos;

namespace EulerDomain
{
    public class EulerRepo
    {
        public Numbers Numbers { get; }
        public Problems Problems { get; }
        public Tests Tests { get; }

        public EulerRepo(EulerDbContextFactory dbFactory)
        {
            Numbers = new Numbers(dbFactory);
            Problems = new Problems(dbFactory);
            Tests = new Tests(dbFactory);
        }
    }
}
