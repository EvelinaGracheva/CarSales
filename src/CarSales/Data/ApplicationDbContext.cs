
using CarSales.Models;

using Microsoft.EntityFrameworkCore;

namespace CarSales.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }

    }
}
