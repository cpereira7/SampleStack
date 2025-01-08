using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SampleStack.Repository.Models;

namespace SampleStack.Repository.Infrastructure
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(builder =>
            {
                builder.Property(x => x.DateOfBirth).HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

                builder.Property(x => x.Id).UseIdentityColumn();
            });

            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 3, FirstName = "David ", LastName = "Moore", DateOfBirth = new DateTime(1990, 1, 1) },
                new Person { Id = 4, FirstName = "Linda", LastName = "Davis", DateOfBirth = new DateTime(1985, 5, 15) }
            );
        }
    }

    internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql();

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
