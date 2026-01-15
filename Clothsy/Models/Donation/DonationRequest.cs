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
        public int DonationId { get; set; }

        [Required]
        public int RequesterId { get; set; } 


        [Required]
        public int AddressId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        public DateTime RequestedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }

        // Navigation properties (with DeleteBehavior configured in DbContext)
        [ForeignKey("DonationId")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Donation? Donation { get; set; }

        [ForeignKey("RequesterId")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? Requester { get; set; }

        [ForeignKey("AddressId")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Address? Address { get; set; }
    }
}