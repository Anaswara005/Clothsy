
using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.HomepageDTOs
{
    public class CreateDonationRequest
    {
        [Required]
        public string? Category { get; set; }

        [Required]
        public string? ClothingType { get; set; }

        [Required]
        public string? Size { get; set; }


        public string? Description { get; set; }

        [Required]
        public string? District { get; set; }

        [Required]
        public List<IFormFile>? Images { get; set; } // 1-3 images
    }
}
