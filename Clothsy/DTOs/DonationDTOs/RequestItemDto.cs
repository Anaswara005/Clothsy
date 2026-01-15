using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.DonationDTOs
{
    public class RequestItemDto
    {
        [Required]
        public int AddressId { get; set; }
    }
}
