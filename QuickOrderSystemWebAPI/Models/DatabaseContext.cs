using Microsoft.EntityFrameworkCore;
using QuickOrderSystemClassLibrary;

namespace QuickOrderSystemWebAPI.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<QrCode> QrCodes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CategoryId);
        }
    }
}