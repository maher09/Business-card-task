using Microsoft.EntityFrameworkCore;

namespace Business_Card.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BusinessCard> BusinessCards { get; set; }

    }
    
    
}
