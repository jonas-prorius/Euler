using EulerDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace EulerDb
{
    public class EulerDbContext : DbContext
    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Test> Tests { get; set; }

        public EulerDbContext(DbContextOptions<EulerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Problem>().HasKey(p => p.Id);
            modelBuilder.Entity<Problem>()
                .HasMany(p => p.Tests)
                .WithOne(t => t.Problem)
                .HasForeignKey(t => t.ProblemId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Test>().HasKey(p => p.Id);
            modelBuilder.Entity<Test>()
                .HasOne(t => t.Problem)
                .WithMany(p => p.Tests)
                .HasForeignKey(t => t.ProblemId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
