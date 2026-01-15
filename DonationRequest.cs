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
        public int RequesterUserId { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        // ?? Navigation properties (IMPORTANT)
        [ForeignKey(nameof(DonationId))]
        public Donation Donation { get; set; }

        [ForeignKey(nameof(RequesterUserId))]
        public User RequesterUser { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }
    }
}
