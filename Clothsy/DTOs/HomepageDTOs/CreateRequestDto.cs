
using System.ComponentModel.DataAnnotations;


namespace Clothsy.DTOs.HomepageDTOs
{
    public class CreateRequestDto
    {
        [Required]
        public int DonationId { get; set; }

        [Required]
        public int AddressId { get; set; }

        public string? DeliveryInstructions { get; set; }
    }
}