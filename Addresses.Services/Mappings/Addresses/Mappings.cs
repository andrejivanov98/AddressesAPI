using Addresses.DataContext.Entities;
using Addresses.Services.Models.Address;

namespace Addresses.Services.Mappings.Addresses
{
    public static class Mappings
    {
        public static AddressModel ToModel(this Address address)
        {
#pragma warning disable CS8603 // Possible null reference return.
            if (address == null) return null;
#pragma warning restore CS8603 // Possible null reference return.

            return new AddressModel
            {
                Id = address.Id,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                ZipCode = address.ZipCode,
                City = address.City,
                Country = address.Country
            };
        }

        public static Address ToAddress(this AddressCreate addressCreate)
        {
#pragma warning disable CS8603 // Possible null reference return.
            if (addressCreate == null) return null;
#pragma warning restore CS8603 // Possible null reference return.

            return new Address
            {
                Street = addressCreate.Street,
                HouseNumber = addressCreate.HouseNumber,
                ZipCode = addressCreate.ZipCode,
                City = addressCreate.City,
                Country = addressCreate.Country
            };
        }

        public static void UpdateFromDto(this Address address, AddressUpdate addressUpdate)
        {
            if (addressUpdate == null) return;

            address.Street = addressUpdate.Street;
            address.HouseNumber = addressUpdate.HouseNumber;
            address.ZipCode = addressUpdate.ZipCode;
            address.City = addressUpdate.City;
            address.Country = addressUpdate.Country;
        }
    }
}
