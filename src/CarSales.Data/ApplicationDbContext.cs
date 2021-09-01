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

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
