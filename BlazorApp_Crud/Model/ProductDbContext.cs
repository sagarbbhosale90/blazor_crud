using Microsoft.EntityFrameworkCore;

namespace BlazorApp_Crud.Model
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Products> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
              .Property(p => p.Price)
              .HasPrecision(18, 4);

            modelBuilder.Entity<Products>()
                .HasIndex(e => e.ProductName)
                .IsUnique();
        }
    }
}
