using System.ComponentModel.DataAnnotations;

namespace Clothsy.DTOs.Profile
{
    public class SupportReplyRequest
    {
        [Required]
        public string Message { get; set; } = string.Empty;

        public string? NewStatus { get; set; }
    }
}
