namespace Clothsy.DTOs.Web.Hub
{
    public class SupportTicketReplyDto
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public string SenderRole { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
