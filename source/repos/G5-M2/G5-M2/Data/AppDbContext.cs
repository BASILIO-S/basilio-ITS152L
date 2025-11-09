using Microsoft.EntityFrameworkCore;
using G5M2.Models;

namespace G5M2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Squirtle", Code = "SCR148", Brand = "Pokemon", UnitPrice = 4500.00m },
                new Item { Id = 2, Name = "Fezandipiti EX", Code = "SFA", Brand = "Pokemon", UnitPrice = 950.00m }
            );
        }
    }
}
