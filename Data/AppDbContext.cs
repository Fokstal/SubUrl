using Microsoft.EntityFrameworkCore;
using SubUrl.Models;

namespace SubUrl.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Url> Url { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}