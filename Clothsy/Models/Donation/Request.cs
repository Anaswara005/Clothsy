
using Clothsy.Models.Profile;
using Clothsy.Models.SignupModels;
using Clothsy.Models.Web.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Clothsy.Models.Donation

{
    [Table("Requests")]
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DonationId { get; set; }

        [Required]
        public int RequesterId { get; set; }

        [Required]
        public int AddressId { get; set; }

        // Status: Pending, Confirmed, Completed, Cancelled
        [Required]
        [MaxLength(50)]
        public string? Status { get; set; } = "Pending";

        [MaxLength(500)]
        public string? DeliveryInstructions { get; set; }

        public int? HubId { get; set; }

        [ForeignKey("HubId")]
        public WebUser? Hub { get; set; }


        public DateTime RequestedAt { get; set; } = DateTime.Now;

        public DateTime? CompletedAt { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties

        [ForeignKey("DonationId")]
        public virtual Donation? Donation { get; set; }

        [ForeignKey("RequesterId")]
        public User? Requester { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address? DeliveryAddress { get; set; }
    }
}