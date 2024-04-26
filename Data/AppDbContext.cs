using Microsoft.EntityFrameworkCore;
using SubUrl.Models;

namespace SubUrl.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Url> Url { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql
            (
                "server=localhost;user=root;password=pass1234;database=suburldb;", 
                new MySqlServerVersion(new Version(8, 0, 31))
            );
        }
    }
}