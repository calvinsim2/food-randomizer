using FunWithFoodDomain.Models;
using FunWithFoodInfrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FunWithFoodInfrastructure
{
    public class FoodDbContext : DbContext
    {
        // to connect our db to .net core
        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options)
        {

        }

        // Convert Models to DbSets, queries against this DbSet will be translated to queries against database.
        public DbSet<Cuisine> Cuisine { get; set; } = null!;
        public DbSet<Food> Food { get; set; } = null!;
        public DbSet<Admin> Admin { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CuisineEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FoodEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AdminEntityConfiguration());
        }
    }
}
