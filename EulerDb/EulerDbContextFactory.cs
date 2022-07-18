using Microsoft.EntityFrameworkCore;

namespace EulerDb

{
    public class EulerDbContextFactory
    {
        private readonly string connectionString;

        public EulerDbContext CreateDbContext()
        {
            DbContextOptions<EulerDbContext> options = new DbContextOptionsBuilder<EulerDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new(options);
        }

        public EulerDbContextFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
