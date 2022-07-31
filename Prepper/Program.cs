using EulerDb;
using EulerDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prepper.Workers;

namespace Prepper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = @"Server=srv-home,1435;Database=Euler;User Id=sa;Password=mssql-euler2;";
            var dbFactory = new EulerDbContextFactory(connectionString);
            DbContextOptions<EulerDbContext> dbContextOptions = new();
            DbContextOptionsBuilder optionsBuilder = new(dbContextOptions);
            optionsBuilder.UseSqlServer(connectionString);

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<TestCreator>();
                    services.AddHostedService<EnsureNumbersExist>();
                    //services.AddHostedService<Solver>();

                    services.AddDbContextFactory<EulerDbContext>(options
                        => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
                    services.AddSingleton(dbFactory);
                    services.AddSingleton(new EulerRepo(dbFactory));
                })
                .Build();

            host.Run();
        }
    }
}
