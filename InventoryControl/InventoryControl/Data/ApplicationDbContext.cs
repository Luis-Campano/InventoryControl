using InventoryControl.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movement> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Code)
                .IsUnique();
        }
    }
}
