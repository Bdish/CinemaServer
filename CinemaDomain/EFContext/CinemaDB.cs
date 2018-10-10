
using CinemaDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaDomain.EFContext
{
    public class CinemaDB : DbContext
    {
        public CinemaDB()
        {
           // Database.EnsureDeleted();
            //Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<Order> Orders { get; set; }
        
        public DbSet<Seance> Seances { get; set; }
      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = CinemaDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

        }
    }
}
