#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Addresses.Services.Models.Address
{
    public class AddressUpdate
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
