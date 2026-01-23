namespace Clothsy.DTOs.Web.Hub
{
    public class HubRequestDto
    {
        public int RequestId { get; set; }
        public int DonationId { get; set; }
        public string DonationCode { get; set; } = "";

        public string ItemTitle { get; set; } = "";
        public string Category { get; set; } = "";

        public string RequesterName { get; set; } = "";
        public string RequesterPhone { get; set; } = "";

        public string District { get; set; } = "";
        public DateTime RequestedAt { get; set; }

        public string Status { get; set; } = "";
        public string? RejectionReason { get; set; }

        public string? ThumbnailImageUrl { get; set; } // 👈 IMAGE
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

    }

}
