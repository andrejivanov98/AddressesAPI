using Addresses.DataContext.Entities;
using Addresses.DataContext.Helpers;
using Addresses.Services.Abstractions.Repository;
using Addresses.Services.Abstractions.Service;
using Addresses.Services.Mappings.Addresses;
using Addresses.Services.Models.Address;
using System.Reflection;

namespace Addresses.Services.Services.Query
{
    public class AddressesQueryService : IAddressesQueryService
    {
        private readonly IRepository<Address> _repository;

        public AddressesQueryService(IRepository<Address> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AddressModel>> GetAllAsync(string search, string sortBy, bool sortDesc)
        {
            var addresses = await _repository.GetAllAsync();

            // Search
            if (!string.IsNullOrEmpty(search))
            {
                var addressProperties = typeof(Address).GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                addresses = addresses.Where(a =>
                    addressProperties.Any(p =>
                        p.GetValue(a) is string propertyValue && propertyValue.Contains(search, StringComparison.OrdinalIgnoreCase)
                    )
                );
            }

            if (!addresses.Any())
            {
                throw new KeyNotFoundException("Address not found");
            }

            // Sort
            if (!string.IsNullOrEmpty(sortBy))
            {
                var prop = typeof(Address).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop != null)
                {
                    addresses = sortDesc ?
                        addresses.OrderByDescending(a => prop.GetValue(a, null)) :
                        addresses.OrderBy(a => prop.GetValue(a, null));
                }
            }

            return addresses.Select(a => a.ToModel());
        }

        public async Task<AddressModel> GetByIdAsync(int id)
        {
            var address = await _repository.GetByIdAsync(id);

            if (address == null)
            {
                throw new KeyNotFoundException($"Address with ID {id} does not exist.");
            }

            return address.ToModel();
        }

        public async Task<string> GetDistanceAsync(int id1, int id2)
        {
            var address1 = await _repository.GetByIdAsync(id1);
            var address2 = await _repository.GetByIdAsync(id2);

            if (address1 == null || address2 == null)
            {
                throw new KeyNotFoundException("Address not found");
            }

            var point1 = await GetLatLongFromAddress(GetAddressString(address1));
            var point2 = await GetLatLongFromAddress(GetAddressString(address2));

            var distance = GetDistance(point1, point2);

            return $"The distance between {address1.Street} {address1.HouseNumber}, {address1.City} and {address2.Street} {address2.HouseNumber}, {address2.City} is {distance:F2} km.";
        }

        private string GetAddressString(Address address)
        {
            return $"{address.Street} {address.HouseNumber}, {address.ZipCode} {address.City}, {address.Country}";
        }

        private double GetDistance(MapPoint point1, MapPoint point2)
        {
            const double earthRadiusKm = 6371.0;

            var dLat = (point2.Latitude - point1.Latitude) * Math.PI / 180;
            var dLon = (point2.Longitude - point1.Longitude) * Math.PI / 180;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(point1.Latitude * Math.PI / 180) * Math.Cos(point2.Latitude * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        private async Task<MapPoint> GetLatLongFromAddress(string address)
        {
            string urlEncodedAddress = System.Net.WebUtility.UrlEncode(address);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("AddressesApp/1.0 (ivanovandrej1995@gmail.com)");

            HttpResponseMessage response = await client.GetAsync($"https://nominatim.openstreetmap.org/search?q={urlEncodedAddress}&format=json&addressdetails=1&limit=1");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic? parsedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                if (parsedJson is Array && parsedJson.Length == 0)
                {
                    throw new Exception($"Could not geocode the address: {address}");
                }

                double lat = double.Parse(parsedJson![0].lat.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                double lon = double.Parse(parsedJson[0].lon.ToString(), System.Globalization.CultureInfo.InvariantCulture);

                return new MapPoint(lat, lon);
            }
            else
            {
                throw new Exception("Error fetching geocoding data from Nominatim.");
            }
        }
    }
}
