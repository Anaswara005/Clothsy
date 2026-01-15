using Clothsy.Models.Donation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothsy.Models.Web.Auth
{
    public class WebUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty; // ADMIN | HUB

        public bool IsActive { get; set; } = true;
        [MaxLength(20)]
        public string? HubCode { get; set; }
    }
}
