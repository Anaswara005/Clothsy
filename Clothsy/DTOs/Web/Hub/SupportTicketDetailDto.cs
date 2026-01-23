using System;
using System.Collections.Generic;

namespace Clothsy.DTOs.Web.Hub
{
    public class SupportTicketDetailDto
    {
        public int TicketId { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        // 🔥 REQUIRED FOR MODAL IMAGE
        public string? ImageUrl { get; set; }

        // 🔥 REQUIRED FOR CONVERSATION
        public List<SupportTicketReplyDto> Replies { get; set; }
            = new();
    }
}
