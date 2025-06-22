using EulerDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace EulerDb
{
    /// <summary>
    /// Entity Framework DbContext for the Euler problems database.
    /// </summary>
    public class EulerDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the collection of Project Euler problems.
        /// </summary>
        public DbSet<Problem> Problems { get; set; } = null!;
        
        /// <summary>
        /// Gets or sets the collection of test cases for problems.
        /// </summary>
        public DbSet<Test> Tests { get; set; } = null!;

        /// <summary>
        /// Initializes a new instance of the EulerDbContext class.
        /// </summary>
        /// <param name="options">Database context options</param>
        public EulerDbContext(DbContextOptions<EulerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Configures the database connection.
        /// </summary>
        /// <param name="optionsBuilder">Options builder for database configuration</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer();

        /// <summary>
        /// Configures the entity relationships and constraints.
        /// </summary>
        /// <param name="modelBuilder">Model builder for entity configuration</param>
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
