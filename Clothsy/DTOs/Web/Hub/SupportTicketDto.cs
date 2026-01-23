namespace Clothsy.DTOs.Web.Hub
{
    public class SupportTicketDto
    {
        public int TicketId { get; set; }

        public string UserName { get; set; } = "";
        public string Category { get; set; } = "";
        public string Message { get; set; } = "";

        public string District { get; set; } = "";   // ✅ ADD THIS

        public string? ImageUrl { get; set; }

        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
