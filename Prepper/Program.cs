using EulerDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prepper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = @"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;";
            DbContextOptions<EulerDbContext> dbContextOptions = new();
            DbContextOptionsBuilder optionsBuilder = new(dbContextOptions);
            optionsBuilder.UseSqlServer(connectionString);

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContextFactory<EulerDbContext>(options
                        => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
                })
                .Build();

            host.Run();
        }
    }
}
