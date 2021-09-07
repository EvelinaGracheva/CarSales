using CarSales.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace CarSales.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;

        public virtual DbSet<Listing> Listings { get; set; } = null!;

        public virtual DbSet<Purchase> Purchases { get; set; } = null!;

        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(b =>
            {
                b.HasQueryFilter(t => !t.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Listing>(b =>
            {
                b.Property(t => t.Price).HasPrecision(18, 4);
                b.HasQueryFilter(t => !t.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Purchase>(b =>
            {
                b.HasQueryFilter(t => !t.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Vehicle>(b =>
            {
                b.HasQueryFilter(t => !t.DeletedAt.HasValue);
            });
        }
    }
}
