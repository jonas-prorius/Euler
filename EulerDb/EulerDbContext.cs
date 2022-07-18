using EulerDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace EulerDb
{
    public class EulerDbContext : DbContext
    {
        public DbSet<Factor> Factors { get; set; }
        public DbSet<Number> Numbers { get; set; }

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
            //modelBuilder.Entity<Factor>().HasKey(f => f.Id);
            //modelBuilder.Entity<Factor>()
            //    .HasOne(f => f.Number)
            //    .WithMany(n => n.Factors)
            //    .HasForeignKey(f => f.NumberId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Factor>()
            //    .HasOne(f => f.FactorNumber)
            //    .WithMany(fn => fn.FactorToNumbers)
            //    .HasForeignKey(f => f.FactorNumberId)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Number>().HasKey(n => n.Id);
            modelBuilder.Entity<Number>()
                .HasMany(n => n.Factors)
                .WithMany(f => f.FactorToNumbers);
            modelBuilder.Entity<Number>()
                .HasMany(n => n.FactorToNumbers)
                .WithMany(fn => fn.Factors)
                .UsingEntity<Factor>(f => f.ToTable("factor"));

            modelBuilder.Entity<Problem>().HasKey(p => p.Id);

            modelBuilder.Entity<Test>().HasKey(p => p.Id);
            modelBuilder.Entity<Test>()
                .HasOne(t => t.Problem)
                .WithMany(p => p.Tests)
                .HasForeignKey(t => t.ProblemId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
