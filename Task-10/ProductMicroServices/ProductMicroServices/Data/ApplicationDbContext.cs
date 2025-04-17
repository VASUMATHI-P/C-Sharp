using Microsoft.EntityFrameworkCore;
using ProductMicroServices.Models;

namespace ProductMicroServices.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
    }
}
