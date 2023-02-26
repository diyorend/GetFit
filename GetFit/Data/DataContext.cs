using GetFit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GetFit.Data
{
    public class DataContext: IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
                
        }

        public DbSet<Home> Homes { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
