using Clothsy.Models.SignupModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Clothsy.Models.Donation
{
    public class Donation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string DonationId { get; set; } = string.Empty; // CLO-1763630985695

        [Required]
        public int DonorUserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty; // Men, Women, Kids

        [Required]
        [MaxLength(50)]
        public string ClothingType { get; set; } = string.Empty; // Shirt, Kurti, T-Shirt

        [Required]
        [MaxLength(20)]
        public string Size { get; set; } = string.Empty; // S, M, L, XL

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string District { get; set; } = string.Empty; // Trivandrum, Kollam, Kochi

        [Required]
        public int AssignedHubId { get; set; }

        [MaxLength(500)]
        public string? Photo1Url { get; set; }

        [MaxLength(500)]
        public string? Photo2Url { get; set; }

        [MaxLength(500)]
        public string? Photo3Url { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Waiting"; // Waiting, Approved, Rejected

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovedAt { get; set; }

        public DateTime? RejectedAt { get; set; }

        // Navigation properties
        [ForeignKey("DonorUserId")]
        public User? Donor { get; set; }

        [ForeignKey("AssignedHubId")]
        public Hub? AssignedHub { get; set; }
    }
}