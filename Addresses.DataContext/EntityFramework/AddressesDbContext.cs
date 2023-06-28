using Addresses.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Addresses.DataContext.EntityFramework
{
    public class AddressesDbContext : DbContext
    {
        public AddressesDbContext(DbContextOptions<AddressesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
    }
}
