namespace Clothsy.DTOs.Profile
{
    public class CreateSupportRequest
    {
        public string Category { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? ImageBase64 { get; set; }
    }
}
