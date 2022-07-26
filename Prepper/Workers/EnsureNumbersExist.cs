using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;
using Microsoft.Extensions.Hosting;

namespace Prepper.Workers
{
    internal class EnsureNumbersExist : BackgroundService
    {
        private readonly EulerDbContextFactory _dbFactory;

        public EnsureNumbersExist(EulerDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                if (!db.Numbers.Any())
                    await db.Numbers.AddAsync(new Number(0), stoppingToken);

                await db.SaveChangesAsync(stoppingToken);
            }
        }
    }
}
