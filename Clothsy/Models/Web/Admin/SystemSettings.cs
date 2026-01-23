namespace Clothsy.Data
{
    public class SystemSettings
    {
        public int Id { get; set; }

        public string? SystemName { get; set; }
        public string? AdminEmail { get; set; }
        public string? SupportEmail { get; set; }

        public int AutoArchiveDays { get; set; }
        public bool EmailNotificationsEnabled { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
