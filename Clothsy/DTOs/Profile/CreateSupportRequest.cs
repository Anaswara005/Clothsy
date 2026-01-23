// File: DTOs/Profile/CreateSupportRequest.cs

using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.Profile
{
    public class CreateSupportRequest
    {
        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public int DistrictId { get; set; }

        // Optional base64 encoded image
        public string? ImageBase64 { get; set; }
    }

   
}