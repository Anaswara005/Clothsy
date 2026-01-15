using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.Profile
{
    public class UpdateProfileRequest
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
