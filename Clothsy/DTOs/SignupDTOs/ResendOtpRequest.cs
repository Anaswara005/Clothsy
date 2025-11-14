
using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.SignupDTOs
{
    public class ResendOtpRequest
    {
        [Required(ErrorMessage = "Contact type is required")]
        public string ContactType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact value is required")]
        public string ContactValue { get; set; } = string.Empty;
    }
}