using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp2
{
    public class StarWarsDbContext : DbContext
    {

        public DbSet<StarWarsPerson> Person { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<StarWarsPerson>();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("Appsettings.Json")
                .Build();

            var defaultConnection = configuration.GetConnectionString("DefaultConnection");
            optionBuilder.UseSqlServer(defaultConnection);
        }

    }
}
