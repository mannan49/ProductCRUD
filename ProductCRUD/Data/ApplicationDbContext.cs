using Microsoft.EntityFrameworkCore;
using ProductCRUD.Models.Entities;


namespace ProductCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
