using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.HomepageDTOs
{
    public class RejectDonationDto
    {
        [Required]
        public string? Reason { get; set; }
    }
}