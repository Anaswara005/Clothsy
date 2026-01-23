namespace Clothsy.DTOs.Web.Hub
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        public string DonationId { get; set; } = "";

        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public string ClothingType { get; set; } = "";
        public string Size { get; set; } = "";

        public string? Description { get; set; }
        public string? ThumbnailImageUrl { get; set; } // 👈 IMAGE VISIBLE

        public string DonorName { get; set; } = "";
        public string District { get; set; } = "";

        public DateTime ApprovedAt { get; set; }
    }
}
