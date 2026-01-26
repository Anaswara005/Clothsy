using Clothsy.Models.Profile;
using Clothsy.Models.SignupModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothsy.Models.Donation
{
    public class DonationRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RequestId { get; set; } // Added RequestId

        [Required]
        public int DonationId { get; set; }

        [Required]
        public int RequesterId { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Submitted";
        // Pending → Approved → Collected
        // Pending → Rejected

        public DateTime RequestedAt { get; set; }

        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }


        // Navigation properties
        [ForeignKey(nameof(DonationId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Donation? Donation { get; set; }

        [ForeignKey(nameof(RequesterId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? Requester { get; set; }

        [ForeignKey(nameof(AddressId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Address? Address { get; set; }
        public string? RejectionReason { get; set; }

    }
}
