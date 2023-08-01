using Microsoft.EntityFrameworkCore;

namespace Case
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Set the PostgreSQL database connection string here
            string connectionString = "Host=localhost;Port=5432;Database=case;Username=postgres;Password=122333";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
