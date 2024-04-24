using Microsoft.EntityFrameworkCore;
using SubUrl.Models;

namespace SubUrl.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Url> Url { get; set; } = null!;

        public AppDbContext()
        {
            Database.EnsureCreatedAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AppData/Database.db");
        }
    }
}