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
        public string DonationId { get; set; } = string.Empty;

        [Required]
        public int DonorUserId { get; set; } 


        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string ClothingType { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Size { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string District { get; set; } = string.Empty;

        [Required]
        public int AssignedHubId { get; set; }
 


        [MaxLength(500)]
        public string? ThumbnailImageUrl { get; set; } // 👈 NEW - Main image

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Waiting"; // waiting, Approved, Rejected

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReceivedAt { get; set; }

        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public string? RejectionReason { get; set; }


        // Navigation properties
        [ForeignKey("DonorUserId")]
        public User? Donor { get; set; }

        [ForeignKey("AssignedHubId")]
        public Hub? AssignedHub { get; set; }
        public DateTime? CollectedAt { get; set; }

        public ICollection<DonationImage> Images { get; set; } = new List<DonationImage>(); // 👈 NEW

       
        public ICollection<DonationRequest> DonationRequests { get; set; }
            = new List<DonationRequest>();

        public bool IsAvailable { get; set; } = true;

    }
}