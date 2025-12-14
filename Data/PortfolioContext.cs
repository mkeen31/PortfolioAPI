using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;

namespace PortfolioAPI.Data 
{
    public class PortfolioContext(IConfiguration config) : DbContext
    {
        private readonly IConfiguration _config = config;
        public DbSet<Education> Educations => Set<Education>();
        public DbSet<Experience> Experiences => Set<Experience>();
        public DbSet<Employment> Employments => Set<Employment>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("PortfolioContext"));
        }
    }
}