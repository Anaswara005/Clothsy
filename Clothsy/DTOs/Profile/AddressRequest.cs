
using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.Profile
{
    public class AddressRequest
    {
        [Required]
        public string? PinCode { get; set; }

        [Required]
        public string? District { get; set; }

        [Required]
        public string? HouseAddress { get; set; }

        public string? RoadName { get; set; }
    }

}