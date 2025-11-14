using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothsy.Models.SignupModels
{
    public class OtpLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string ContactType { get; set; } = string.Empty; // "Phone" or "Email"

        [Required]
        [MaxLength(100)]
        public string ContactValue { get; set; } = string.Empty;

        [Required]
        [MaxLength(6)]
        public string OtpCode { get; set; } = string.Empty;

        public DateTime ExpiryTime { get; set; }

        public int ResendCount { get; set; } = 0;

        public bool IsUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
