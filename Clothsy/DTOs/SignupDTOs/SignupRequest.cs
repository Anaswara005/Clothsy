
using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.SignupDTOs
{
    public class SignupRequest
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        [Required(ErrorMessage = "Contact type is required")]
        public string ContactType { get; set; } = string.Empty; // "Phone" or "Email"
    }
}