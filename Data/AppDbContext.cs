using Microsoft.EntityFrameworkCore;
using TechritizeAuthAPI.Models;

namespace TechritizeAuthAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets represent tables in the database
        public DbSet<User> Users { get; set; }
        public DbSet<TwoFactorCode> TwoFactorCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure email is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // TwoFactorCode - we want to quickly find by email
            modelBuilder.Entity<TwoFactorCode>()
                .HasIndex(t => t.Email);
        }
    }
}
