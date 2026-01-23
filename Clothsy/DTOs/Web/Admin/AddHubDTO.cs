namespace Clothsy.DTOs.Admin
{
    public class AddHubDto
    {
        public string? HubName { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? Pincode { get; set; }
        public string? HubEmail { get; set; }
        public string? HubPhone { get; set; }
        public bool IsActive { get; set; }
    }
}
