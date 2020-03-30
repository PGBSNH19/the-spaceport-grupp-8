using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;


namespace Coder
{

    public class StarWarsDBContext : DbContext
    {      
        public DbSet<StarWarsPerson> person { get; set; }

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