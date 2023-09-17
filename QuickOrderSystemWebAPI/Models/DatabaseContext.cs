using Microsoft.EntityFrameworkCore;
using QuickOrderSystemWebAPI.Models;

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
    }
}