using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.Web.Hub
{
    public class RejectDonationDto
    {
        [Required]
        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;
    }
}
