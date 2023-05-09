using DigitalPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortfolio.Dal
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            });
        }
    }
}