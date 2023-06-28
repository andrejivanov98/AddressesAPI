using Addresses.Services.Models.Address;

namespace Addresses.Services.Abstractions.Service
{
    public interface IAddressesCommandService
    {
        Task<AddressModel> CreateAsync(AddressCreate address);
        Task<AddressModel> UpdateAsync(int id, AddressUpdate address);
        Task DeleteAsync(int id);
    }
}
