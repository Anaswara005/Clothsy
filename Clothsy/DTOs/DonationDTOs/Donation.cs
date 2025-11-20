using System.ComponentModel.DataAnnotations;
namespace Clothsy.DTOs.DonationDTOs

{
    public class CreateDonationRequest
    {
        [Required]
        public string Category { get; set; } = string.Empty; // Men, Women, Kids
        [Required]
        public string ClothingType { get; set; } = string.Empty; // Shirt, Kurti, T-Shirt

        [Required]
        public string Size { get; set; } = string.Empty; // S, M, L, XL

        public string? Description { get; set; }

        [Required]
        public string District { get; set; } = string.Empty; // Trivandrum, Kollam, Kochi

        public string? Photo1Url { get; set; }
        public string? Photo2Url { get; set; }
        public string? Photo3Url { get; set; }
    }

    public class DonationResponse
    {
        public int Id { get; set; }
        public string DonationId { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ClothingType { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string District { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public HubInfoDto? HubInfo { get; set; }
    }

    public class HubInfoDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}