using Addresses.DataContext.Entities;
using Addresses.Services.Abstractions.Repository;
using Addresses.Services.Abstractions.Service;
using Addresses.Services.Mappings.Addresses;
using Addresses.Services.Models.Address;

namespace Addresses.Services.Services.Command
{
    public class AddressesCommandService : IAddressesCommandService
    {
        private readonly IRepository<Address> _repository;

        public AddressesCommandService(IRepository<Address> repository)
        {
            _repository = repository;
        }

        public async Task<AddressModel> CreateAsync(AddressCreate newAddress)
        {
            if (string.IsNullOrEmpty(newAddress.Street) ||
                string.IsNullOrEmpty(newAddress.HouseNumber) ||
                string.IsNullOrEmpty(newAddress.ZipCode) ||
                string.IsNullOrEmpty(newAddress.City) ||
                string.IsNullOrEmpty(newAddress.Country))
            {
                throw new ArgumentException("All fields are required.");
            }

            var address = newAddress.ToAddress();
            var result = await _repository.CreateAsync(address);

            return result.ToModel();
        }

        public async Task<AddressModel> UpdateAsync(int id, AddressUpdate updatedAddress)
        {
            if (string.IsNullOrEmpty(updatedAddress.Street) ||
                string.IsNullOrEmpty(updatedAddress.HouseNumber) ||
                string.IsNullOrEmpty(updatedAddress.ZipCode) ||
                string.IsNullOrEmpty(updatedAddress.City) ||
                string.IsNullOrEmpty(updatedAddress.Country))
            {
                throw new ArgumentException("All fields are required.");
            }

            var existingAddress = await _repository.GetByIdAsync(id);

            if (existingAddress != null)
            {
                existingAddress.UpdateFromDto(updatedAddress);
                var result = await _repository.UpdateAsync(existingAddress);
                return result.ToModel();
            }
            else
            {
                throw new ArgumentException($"Address with ID {id} does not exist.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var address = await _repository.GetByIdAsync(id);

            if (address != null)
            {
                await _repository.DeleteAsync(address.Id);
            }
        }
    }
}
