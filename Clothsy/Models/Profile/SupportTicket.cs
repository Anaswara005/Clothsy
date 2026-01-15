using Clothsy.Models.SignupModels;

namespace Clothsy.Models.Profile
{
    public class SupportTicket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Category { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string Status { get; set; } = "Open";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}