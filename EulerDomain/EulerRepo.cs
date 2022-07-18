using EulerDb;
using EulerDomain.Repos;

namespace EulerDomain
{
    public class EulerRepo
    {
        public Numbers Numbers { get; }
        public Tests Tests { get; }

        public EulerRepo(EulerDbContextFactory dbFactory)
        {
            Numbers = new Numbers(dbFactory);
            Tests = new Tests(dbFactory);
        }
    }
}
