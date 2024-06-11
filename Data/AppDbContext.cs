using Microsoft.EntityFrameworkCore;
using SubUrl.Models.Entities;

namespace SubUrl.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UrlEntity> Url { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}