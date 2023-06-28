using Addresses.Services.Models.Address;

namespace Addresses.Services.Abstractions.Service
{
    public interface IAddressesQueryService
    {
        Task<IEnumerable<AddressModel>> GetAllAsync(string search, string sort, bool sortDesc);
        Task<AddressModel> GetByIdAsync(int id);
        Task<string> GetDistanceAsync(int id1, int id2);
    }
}
