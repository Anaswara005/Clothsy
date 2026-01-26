using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Clothsy.Models.Donation
{
    public class Hub
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        public int DistrictId { get; set; }


        [Required]
        [MaxLength(100)]
        public string District { get; set; } = string.Empty;

        [MaxLength(10)]
        public string? Pincode { get; set; }
       

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string HubCode { get; set; } = string.Empty;

        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public string WorkingDays { get; set; } = string.Empty;



        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}