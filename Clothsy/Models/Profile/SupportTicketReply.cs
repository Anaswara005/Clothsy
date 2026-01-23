using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothsy.Models.Profile
{
    public class SupportTicketReply
    {
        public int Id { get; set; }

        // 🔗 FK → SupportTicket
        [Required]
        public int TicketId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public SupportTicket Ticket { get; set; } = null!;

        // HUB | USER
        [Required]
        [MaxLength(10)]
        public string SenderRole { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Message { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
