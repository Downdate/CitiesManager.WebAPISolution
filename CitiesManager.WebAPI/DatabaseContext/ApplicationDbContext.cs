using CitiesManager.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.WebAPI.DatabaseContext
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public ApplicationDbContext()
        {
            
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<City>().HasData(new City() { CityID = Guid.Parse("9c639a28-d5dc-4dc2-be07-002eb9e68cce"), CityName = "New York" });
             modelBuilder.Entity<City>().HasData(new City() { CityID = Guid.Parse("6501b773-4cee-4722-9ab7-7921ddebee68"), CityName = "Tehran" });
        }

    }
}
