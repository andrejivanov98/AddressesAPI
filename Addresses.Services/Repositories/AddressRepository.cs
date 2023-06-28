using Addresses.DataContext.Entities;
using Addresses.DataContext.EntityFramework;
using Addresses.Services.Abstractions.Repository;

namespace Addresses.Services.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(AddressesDbContext context) : base(context) { }
    }
}
