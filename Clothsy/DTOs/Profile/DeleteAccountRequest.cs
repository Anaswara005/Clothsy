
using System.ComponentModel.DataAnnotations;

namespace ClothsyAPI.DTOs.Profile
{
    public class DeleteAccountRequest
    {
        [Required]
        public string? Password { get; set; }
    }
}