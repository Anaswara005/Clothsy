namespace Clothsy.DTOs.Web.Hub
{
    public class PendingDonationDto
    {
        public int Id { get; set; }                 // Internal DB Id (for actions)
        public string DonationId { get; set; } = ""; // Public donation id

        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string Category { get; set; } = "";

        public string? ThumbnailImageUrl { get; set; }

        public string DonorName { get; set; } = "";
        public DateTime SubmittedAt { get; set; }
        public string Status { get; set; } = "";
    }
}
