using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.SignupDTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Phone number or email is required")]
        public string PhoneOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
