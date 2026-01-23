namespace Clothsy.DTOs.Admin
{
    public class UpdateSystemSettingsDto
    {
        public string? SystemName { get; set; }
        public string? AdminEmail { get; set; }
        public string? SupportEmail { get; set; }
        public int AutoArchiveDays { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
    }
}
